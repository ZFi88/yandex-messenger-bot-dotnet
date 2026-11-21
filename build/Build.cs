using Fallout.Common;
using Fallout.Common.CI.GitHubActions;
using Fallout.Common.Git;
using Fallout.Common.IO;
using Fallout.Solutions;

[GitHubActions("CI",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranches = new[]
    {
        "develop", "release/*", "master", "bugfix/*"
    },
    InvokedTargets = new[]
    {
        nameof(Publish)
    },
    ReadPermissions = new[]
    {
        GitHubActionsPermissions.Contents
    },
    WritePermissions = new[]
    {
        GitHubActionsPermissions.IdToken
    })]
[GitHubActions("CI-Feature",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranches = new[]
    {
        "feature/*"
    },
    InvokedTargets = new[]
    {
        nameof(Test)
    })]
partial class Build : FalloutBuild
{
    [Solution("yandex-messenger-bot-dotnet.sln")] Solution Solution;
    [GitRepository] readonly GitRepository Repository;
    AbsolutePath Artifacts = RootDirectory / "artifacts";
    public static int Main() => Execute<Build>(x => x.Compile);
}
