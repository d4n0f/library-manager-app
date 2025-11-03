using LibraryManager.Shared.DTOs;
using LibraryManager.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookController : ControllerBase
    {
        private readonly LibraryDataContext _booksDataContext;

        public BookController(LibraryDataContext booksDataContext)
        {
            _booksDataContext = booksDataContext;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            _booksDataContext.Books.Add(book);
            await _booksDataContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpDelete("{inventoryNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int inventoryNumber)
        {
            try
            {
                var existingBook = await _booksDataContext.Books.FindAsync(inventoryNumber);

                if (existingBook is null)
                {
                    return NotFound("A könyv nem található.");
                }

                _booksDataContext.Books.Remove(existingBook);
                await _booksDataContext.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                    return BadRequest("A könyv nem törölhető, mert van hozzá kölcsönzés.");

                return BadRequest("Hiba történt a törlés során.");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] RemoveBookDTO dto)
        {
            try 
            {
                var existingBook = await _booksDataContext.Books.FindAsync(dto.InventoryNumber);

                if (existingBook == null)
                {
                    return NotFound();
                }

                _booksDataContext.Books.Remove(existingBook);
                await _booksDataContext.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                    return BadRequest("A könyv nem törölhető, mert van hozzá kölcsönzés.");

                return BadRequest("Hiba történt a törlés során.");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Book>>> GetAll()
        {
            var books = await _booksDataContext.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("{inventoryNumber}")]
        [Authorize]
        public async Task<ActionResult<Book>> Get(int inventoryNumber)
        {
            var book = await _booksDataContext.Books.FindAsync(inventoryNumber);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPut("{inventoryNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int inventoryNumber, [FromBody] Book book)
        {
            if (inventoryNumber != book.InventoryNumber)
            {
                return BadRequest();
            }

            var oldBook = await _booksDataContext.Books.FindAsync(inventoryNumber);

            if (oldBook is null)
            {
                return NotFound();
            }

            oldBook.Title = book.Title;
            oldBook.Author = book.Author;
            oldBook.Publisher = book.Publisher;
            oldBook.PublicationYear = book.PublicationYear;

            _booksDataContext.Books.Update(oldBook);
            await _booksDataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
