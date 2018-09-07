using Microsoft.AspNetCore.Mvc;

namespace Web.Config
{
  public class Config : Controller
  {
    [HttpGet("/config")]
    public string Index()
    {
      return "Web.Test";
    }
  }
}
