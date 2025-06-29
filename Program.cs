using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Components;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Infra;
using StarcraftOrganizer.Services;


var builder = WebApplication.CreateBuilder(args);

// för att exponera lokalt
//builder.WebHost.UseUrls("http://192.168.56.68:5000", "https://192.168.56.68:5001");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Configuration.AddUserSecrets<Program>();

// Försök läsa connection string
builder.Configuration.AddEnvironmentVariables();

// Här prioriterar vi: miljövariabel > appsettings
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                    ?? builder.Configuration.GetConnectionString("Default")
                    ?? throw new InvalidOperationException("Ingen giltig databasanslutning kunde hittas");

builder.Services.AddDbContextFactory<DataContext>(options =>
{
    options.UseNpgsql(connectionString);
});



// Auth
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CoreAuthenticationStateProvider>();
builder.Services.AddScoped<AuthService>();


#region ScopedServices
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<MapService>();
builder.Services.AddScoped<ChallengeService>();
builder.Services.AddScoped<ChallengeMapsService>();
#endregion






var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    try
    {
        context.Database.Migrate();
        Console.WriteLine("Database migrations completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration failed: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

var urls = app.Configuration.GetValue<string>("ASPNETCORE_URLS") ?? "";
if (urls.StartsWith("http://") || !app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
