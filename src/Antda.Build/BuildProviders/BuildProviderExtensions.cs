namespace Antda.Build.BuildProviders;

public static class BuildProviderExtensions
{
  public static bool IsLocalBuild(this IBuildProvider buildProvider) => buildProvider.Type == BuildProviderType.Local;
}