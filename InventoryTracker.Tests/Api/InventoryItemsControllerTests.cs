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
}