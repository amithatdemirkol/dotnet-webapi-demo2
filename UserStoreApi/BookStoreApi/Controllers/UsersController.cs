using UserstoreApi.Models;
using UserstoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserstoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _UsersService;

    public UsersController(UsersService UsersService) =>
        _UsersService = UsersService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _UsersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var book = await _UsersService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newBook)
    {
        await _UsersService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedBook)
    {
        var book = await _UsersService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _UsersService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _UsersService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _UsersService.RemoveAsync(id);

        return NoContent();
    }
}