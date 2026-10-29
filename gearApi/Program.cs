using gearApi.DTOs;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


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

var gear = app.MapGroup("/gearlist");

gear.MapGet("/", GetAllGear);
gear.MapGet("/{id}", GetGear);
gear.MapPost("/", CreateGear);
gear.MapPut("/{id}", UpdateGear);
gear.MapDelete("/{id}", DeleteGear);

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
        Category = request.Category,
        Weight = request.Weight
    };

    db.Gear.Add(gear);

    await db.SaveChangesAsync();

    return TypedResults.Created($"/gearlist/{gear.Id}", gear);
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
    gear.Category = request.Category;
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
    if (string.IsNullOrWhiteSpace(request.Category))
    {
        return "Category cannot be empty";
    }
    
    return null;
}
