using InventoryTracker.Domain;
using System.Security.Cryptography.X509Certificates;

List<Category> categories = new()
{
    new Category
    {
        CategoryId = 1,
        Name = "Hardware"
    },
    new Category
    {
        CategoryId = 2,
        Name = "Networking"
    },
    new Category
    {
        CategoryId = 3,
        Name = "Storage"
    },
    new Category
    {
        CategoryId = 4,
        Name = "Accessories"
    }
};

List<InventoryItem> items = new()
{
    new InventoryItem
    {
        InventoryItemId = 1,
        Name = "24-inch Monitor",
        Description = "Standard office monitor",
        Quantity = 4,
        ReorderLevel = 5,
        IsActive = true,
        CategoryId = 1
    },
    new InventoryItem
    {
        InventoryItemId = 2,
        Name = "Laptop Dock",
        Description = "USB-C docking station",
        Quantity = 7,
        ReorderLevel = 4,
        IsActive = true,
        CategoryId = 1
    },
    new InventoryItem
    {
        InventoryItemId = 3,
        Name = "Webcam",
        Description = "1080p USB webcam",
        Quantity = 2,
        ReorderLevel = 3,
        IsActive = true,
        CategoryId = 1
    },
    new InventoryItem
    {
        InventoryItemId = 4,
        Name = "Desk Phone",
        Description = "VoIP desk phone",
        Quantity = 0,
        ReorderLevel = 2,
        IsActive = false,
        CategoryId = 1
    },
    new InventoryItem
    {
        InventoryItemId = 5,
        Name = "Wireless Access Point",
        Description = "Business wireless AP",
        Quantity = 3,
        ReorderLevel = 3,
        IsActive = true,
        CategoryId = 2
    },
    new InventoryItem
    {
        InventoryItemId = 6,
        Name = "Network Switch",
        Description = "24-port managed switch",
        Quantity = 8,
        ReorderLevel = 2,
        IsActive = true,
        CategoryId = 2
    },
    new InventoryItem
    {
        InventoryItemId = 7,
        Name = "Ethernet Adapter",
        Description = "USB-to-Ethernet adapter",
        Quantity = 1,
        ReorderLevel = 4,
        IsActive = true,
        CategoryId = 2
    },
    new InventoryItem
    {
        InventoryItemId = 8,
        Name = "Patch Cable",
        Description = "6-foot Cat6 cable",
        Quantity = 25,
        ReorderLevel = 10,
        IsActive = true,
        CategoryId = 2
    },
    new InventoryItem
    {
        InventoryItemId = 9,
        Name = "External SSD",
        Description = "1TB portable SSD",
        Quantity = 2,
        ReorderLevel = 3,
        IsActive = true,
        CategoryId = 3
    },
    new InventoryItem
    {
        InventoryItemId = 10,
        Name = "USB Flash Drive",
        Description = "64GB flash drive",
        Quantity = 18,
        ReorderLevel = 8,
        IsActive = true,
        CategoryId = 3
    },
    new InventoryItem
    {
        InventoryItemId = 11,
        Name = "External Hard Drive",
        Description = "4TB external HDD",
        Quantity = 5,
        ReorderLevel = 3,
        IsActive = true,
        CategoryId = 3
    },
    new InventoryItem
    {
        InventoryItemId = 12,
        Name = "SD Card",
        Description = "128GB SD card",
        Quantity = 0,
        ReorderLevel = 5,
        IsActive = true,
        CategoryId = 3
    },
    new InventoryItem
    {
        InventoryItemId = 13,
        Name = "USB-C Cable",
        Description = "6-foot USB-C cable",
        Quantity = 20,
        ReorderLevel = 10,
        IsActive = true,
        CategoryId = 4
    },
    new InventoryItem
    {
        InventoryItemId = 14,
        Name = "Keyboard",
        Description = "Wired USB keyboard",
        Quantity = 6,
        ReorderLevel = 5,
        IsActive = true,
        CategoryId = 4
    },
    new InventoryItem
    {
        InventoryItemId = 15,
        Name = "Mouse",
        Description = "Wireless mouse",
        Quantity = 3,
        ReorderLevel = 6,
        IsActive = true,
        CategoryId = 4
    },
    new InventoryItem
    {
        InventoryItemId = 16,
        Name = "Headset",
        Description = "USB office headset",
        Quantity = 4,
        ReorderLevel = 4,
        IsActive = false,
        CategoryId = 4
    },
};

foreach (InventoryItem item in items)
{
    Category? category = categories
        .FirstOrDefault(category => category.CategoryId == item.CategoryId);

    if (category != null)
    {
        item.Category = category;
        category.InventoryItems.Add(item);
    }
}

foreach (InventoryItem item in items)
{
    if (item.Quantity <= item.ReorderLevel)
    {
        Console.WriteLine($"{item.Name} | {item.Category.Name} | Qty: {item.Quantity} | LOW STOCK");
    }
    else
    {
        Console.WriteLine($"{item.Name} | {item.Category.Name} | Qty: {item.Quantity}");
    }
}

Console.WriteLine();

foreach (Category category in categories)
{
    Console.WriteLine($"{category.Name}: {category.InventoryItems.Count()} items");
}
