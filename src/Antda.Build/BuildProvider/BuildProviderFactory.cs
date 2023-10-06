using System;
using Antda.Build.BuildProvider.Agents;
using Cake.Common.Build;
using Cake.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build.BuildProvider;

public class BuildProviderFactory : IBuildProviderFactory
{
  private readonly ICakeContext _context;
  private readonly IServiceProvider _serviceProvider;

  public BuildProviderFactory(ICakeContext context, IServiceProvider serviceProvider)
  {
    _context = context;
    _serviceProvider = serviceProvider;
  }

  public IBuildProvider Create() =>
    _context.BuildSystem() switch
    {
      { IsLocalBuild: true } => _serviceProvider.GetRequiredService<LocalBuildProvider>(),
      { IsRunningOnAppVeyor: true } => _serviceProvider.GetRequiredService<AppVeyorBuildProvider>(),
      { IsRunningOnGitHubActions: true } => _serviceProvider.GetRequiredService<GitHubActionsBuildProvider>(),
      _ => throw new InvalidOperationException("The current build provider is not supported.")
    };
}