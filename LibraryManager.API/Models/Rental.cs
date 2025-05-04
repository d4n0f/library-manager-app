using LibraryManager.API.Validations;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.API.Models
{
    public class Rental
    {
        [Required]
        public int ReaderNumber { get; set; }

        [Required]
        public int InventoryNumber { get; set; }

        [Required]
        //[NotOlderThanTodaysDate]
        public DateOnly RentalDate { get; set; }

        [Required]
        //[DueDateIsLaterThanRentalDate]
        public DateOnly DueDate { get; set; }
    }
}
