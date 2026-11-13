namespace InventoryTracker.Web.Models;

public class InventoryItemViewModel
{
    public int InventoryItemId { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
}