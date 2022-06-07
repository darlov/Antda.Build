using Antda.Build.Types;

namespace Antda.Build.BuildProviders;

public class Repository
{
  public Repository(string name, bool exist)
  {
    Name = name;
    Exist = exist;
  }

  public bool Exist { get; }

  public bool IsPullRequest { get; init; }

  public string Name { get; }

  public string BranchName { get; init; } = StringNone.Value;

  public bool IsTag { get; init; }

  public string? TagName { get; init; }
}