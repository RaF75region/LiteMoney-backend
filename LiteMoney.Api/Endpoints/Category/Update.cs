using System.Security.Claims;
using FastEndpoints;
using LiteMoney.Api.Mappers;
using LiteMoney.Api.Requests.Category;
using LiteMoney.Api.Responses.Category;
using LiteMoney.Application.Services;

namespace LiteMoney.Api.Endpoints.Category;

public class Update(ICategoryService categoryService): Endpoint<UpdateCategoryRequest, UpdateCategoryResponse, UpdateCategoryMapper>
{
    public override void Configure()
    {
        Put(CategoryRoutes.Base);
    }

    public override async Task HandleAsync(UpdateCategoryRequest req, CancellationToken ct)
    {
        var category = Map.ToEntity(req);
        category.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        await categoryService.UpdateAsync(category, ct);
        var categoryOut = Map.FromEntity(category);
        await Send.OkAsync(categoryOut, ct);
    }
}