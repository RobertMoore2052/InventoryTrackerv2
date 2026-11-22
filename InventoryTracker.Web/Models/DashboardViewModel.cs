namespace InventoryTracker.Web.Models;

public class DashboardViewModel
{
    public int TotalInventoryItems { get; set; }
    public int ActiveInventoryItems { get; set; }
    public int LowStockItems { get; set; }
    public int TotalCategories { get; set; }
}
