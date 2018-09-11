using Web.Config;

namespace Web.Test
{
  public class StaticConfiguration : IConfigureServers
  {
    public string Source { get; set; }
  }
}
