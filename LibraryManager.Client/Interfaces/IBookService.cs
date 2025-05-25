using System;
using LibraryManager.Shared.Models;

namespace LibraryManager.Client.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();

        Task<Book> GetBookAsync(int inventoryNumber);

        Task AddBookAsync(Book book);

        Task UpdateBookAsync(int inventoryNumber, Book book);

        Task DeleteBookAsync(int inventoryNumber);
    }
}
