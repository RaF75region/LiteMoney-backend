using LiteMoney.Infrastructure.Data;
using LiteMoney.Application.Interfaces;
using LiteMoney.Application.Services;
using LiteMoney.Infrastructure.Repositories;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.GroupMaps;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<LiteMoneyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
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
    app.MapScalarApiReference();
}

app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();
app.MapEndpointGroups();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
