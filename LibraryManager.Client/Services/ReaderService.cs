using LibraryManager.Client.Interfaces;
using LibraryManager.Shared.Models;
using System.Net.Http;
using System;
using System.Net.Http.Json;

namespace LibraryManager.Client.Services
{
    public class ReaderService : IReaderService
    {

        private readonly HttpClient _httpClient;

        public ReaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reader>> GetAllReadersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Reader>>("people");
        }

        public async Task<Reader> GetReaderAsync(int readerNumber)
        {
            return await _httpClient.GetFromJsonAsync<Reader>($"people/{readerNumber}");
        }

        public async Task AddReaderAsync(Reader reader)
        {
            await _httpClient.PostAsJsonAsync("people", reader);
        }

        public async Task UpdateReaderAsync(int readerNumber, Reader reader)
        {
            await _httpClient.PutAsJsonAsync($"people/{readerNumber}", reader);
        }

        public async Task RemoveReaderAsync(int readerNumber)
        {
            await _httpClient.DeleteAsync($"people/{readerNumber}");
        }
    }
}
