using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Components;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("SQL");
builder.Services.AddDbContextFactory<DataContext>(x => x.UseSqlServer(connectionString));







#region ScopedServices

builder.Services.AddScoped<PlayerService, PlayerService>();
builder.Services.AddScoped<MatchService, MatchService>();
builder.Services.AddScoped<MapService, MapService>();
builder.Services.AddScoped<ChallengeService, ChallengeService>();
builder.Services.AddScoped<ChallengeMapsService, ChallengeMapsService>();


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
