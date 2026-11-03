namespace gearApi.Endpoints;

using gearApi.DTOs;
using gearApi.Models;
using Microsoft.EntityFrameworkCore;

public static class GearEndpoints
{
    public static void MapGearEndpoints(this WebApplication app)
    {
        var gear = app.MapGroup("/gear");

        gear.MapGet("/", GetAllGear);
        gear.MapGet("/{id}", GetGear);
        gear.MapPost("/", CreateGear);
        gear.MapPut("/{id}", UpdateGear);
        gear.MapDelete("/{id}", DeleteGear);
    }

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

    private static async Task<IResult> CreateGear(GearRequest request, GearDb db)
    {
        string? error = ValidateGear(request);
        if (error is not null)
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

        return TypedResults.Created($"/gear/{gear.GearId}", gear);
    }

    private static async Task<IResult> UpdateGear(int GearId, GearRequest request, GearDb db)
    {
        string? error = ValidateGear(request);
        if (error is not null)
        {
            return TypedResults.BadRequest(error);
        }

        var gear = await db.Gear.FindAsync(GearId);
        if (gear is null) return TypedResults.NotFound();


        gear.Name = request.Name;
        gear.CategoryId = request.CategoryId;
        gear.Weight = request.Weight;

        await db.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteGear(int GearId, GearDb db)
    {
        if (await db.Gear.FindAsync(GearId) is Gear gear)
        {
            db.Gear.Remove(gear);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }

    private static string? ValidateGear(GearRequest request)
    {
        if (request.Weight < 0)
        {
            return "Invalid Weight";
        }
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return "Name cannot be empty";
        }
        if (request.CategoryId < 0)
        {
            return "The Category Id is not valid";
        }

        return null;
    }
}