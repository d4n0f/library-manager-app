using LibraryManager.API.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.API.Validations
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
