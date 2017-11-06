using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
  [Route("")]
  public class HomeController : Controller
  {
    [HttpGet]
    [HttpPost]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("Error")]
    public IActionResult Error()
    {
      return View();
    }
  }
}