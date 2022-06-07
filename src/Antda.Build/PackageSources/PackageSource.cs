namespace Antda.Build.PackageSources;

public class PackageSource
{
  public PackageSource(string prefixName)
  {
    PrefixName = prefixName;
  }

  public string PrefixName { get; }

  public string? PushSourceUrl { get; set; }

  public bool PreRelease { get; set; }

  public string? ApiKey { get; set; }
}