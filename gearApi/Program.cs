using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GearDb>(opt => opt.UseInMemoryDatabase("GearList"));
builder.Services.AddOpenApi();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var gear = app.MapGroup("/gearlist");

gear.MapGet("/", GetAllGear);
gear.MapGet("/packed", GetPackedGear);
gear.MapGet("/{id}", GetGear);
gear.MapPost("/", CreateGear);
gear.MapPut("/{id}", UpdateGear);
gear.MapDelete("/{id}", DeleteGear);

app.Run();

static async Task<IResult> GetAllGear(GearDb db)
{
    return TypedResults.Ok(await db.Gear.ToArrayAsync());
}

static async Task<IResult> GetPackedGear(GearDb db)
{
    return TypedResults.Ok(await db.Gear.Where(t => t.Packed).ToListAsync());
}

static async Task<IResult> GetGear(int id, GearDb db)
{
    return await db.Gear.FindAsync(id)
        is Gear gear
            ? TypedResults.Ok(gear)
            : TypedResults.NotFound();
}

static async Task<IResult> CreateGear(Gear gear, GearDb db)
{
    db.Gear.Add(gear);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/gearlist/{gear.Id}", gear);
}

static async Task<IResult> UpdateGear(int id, Gear inputGear, GearDb db)
{
    var gear = await db.Gear.FindAsync(id);

    if (gear is null) return TypedResults.NotFound();

    gear.Name = inputGear.Name;
    gear.Category = inputGear.Category;
    gear.Weight = inputGear.Weight;
    gear.Packed = inputGear.Packed;

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
