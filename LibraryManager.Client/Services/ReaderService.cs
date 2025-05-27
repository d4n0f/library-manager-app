using LibraryManager.Client.Interfaces;
using LibraryManager.Shared.Models;
using System.Net.Http;
using System;
using System.Net.Http.Json;
using LibraryManager.Shared.DTOs;

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
            return await _httpClient.GetFromJsonAsync<List<Reader>>("readers");
        }

        public async Task<Reader> GetReaderAsync(int readerNumber)
        {
            return await _httpClient.GetFromJsonAsync<Reader>($"readers/{readerNumber}");
        }

        public async Task AddReaderAsync(Reader reader)
        {
            await _httpClient.PostAsJsonAsync("readers", reader);
        }

        public async Task UpdateReaderAsync(int readerNumber, Reader reader)
        {
            await _httpClient.PutAsJsonAsync($"readers/{readerNumber}", reader);
        }

        public async Task RemoveReaderAsync(int readerNumber)
        {
            var dto = new RemoveReaderDTO { ReaderNumber = readerNumber };
            await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"readers", UriKind.Relative),
                Content = JsonContent.Create(dto)
            });
        }
    }
}
