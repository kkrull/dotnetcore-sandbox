using Microsoft.AspNetCore.Mvc;

namespace Web.Config
{
  public class Config : Controller
  {
    public IActionResult Index()
    {
      //TODO KDK: Configure the route and return a faked value
      //TODO KDK: Then, look up the service needed to access configuration and use that to get settings from appsettings.json
      return View();
    }
  }
}
