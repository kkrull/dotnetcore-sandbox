using Xunit;

namespace Cli.Test
{
  public class ProgramTest
  {
    [Fact(DisplayName = "it loads configuration from the test assembly")]
    public void LoadsTestConfig()
    {
      var program = new Program().Configure();
      Assert.Equal("Cli.Test/appsettings.json", program.Source);
    }

    [Fact(DisplayName = "it loads Razor pages from the Cli assembly directory", Skip = "pending")]
    public void LoadPagesFromContentRoot()
    {
    }
  }
}
