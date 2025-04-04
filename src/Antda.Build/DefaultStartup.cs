using System;
using System.Collections.Generic;
using Antda.Build.BuildProvider;
using Antda.Build.BuildProvider.Agents;
using Antda.Build.Context;
using Antda.Build.Context.Configurations;
using Antda.Build.Output;
using Cake.Frosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build;

public class DefaultStartup : IHostStartup
{
  public void Configure(IServiceCollection services, IConfiguration configuration)
  {
    foreach (var toolUrl in GetTools())
    {
      services.UseTool(new Uri(toolUrl));
    }

    services.AddOptions<ParameterOptions>()
      .Bind(configuration.GetSection(ParameterOptions.SectionName))
      .ValidateDataAnnotations();

    services.AddOptions<PathOptions>()
      .Bind(configuration.GetSection(PathOptions.SectionName))
      .ValidateDataAnnotations();

    services.AddOptions<PatternOptions>()
      .Bind(configuration.GetSection(PatternOptions.SectionName))
      .ValidateDataAnnotations();

    services.AddOptions<VariableOptions>()
      .Bind(configuration.GetSection(VariableOptions.SectionName))
      .ValidateDataAnnotations();

    services.ConfigureOptions<ParameterOptionsPostConfigure>();
    services.ConfigureOptions<PathOptionsPostConfigure>();
    services.ConfigureOptions<GithubOptionsConfigure>();
    services.AddSingleton<BuildPlatform>();

    services.UseContext<DefaultBuildContext>();
    services.UseLifetime<DefaultLifetime>();
    services.AddSingleton<IBuildProviderFactory, BuildProviderFactory>();
    services.AddSingleton(m => m.GetRequiredService<IBuildProviderFactory>().Create());
    services.AddSingleton<LocalBuildProvider>();
    services.AddSingleton<GitHubActionsBuildProvider>();
    services.AddSingleton<AppVeyorBuildProvider>();

    services.AddLogObjectProvider<ParameterOptionsOutput>();
    services.AddLogObjectProvider<PathOptionsOutput>();
    services.AddLogObjectProvider<PatternOptionsOutput>();
    services.AddLogObjectProvider<DefaultBuildContextOutput>();
    services.AddLogObjectProvider<BuildProviderOutput>();
    services.AddLogObjectProvider<PackageSourcesOutput>();
  }

  protected virtual IEnumerable<string> GetTools() =>
  [
    "dotnet:?package=GitVersion.Tool&version=6.2.0",
    "dotnet:?package=GitReleaseManager.Tool&version=0.20.0",
    "dotnet:?package=dotnet-reportgenerator-globaltool&version=5.4.5"
  ];
}