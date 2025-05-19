using LibraryManager.API.Interfaces;
using LibraryManager.Shared.Models;

namespace LibraryManager.API.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = [];
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBook(int inventoryNumber)
        {
            return _books.Find(x => x.InventoryNumber == inventoryNumber);
        }

        public void RemoveBook(int inventoryNumber)
        {
            _books.RemoveAll(x => x.InventoryNumber == inventoryNumber);
        }

        public void UpdateBook(Book book)
        {
            var oldBook = GetBook(book.InventoryNumber);

            oldBook.Title = book.Title;
            oldBook.Author = book.Author;
            oldBook.Publisher = book.Publisher;
            oldBook.PublicationYear = book.PublicationYear;
        }
    }
}
