using System.Collections;
using System.Text;

namespace Antda.Build.Output;

public class EnumerableValueFormatter(IEnumerable values)
{
  public override string ToString()
  {
    var sb = new StringBuilder();
    var count = 0;

    sb.Append('[');

    foreach (var value in values)
    {
      if (count > 0)
      {
        sb.Append(", ");
      }

      sb.Append(value ?? "NULL");

      count++;
    }

    sb.Append(']');

    return sb.ToString();
  }
}