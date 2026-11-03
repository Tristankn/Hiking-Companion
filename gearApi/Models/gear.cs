namespace gearApi.Models;

public class Gear
{
    public int GearId { get; set; }
    public string? Name { get; set; }
    public int Weight { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

}