namespace InventoryTracker.Data;

public static class DatabaseSettings
{
    public static string GetConnectionString()
    {
        var folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "InventoryTrackerV2");

        Directory.CreateDirectory(folder);

        var databasePath = Path.Combine(
            folder,
            "InventoryTracker.db");

        return $"Data Source={databasePath}";
    }
}