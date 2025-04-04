using Microsoft.Extensions.Configuration;

namespace Antda.Build.PackageSources;

public class GeneralPackageSourceResolver(IConfiguration configuration) : IPackageSourceResolver
{
  public PackageSource? ResolveConfiguration(PackageSourceConfig config)
  {
    var packageSource = new PackageSource(config.PrefixName)
    {
      PreRelease = config.PreRelease,
      PushSourceUrl = config.PushSourceUrl
    };

    configuration.GetSection(config.PrefixName).Bind(packageSource);

    return packageSource;
  }
}