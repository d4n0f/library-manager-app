using Blazored.LocalStorage;
using LibraryManager.Client;
using LibraryManager.Client.Interfaces;
using LibraryManager.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<TokenProvider>();

builder.Services.AddScoped(sp =>
{
    var tokenProvider = sp.GetRequiredService<TokenProvider>();
    return new AuthMessageHandler(tokenProvider)
    {
        InnerHandler = new HttpClientHandler() // WASM alatt ez nem igazán kell, de placeholder
    };
});

// HttpClient, scoped, AuthMessageHandler-rel
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthMessageHandler>();
    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:5005")
    };
});



builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();