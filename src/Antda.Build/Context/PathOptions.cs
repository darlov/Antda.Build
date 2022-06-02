using System.ComponentModel.DataAnnotations;

namespace Antda.Build.Context;

public class PathOptions
{
  public const string SectionName = "Path";
    
  public const string RootKey = $"{SectionName}:{nameof(Root)}";
  public const string SourceKey = $"{SectionName}:{nameof(Source)}";
  public const string OutputKey = $"{SectionName}:{nameof(Output)}";
  public const string ProjectFileKey = $"{SectionName}:{nameof(ProjectFile)}";
  public const string OutputNugetPackagesKey = $"{SectionName}:{nameof(OutputNugetPackages)}";

  public string Root { get; set; } = string.Empty;

  [Required]
  public string Source { get; set; } = string.Empty;

  [Required]
  public string Output { get; set;} = string.Empty;
  
  [Required]
  public string ProjectFile { get; set; } = string.Empty;
  
  [Required]
  public string OutputNugetPackages { get; set;} = string.Empty;
}