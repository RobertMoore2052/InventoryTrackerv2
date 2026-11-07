using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryTracker.Data;

public class InventoryTrackerDbContextFactory
    : IDesignTimeDbContextFactory<InventoryTrackerDbContext>
{
    public InventoryTrackerDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<InventoryTrackerDbContext>()
            .UseSqlite(DatabaseSettings.GetConnectionString())
            .Options;

        return new InventoryTrackerDbContext(options);
    }
}