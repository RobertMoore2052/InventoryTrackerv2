namespace InventoryTracker.Web.Models;

public class CreateInventoryItemViewModel
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
}
