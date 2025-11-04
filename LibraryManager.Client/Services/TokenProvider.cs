using Blazored.LocalStorage;

namespace LibraryManager.Client.Services
{
    public class TokenProvider
    {
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";

        public TokenProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SaveTokenAsync(string token)
        => await _localStorage.SetItemAsync(TokenKey, token);

        public async Task<string?> GetTokenAsync()
            => await _localStorage.GetItemAsync<string>(TokenKey);

        public async Task ClearTokenAsync()
            => await _localStorage.RemoveItemAsync(TokenKey);
    }
}
