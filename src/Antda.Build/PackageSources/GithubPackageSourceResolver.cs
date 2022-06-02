using Antda.Build.Context;
using Cake.Common.Diagnostics;
using Cake.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Antda.Build.PackageSources;

public class GithubPackageSourceResolver: IPackageSourceResolver
{
  private readonly IConfiguration _configuration;
  private readonly ICakeContext _cakeContext;
  private readonly GithubOptions _githubOptions;

  public GithubPackageSourceResolver(ICakeContext cakeContext, IOptions<GithubOptions> githubOptions, IConfiguration configuration)
  {
    _cakeContext = cakeContext;
    _configuration = configuration;
    _githubOptions = githubOptions.Value;
  }

  public PackageSource? ResolveConfiguration(PackageSourceConfig config)
  {
    if (string.IsNullOrEmpty(_githubOptions.RepositoryOwner))
    {
      _cakeContext.Warning("Cannot resolve PushSourceUrl for {0} as RepositoryOwner is empty", config.PrefixName);
      return null;
    }
    
    var packageSource = new PackageSource(config.PrefixName)
    {
      PreRelease = config.PreRelease,
    };

    _configuration.GetSection(config.PrefixName).Bind(packageSource);

    packageSource.PushSourceUrl ??= $"https://nuget.pkg.github.com/{_githubOptions.RepositoryOwner}/index.json";
    packageSource.ApiKey ??= _githubOptions.GithubToken;
    
    return packageSource;
  }
}