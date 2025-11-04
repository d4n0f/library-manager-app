using LibraryManager.Models.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Shared.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalID { get; set; }

        [Required(ErrorMessage = "A mező nem lehet üres!")]
        public int ReaderNumber { get; set; }

        [ForeignKey(nameof(ReaderNumber))]
        public virtual Reader? Reader { get; set; } = default!;

        [Required(ErrorMessage = "A mező nem lehet üres!")]
        public int InventoryNumber { get; set; }

        [ForeignKey(nameof(InventoryNumber))]
        public virtual Book? Book { get; set; } = default!;

        [Required(ErrorMessage = "A mező nem lehet üres!")]
        [NotOlderThanTodaysDate]
        public DateOnly RentalDate { get; set; }

        [Required(ErrorMessage = "A mező nem lehet üres!")]
        [DueDateIsLaterThanRentalDate]
        public DateOnly DueDate { get; set; }
    }
}