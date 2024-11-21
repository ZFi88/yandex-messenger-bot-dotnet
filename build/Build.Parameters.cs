using Nuke.Common;

partial class Build
{
    [Parameter] string Configuration = "Release";
    [Parameter] [Secret] string NugetApiKey;
    [Parameter] string NugetSource = "https://api.nuget.org/v3/index.json";
}