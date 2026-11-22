using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.Api.Dtos;

public class UpdateInventoryItemDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue)]
    public int ReorderLevel { get; set; }

    public bool IsActive { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}
