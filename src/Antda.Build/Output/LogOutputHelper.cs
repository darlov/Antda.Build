using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Cake.Common.Diagnostics;
using Cake.Core;
using Humanizer;

namespace Antda.Build.Output;

public static class LogOutputHelper
{
  public static void Log(ICakeContext context, LogObject logObject, char padChar = ' ', int leftPadSize = 2)
  {
    if (logObject == LogObject.EmptyLine)
    {
      context.Information(string.Empty);
    }
    else
    {
      Log(context, logObject.Value, logObject.Title, padChar, leftPadSize);
    }
  
  }
  
  public static void Log(ICakeContext context, object? value, [CallerArgumentExpression("value")] string title = "None", char padChar = ' ', int leftPadSize = 2)
  {
    var padding = new string(padChar, leftPadSize);
    context.Information($"{padding}{NormalizeTitle(title)}: {{0}}", value);
  }
  
  public static void LogGroup(ICakeContext context, string title)
  {
    context.Information("{0}:", NormalizeTitle(title));
  }

  private static string NormalizeTitle(string title)
  {
    var parts = title.Split('.', StringSplitOptions.RemoveEmptyEntries);

    if (parts.Length > 2)
    {
      return string.Join(" => ", parts.Skip(1).Select(s => s.Humanize(LetterCasing.Title)));
    }

    return (parts.LastOrDefault() ?? title).Humanize(LetterCasing.Title);
  }
}