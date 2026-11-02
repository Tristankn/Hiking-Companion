namespace gearApi.Models;

public class Gear
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Weight { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

}