namespace gearApi.Endpoints;

using gearApi.DTOs;
using gearApi.Models;
using Microsoft.EntityFrameworkCore;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var categories = app.MapGroup("/categories");

        categories.MapGet("/", GetAllCategories);
        categories.MapGet("/{CategoryId}/gear", GetGearInCategory);
        categories.MapPost("/", CreateCategory);
        categories.MapDelete("/{id}", DeleteCategory);
    }

    private static async Task<IResult> GetAllCategories(GearDb db)
    {
        return TypedResults.Ok(await db.Categories.ToArrayAsync());
    }

    private static async Task<IResult> GetGearInCategory(int CategoryId, GearDb db, bool includeChildren = false)
    {
        if (!includeChildren)
        {

            var gear = await db.Gear
                .Where(gear => gear.CategoryId == CategoryId)
                .ToArrayAsync();

            return TypedResults.Ok(gear);
        }
        else
        {
            var categories = await db.Categories.ToListAsync();
            var result = GetSubCategories(CategoryId, categories);
            var gear = await db.Gear
            .Where(g => result.Contains(g.CategoryId))
            .ToListAsync();

            return TypedResults.Ok(gear);
        }
    }

    private static List<int> GetSubCategories(int CategoryId, List<Category> categories)
    {
        List<int> result = new List<int>();
        result.Add(CategoryId);
        var childrenCategories = categories
            .Where(c => c.ParentId == CategoryId);

        foreach (var category in childrenCategories)
        {
            result.AddRange(GetSubCategories(category.CategoryId, categories));
        }

        return result;

    }

    private static async Task<IResult> CreateCategory(CategoryRequest request, GearDb db)
    {

        var category = new Category
        {
            Name = request.Name,
            ParentId = request.ParentId
        };

        db.Categories.Add(category);

        await db.SaveChangesAsync();

        return TypedResults.Created($"/categories/{category.CategoryId}", category);
    }

    private static async Task<IResult> DeleteCategory(int id, GearDb db)
    {
        if (await db.Categories.FindAsync(id) is Category category)
        {
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }

}