using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    /// <summary>
    /// Очищает папки в решении, заданные в <see cref="BaseCleanPaths"/> и <see cref="CleanPaths"/>.
    /// </summary>
    Target Clean => _ => _
        .Executes(
            () =>
            {
                Solution.Directory.GlobDirectories(CleanPaths)
                    .Where(x => !BuildProjectDirectory.Contains(x))
                    .ForEach(x => ((AbsolutePath)x).DeleteDirectory());
            });

    string[] CleanPaths =>
    [
        "**/bin",
        "**/obj",
        "output",
        "artifacts"
    ];

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(options => options
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .Requires(() => Configuration)
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(options => options
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(options => options
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild());
        });
}