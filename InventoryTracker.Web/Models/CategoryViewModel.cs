namespace InventoryTracker.Web.Models;

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = "";
    public int InventoryItemCount { get; set; }
}
