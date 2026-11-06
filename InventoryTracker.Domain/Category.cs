namespace InventoryTracker.Domain;

public class Category : BaseDomainModel
{
    public int CategoryId { get; set; }
    public InventoryItems InventoryItemId { get; set; } = null!;
}
