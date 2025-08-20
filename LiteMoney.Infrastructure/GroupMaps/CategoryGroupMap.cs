using System.Threading;
using LiteMoney.Application.Interfaces;
using LiteMoney.Application.Services;
using LiteMoney.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LiteMoney.Infrastructure.GroupMaps;

public class CategoryGroupMap : IEndpointGroup
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/categories");

        // group.MapGet("/", async (ICategoryService service, CancellationToken ct) =>
        //     Results.Ok(await service.GetAllAsync(ct)));
        //
        // group.MapGet("/{id:int}", async (int id, ICategoryService service, CancellationToken ct) =>
        //     await service.GetByIdAsync(id, ct) is Category category
        //         ? Results.Ok(category)
        //         : Results.NotFound());
        //
        // group.MapPost("/", async (Category category, ICategoryService service, CancellationToken ct) =>
        // {
        //     var created = await service.CreateAsync(category, ct);
        //     return Results.Created($"/categories/{created.Id}", created);
        // });

        group.MapDelete("/{id:int}", async (int id, ICategoryService service, CancellationToken ct) =>
            await service.DeleteAsync(id, ct)
                ? Results.NoContent()
                : Results.NotFound());
    }
}
