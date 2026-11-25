using InventoryTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Data.Seeders;

public static class DbSeeder
{
    public static async Task SeedAsync(
        InventoryTrackerDbContext context)
    {
        // Seed categories
        var electronics = await GetOrCreateCategoryAsync(
            context, "Electronics");

        var officeSupplies = await GetOrCreateCategoryAsync(
            context, "Office Supplies");

        var tools = await GetOrCreateCategoryAsync(
            context, "Tools");

        // Seed inventory items
        await AddInventoryItemAsync(context, new InventoryItem
        {
            Name = "Wireless Mouse",
            Description = "Wireless optical mouse",
            Quantity = 25,
            ReorderLevel = 5,
            IsActive = true,
            CategoryId = electronics.CategoryId
        });

        await AddInventoryItemAsync(context, new InventoryItem
        {
            Name = "Keyboard",
            Description = "USB keyboard",
            Quantity = 15,
            ReorderLevel = 5,
            IsActive = true,
            CategoryId = electronics.CategoryId
        });

        await AddInventoryItemAsync(context, new InventoryItem
        {
            Name = "Printer Paper",
            Description = "500-sheet ream of paper",
            Quantity = 50,
            ReorderLevel = 10,
            IsActive = true,
            CategoryId = officeSupplies.CategoryId
        });

        await AddInventoryItemAsync(context, new InventoryItem
        {
            Name = "Ballpoint Pens",
            Description = "Black ink pens",
            Quantity = 100,
            ReorderLevel = 20,
            IsActive = true,
            CategoryId = officeSupplies.CategoryId
        });

        await AddInventoryItemAsync(context, new InventoryItem
        {
            Name = "Hammer",
            Description = "16-ounce claw hammer",
            Quantity = 12,
            ReorderLevel = 3,
            IsActive = true,
            CategoryId = tools.CategoryId
        });

        await AddInventoryItemAsync(context, new InventoryItem
        {
            Name = "Screwdriver Set",
            Description = "Multi-piece screwdriver set",
            Quantity = 8,
            ReorderLevel = 10,
            IsActive = true,
            CategoryId = tools.CategoryId
        });

        await context.SaveChangesAsync();
    }

    private static async Task<Category> GetOrCreateCategoryAsync(
        InventoryTrackerDbContext context,
        string name)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(c => c.Name == name);

        if (category is not null)
        {
            return category;
        }

        category = new Category
        {
            Name = name
        };

        context.Categories.Add(category);

        await context.SaveChangesAsync();

        return category;
    }

    private static async Task AddInventoryItemAsync(
        InventoryTrackerDbContext context,
        InventoryItem item)
    {
        var exists = await context.InventoryItems.AnyAsync(i =>
            i.Name == item.Name &&
            i.CategoryId == item.CategoryId);

        if (!exists)
        {
            context.InventoryItems.Add(item);
        }
    }
}