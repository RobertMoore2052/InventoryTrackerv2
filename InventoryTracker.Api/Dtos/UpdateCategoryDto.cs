using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.Api.Dtos
{
    public class UpdateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";
    }
}
