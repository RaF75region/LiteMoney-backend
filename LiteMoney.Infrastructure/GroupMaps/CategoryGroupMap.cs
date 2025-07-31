using LiteMoney.Application.Interfaces;
using LiteMoney.Domain.Models;

namespace LiteMoney.Infrastructure.GroupMaps;

public class CategoryGroupMap : IEndpointGroup
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/categories");

        group.MapGet("/", async (IRepository<Category> repo, CancellationToken ct) =>
            Results.Ok(await repo.GetAllAsync(ct)));

        group.MapGet("/{id:int}", async (int id, IRepository<Category> repo, CancellationToken ct) =>
            await repo.GetByIdAsync(id, ct) is Category category
                ? Results.Ok(category)
                : Results.NotFound());

        group.MapPost("/", async (Category category, IRepository<Category> repo, CancellationToken ct) =>
        {
            await repo.AddAsync(category, ct);
            await repo.SaveChangesAsync(ct);
            return Results.Created($"/categories/{category.Id}", category);
        });

        group.MapDelete("/{id:int}", async (int id, IRepository<Category> repo, CancellationToken ct) =>
        {
            var category = await repo.GetByIdAsync(id, ct);
            if (category is null) return Results.NotFound();
            repo.Remove(category);
            await repo.SaveChangesAsync(ct);
            return Results.NoContent();
        });
    }
}
