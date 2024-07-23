using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryTask;
using LibraryTask.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly MongoDBContext _context;

    public AuthorController(MongoDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Author>> Get()
    {
        return await _context.Authors.Find(_ => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> Get(string id)
    {
        var author = await _context.Authors.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (author == null)
        {
            return NotFound();
        }

        return author;
    }

    [HttpPost]
    public async Task<ActionResult<Author>> Create(Author author)
    {
        await _context.Authors.InsertOneAsync(author);
        return CreatedAtRoute(new { id = author.Id }, author);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Author authorIn)
    {
        var author = await _context.Authors.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (author == null)
        {
            return NotFound();
        }

        await _context.Authors.ReplaceOneAsync(p => p.Id == id, authorIn);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var product = await _context.Authors.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        await _context.Authors.DeleteOneAsync(p => p.Id == id);

        return NoContent();
    }
}