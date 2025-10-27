using LibraryManager.Client.Interfaces;
using LibraryManager.Shared.DTOs;
using System.Net.Http.Json;

namespace LibraryManager.Client.Services
{
    public class AuthService: IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }
        
        public async Task<bool> Register(RegisterRequest request)
        {
            var response = await _http.PostAsJsonAsync("/register", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<string?> Login(LoginRequest request)
        {
            var response = await _http.PostAsJsonAsync("/login", request);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode}, Content: {content}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return json?.AccessToken;
        }
    }
}
