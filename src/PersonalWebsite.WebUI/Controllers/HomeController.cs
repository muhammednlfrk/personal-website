using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.WebUI.Controllers;

[Route("")]
public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult AboutMe()
    {
        ViewData["Title"] = "Muhammed Nilifırka";
        return View();
    }

    [HttpGet("blog")]
    public IActionResult Blog()
    {
        ViewData["Title"] = "Yazılarım";
        return View();
    }

    [HttpGet("blog/{id}")]
    public IActionResult Article(string id)
    {
        ViewData["Title"] = id;
        return View("BlogPost");
    }
}
