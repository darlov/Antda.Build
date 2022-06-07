namespace Antda.Build.PackageSources;

public class PackageSourceConfig
{
  private readonly IPackageSourceResolver _resolver;

  public PackageSourceConfig(IPackageSourceResolver resolver, string prefixName, string? pushSourceUrl, bool preRelease)
  {
    _resolver = resolver;

    PrefixName = prefixName;
    PushSourceUrl = pushSourceUrl;
    PreRelease = preRelease;
  }

  public string PrefixName { get; }

  public string? PushSourceUrl { get; }

  public bool PreRelease { get; }

  public PackageSource? Resolve() => _resolver.ResolveConfiguration(this);
}