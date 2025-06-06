﻿using System.Collections.Generic;
using System.Reflection;
using Cake.Frosting;

namespace Antda.Build.Output;

public class DefaultBuildContextOutput(IFrostingContext defaultBuildContext) : ILogObjectProvider<DefaultBuildContext>
{
  private readonly DefaultBuildContext _defaultBuildContext = (DefaultBuildContext)defaultBuildContext;

  public IEnumerable<LogObject> GetLogs(DefaultBuildContext target)
  {
    var version = Assembly.GetCallingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

    return
    [
      new(version, "Antda.Build Version", false),
      new(target.IsMainRepository),
      new(target.PublishType),
      new(target.BranchType),
      new(target.BuildVersion.Milestone, nameof(target.BuildVersion.Milestone)),
      new(target.BuildVersion.SemVersion, nameof(target.BuildVersion.SemVersion)),
      new(target.BuildVersion.InformationalVersion, nameof(target.BuildVersion.InformationalVersion)),
      new(target.BuildPlatform.PlatformFamily),
      new(target.BuildPlatform.Description),
      new(target.BuildPlatform.Runtime),
      new(target.BuildPlatform.Architecture),
      new(target.BuildPlatform.Framework)
    ];
  }

  public string Name => "Build Context";

  public IEnumerable<LogObject> GetLogs() => GetLogs(_defaultBuildContext);
}