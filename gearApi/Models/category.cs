namespace gearApi.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }

    public int? ParentId { get; set; }
    public string? ParentName { get; set; }
}