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

var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("PostgreSQL")
    ?? builder.Configuration.GetConnectionString("SQL"); // Fallback

builder.Services.AddDbContextFactory<DataContext>(x => x.UseNpgsql(connectionString)); // Ändrat från UseSqlServer




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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
