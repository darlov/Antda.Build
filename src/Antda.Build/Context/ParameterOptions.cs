using System.Collections.Generic;

namespace Antda.Build.Context;

public class ParameterOptions
{
  public const string SectionName = "Parameter";

  public const string UpdateBuildNumberKey = $"{SectionName}:{nameof(UpdateBuildNumber)}";
  public const string ForceRunKey = $"{SectionName}:{nameof(ForceRun)}";
  public const string TitleKey = $"{SectionName}:{nameof(Title)}";
  public const string RepositoryNameKey = $"{SectionName}:{nameof(RepositoryName)}";
  public const string RepositoryOwnerKey = $"{SectionName}:{nameof(RepositoryOwner)}";
  public const string ReleaseBranchesKey = $"{SectionName}:{nameof(ReleaseBranches)}";
  public const string PreReleaseBranchesKey = $"{SectionName}:{nameof(PreReleaseBranches)}";

  public bool UpdateBuildNumber { get; set; }

  public bool ForceRun { get; set; }

  public bool UsePreRelease { get; set; }

  public string? Title { get; set; }

  public string RepositoryName { get; set; } = string.Empty;

  public string RepositoryOwner { get; set; } = string.Empty;

  public required string Target { get; set; }

  public required string Configuration { get; set; }

  public required IReadOnlyCollection<BranchType> ReleaseBranches { get; set; }

  public required IReadOnlyCollection<BranchType> PreReleaseBranches { get; set; }
}