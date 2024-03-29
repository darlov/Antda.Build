﻿using System.ComponentModel.DataAnnotations;

namespace Antda.Build.Context;

public class PatternOptions
{
  public const string SectionName = "Pattern";

  public const string ProjectsKey = $"{SectionName}:{nameof(Projects)}";
  public const string TestProjectsKey = $"{SectionName}:{nameof(TestProjects)}";
  public const string MasterBranchKey = $"{SectionName}:{nameof(MasterBranch)}";
  public const string DevelopBranchKey = $"{SectionName}:{nameof(DevelopBranch)}";
  public const string HotfixBranchKey = $"{SectionName}:{nameof(HotfixBranch)}";
  public const string ReleaseBranchKey = $"{SectionName}:{nameof(ReleaseBranch)}";

  [Required]
  public string Projects { get; set; } = null!;

  [Required]
  public string TestProjects { get; set; } = null!;

  [Required]
  public string MasterBranch { get; set; } = null!;

  [Required]
  public string DevelopBranch { get; set; } = null!;

  [Required]
  public string HotfixBranch { get; set; } = null!;

  [Required]
  public string ReleaseBranch { get; set; } = null!;
}