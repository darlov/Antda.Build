using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

  public bool UpdateBuildNumber { get; set; }

  public bool ForceRun { get; set; }
  
  [Required]
  public string Title { get; set; } = string.Empty;

  public string RepositoryName { get; set; } = string.Empty;

  public string RepositoryOwner { get; set; } = string.Empty;
  
  [Required]
  public string Target { get; set; } = null!;
  
  [Required]
  public string Configuration { get; set; } = null!;
  
  [Required]
  public string ReleaseBranches { get; set; } = null!;
  
  public IReadOnlyCollection<BranchType> BranchesToRelease { get; set; } = ArraySegment<BranchType>.Empty;
}