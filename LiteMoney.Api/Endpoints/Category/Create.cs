using System.Security.Claims;
using FastEndpoints;
using LiteMoney.Api.Endpoints.Category;
using LiteMoney.Api.Mappers;
using LiteMoney.Api.Requests.Category;
using LiteMoney.Api.Responses.Category;
using LiteMoney.Application.Services;

namespace LiteMoney.Api.Endpoints;

public class Create(ICategoryService categoryService): Endpoint<AddCategoryRequest,  CategoryResponse, CategoryMapper>
{
    public override void Configure()
    {
        Post(CategoryRoutes.Base);
    }

    public override async Task HandleAsync(AddCategoryRequest newCategory, CancellationToken ct)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var category = Map.ToEntity(newCategory);
        category.UserId = userId;
        await categoryService.CreateAsync(category, ct);
        await Send.OkAsync(ct);
    }
}