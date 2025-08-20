using System.Security.Claims;
using FastEndpoints;
using LiteMoney.Api.Mappers;
using LiteMoney.Api.Responses.Category;
using LiteMoney.Application.Services;

namespace LiteMoney.Api.Endpoints.Category;

static class CategoryRoutes
{
    public const string Base = "/category";
}

public class Get(ICategoryService categoryService) : EndpointWithoutRequest<List<CategoryResponse>, CategoryMapper>
{
    public override void Configure()
    {
        Get(CategoryRoutes.Base);
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var categories = await categoryService.GetFromUser(userId, cancellationToken);
        var result = Map.FromEntities(categories);
        await Send.OkAsync(result, cancellationToken);
    }
}