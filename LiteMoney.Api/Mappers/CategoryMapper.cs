using FastEndpoints;
using LiteMoney.Api.Requests.Category;
using LiteMoney.Api.Responses.Category;
using LiteMoney.Domain.Models;

namespace LiteMoney.Api.Mappers;

public class CategoryMapper: Mapper<AddCategoryRequest, CategoryResponse, Category>
{
    public override Category ToEntity(AddCategoryRequest newCategory) =>
        new()
        {
            Name = newCategory.Name,
            Type = newCategory.Type,
            Icon = newCategory.Icon,
            IconColor = newCategory.IconColor,
        };

    public override CategoryResponse FromEntity(Category entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Type = entity.Type,
        Icon = entity.Icon,
        IconColor = entity.IconColor,
    };

    public List<CategoryResponse> FromEntities(IEnumerable<Category> entities) =>
        entities.Select(c => new CategoryResponse()
        {
            Id = c.Id,
            Name = c.Name,
            Type = c.Type,
            Icon = c.Icon,
            IconColor = c.IconColor
        }).ToList();
}

public class UpdateCategoryMapper : Mapper<UpdateCategoryRequest, UpdateCategoryResponse, Category>
{
    public override Category ToEntity(UpdateCategoryRequest r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Type = r.Type,
        Icon = r.Icon,
        IconColor = r.IconColor
    };

    public override UpdateCategoryResponse FromEntity(Category r) => new(r.Id,
        r.Name, r.Type, r.Icon, r.IconColor);
}
