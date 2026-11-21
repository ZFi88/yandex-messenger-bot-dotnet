using Fallout.Common.CI.GitHubActions;
using Fallout.Common.CI.GitHubActions.Configuration;

partial class Build : IConfigureGitHubActions
{
    /// <summary>
    /// Вставляет шаги NuGet Trusted Publishing (OIDC -> временный API-ключ) в генерируемый workflow CI.
    /// Ключ экспортируется в <c>GITHUB_ENV</c> и подхватывается параметром <c>NugetApiKey</c> таргета Publish.
    /// </summary>
    public void ConfigureSteps(GitHubActionsStepPipeline pipeline)
    {
        if (pipeline.WorkflowName != "CI")
        {
            return;
        }

        pipeline.Insert(GitHubActionsStepPosition.PreRun, new GitHubActionsCustomStep
        {
            Name = "NuGet login (OIDC -> temp API key)",
            Id = "nuget-login",
            Uses = "NuGet/login@v1",
            With =
            {
                ["user"] = "${{ vars.NUGET_USER }}"
            }
        });

        pipeline.Insert(GitHubActionsStepPosition.PreRun, new GitHubActionsCustomStep
        {
            Name = "Export NuGet API key",
            Shell = "bash",
            Run =
            [
                "echo \"NUGET_API_KEY=${{ steps.nuget-login.outputs.NUGET_API_KEY }}\" >> \"$GITHUB_ENV\""
            ]
        });
    }
}
