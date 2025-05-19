using System.ComponentModel.DataAnnotations;
using LibraryManager.Shared.Models;

namespace LibraryManager.Models.Validations
{
    public class DueDateIsLaterThanRentalDateAttribute : ValidationAttribute
    {
        public DueDateIsLaterThanRentalDateAttribute()
        {
            ErrorMessage = "A visszahozási határidő nem lehet korábbi, mint a kikölcsönzés ideje.";
        }

        public override bool IsValid(object value)
        {
            if (value is not Rental rental)
            {
                return true;
            }

            return rental.DueDate > rental.RentalDate;
        }
    }
}
