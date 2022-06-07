namespace Antda.Build.Context;

public class VariableOptions
{
  public const string SectionName = "Variable";

  public const string GithubTokenKey = $"{SectionName}:{nameof(GithubToken)}";

  public string? GithubToken { get; set; }
}