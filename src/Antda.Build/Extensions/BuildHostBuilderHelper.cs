using Antda.Build.Context;

namespace Antda.Build.Extensions;

public static class BuildHostBuilderHelper
{
  public static BuildHostBuilder ConfigureDefaults(BuildHostBuilder builder) =>
    builder
      .WithOption(PathOptions.SourceKey, "src")
      .WithOption(PathOptions.OutputKey, "artifacts")
      .WithOption(PathOptions.OutputNugetPackagesKey, "nuget")
      .WithOption(PatternOptions.ProjectsKey, "**/*.csproj")
      .WithOption(PatternOptions.TestProjectsKey, "**/*Tests.csproj")
      .WithOption(PatternOptions.MasterBranchKey, "main")
      .WithOption(PatternOptions.ReleaseBranchKey, "release")
      .WithOption(PatternOptions.DevelopBranchKey, "develop")
      .WithOption(PatternOptions.HotfixBranchKey, "hotfix")
      .WithOption(ParameterOptions.UpdateBuildNumberKey, bool.TrueString)
      .WithOption(ParameterOptions.ForceRunKey, bool.FalseString)
      .WithOptions(ParameterOptions.ReleaseBranchesKey, BranchType.Master.ToString(), BranchType.Release.ToString(), BranchType.Hotfix.ToString())
      .WithOptions(ParameterOptions.PreReleaseBranchesKey, BranchType.Develop.ToString())
      .WithOption(VariableOptions.GithubTokenKey, "GITHUB_PAT");
}