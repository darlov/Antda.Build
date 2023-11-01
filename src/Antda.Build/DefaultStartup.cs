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

public class DefaultStartup : IFrostingStartup
{
  private readonly IConfiguration _configuration;

  public DefaultStartup(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public void Configure(IServiceCollection services)
  {
    foreach (var toolUrl in GetTools())
    {
      services.UseTool(new Uri(toolUrl));
    }

    services.AddOptions<ParameterOptions>()
      .Bind(_configuration.GetSection(ParameterOptions.SectionName))
      .ValidateDataAnnotations();

    services.AddOptions<PathOptions>()
      .Bind(_configuration.GetSection(PathOptions.SectionName))
      .ValidateDataAnnotations();

    services.AddOptions<PatternOptions>()
      .Bind(_configuration.GetSection(PatternOptions.SectionName))
      .ValidateDataAnnotations();

    services.AddOptions<VariableOptions>()
      .Bind(_configuration.GetSection(VariableOptions.SectionName))
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

  protected virtual IEnumerable<string> GetTools()
  {
    return new[]
    {
      "dotnet:?package=GitVersion.Tool&version=5.12.0",
      "dotnet:?package=GitReleaseManager.Tool&version=0.15.0"
    };
  }
}