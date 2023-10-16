using System;
using System.Linq;
using Cake.Common;
using Cake.Core;
using Humanizer;
using Microsoft.Extensions.Options;

namespace Antda.Build.Context.Configurations;

public class ParameterOptionsPostConfigure : IPostConfigureOptions<ParameterOptions>
{
  private readonly ICakeContext _context;

  public ParameterOptionsPostConfigure(ICakeContext context)
  {
    _context = context;
  }

  public void PostConfigure(string? name, ParameterOptions options)
  {
    options.UsePreRelease = _context.HasArgument("usePreRelease");
    options.ForceRun = _context.HasArgument("forceRun");
    options.Target = _context.Argument("target", "Default");
    options.Configuration = _context.Argument("configuration", "Release");

    options.Title = string.IsNullOrEmpty(options.Title)
      ? options.RepositoryName
      : options.Title;
  }
}