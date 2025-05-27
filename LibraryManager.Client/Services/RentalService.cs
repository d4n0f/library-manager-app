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

        public async Task<List<Rental>> GetAllRentedBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Rental>>("rentals");
        }

        public async Task<List<Rental>> GetRentedBooksByPersonAsync(int readerNumber)
        {
            return await _httpClient.GetFromJsonAsync<List<Rental>>($"rentals/{readerNumber}");
        }

        public async Task<Rental> GetRentedBookByPersonAsync(int readerNumber, int inventoryNumber)
        {
            return await _httpClient.GetFromJsonAsync<Rental>($"rentals/{readerNumber}/{inventoryNumber}");
        }

        public async Task RentBookAsync(Rental inventoryNumber)
        {
            await _httpClient.PostAsJsonAsync("rentals", inventoryNumber);
        }

        public async Task ReturnBookAsync(int readerNumber, int inventoryNumber)
        {
            await _httpClient.DeleteAsync($"rentals/{readerNumber}/{inventoryNumber}");
        }

        public async Task UpdateRentedAsync(int rentalId, Rental rental)
        {
            await _httpClient.PutAsJsonAsync($"rentals/{rentalId}", rental);
        }

        public async Task<Rental> GetRentalByIdAsync(int rentalId)
        {
            return await _httpClient.GetFromJsonAsync<Rental>($"rentals/id/{rentalId}");
        }
    }
}
