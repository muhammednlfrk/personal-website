using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Data;

namespace PersonalWebsite.MVCWebUI.Controllers;

[Route("")]
public class HomeController(IPostRepository postRepository) : Controller
{
    private readonly IPostRepository _postRepository = postRepository;

    [HttpGet("")]
    public IActionResult AboutMe() => View();

    [HttpGet("blog")]
    public async Task<IActionResult> Blog()
    {
        ViewBag.Posts = (await _postRepository.GetAllAsync()).ToArray();
        return View();
    }

    [HttpGet("blog/{blogId}")]
    public async Task<IActionResult> Post(string blogId)
    {
        ViewBag.Post = await _postRepository.GetAsync(blogId);
        return View();
    }
}
