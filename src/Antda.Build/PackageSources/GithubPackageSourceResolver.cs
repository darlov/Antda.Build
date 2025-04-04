using Antda.Build.Context;
using Cake.Common.Diagnostics;
using Cake.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Antda.Build.PackageSources;

public class GithubPackageSourceResolver(ICakeContext cakeContext, IOptions<GithubOptions> githubOptions, IConfiguration configuration) : IPackageSourceResolver
{
  private readonly GithubOptions _githubOptions = githubOptions.Value;

  public PackageSource? ResolveConfiguration(PackageSourceConfig config)
  {
    if (string.IsNullOrEmpty(_githubOptions.RepositoryOwner))
    {
      cakeContext.Warning("Cannot resolve PushSourceUrl for {0} as RepositoryOwner is empty", config.PrefixName);
      return null;
    }

    var packageSource = new PackageSource(config.PrefixName)
    {
      PreRelease = config.PreRelease
    };

    configuration.GetSection(config.PrefixName).Bind(packageSource);

    packageSource.PushSourceUrl ??= $"https://nuget.pkg.github.com/{_githubOptions.RepositoryOwner}/index.json";
    packageSource.ApiKey ??= _githubOptions.GithubToken;

    return packageSource;
  }
}