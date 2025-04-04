namespace Antda.Build.Context;

public class BuildVersion(string milestone, string version, string semVersion, string informationalVersion)
{
  public string Milestone { get; } = milestone;

  public string Version { get; } = version;

  public string SemVersion { get; } = semVersion;

  public string InformationalVersion { get; } = informationalVersion;
}