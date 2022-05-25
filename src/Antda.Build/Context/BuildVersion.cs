namespace Antda.Build.Context;

public class BuildVersion
{
  public BuildVersion(string semVer, string informationalVersion)
  {
    SemVer = semVer;
    InformationalVersion = informationalVersion;
  }

  public string SemVer { get; }

  public string InformationalVersion { get; }
}