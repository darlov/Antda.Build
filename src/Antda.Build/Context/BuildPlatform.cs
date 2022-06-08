using System.Runtime.InteropServices;
using Cake.Core;

namespace Antda.Build.Context;

public class BuildPlatform
{
  public BuildPlatform(ICakeContext context)
  {
    PlatformFamily = context.Environment.Platform.Family;
    Description = RuntimeInformation.OSDescription;
    Runtime = RuntimeInformation.RuntimeIdentifier;
    Architecture = RuntimeInformation.OSArchitecture.ToString();
    Framework = RuntimeInformation.FrameworkDescription;
  }

  public PlatformFamily PlatformFamily { get; }
  public string Framework { get; }

  public string Description { get; }

  public string Runtime { get; }

  public string Architecture { get; }
}