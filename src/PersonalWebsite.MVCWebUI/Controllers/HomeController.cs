using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.MVCWebUI.Controllers;

[Route("")]
public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult AboutMe() => View();

    [HttpGet("blog")]
    public IActionResult Blog() => View();

    [HttpGet("blog/{blogId}")]
    public IActionResult BlogPost(string blogId) => View();
}
