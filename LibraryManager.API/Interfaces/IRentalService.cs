using LibraryManager.API.Models;

namespace LibraryManager.API.Interfaces
{
    public interface IRentalService
    {
        Rental GetRental(int readerNumber, int inventoryNumber);

        List<Rental> GetAllRentals();

        bool RentBook(Rental rental);

        bool ReturnBook(int readerNumber, int inventoryNumber);
    }
}