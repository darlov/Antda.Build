using System;
using System.Collections.Generic;
using Antda.Build.Context;
using Antda.Build.Extensions;
using Cake.Frosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build;

public class BuildHostBuilder
{
  private readonly IDictionary<string, string> _buildConfigurations;
  private readonly IList<Action<IServiceCollection>> _serviceConfigurations;

  private BuildHostBuilder(string projectFilePath)
  {
    _buildConfigurations = new Dictionary<string, string>();
    _serviceConfigurations = new List<Action<IServiceCollection>>();

    WithOption(PathOptions.ProjectFileKey, projectFilePath);

    ConfigureServices(services =>
    {
      var configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(_buildConfigurations)
        .AddEnvironmentVariables()
        .Build();

      services.AddSingleton<IConfiguration>(configuration);
      var startup = new DefaultStartup(configuration);
      startup.Configure(services);
    });
  }

  public static BuildHostBuilder CreateDefault(string projectFilePath) 
    => BuildHostBuilderHelper.ConfigureDefaults(new BuildHostBuilder(projectFilePath));
  
  public static BuildHostBuilder CreateEmpty(string projectFilePath) => new(projectFilePath);

  public BuildHostBuilder ConfigureServices(Action<IServiceCollection> services)
  {
    _serviceConfigurations.Add(services);
    return this;
  }

  public CakeHost Build<TContext>()
    where TContext : class, IFrostingContext
  {
    return new CakeHost()
      .ConfigureServices(services =>
      {
        foreach (var serviceConfiguration in _serviceConfigurations)
        {
          serviceConfiguration.Invoke(services);
        }
      })
      .AddAssembly(typeof(DefaultStartup).Assembly)
      .UseContext<TContext>();
  }

  public CakeHost Build()
  {
    return Build<DefaultBuildContext>();
  }

  public BuildHostBuilder WithOption(string name, string value)
  {
    _buildConfigurations[name] = value;
    return this;
  }
}