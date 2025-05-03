using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.API.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryNumber { get; set; }

        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Author { get; set; }

        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Publisher { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PublicationYear { get; set; }
    }
}
