using Cake.Common;
using Cake.Core;
using Microsoft.Extensions.Options;

namespace Antda.Build.Context.Configurations;

public class ParameterOptionsPostConfigure(ICakeContext context) : IPostConfigureOptions<ParameterOptions>
{
  public void PostConfigure(string? name, ParameterOptions options)
  {
    options.UsePreRelease = context.HasArgument("usePreRelease");
    options.ForceRun = context.HasArgument("forceRun");
    options.Target = context.Argument("target", "Default");
    options.Configuration = context.Argument("configuration", "Release");

    options.Title = string.IsNullOrEmpty(options.Title)
      ? options.RepositoryName
      : options.Title;
  }
}