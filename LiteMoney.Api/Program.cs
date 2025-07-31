using LiteMoney.Infrastructure.Data;
using LiteMoney.Application.Interfaces;
using LiteMoney.Infrastructure.Repositories;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.GroupMaps;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<LiteMoneyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(LiteMoney.Application.Interfaces.IRepository<>), typeof(LiteMoney.Infrastructure.Repositories.Repository<>));
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<LiteMoneyDbContext>();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>();
builder.Services.AddAuthorization();
builder.Services.AddEndpointGroups(typeof(AccountGroupMap).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();
app.MapEndpointGroups();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
