using Antda.Build.Context;
using Antda.Build.PackageSources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Antda.Build.Extensions;

public static class BuildHostBuilderOptionsExtensions
{
  public static BuildHostBuilder WithTitle(this BuildHostBuilder builder, string title)
    => builder.WithOption(ParameterOptions.TitleKey, title);

  public static BuildHostBuilder WithSource(this BuildHostBuilder builder, string sourceDirectoryPath)
    => builder.WithOption(PathOptions.SourceKey, sourceDirectoryPath);

  public static BuildHostBuilder WithRepository(this BuildHostBuilder builder, string repositoryName, string repositoryOwner)
    => builder
      .WithOption(ParameterOptions.RepositoryNameKey, repositoryName)
      .WithOption(ParameterOptions.RepositoryOwnerKey, repositoryOwner);

  public static BuildHostBuilder UsePackageSource(this BuildHostBuilder builder, string prefixName, bool preRelease = false)
    => builder.UsePackageSourceResolver<GeneralPackageSourceResolver>(prefixName, null, preRelease);
  
  public static BuildHostBuilder UseGithubPackageSource(this BuildHostBuilder builder, string prefixName = "Github", bool preRelease = true)
    => builder.UsePackageSourceResolver<GithubPackageSourceResolver>(prefixName, null, preRelease);
  
  public static BuildHostBuilder UseNugetPackageSource(this BuildHostBuilder builder, string prefixName = "Nuget", bool preRelease = false)
    => builder.UsePackageSourceResolver<NugetPackageSourceResolver>(prefixName, null, preRelease);

  public static BuildHostBuilder UsePackageSourceResolver<T>(this BuildHostBuilder builder, string prefixName, string? pushSourceUrl, bool preRelease)
    where T : class, IPackageSourceResolver
  {
    return builder.ConfigureServices(
      services =>
      {
        services.TryAddSingleton<IPackageSourceProvider, PackageSourceProvider>();
        services.TryAddSingleton<T, T>();
        services.AddSingleton(s => new PackageSourceConfig(s.GetRequiredService<T>(),prefixName, pushSourceUrl, preRelease));
      });
  }
}