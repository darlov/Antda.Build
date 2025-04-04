namespace Antda.Build.PackageSources;

public class PackageSource(string prefixName)
{
  public string PrefixName { get; } = prefixName;

  public string? PushSourceUrl { get; set; }

  public bool PreRelease { get; set; }

  public string? ApiKey { get; set; }
}