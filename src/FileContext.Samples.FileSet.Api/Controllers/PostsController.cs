using FileContext.Samples.FileSet.Api.DataAccess;
using FileContext.Samples.FileSet.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileContext.Samples.FileSet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly AppDataContext _appDataContext;

    public PostsController(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var result = await _appDataContext.Posts.ToListAsync();
        return result?.Any() ?? false ? Ok(result) : NotFound();
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> Get(Guid id)
    {
        var result = await _appDataContext.Posts.FindAsync(id);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> BlogPost(BlogPost post)
    {
        var createdUser = await _appDataContext.Posts.AddAsync(post);
        await _appDataContext.SaveChangesAsync();
        return Ok(createdUser);
    }

    [HttpPut]
    public async ValueTask<IActionResult> Put(BlogPost post)
    {
        var foundPost = await _appDataContext.Posts.FindAsync(post.Id);
        if (foundPost == null)
            return NotFound();

        await _appDataContext.Posts.UpdateAsync(post);
        await _appDataContext.SaveChangesAsync();

        return Ok(foundPost);
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> Delete(Guid id)
    {
        var foundPost = await _appDataContext.Posts.FindAsync(id);
        if (foundPost == null)
            return NotFound();

        await _appDataContext.Posts.RemoveAsync(foundPost);
        await _appDataContext.SaveChangesAsync();

        return Ok(foundPost);
    }
}