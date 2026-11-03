namespace gearApi.Models;

public class PackingListItem
{
    public int PackingListItemId { get; set; }
    public int GearId { get; set; }
    public int GearName { get; set; }
    public int Quantity { get; set; }

    public Gear Gear { get; set; } = null!;

    public int PackingListId { get; set; }

    public bool IsPacked { get; set; } = false;
    public bool IsWorn { get; set; } = false;

    public int TotalWeight => Gear.Weight * Quantity;

}