using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcClient.Controllers
{
  public class Home : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    [Authorize]
    public IActionResult Secure()
    {
      ViewData["Message"] = "Secure page.";
      return View();
    }
  }
}