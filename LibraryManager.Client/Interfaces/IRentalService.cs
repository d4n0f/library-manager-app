using LibraryManager.Shared.Models;

namespace LibraryManager.Client.Interfaces
{
    public interface IRentalService
    {
        Task<List<Rental>> GetAllRentedBooksAsync();

        Task<List<Rental>> GetRentedBooksByPersonAsync(int readerNumber);

        Task<Rental> GetRentedBookByPersonAsync(int readerNumber, int inventoryNumber);

        Task RentBookAsync(Rental inventoryNumber);

        Task ReturnBookAsync(int readerNumber, int inventoryNumber);

        Task UpdateRentedAsync(int rentalId, Rental rental);
    }
}
