using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.Web.Models;

public class EditCategoryViewModel
{
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";
}
