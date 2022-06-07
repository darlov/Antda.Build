namespace Antda.Build.PackageSources;

public interface IPackageSourceResolver
{
  public PackageSource? ResolveConfiguration(PackageSourceConfig config);
}