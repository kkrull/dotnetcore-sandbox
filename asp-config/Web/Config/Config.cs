using Microsoft.AspNetCore.Mvc;

namespace Web.Config
{
  public class Config : Controller
  {
    private readonly IConfigureServers _config;

    public Config(IConfigureServers config)
    {
      _config = config;
    }

    [HttpGet("/config/source")]
    public string Index()
    {
      return _config.Source;
    }
  }
}
