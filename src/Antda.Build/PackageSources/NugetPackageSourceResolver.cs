using Microsoft.Extensions.Configuration;

namespace Antda.Build.PackageSources;

public class NugetPackageSourceResolver(IConfiguration configuration) : IPackageSourceResolver
{
  public PackageSource? ResolveConfiguration(PackageSourceConfig config)
  {
    var packageSource = new PackageSource(config.PrefixName)
    {
      PreRelease = config.PreRelease
    };

    configuration.GetSection(config.PrefixName).Bind(packageSource);

    packageSource.PushSourceUrl ??= "https://api.nuget.org/v3/index.json";

    return packageSource;
  }
}