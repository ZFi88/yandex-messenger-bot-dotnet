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
    ImportSecrets = new[]
    {
        "NUGET_API_KEY"
    })]
partial class Build : FalloutBuild
{
    [Solution("yandex-messenger-bot-dotnet.sln")] Solution Solution;
    [GitRepository] readonly GitRepository Repository;
    AbsolutePath Artifacts = RootDirectory / "artifacts";
    public static int Main() => Execute<Build>(x => x.Compile);
}
