using InventoryTracker.Data;
using InventoryTracker.Domain;
using Microsoft.EntityFrameworkCore;

var databaseFolder = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "InventoryTrackerV2");

Directory.CreateDirectory(databaseFolder);

var databasePath = Path.Combine(
    databaseFolder,
    "InventoryTracker.db");

var options = new DbContextOptionsBuilder<InventoryTrackerDbContext>()
    .UseSqlite(DatabaseSettings.GetConnectionString())
    .Options;

using var context = new InventoryTrackerDbContext(options);