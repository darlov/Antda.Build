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

  public void PostConfigure(string name, ParameterOptions options)
  {
    options.ForceRun = _context.Argument("forceRun", options.ForceRun);
    options.Target = _context.Argument("target", "Default");
    options.Configuration = _context.Argument("configuration", "Release");

    options.Title = string.IsNullOrEmpty(options.Title)
      ? options.RepositoryName
      : options.Title;
  }
}