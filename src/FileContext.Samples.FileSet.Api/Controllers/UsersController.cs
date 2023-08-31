using FileContext.Samples.FileSet.Api.DataAccess;
using FileContext.Samples.FileSet.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileContext.Samples.FileSet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDataContext _appDataContext;

    public UsersController(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get()
    {
        var result = await _appDataContext.Users.ToListAsync();
        return result?.Any() ?? false ? Ok(result) : NotFound();
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> Get(Guid id)
    {
        var result = await _appDataContext.Users.FindAsync(id);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Post(User user)
    {
        var createdUser = await _appDataContext.Users.AddAsync(user);
        await _appDataContext.SaveChangesAsync();
        return Ok(createdUser);
    }

    [HttpPut]
    public async ValueTask<IActionResult> Put(User user)
    {
        var foundUser = await _appDataContext.Users.FindAsync(user.Id);
        if (foundUser == null)
            return NotFound();

        await _appDataContext.Users.UpdateAsync(user);
        await _appDataContext.SaveChangesAsync();

        return Ok(foundUser);
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> Delete(Guid id)
    {
        var foundUser = await _appDataContext.Users.FindAsync(id);
        if (foundUser == null)
            return NotFound();

        await _appDataContext.Users.RemoveAsync(foundUser);
        await _appDataContext.SaveChangesAsync();

        return Ok(foundUser);
    }
}