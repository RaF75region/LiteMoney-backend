using FastEndpoints;
using FastEndpoints.Swagger;
using LiteMoney.Infrastructure.Data;
using LiteMoney.Application.Interfaces;
using LiteMoney.Application.Services;
using LiteMoney.Infrastructure.Repositories;
using LiteMoney.Domain.Models;
using LiteMoney.Infrastructure.GroupMaps;
using LiteMoney.Infrastructure.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<LiteMoneyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAccountSharingService, AccountSharingService>();

builder.Services.AddSignalR();
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LiteMoneyDbContext>();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>();
builder.Services.AddAuthorization();
builder.Services.AddEndpointGroups(typeof(AccountGroupMap).Assembly);
builder.Services
    .AddFastEndpoints()
    .SwaggerDocument()
    .AddAuthentication()
    .AddBearerToken();
builder.Services.AddEndpointsApiExplorer();
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

app.MapIdentityApi<ApplicationUser>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerGen();
app.UseFastEndpoints();
app.MapHub<SharingHub>("/hubs/sharing");
app.UseCors("AllowFrontend");
app.Run();

