using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryTask;
using LibraryTask.Models;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly MongoDBContext _context;

    public ProductsController(MongoDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Book>> Get()
    {
        return await _context.Books.Find(_ => true).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await _context.Books.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book product)
    {
        await _context.Books.InsertOneAsync(product);
        return CreatedAtRoute(new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Book productIn)
    {
        var book = await _context.Books.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound();
        }

        await _context.Books.ReplaceOneAsync(p => p.Id == id, productIn);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _context.Books.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound();
        }

        await _context.Books.DeleteOneAsync(p => p.Id == id);

        return NoContent();
    }
}