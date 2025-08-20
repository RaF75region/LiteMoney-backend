using FastEndpoints;
using LiteMoney.Api.Requests.Category;
using LiteMoney.Application.Interfaces;
using LiteMoney.Application.Services;

namespace LiteMoney.Api.Endpoints.Category;

public class DeleteCategory(ICategoryService categoryService): Endpoint<DeleteCategoryRequest>
{
    public override void Configure()
    {
        Delete(CategoryRoutes.Base);
        Summary(s =>
        {
            s.Summary = "Удаление категории по ID";
            s.Description = "Удаляет категорию по маршруту /category/{id}";
            s.Params["id"] = "ID категории для удаления";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        await categoryService.DeleteAsync(req.CategoryId, ct);
        await Send.OkAsync(cancellation: ct);
    }
}