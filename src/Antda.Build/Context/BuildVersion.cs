namespace Antda.Build.Context;

public class BuildVersion
{
  public BuildVersion(string milestone, string version, string semVersion, string informationalVersion)
  {
    SemVersion = semVersion;
    InformationalVersion = informationalVersion;
    Version = version;
    Milestone = milestone;
  }

  public string Milestone { get; }

  public string Version { get; }

  public string SemVersion { get; }

  public string InformationalVersion { get; }
}