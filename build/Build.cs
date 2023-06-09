using Bimlab.Nuke;
using Bimlab.Nuke.Components;
using Nuke.Common.CI.GitHubActions;

[GitHubActions("CI",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranches = new[]
    {
        "develop"
    },
    InvokedTargets = new[]
    {
        nameof(IPublish.Publish)
    },
    ImportSecrets = new[]
    {
        "NUGET_API_KEY", "ALL_PACKAGES"
    })]
class Build : BimLabBuild, IPublish, ITest
{
    public static int Main() => Execute<Build>(x => x.From<ICompile>().Compile);
}