using System.Collections.Generic;

namespace Antda.Build.Context;

public class PathOptions
{
  public const string SectionName = "Path";

  public const string RootKey = $"{SectionName}:{nameof(Root)}";
  public const string SourceKey = $"{SectionName}:{nameof(Source)}";
  public const string OutputKey = $"{SectionName}:{nameof(Output)}";
  public const string ProjectFilesKey = $"{SectionName}:{nameof(ProjectFiles)}";
  public const string OutputNugetPackagesKey = $"{SectionName}:{nameof(OutputNugetPackages)}";
  public const string OutputTestCoverageKey = $"{SectionName}:{nameof(OutputTestCoverage)}";

  public string Root { get; set; } = string.Empty;

  public required string Source { get; set; }

  public required string Output { get; set; }

  public IReadOnlyCollection<string>? ProjectFiles { get; set; }

  public required string OutputNugetPackages { get; set; }

  public string? GitRoot { get; set; }

  public required string OutputTestCoverage { get; set; } = string.Empty;
}