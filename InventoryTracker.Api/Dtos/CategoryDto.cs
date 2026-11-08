namespace InventoryTracker.Api.Dtos;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = "";
    public int InventoryItemCount { get; set; }
}