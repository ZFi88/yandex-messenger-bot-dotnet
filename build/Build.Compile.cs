using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

partial class Build
{
    Target Clean => _ => _
        .Executes(() =>
        {
            foreach (var dir in Solution.Directory.GlobDirectories("**/bin", "**/obj", "artifacts"))
            {
                Log.Information("Deleting - {dir}", dir);
                dir.DeleteDirectory();
            }
        });

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
}