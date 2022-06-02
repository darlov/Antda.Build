using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Antda.Build.Output;

public record LogObject(object? Value, [CallerArgumentExpression("Value")] string Title = "<None>")
{
  public static LogObject EmptyLine { get; } = new("<NewLine>");
  
  public override string ToString()
  {
    return KeyValuePair.Create(Title, Value).ToString();
  }
}