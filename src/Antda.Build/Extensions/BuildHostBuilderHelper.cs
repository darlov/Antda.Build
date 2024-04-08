using Antda.Build.Context;

namespace Antda.Build.Extensions;

public static class BuildHostBuilderHelper
{
  public static BuildHostBuilder ConfigureDefaults(BuildHostBuilder builder) =>
    builder
      .WithOption(PathOptions.SourceKey, "src")
      .WithOption(PathOptions.OutputKey, "artifacts")
      .WithOption(PathOptions.OutputNugetPackagesKey, "nuget")
      .WithOption(PathOptions.OutputTestCoverageKey, "TestCoverage")
      .WithOption(PatternOptions.ProjectsKey, "**/*.csproj")
      .WithOption(PatternOptions.TestProjectsKey, "**/*Tests.csproj")
      .WithOption(PatternOptions.CoverageResultKey, "**/coverage.cobertura.xml")
      .WithOption(PatternOptions.MasterBranchKey, "main")
      .WithOption(PatternOptions.ReleaseBranchKey, "release")
      .WithOption(PatternOptions.DevelopBranchKey, "develop")
      .WithOption(PatternOptions.HotfixBranchKey, "hotfix")
      .WithOption(ParameterOptions.UpdateBuildNumberKey, bool.TrueString)
      .WithOption(ParameterOptions.ForceRunKey, bool.FalseString)
      .WithOptions(ParameterOptions.ReleaseBranchesKey, nameof(BranchType.Master), nameof(BranchType.Release), nameof(BranchType.Hotfix))
      .WithOptions(ParameterOptions.PreReleaseBranchesKey, nameof(BranchType.Develop))
      .WithOption(VariableOptions.GithubTokenKey, "ANTDA_GITHUB_PAT");
}