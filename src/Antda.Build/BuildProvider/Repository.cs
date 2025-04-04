using Antda.Build.Types;

namespace Antda.Build.BuildProvider;

public class Repository(string name, bool exist)
{
  public bool Exist { get; } = exist;

  public bool IsPullRequest { get; init; }

  public string Name { get; } = name;

  public string BranchName { get; init; } = StringNone.Value;

  public bool IsTag { get; init; }

  public string? TagName { get; init; }

  public bool IsTagged => IsTag && !string.IsNullOrEmpty(TagName);
}