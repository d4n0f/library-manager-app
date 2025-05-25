using LibraryManager.Shared.Models;

namespace LibraryManager.Client.Interfaces
{
    public interface IRentalService
    {
        Task<List<Book>> GetAllBooksAsync();

        Task<List<Book>> GetRentedBooksAsync();

        Task<Book> GetRentedBookAsync(int inventoryNumber);

        Task<Rental> GetRentedBooksByPersonAsync(int readerNumber, int inventoryNumber);

        Task RentBookAsync(Book book);

        Task UpdateDueTimeAsync(int inventoryNumber, Book book);

        Task ReturnBookAsync(int inventoryNumber);
    }
}
