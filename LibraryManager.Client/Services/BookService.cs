using LibraryManager.Client.Interfaces;
using LibraryManager.Shared.DTOs;
using LibraryManager.Shared.Models;
using System.Net.Http.Json;

namespace LibraryManager.Client.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Book>>("books");
        }

        public async Task<Book> GetBookAsync(int inventoryNumber)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"books/{inventoryNumber}");
        }

        public async Task AddBookAsync(Book book)
        {
            var response = await _httpClient.PostAsJsonAsync("books", book);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Hiba a könyv hozzáadásakor: {response.StatusCode} - {content}");
            }
        }

        public async Task UpdateBookAsync(int inventoryNumber, Book book)
        {
            var response = await _httpClient.PutAsJsonAsync($"books/{inventoryNumber}", book);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Hiba a könyv frissítésekor: {response.StatusCode} - {content}");
            }
        }

        public async Task DeleteBookAsync(int inventoryNumber)
        {
            var dto = new RemoveBookDTO { InventoryNumber = inventoryNumber };
            await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"books", UriKind.Relative),
                Content = JsonContent.Create(dto)
            });
            /*
            var response = await _httpClient.DeleteAsync($"books/{inventoryNumber}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Hiba a könyv törlésekor: {response.StatusCode} - {content}");
            }*/
        }
    }
}
