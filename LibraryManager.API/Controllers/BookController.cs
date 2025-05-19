using LibraryManager.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        private readonly LibraryDataContext _demoDataContext;

        public BookController(LibraryDataContext demoDataContext)
        {
            _demoDataContext = demoDataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            var existingBook = await _demoDataContext.Books.FindAsync(book.InventoryNumber);

            if (existingBook is not null)
            {
                return Conflict();
            }

            _demoDataContext.Books.Add(book);
            await _demoDataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{inventoryNumber}")]
        public async Task<IActionResult> Delete(int inventoryNumber)
        {
            var existingBook = await _demoDataContext.Books.FindAsync(inventoryNumber);

            if (existingBook is null)
            {
                return NotFound();
            }

            _demoDataContext.Books.Remove(existingBook);
            await _demoDataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAll()
        {
            var books = await _demoDataContext.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("{inventoryNumber}")]
        public async Task<ActionResult<Book>> Get(int inventoryNumber)
        {
            var book = await _demoDataContext.Books.FindAsync(inventoryNumber);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPut("{inventoryNumber}")]
        public async Task<IActionResult> Update(int inventoryNumber, [FromBody] Book book)
        {
            if (inventoryNumber != book.InventoryNumber)
            {
                return BadRequest();
            }

            var oldBook = await _demoDataContext.Books.FindAsync(inventoryNumber);

            if (oldBook is null)
            {
                return NotFound();
            }

            oldBook.Title = book.Title;
            oldBook.Author = book.Author;
            oldBook.Publisher = book.Publisher;
            oldBook.PublicationYear = book.PublicationYear;

            _demoDataContext.Books.Update(oldBook);
            await _demoDataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
