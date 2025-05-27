using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Shared.Models
{
    public class Reader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReaderNumber { get; set; }

        [Required(ErrorMessage = "A mező nem lehet üres!")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A mező nem tartalmazhat csak whitespace karaktereket!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A mező nem lehet üres!")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A mező nem tartalmazhat csak whitespace karaktereket!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "A mező nem lehet üres!")]
        [Range(typeof(DateOnly), "01/01/1900", "12/31/9999", ErrorMessage = "Nem lehet 1900-nál kisebb évet megadni!")]
        public DateOnly BirthDate { get; set; }
    }
}
