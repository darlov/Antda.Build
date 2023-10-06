using System.Runtime.CompilerServices;
using Antda.Build.Types;

namespace Antda.Build.Output;

public record LogObject(object? Value, [CallerArgumentExpression("Value")] string Title = StringNone.Value, bool HumanizeTitle = true)
{
  public static LogObject EmptyLine { get; } = new(null, "<EmptyLine>");

  public override string ToString() => string.Create(null, stackalloc char[256], $"[{Title}, {Value}]");
}