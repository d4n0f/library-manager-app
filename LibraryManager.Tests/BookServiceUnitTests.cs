using LibraryManager.Shared.Models;
using LibraryManager.API.Services;

namespace LibraryManager.Tests
{
    public class BookServiceUnitTests
    {
        [Fact]
        public void Book_AddBook_StoresBook()
        {
            var service = new BookService();
            var book = new Book { InventoryNumber = 1, Title = "Mock Book" };

            service.AddBook(book);
            var result = service.GetBook(1);

            Assert.Equal("Mock Book", result.Title);
        }

        [Fact]
        public void Book_GetAllBooks_ReturnsAllBooks()
        {
            var service = new BookService();
            service.AddBook(new Book { InventoryNumber = 2 });
            service.AddBook(new Book { InventoryNumber = 3 });

            var result = service.GetAllBooks();

            Assert.True(result.Count == 2);
        }

        [Fact]
        public void Book_GetBook_ReturnsCorrectBook()
        {
            var service = new BookService();
            var book = new Book { InventoryNumber = 4, Title = "Mock Book2" };
            service.AddBook(book);

            var result = service.GetBook(4);

            Assert.Equal(book, result);
        }

        [Fact]
        public void Book_RemoveBook_RemoveFromStored()
        {
            var service = new BookService();
            var book = new Book { InventoryNumber = 5, Title = "Mock Book3" };
            service.AddBook(book);

            service.RemoveBook(5);
            var result = service.GetBook(5);

            Assert.Null(result);
        }

        [Fact]
        public void Book_UpdateBook_BookIsModified()
        {
            var service = new BookService(); 
            var book = new Book { InventoryNumber = 6, Title = "Mock Book4" };
            service.AddBook(book);

            var updatedBook = new Book
            {
                InventoryNumber = 6,
                Title = "New Mock Book",
                Author = "New Mock Author",
                Publisher = "New Mock Author",
                PublicationYear = 2015
            };

            service.UpdateBook(updatedBook);
            var result = service.GetBook(6);

            Assert.Equal("New Mock Book", result.Title);
            Assert.Equal("New Mock Author", result.Author);
            Assert.Equal("New Mock Author", result.Publisher);
            Assert.Equal(2015, result.PublicationYear);
        }
    }
}
