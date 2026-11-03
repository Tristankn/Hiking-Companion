namespace gearApi.Models;

public class PackingList
{
    public int PackingListId { get; set; }
    public string? Name { get; set; }

    public List<PackingListItem> Items { get; set;} = new();

    public int TotalWeight => Items.Sum(item => item.TotalWeight);

}