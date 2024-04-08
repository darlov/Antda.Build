using Microsoft.Extensions.Configuration;

namespace Antda.Build.PackageSources;

public class GeneralPackageSourceResolver : IPackageSourceResolver
{
  private readonly IConfiguration _configuration;

  public GeneralPackageSourceResolver(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public PackageSource ResolveConfiguration(PackageSourceConfig config)
  {
    var packageSource = new PackageSource(config.PrefixName)
    {
      PreRelease = config.PreRelease,
      PushSourceUrl = config.PushSourceUrl
    };

    _configuration.GetSection(config.PrefixName).Bind(packageSource);

    return packageSource;
  }
}