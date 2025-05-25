using LibraryManager.Shared.Models;
using System.Net.Http.Json;
using System;
using LibraryManager.Client.Interfaces;

namespace LibraryManager.Client.Services
{
    public class RentalService : IRentalService
    {
        private readonly HttpClient _httpClient;

        public RentalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetRentedBooksAsync()
        {
            try
            {
                var books = await _httpClient.GetFromJsonAsync<List<Book>>("books");
                return books ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching books: {ex.Message}");
                return new List<Book>();
            }
        }

        public async Task<Book> GetRentedBookAsync(int inventoryNumber)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"books/{inventoryNumber}");
        }

        public async Task<Rental> GetRentedBooksByPersonAsync(int readerNumber, int inventoryNumber)
        {
            //throw new NotImplementedException();
            return await _httpClient.GetFromJsonAsync<Rental>($"rentals/{readerNumber}");
        }

        public async Task RentBookAsync(Book book)
        {
            await _httpClient.PostAsJsonAsync("books", book);
        }

        public async Task UpdateDueTimeAsync(int inventoryNumber, Book book)
        {
            await _httpClient.PutAsJsonAsync($"books/{inventoryNumber}", book);
        }

        public async Task ReturnBookAsync(int inventoryNumber)
        {
            await _httpClient.DeleteAsync($"books/{inventoryNumber}");
        }
    }
}
