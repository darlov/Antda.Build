using Antda.Build.Context;
using Cake.Common.Build;
using Cake.Core;
using Microsoft.Extensions.Options;

namespace Antda.Build.BuildProviders;

public class BuildProviderFactory : IBuildProviderFactory
{
  private readonly ICakeContext _context;
  private readonly BuildOptions _buildOptions;

  public BuildProviderFactory(IOptions<BuildOptions> buildOptions, ICakeContext context)
  {
    _buildOptions = buildOptions.Value;
    _context = context;
  }

  public IBuildProvider Create()
  {
    var buildSystem = _context.BuildSystem();
    if (buildSystem.IsLocalBuild)
    {
      return new LocalBuildProvider(_context, _buildOptions);
    }

    if (buildSystem.IsRunningOnAppVeyor)
    {
      return new AppVeyorBuildProvider(buildSystem.AppVeyor);
    }
    
    return new LocalBuildProvider(_context, _buildOptions);
  }
}