namespace InventoryTracker.Domain;

public class InventoryItems
{
    public int InventoryItemId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public bool LowStock { get; set; };
}
