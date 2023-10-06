using System.Text;
using Antda.Build.Parsers;

namespace Antda.Build.Tests.Parsers;

public class EnvParserTests
{
  [Fact]
  
  public void ParseEnvironmentVariables()
  {
    var values = EnvParser.ParseEnvironmentVariables(new MemoryStream(
      """
        
        test_name1=123123
        testname2<<EOF
        line1
        line2
        EOF
        
        """u8.ToArray()));
    
    Assert.NotEmpty(values);
    Assert.Equal(2, values.Count);
    
    Assert.Equal(("test_name1", "123123"), values.ElementAt(0));
    Assert.Equal(("testname2", $"line1{Environment.NewLine}line2"), values.ElementAt(1));
  }
}