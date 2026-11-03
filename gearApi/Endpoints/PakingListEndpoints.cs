namespace gearApi.Endpoints;

using gearApi.DTOs;
using gearApi.Models;
using Microsoft.EntityFrameworkCore;

public static class PackingListEndpoints
{
    public static void MapPackingListEndpoints(this WebApplication app)
    {
        var packinglist = app.MapGroup("/packinglists");

        packinglist.MapPost("/", CreatePackinglist);
        packinglist.MapGet("/", GetAllPackingLists);
        packinglist.MapGet("/{PackingListId}", GetPackingList);
        packinglist.MapGet("/{PackingListId}/items", GetGearInPackingList);
        packinglist.MapPost("/{PackingListId}/items/{GearId}", AddGearToPackingList);
        packinglist.MapPatch("/{PackingListId}/items/{GearId}", ChangeGearQuantity);
    }

    private static async Task<IResult> CreatePackinglist(PackingListRequest request, GearDb db)
    {
        var packinglist = new PackingList
        {
            Name = request.Name
        };

        db.PackingLists.Add(packinglist);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/packinglists/{packinglist.PackingListId}", packinglist);
    }

    private static async Task<IResult> GetAllPackingLists(GearDb db)
    {
        var packinglist = await db.PackingLists
        .Include(p => p.Items)
        .ThenInclude(i => i.Gear)
        .ToListAsync();

        return TypedResults.Ok(packinglist);
    }

    static async Task<IResult> GetPackingList(int PackingListId, GearDb db)
    {
        return await db.PackingLists.FindAsync(PackingListId)
            is PackingList packingList
                ? TypedResults.Ok(packingList)
                : TypedResults.NotFound();
    }

    private static async Task<IResult> GetGearInPackingList(int PackingListId, GearDb db)
    {
        var packinglist = await db.PackingLists
            .Where(p => p.PackingListId == PackingListId)
            .SelectMany(p => p.Items)
            .Include(i => i.Gear)
            .ToListAsync();

        return TypedResults.Ok(packinglist);
    }

    private static async Task<IResult> AddGearToPackingList(int PackingListId, int GearId, GearDb db)
    {
        var packinglist = await db.PackingLists.FindAsync(PackingListId);
        if (packinglist is null) return TypedResults.NotFound();

        var item = await db.PackingListItems
            .FirstOrDefaultAsync(i =>
            i.PackingListId == PackingListId &&
            i.GearId == GearId);

        if (item != null)
        {
            item.Quantity += 1;
        }
        else
        {
            var gear = await db.Gear.FindAsync(GearId);
            if (gear is null) return TypedResults.NotFound();

            var packeditem = new PackingListItem();
            packeditem.Gear = gear;

            packinglist.Items.Add(packeditem);

        }

        await db.SaveChangesAsync();
        return TypedResults.NoContent();

    }

    private static async Task<IResult> ChangeGearQuantity(int PackingListId, int GearId, int op, GearDb db)
    {
        var item = await db.PackingListItems
            .FirstOrDefaultAsync(i =>
            i.PackingListId == PackingListId &&
            i.GearId == GearId);

        if (item != null)
        {
            if (op == 1)
            {
                item.Quantity += 1;
            }
            else if (op == -1)
            {
                item.Quantity -= 1;
                if (item.Quantity <= 0)
                {
                    db.PackingListItems.Remove(item);
                }
            }

        }
        else
        {
            return TypedResults.NotFound();
        }

        await db.SaveChangesAsync();
        return TypedResults.NoContent();

    }

}