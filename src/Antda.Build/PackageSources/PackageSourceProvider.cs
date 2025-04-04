using System.Collections.Generic;
using System.Linq;

namespace Antda.Build.PackageSources;

public class PackageSourceProvider(IEnumerable<PackageSourceConfig> packageSourceConfigs) : IPackageSourceProvider
{
  public IEnumerable<PackageSource> GetPackageSources()
    => packageSourceConfigs.Select(config => config.Resolve()).Where(source => source != null).ToList()!;
}