using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Antda.Build.Context;
using Antda.Build.Types;
using Cake.Core.IO;
using Cake.Frosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Build;

public class BuildHostBuilder
{
  private readonly ICollection<KeyValuePair<string, string>> _configuration;
  private readonly ICollection<KeyValuePair<string, string>> _defaultConfiguration;

  public static BuildHostBuilder CreateDefault(string projectFilePath)
  {
    TypeDescriptor.AddAttributes(typeof(DirectoryPath), new TypeConverterAttribute(typeof(DirectoryPathTypeConverter)));
    TypeDescriptor.AddAttributes(typeof(FilePath), new TypeConverterAttribute(typeof(FilePathTypeConverter)));
    
    return new BuildHostBuilder(projectFilePath);
  }

  public BuildHostBuilder WithTitle(string title)
  {
    _configuration.Add(new(ConfigurationNames.Title, title));
    return this;
  }
  
  public BuildHostBuilder WithRepository(string repositoryName, string repositoryOwner)
  {
    _configuration.Add(new(ConfigurationNames.RepositoryName, repositoryName));
    _configuration.Add(new(ConfigurationNames.RepositoryOwner, repositoryOwner));
    return this;
  }

  private BuildHostBuilder(string projectFilePath)
  {
    _defaultConfiguration = new List<KeyValuePair<string, string>>
    {
      new(ConfigurationNames.SourceDirectoryPath, "src"),
      new(ConfigurationNames.ProjectsPattern, "**/*.csproj"),
      new(ConfigurationNames.TestProjectsPattern, "**/*.csproj"),
      new(ConfigurationNames.OutputDirectoryPath, "artifacts")
    };
    
    _configuration = new List<KeyValuePair<string, string>>();
    _configuration.Add(new(ConfigurationNames.ProjectFilePath, projectFilePath));
  }

  public CakeHost Build<TContext>()
    where TContext : class, IFrostingContext
  {
    var configuration = new ConfigurationBuilder()
      .AddInMemoryCollection(_defaultConfiguration)
      .AddInMemoryCollection(_configuration)
      .Build();

    return new CakeHost()
      .ConfigureServices(services =>
      {
        services.AddSingleton<IConfiguration>(configuration);
        var startup = new DefaultStartup(configuration);
        startup.Configure(services);
      })
      .AddAssembly(typeof(DefaultStartup).Assembly)
      .UseContext<TContext>();
  }

  public CakeHost Build() => Build<DefaultBuildContext>();
}