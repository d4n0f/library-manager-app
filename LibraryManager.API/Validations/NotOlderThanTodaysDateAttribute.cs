using System.ComponentModel.DataAnnotations;

namespace LibraryManager.API.Validations
{
    public class NotOlderThanTodaysDateAttribute : ValidationAttribute
    {
        public NotOlderThanTodaysDateAttribute()
        {
            ErrorMessage = "A dátum nem lehet korábbi, mint a mai nap.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is DateOnly date)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);

                return date >= today;
            }

            return false;
        }
    }
}
