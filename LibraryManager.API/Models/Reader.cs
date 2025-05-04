using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.API.Models
{
    public class Reader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReaderNumber { get; set; }

        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(?!\s*$).+")]
        public string Address { get; set; }

        [Required]
        [Range(typeof(DateOnly), "01/01/1900", "12/31/9999")]
        public DateOnly BirthDate { get; set; }
    }
}
