using System.Collections.Generic;

namespace Antda.Build.PackageSources;

public interface IPackageSourceProvider
{
  public IEnumerable<PackageSource> GetPackageSources();
}