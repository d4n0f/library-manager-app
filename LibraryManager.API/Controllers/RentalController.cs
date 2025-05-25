using LibraryManager.Shared.Models;
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
        public async Task<IActionResult> Add([FromBody] Rental rental)
        {
            var existingReader = await _rentalsDataContext.Readers.FindAsync(rental.ReaderNumber);

            if (existingReader is not null)
            {
                return Conflict();
            }

            _rentalsDataContext.Rentals.Add(rental);
            await _rentalsDataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{rentalId}")]
        public async Task<IActionResult> Delete(int rentalId)
        {
            var existingRental = await _rentalsDataContext.Rentals.FindAsync(rentalId);

            if (existingRental is null)
            {
                return NotFound();
            }

            _rentalsDataContext.Rentals.Remove(existingRental);
            await _rentalsDataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Rental>>> GetAll()
        {
            var rentals = await _rentalsDataContext.Rentals.ToListAsync();
            return Ok(rentals);
        }
            
        [HttpGet("{rentalId}")]
        public async Task<ActionResult<Rental>> Get(int rentalId)
        {
            var rental = await _rentalsDataContext.Rentals.FindAsync(rentalId);

            if (rental is null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        [HttpPut("{rentalId}")]
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
