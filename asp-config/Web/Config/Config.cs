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

    [HttpGet("/config")]
    public string Index()
    {
      return _config.Source;
    }
  }
}
