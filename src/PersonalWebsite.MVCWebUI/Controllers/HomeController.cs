using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Data;
using PersonalWebsite.Data.Entities;

namespace PersonalWebsite.MVCWebUI.Controllers;

[Route("")]
public class HomeController(IPostRepository postRepository, IWebHostEnvironment webHostEnvironment) : Controller
{
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    [HttpGet("")]
    public IActionResult AboutMe() => View();

    [HttpGet("blog")]
    public async Task<IActionResult> Blog()
    {
        ViewBag.Posts = (await _postRepository.GetAllAsync()).ToArray();
        return View();
    }

    [HttpGet("blog/{blogId}")]
    public async Task<IActionResult> BlogPost(string blogId)
    {
        Post? post = await _postRepository.GetAsync(blogId);
        if (post == null) return NotFound();
        ViewBag.Post = post;
        string htmlFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "posts", $"{blogId}.html");
        string htmlContent = System.IO.File.ReadAllText(htmlFilePath);
        ViewBag.PostHtmlContent = htmlContent;
        return View();
    }
}
