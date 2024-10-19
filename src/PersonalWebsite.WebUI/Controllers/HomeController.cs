using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.WebUI.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
