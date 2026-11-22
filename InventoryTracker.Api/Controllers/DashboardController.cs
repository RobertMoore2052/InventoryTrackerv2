

using InventoryTracker.Api.Dtos;
using InventoryTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly InventoryTrackerDbContext _context;

    public DashboardController(InventoryTrackerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var dashboard = new DashboardDto
        {
            TotalInventoryItems =
                await _context.InventoryItems.CountAsync(),

            ActiveInventoryItems =
                await _context.InventoryItems
                    .CountAsync(item =>
                        item.IsActive),

            LowStockItems =
                await _context.InventoryItems
                    .CountAsync(item =>
                        item.IsActive &&
                        item.Quantity <= item.ReorderLevel),

            TotalCategories =
                await _context.Categories.CountAsync()
        };

        return Ok(dashboard);
    }
}
