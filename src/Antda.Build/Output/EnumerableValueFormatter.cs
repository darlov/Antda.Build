using System.Collections;
using System.Text;

namespace Antda.Build.Output;

public class EnumerableValueFormatter
{
  private readonly IEnumerable _values;
  
  public EnumerableValueFormatter(IEnumerable values)
  {
    _values = values;
  }

  public override string? ToString()
  {
    var sb = new StringBuilder();
    var count = 0;
    
    sb.Append('[');
    
    foreach (var value in _values)
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