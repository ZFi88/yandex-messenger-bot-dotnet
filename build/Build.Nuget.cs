using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Pack => _ => _
        .Requires(() => Configuration)
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPack(options => options
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(Artifacts)
                .EnableNoRestore()
                .EnableNoBuild());
        });

    Target Tag => _ => _
        .Executes(() =>
        {
            foreach (var project in PackableProjects())
            {
                GitTasks.Git($"tag {project.Name}.{project.GetProperty("Version").NotNull()}");
            }
        });

    Target Publish => _ => _
        .OnlyWhenDynamic(CheckTags)
        .Requires(() => Configuration)
        .Requires(() => NugetApiKey)
        .DependsOn(Pack)
        .Executes(() =>
        {
            var sha = GetCurrentCommitSha();
            var tags = GetTagsPointsOnSha(sha);

            foreach (var tag in tags)
            {
                Log.Information("Publishing - {tag}", tag);
                DotNetNuGetPush(options => options
                    .SetTargetPath(Artifacts / $"{tag}.nupkg")
                    .SetApiKey(NugetApiKey)
                    .SetSource(NugetSource));
            }
        });
}