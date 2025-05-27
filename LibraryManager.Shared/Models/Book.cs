using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Shared.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryNumber { get; set; }

        [Required(ErrorMessage = "Cím megadása kötelező!")]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Szerző megadása kötelező!")]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Kiadó megadása kötelező!")]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Kiadás évének megadása kötelező!")]
        [Range(1, 2025, ErrorMessage = "A kiadás éve nem lehet negatív, illetve nem lehet jövőbeli!")]
        public int PublicationYear { get; set; }
    }
}
