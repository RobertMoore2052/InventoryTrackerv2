namespace InventoryTracker.Api.Dtos;

public class UpdateInventoryItemDto
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsActive { get; set; }
    public int CategoryId { get; set; }
}
