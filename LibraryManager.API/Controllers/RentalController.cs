using LibraryManager.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers
{
    [ApiController]
    [Route("rentals")]
    public class RentalController : ControllerBase
    {
        private readonly LibraryDataContext _rentalsDataContext;

        public RentalController(LibraryDataContext rentalsDataContext)
        {
            _rentalsDataContext = rentalsDataContext;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Rental rental)
        {
            var existingReader = await _rentalsDataContext.Readers.FindAsync(rental.ReaderNumber);
            if (existingReader is null)
            {
                return NotFound("Az olvasó nem található.");
            }

            var existingBook = await _rentalsDataContext.Books
                .FirstOrDefaultAsync(b => b.InventoryNumber == rental.InventoryNumber);

            if (existingBook is null)
            {
                return NotFound("A könyv nem található.");
            }

            var alreadyRented = await _rentalsDataContext.Rentals
                .AnyAsync(r => r.InventoryNumber == rental.InventoryNumber);

            if (alreadyRented)
            {
                return Conflict("Ez a könyv már ki van kölcsönözve.");
            }

            _rentalsDataContext.Rentals.Add(rental);
            await _rentalsDataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{readerNumber}/{inventoryNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int readerNumber, int inventoryNumber)
        {
            var existingRental = await _rentalsDataContext.Rentals
                .FirstOrDefaultAsync(r => r.ReaderNumber == readerNumber && r.InventoryNumber == inventoryNumber);

            if (existingRental is null)
            {
                return NotFound("A megadott kölcsönzés nem található.");
            }

            _rentalsDataContext.Rentals.Remove(existingRental);
            await _rentalsDataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Rental>>> GetAll()
        {
            var rentals = await _rentalsDataContext.Rentals.ToListAsync();
            return Ok(rentals);
        }

        [HttpGet("{readerNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Rental>>> Get(int readerNumber)
        {
            var rentals = await _rentalsDataContext.Rentals
                .Where(r => r.ReaderNumber == readerNumber)
                .ToListAsync();

            if (rentals is null)
            {
                return NotFound();
            }

            return Ok(rentals);
        }

        [HttpGet("id/{rentalId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Rental>> GetRentalById(int rentalId)
        {
            var rental = await _rentalsDataContext.Rentals.FindAsync(rentalId);

            if (rental == null)
                return NotFound();

            return Ok(rental);
        }

        [HttpGet("{readerNumber}/{inventoryNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Rental>> GetRental(int readerNumber, int inventoryNumber)
        {
            var rental = await _rentalsDataContext.Rentals
                .FirstOrDefaultAsync(r => r.ReaderNumber == readerNumber && r.InventoryNumber == inventoryNumber);

            if (rental == null)
                return NotFound();

            return Ok(rental);
        }

        [HttpPut("{rentalId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int rentalId, [FromBody] Rental rental)
        {
            if (rentalId != rental.RentalID)
            {
                return BadRequest();
            }

            var oldRental = await _rentalsDataContext.Rentals.FindAsync(rentalId);

            if (oldRental is null)
            {
                return NotFound();
            }

            oldRental.RentalID = rental.RentalID;
            oldRental.InventoryNumber = rental.InventoryNumber;
            oldRental.ReaderNumber = rental.ReaderNumber;
            oldRental.RentalDate = rental.RentalDate;
            oldRental.DueDate = rental.DueDate;

            _rentalsDataContext.Rentals.Update(oldRental);
            await _rentalsDataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
