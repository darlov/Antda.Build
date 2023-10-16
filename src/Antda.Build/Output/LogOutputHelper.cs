using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Antda.Build.Types;
using Cake.Common.Diagnostics;
using Cake.Core;
using Humanizer;

namespace Antda.Build.Output;

public static class LogOutputHelper
{
  public static void Log(ICakeContext context, IEnumerable<LogObjectGroup> groups)
  {
    var itemGroups = groups
      .GroupBy(m => m.Name, g => g.Items.Select(s => (NormalizedTitle: NormalizeTitle(s.Title, s.HumanizeTitle), s.Value)).ToList())
      .ToList();

    var maxLength = itemGroups.SelectMany(s => s.SelectMany(m => m.Select(d => d.NormalizedTitle.Length))).Max();

    foreach (var itemGroup in itemGroups)
    {
      LogGroup(context, itemGroup.Key, maxLength);
      foreach (var item in itemGroup.SelectMany(m => m))
      {
        Log(context, item.Value, item.NormalizedTitle, maxLength);
      }
    }
  }

  public static void Log(ICakeContext context, LogObject logObject, int padRightSize = 2)
  {
    if (logObject == LogObject.EmptyLine)
    {
      context.Information(string.Empty);
    }
    else
    {
      Log(context, logObject.Value, logObject.Title, logObject.HumanizeTitle, padRightSize);
    }
  }

  public static void Log(ICakeContext context, object? value, [CallerArgumentExpression("value")] string title = StringNone.Value, bool humanizeTitle = true, int padRightSize = 2)
  {
    var normalizedTitle = NormalizeTitle(title, humanizeTitle);
    Log(context, value, normalizedTitle, padRightSize);
  }

  public static void Log(ICakeContext context, object? value, string title, int padRightSize = 2)
  {
    if (string.IsNullOrEmpty(title))
    {
      context.Information(string.Empty);
    }
    else
    {
      var paddedTitle = $"{title}".PadRight(padRightSize);

      if (value != null && typeof(string) != value.GetType() && value is IEnumerable enumerableValue)
      {
        value = new EnumerableValueFormatter(enumerableValue);
      }

      context.Information("{0}: {1}", paddedTitle, value);
    }
  }

  public static void LogGroup(ICakeContext context, string title, int padRightSize = 0, bool humanizeTitle = true)
  {
    var normalizedTitle = NormalizeTitle(title, humanizeTitle);
    if (padRightSize != 0)
    {
      var titleLength = normalizedTitle.Length + 2;

      var length = Math.Max(padRightSize - titleLength, 2);
      var startCount = length / 2;

      var startSpace = new string('-', startCount);
      var endSpace = new string('-', startCount + (padRightSize - (startCount + titleLength + startCount)));
      normalizedTitle = $"{startSpace} {normalizedTitle} {endSpace}";
    }

    context.Information("{0}", normalizedTitle);
  }

  private static string NormalizeTitle(string title, bool humanizeTitle)
  {
    if (LogObject.EmptyLine.Title.Equals(title))
    {
      return string.Empty;
    }

    string Humanize(string value) => value.Humanize(LetterCasing.Title);

    if (humanizeTitle)
    {
      var parts = title.Split('.', StringSplitOptions.RemoveEmptyEntries);

      return parts.Length > 2
        ? string.Join(" => ", parts.Skip(1).Select(Humanize))
        : Humanize(parts.LastOrDefault() ?? title);
    }

    return title;
  }
}