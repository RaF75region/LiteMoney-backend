using System.Security.Claims;
using LiteMoney.Infrastructure.Data;
using LiteMoney.Application.Interfaces;
using LiteMoney.Application.Services;
using LiteMoney.Infrastructure.Repositories;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.GroupMaps;
using LiteMoney.Infrastructure.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<LiteMoneyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAccountSharingService, AccountSharingService>();
builder.Services.AddSignalR();
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    // .AddClaimsPrincipalFactory<ApplicationUser>()
    .AddEntityFrameworkStores<LiteMoneyDbContext>();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>();
builder.Services.AddAuthorization();
builder.Services.AddEndpointGroups(typeof(AccountGroupMap).Assembly);
builder.Services.AddAuthentication()
    .AddBearerToken();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("CanDelete", policy => policy.RequireClaim("permission", "can:delete"));
});

builder.Services.AddCors(options =>
   {
       options.AddPolicy("AllowFrontend", policy =>
           policy.WithOrigins("*")
                 .AllowAnyHeader()
                 .AllowAnyMethod());
   });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapIdentityApi<ApplicationUser>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpointGroups();
app.MapHub<SharingHub>("/hubs/sharing");
app.UseCors("AllowFrontend");
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}



