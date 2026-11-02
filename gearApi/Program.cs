using gearApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using gearApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GearDb>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("GearDb")));

builder.Services.AddOpenApi();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var gear = app.MapGroup("/gear");

gear.MapGet("/", GetAllGear);
gear.MapGet("/{id}", GetGear);
gear.MapPost("/", CreateGear);
gear.MapPut("/{id}", UpdateGear);
gear.MapDelete("/{id}", DeleteGear);

var categories = app.MapGroup("/categories");

categories.MapGet("/", GetAllCategories);
categories.MapGet("/{CategoryId}/gear", GetGearInCategory);
categories.MapPost("/", CreateCategory);
categories.MapDelete("/{id}", DeleteCategory);

var packinglist = app.MapGroup("/packinglist");

app.Run();

static async Task<IResult> GetAllGear(GearDb db)
{
    return TypedResults.Ok(await db.Gear.ToArrayAsync());
}

static async Task<IResult> GetGear(int id, GearDb db)
{
    return await db.Gear.FindAsync(id)
        is Gear gear
            ? TypedResults.Ok(gear)
            : TypedResults.NotFound();
}

static async Task<IResult> CreateGear(GearRequest request, GearDb db)
{
    string? error = ValidateGear(request);
    if(error is not null)
    {
        return TypedResults.BadRequest(error);
    }

    var gear = new Gear
    {
        Name = request.Name,
        CategoryId = request.CategoryId,
        Weight = request.Weight
    };

    db.Gear.Add(gear);

    await db.SaveChangesAsync();

    return TypedResults.Created($"/gear/{gear.Id}", gear);
}

static async Task<IResult> UpdateGear(int id, GearRequest request, GearDb db)
{
    string? error = ValidateGear(request);
    if(error is not null)
    {
        return TypedResults.BadRequest(error);
    }

    var gear = await db.Gear.FindAsync(id);
    if (gear is null) return TypedResults.NotFound();


    gear.Name = request.Name;
    gear.CategoryId = request.CategoryId;
    gear.Weight = request.Weight;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteGear(int id, GearDb db)
{
    if (await db.Gear.FindAsync(id) is Gear gear)
    {
        db.Gear.Remove(gear);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}

static string? ValidateGear(GearRequest request)
{
    if (request.Weight < 0)
    {
        return "Invalid Weight";
    }
    if(string.IsNullOrWhiteSpace(request.Name)){
        return "Name cannot be empty";
    }
    if (request.CategoryId < 0)
    {
        return "The Category Id is not valid";
    }
    
    return null;
}

static async Task<IResult> GetAllCategories(GearDb db)
{
    return TypedResults.Ok(await db.Categories.ToArrayAsync());
}

static async Task<IResult> GetGearInCategory(int CategoryId, GearDb db, bool includeChildren = false)
{
    if (!includeChildren){
        
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

static List<int> GetSubCategories(int CategoryId, List<Category> categories)
{
    List<int> result = new List<int>();
    result.Add(CategoryId);
    var childrenCategories = categories
        .Where(c => c.ParentId == CategoryId);

    foreach (var category in childrenCategories){
        result.AddRange(GetSubCategories(category.CategoryId, categories));
    }

    return result;

}

static async Task<IResult> CreateCategory(CategoryRequest request, GearDb db)
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

static async Task<IResult> DeleteCategory(int id, GearDb db)
{
    if (await db.Categories.FindAsync(id) is Category category)
    {
        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}
