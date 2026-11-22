using InventoryTracker.Api.Dtos;
using InventoryTracker.Data;
using InventoryTracker.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace InventoryTracker.Tests.Api;

public class InventoryItemsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public InventoryItemsControllerTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();

        _factory.ResetDatabase();
    }

    [Fact]
    public async Task GetInventoryItem_WhenItemDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var id = 9999;

        // Act
        var response = await _client.GetAsync(
            $"/api/InventoryItems/{id}");

        // Assert
        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task GetInventoryItem_WhenItemExists_ReturnsItem()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<InventoryTrackerDbContext>();

        var category = new Category
        {
            Name = "Test Hardware"
        };

        var item = new InventoryItem
        {
            Name = "Test Monitor",
            Description = "Integration test monitor",
            Quantity = 4,
            ReorderLevel = 5,
            IsActive = true,
            Category = category
        };

        context.InventoryItems.Add(item);
        await context.SaveChangesAsync();

        var id = item.InventoryItemId;

        // Act
        var response = await _client.GetAsync(
            $"/api/InventoryItems/{id}");

        var responseBody =
            await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(
            response.IsSuccessStatusCode,
            $"Expected success but received {(int)response.StatusCode} " +
            $"{response.StatusCode}. Response: {responseBody}");

        var result =
            await response.Content
                .ReadFromJsonAsync<InventoryItemDto>();

        Assert.NotNull(result);

        Assert.Equal("Test Monitor", result.Name);
        Assert.Equal("Test Hardware", result.CategoryName);
        Assert.Equal(4, result.Quantity);
    }

    [Fact]
    public async Task CreateInventoryItem_WhenCategoryDoesNotExist_ReturnsBadRequest()
    {
        // Arrange
        var newItem = new CreateInventoryItemDto
        {
            Name = "Invalid Category Item",
            Description = "Integration test",
            Quantity = 5,
            ReorderLevel = 2,
            IsActive = true,
            CategoryId = 9999
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/InventoryItems",
            newItem);

        var responseBody =
            await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);

        Assert.Contains(
            "Category does not exist",
            responseBody);
    }

    [Fact]
    public async Task GetLowStockItems_ReturnsOnlyActiveLowStockItems()
    {
        // Arrage
        using var scope = _factory.Services.CreateScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<InventoryTrackerDbContext>();

        var category = new Category
        {
            Name = "Test Hardware"
        };

        var activeLowStockItem = new InventoryItem
        {
            Name = "Low Stock Item",
            Quantity = 2,
            ReorderLevel = 5,
            IsActive = true,
            Category = category
        };

        var inactiveLowStockItem = new InventoryItem
        {
            Name = "Inactive Low Stock Item",
            Quantity = 1,
            ReorderLevel = 5,
            IsActive = false,
            Category = category
        };

        var healthyStockItem = new InventoryItem
        {
            Name = "Healthy Stock Item",
            Quantity = 10,
            ReorderLevel = 5,
            IsActive = true,
            Category = category
        };

        context.InventoryItems.AddRange(
            activeLowStockItem,
            inactiveLowStockItem,
            healthyStockItem);

        await context.SaveChangesAsync();

        // Act
        var response = await _client.GetAsync(
            "/api/InventoryItems/low-stock");

        var results =
            await response.Content
                .ReadFromJsonAsync<List<InventoryItemDto>>();

        // Assert
        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        Assert.NotNull(results);
        var result = Assert.Single(results);

        Assert.Equal(
            "Low Stock Item",
            result.Name);
    }
}