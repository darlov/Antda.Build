using System;
using System.Collections.Generic;
using System.IO;

namespace Antda.Build.Parsers;

public static class EnvParser
{
  public static unsafe IReadOnlyCollection<(string Name, string Value)> ParseEnvironmentVariables(Stream file)
  {
    using var streamReader = new StreamReader(file, leaveOpen: true);
    var reader = streamReader.ReadToEnd().AsSpan();

    var result = new List<(string Name, string Value)>();

    bool readHeredoc = false;
    ReadOnlySpan<char> heredocName = default;
    ReadOnlySpan<char> heredocValue = default;
    ReadOnlySpan<char> heredocDelimiter = default;
    
    foreach (var line in reader.EnumerateLines())
    {
      if (readHeredoc)
      {
        if (line.Equals(heredocDelimiter, StringComparison.Ordinal))
        {
          result.Add((heredocName.ToString(), heredocValue.ToString()));

          heredocName = default;
          heredocValue = default;
          heredocDelimiter = default;
          readHeredoc = false;
        }
        else
        {
          if (heredocValue == default)
          {
            heredocValue = line;
          }
          else
          {
            fixed (char* valuePtr = heredocValue, linePtr = line)
            {
              heredocValue = new ReadOnlySpan<char>(valuePtr, (int)((linePtr + line.Length) - valuePtr));
            }
          }
        }
      }
      else
      {
        var equalsIndex = line.IndexOf("=", StringComparison.Ordinal);
        var heredocIndex = line.IndexOf("<<", StringComparison.Ordinal);
        if (equalsIndex >= 0 && (heredocIndex < 0 || equalsIndex < heredocIndex))
        {
          var name = line[..equalsIndex];
          var value = line[(equalsIndex + 1)..];
          result.Add((name.ToString(), value.ToString()));
        } 
        else if (heredocIndex >= 0 && (equalsIndex < 0 || heredocIndex < equalsIndex))
        {
          heredocName = line[..heredocIndex];
          heredocDelimiter = line[(heredocIndex + 2)..];
          readHeredoc = true;
        
          if (heredocName.IsEmpty || heredocDelimiter.IsEmpty)
          {
            throw new Exception($"Invalid format '{line}'. Name must not be empty and delimiter must not be empty");
          }
        }
      }
    }
    
    if (heredocDelimiter != default)
    {
      throw new Exception($"Invalid value. Matching delimiter not found '{heredocDelimiter}'");
    }

    return result;
  }
}