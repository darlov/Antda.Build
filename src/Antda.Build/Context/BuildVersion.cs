namespace Antda.Build.Context;

public class BuildVersion
{
  public BuildVersion(string semVersion, string informationalVersion)
  {
    SemVersion = semVersion;
    InformationalVersion = informationalVersion;
  }

  public string SemVersion { get; }

  public string InformationalVersion { get; }
}