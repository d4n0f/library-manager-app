using LibraryManager.API;
using LibraryManager.API.Services;
using LibraryManager.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using LibraryManager.API.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryDataContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
        options.UseLazyLoadingProxies();
    });

builder.Services.AddDbContext<AuthDbContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
        options.UseLazyLoadingProxies();
    });

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Library Auth",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Kérjük adja meg a tokent.",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IReaderService, ReaderService>();
builder.Services.AddSingleton<IRentalService, RentalService>();

builder.Services.AddCors();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseHttpsRedirection();

using(var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@admin.com";
    string password = "Admin123!";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();

        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");

    }
}

app.Run();
