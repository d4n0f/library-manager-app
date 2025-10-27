using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LibraryManager.Client.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TokenProvider _tokenProvider;

        public ApiAuthenticationStateProvider(TokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenProvider.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            // Ha van token, az alapján hitelesített user-t adunk vissza
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "AuthenticatedUser")
            }, "Bearer");

            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "AuthenticatedUser")
            }, "Bearer");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
