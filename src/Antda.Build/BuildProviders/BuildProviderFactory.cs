using System;
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

    var type = GetProviderType(buildSystem);

    return type switch
    {
      BuildProviderType.Local => new LocalBuildProvider(_context, _pathOptions),
      BuildProviderType.AppVeyor => new AppVeyorBuildProvider(buildSystem.AppVeyor),
      BuildProviderType.AzurePipelines => new AzurePipelinesBuildProvider(buildSystem.AzurePipelines, _context, _pathOptions),
      _ => throw new NotSupportedException($"The '{buildSystem.Provider & ~BuildProvider.Local}' build system is not supported.")
    };
  }

  private BuildProviderType GetProviderType(BuildSystem buildSystem)
  {
    switch (buildSystem.Provider)
    {
      case BuildProvider.Local:
        return BuildProviderType.Local;
      case var type when (type & BuildProvider.AppVeyor) == BuildProvider.AppVeyor: 
        return BuildProviderType.AppVeyor;
      case var type when (type & BuildProvider.AzurePipelines) == BuildProvider.AzurePipelines: 
        return BuildProviderType.AzurePipelines;
      default:
        return BuildProviderType.None;
    }
  }
}