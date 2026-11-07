using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryTracker.Data;

public class InventoryTrackerDbContextFactory
    : IDesignTimeDbContextFactory<InventoryTrackerDbContext>
{
    public InventoryTrackerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<InventoryTrackerDbContext>();

        optionsBuilder.UseSqlite(
            "Data Source=InventoryTracker.db");

        return new InventoryTrackerDbContext(
            optionsBuilder.Options);
    }
}