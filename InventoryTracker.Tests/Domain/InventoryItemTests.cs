using InventoryTracker.Domain;

namespace InventoryTracker.Tests.Domain;

public class InventoryItemTests
{
    [Fact]
    public void NewInventoryItem_DefaultsToActive()
    {
        // Arrange
        var item = new InventoryItem();

        // Act
        var result = item.IsActive;

        // Assert
        Assert.True(result);
    }
}