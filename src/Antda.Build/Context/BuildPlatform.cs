using System.Runtime.InteropServices;
using Cake.Core;

namespace Antda.Build.Context;

public class BuildPlatform(ICakeContext context)
{
  public PlatformFamily PlatformFamily { get; } = context.Environment.Platform.Family;
  public string Framework { get; } = RuntimeInformation.FrameworkDescription;

  public string Description { get; } = RuntimeInformation.OSDescription;

  public string Runtime { get; } = RuntimeInformation.RuntimeIdentifier;

  public string Architecture { get; } = RuntimeInformation.OSArchitecture.ToString();
}