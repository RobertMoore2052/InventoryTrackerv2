using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.Web.Models;

public class CreateCategoryViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";
}
