using InventoryTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InventoryTracker.Data
{
    public class InventoryTrackerDbContext : DbContext
    {
        public InventoryTrackerDbContext(
            DbContextOptions<InventoryTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<InventoryItem> InventoryItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());
        }
    }

    /*modern equivalent
     *  public class InventoryTrackerDbContext(
     *      DbContextOptions<InventoryTrackerDbContext> options)
     *      : DbContext(options)
     *  {
     *      public DbSet<Category> Categories { get; set; } = null!;
     *      public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
     *  }
     */
}
