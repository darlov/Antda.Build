using Antda.Build.Context;
using Cake.Common.Build;
using Cake.Core;
using Microsoft.Extensions.Options;

namespace Antda.Build.BuildProviders;

public class BuildProviderFactory : IBuildProviderFactory
{
  private readonly ICakeContext _context;
  private readonly PathOptions _pathOptions;

  public BuildProviderFactory(IOptions<PathOptions> pathOptions, ICakeContext context)
  {
    _pathOptions = pathOptions.Value;
    _context = context;
  }

  public IBuildProvider Create()
  {
    var buildSystem = _context.BuildSystem();
    if (buildSystem.IsLocalBuild)
    {
      return new LocalBuildProvider(_context, _pathOptions);
    }

    if (buildSystem.IsRunningOnAppVeyor)
    {
      return new AppVeyorBuildProvider(buildSystem.AppVeyor);
    }
    
    return new LocalBuildProvider(_context, _pathOptions);
  }
}