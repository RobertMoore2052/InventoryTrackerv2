using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.Api.Dtos
{
    public class InventoryItemDto
    {
        [Key]
        public int InventoryItemId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
    }
}
