using System;
using System.Collections.Generic;
using Antda.Build.Extensions;
using Cake.Frosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build;

public class BuildHostBuilder
{
  private readonly Dictionary<string, string?> _buildConfigurations = new();
  private readonly List<Action<IServiceCollection>> _serviceConfigurations = [];

  private BuildHostBuilder()
  { }

  public static BuildHostBuilder CreateDefault()
  {
    var builder = new BuildHostBuilder();
    builder.ConfigureDefaultServices<DefaultStartup>();

    return BuildHostBuilderHelper.ConfigureDefaults(builder);
  }

  public static BuildHostBuilder CreateDefault<T>() where T : IHostStartup, new()
  {
    var builder = new BuildHostBuilder();
    builder.ConfigureDefaultServices<T>();

    return BuildHostBuilderHelper.ConfigureDefaults(builder);
  }

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

  public CakeHost Build() => Build<DefaultBuildContext>();

  public BuildHostBuilder WithOption(string name, string value)
  {
    _buildConfigurations[name] = value;
    return this;
  }

  public BuildHostBuilder WithOptions(string name, params string[] values)
  {
    var strings = values ?? throw new ArgumentNullException(nameof(values));

    for (var index = 0; index < strings.Length; index++)
    {
      _buildConfigurations[$"{name}:{index}"] = strings[index];
    }

    return this;
  }

  private void ConfigureDefaultServices<T>() where T : IHostStartup, new()
  {
    ConfigureServices(services =>
    {
      var configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(_buildConfigurations)
        .AddEnvironmentVariables()
        .Build();

      services.AddSingleton<IConfiguration>(configuration);
      var startup = new T();
      startup.Configure(services, configuration);
    });
  }
}