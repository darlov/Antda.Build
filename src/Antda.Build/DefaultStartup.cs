using System;
using System.Collections.Generic;
using Antda.Build.BuildProviders;
using Antda.Build.Context;
using Cake.Frosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
    
    services.Configure<BuildOptions>(_configuration);
    services.AddSingleton<IPostConfigureOptions<BuildOptions>, BuildOptionsPostConfigure>();

    services.UseContext<DefaultBuildContext>();
    services.UseLifetime<DefaultLifetime>();
    services.AddSingleton<IBuildProviderFactory, BuildProviderFactory>();
    services.AddSingleton(s => s.GetRequiredService<IBuildProviderFactory>().Create());
  }

  protected virtual IEnumerable<string> GetTools()
  {
    return new[]
    {
      "dotnet:?package=GitVersion.Tool&version=5.10.1",
      "dotnet:?package=GitReleaseManager.Tool&version=0.13.0"
    };
  }
}