using LibraryManager.Shared.DTOs;
using LibraryManager.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.API.Controllers
{
    [ApiController]
    [Route("readers")]
    public class ReaderController : ControllerBase
    {
        private readonly LibraryDataContext _readersDataContext;

        public ReaderController(LibraryDataContext readersDataContext)
        {
            _readersDataContext = readersDataContext;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Reader reader)
        {
            var existingReader = await _readersDataContext.Readers.FindAsync(reader.ReaderNumber);

            if (existingReader is not null)
            {
                return Conflict();
            }

            _readersDataContext.Readers.Add(reader);
            await _readersDataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] RemoveReaderDTO dto)
        {

            try
            {
                var existingReader = await _readersDataContext.Readers.FindAsync(dto.ReaderNumber);

                if (existingReader == null)
                {
                    return NotFound();
                }

                _readersDataContext.Readers.Remove(existingReader);
                await _readersDataContext.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                    return BadRequest("Az olvasó nem törölhető, mert van aktív kölcsönzése.");

                return BadRequest("Hiba történt a törlés során.");
            }

        }

        [HttpDelete("{readerNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int readerNumber)
        {
            try
            {
                var existingReader = await _readersDataContext.Readers.FindAsync(readerNumber);

                if (existingReader == null)
                    return NotFound("Az olvasó nem található.");

                _readersDataContext.Readers.Remove(existingReader);
                await _readersDataContext.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                    return BadRequest("Az olvasó nem törölhető, mert van aktív kölcsönzése.");

                return BadRequest("Hiba történt a törlés során.");
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Reader>>> GetAll()
        {
            var readers = await _readersDataContext.Readers.ToListAsync();
            return Ok(readers);
        }

        [HttpGet("{readerNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Reader>> Get(int readerNumber)
        {
            var reader = await _readersDataContext.Readers.FindAsync(readerNumber);

            if (reader is null)
            {
                return NotFound();
            }

            return Ok(reader);
        }

        [HttpPut("{readerNumber}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int readerNumber, [FromBody] Reader reader)
        {
            if (readerNumber != reader.ReaderNumber)
            {
                return BadRequest();
            }

            var oldReader = await _readersDataContext.Readers.FindAsync(readerNumber);

            if (oldReader is null)
            {
                return NotFound();
            }

            oldReader.ReaderNumber = reader.ReaderNumber;
            oldReader.Name = reader.Name;
            oldReader.Address = reader.Address;
            oldReader.BirthDate = reader.BirthDate;

            _readersDataContext.Readers.Update(oldReader);
            await _readersDataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
