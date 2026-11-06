namespace InventoryTracker.Domain;

public class InventoryItem : BaseDomainModel
{
    public int InventoryItemId { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; } = true;
    public int ReorderLevel { get; set; }

    public int CategoryId { get; set; }                     // FK Column
    public Category Category { get; set; } = null!;         // Navigation
}
