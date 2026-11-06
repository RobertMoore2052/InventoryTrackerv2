namespace InventoryTracker.Domain;

public class Category : BaseDomainModel
{
    public int CategoryId { get; set; }

    public List<InventoryItem> InventoryItems { get; set; } = new();
}
