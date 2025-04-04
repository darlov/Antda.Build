namespace Antda.Build.PackageSources;

public class PackageSourceConfig(IPackageSourceResolver resolver, string prefixName, string? pushSourceUrl, bool preRelease)
{
  public string PrefixName => prefixName;

  public string? PushSourceUrl => pushSourceUrl;

  public bool PreRelease => preRelease;

  public PackageSource? Resolve() => resolver.ResolveConfiguration(this);
}