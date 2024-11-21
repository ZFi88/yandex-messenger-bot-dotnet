using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Git;

partial class Build
{
    string GetCurrentCommitSha() =>
        GitHubActions.Instance != null ? GitHubActions.Instance.Sha : Repository.NotNull().Head;

    bool CheckTags()
    {
        var sha = GetCurrentCommitSha();
        var tags = GetTagsPointsOnSha(sha);
        return GetPackableProjectTags(tags).Any();
    }

    IEnumerable<string> GetPackableProjectTags(List<string> tags)
    {
        var projectNames = string.Join('|', PackableProjects().Select(x => x.Name));
        var regex = new Regex($"({projectNames}).[0-9.devrc-]+");
        return tags.Where(x => regex.IsMatch(x));
    }

    List<string> GetTagsPointsOnSha(string sha)
    {
        var tags = GitTasks.Git($"tag --points-at {sha}", logOutput: false, logInvocation: false)
            .Select(x => x.Text)
            .ToList();
        return tags;
    }

    IEnumerable<Project> PackableProjects() => Solution.AllProjects.Where(x => x.GetProperty<bool>("IsPackable"));
}