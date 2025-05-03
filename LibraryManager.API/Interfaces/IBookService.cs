using LibraryManager.API.Models;
using System;

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
