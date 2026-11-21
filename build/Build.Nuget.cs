using Fallout.Common;
using Fallout.Common.Tools.DotNet;
using Fallout.Common.Tools.Git;
using Fallout.Solutions;
using Serilog;
using static Fallout.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Pack => _ => _
        .Requires(() => Configuration)
        .DependsOn(Test)
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

            foreach (var tag in GetPackableProjectTags(tags))
            {
                Log.Information("Publishing - {tag}", tag);
                DotNetNuGetPush(options => options
                    .SetTargetPath(Artifacts / $"{tag}.nupkg")
                    .SetApiKey(NugetApiKey)
                    .SetSource(NugetSource));
            }
        });
}
