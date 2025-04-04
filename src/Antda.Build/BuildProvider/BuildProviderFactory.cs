using System;
using Antda.Build.BuildProvider.Agents;
using Cake.Common.Build;
using Cake.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build.BuildProvider;

public class BuildProviderFactory(ICakeContext context, IServiceProvider serviceProvider) : IBuildProviderFactory
{
  public IBuildProvider Create() =>
    context.BuildSystem() switch
    {
      { IsLocalBuild: true } => serviceProvider.GetRequiredService<LocalBuildProvider>(),
      { IsRunningOnAppVeyor: true } => serviceProvider.GetRequiredService<AppVeyorBuildProvider>(),
      { IsRunningOnGitHubActions: true } => serviceProvider.GetRequiredService<GitHubActionsBuildProvider>(),
      _ => throw new InvalidOperationException("The current build provider is not supported.")
    };
}