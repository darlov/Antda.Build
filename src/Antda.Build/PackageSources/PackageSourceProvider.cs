using System.Collections.Generic;
using System.Linq;

namespace Antda.Build.PackageSources;

public class PackageSourceProvider : IPackageSourceProvider
{
  private readonly IEnumerable<PackageSourceConfig> _packageSourceConfigs;

  public PackageSourceProvider(IEnumerable<PackageSourceConfig> packageSourceConfigs)
  {
    _packageSourceConfigs = packageSourceConfigs;
  }

  public IEnumerable<PackageSource> GetPackageSources() 
    => _packageSourceConfigs.Select(config => config.Resolve()).Where(source => source != null).ToList()!;
}