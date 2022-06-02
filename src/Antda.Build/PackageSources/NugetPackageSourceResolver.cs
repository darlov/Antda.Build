using Microsoft.Extensions.Configuration;

namespace Antda.Build.PackageSources;

public class NugetPackageSourceResolver : IPackageSourceResolver
{
  private readonly IConfiguration _configuration;

  public NugetPackageSourceResolver(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public PackageSource? ResolveConfiguration(PackageSourceConfig config)
  {
    var packageSource = new PackageSource(config.PrefixName)
    {
      PreRelease = config.PreRelease
    };

    _configuration.GetSection(config.PrefixName).Bind(packageSource);
    
    packageSource.PushSourceUrl ??= "https://api.nuget.org/v3/index.json";
    
    return packageSource;
  }
}