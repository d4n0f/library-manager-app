using LibraryManager.Shared.Models;

namespace LibraryManager.API.Interfaces
{
    public interface IBookService
    {
        Book GetBook(int inventoryNumber);

        List<Book> GetAllBooks();

        void AddBook(Book book);

        void RemoveBook(int inventoryNumber);

        void UpdateBook(Book book);
    }
}
