using Antda.Build.Context;

namespace Antda.Build.Extensions;

public static class BuildHostBuilderHelper
{
  public static BuildHostBuilder ConfigureDefaults(BuildHostBuilder builder)
  {
    return builder
      .WithOption(PathOptions.SourceKey, "src")
      .WithOption(PathOptions.OutputKey, "artifacts")
      .WithOption(PathOptions.OutputNugetPackagesKey, "nuget")
      
      .WithOption(PatternOptions.ProjectsKey, "**/*.csproj")
      .WithOption(PatternOptions.TestProjectsKey, "**/*Tests.csproj")
      .WithOption(PatternOptions.MasterBranchKey, "^master$|^main$")
      .WithOption(PatternOptions.ReleaseBranchKey, "^releases?[/-]")
      .WithOption(PatternOptions.DevelopBranchKey, "^dev(elop)?(ment)?$")
      .WithOption(PatternOptions.HotfixBranchKey, "^hotfix(es)?[/-]")

      .WithOption(ParameterOptions.UpdateBuildNumberKey, bool.TrueString)
      .WithOption(ParameterOptions.ForceRunKey, bool.FalseString)
      .WithOption(ParameterOptions.ReleaseBranchesKey, "master;release;hotfix")

      .WithOption(VariableOptions.GithubTokenKey, "GITHUB_PAT");
  }
}