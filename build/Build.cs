using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;

[GitHubActions("CI",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranches = new[]
    {
        "develop", "release/*", "master"
    },
    InvokedTargets = new[]
    {
        nameof(Publish)
    },
    ImportSecrets = new[]
    {
        "NUGET_API_KEY"
    })]
[PublicAPI]
partial class Build : NukeBuild
{
    [Solution("yandex-messenger-bot-dotnet.sln")] Solution Solution;
    [GitRepository] readonly GitRepository Repository;
    AbsolutePath Artifacts = RootDirectory / "artifacts";
    public static int Main() => Execute<Build>(x => x.Compile);
}