using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.Api.Dtos
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";
    }
}
