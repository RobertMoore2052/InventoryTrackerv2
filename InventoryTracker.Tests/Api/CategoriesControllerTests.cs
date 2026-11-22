using InventoryTracker.Data;
using InventoryTracker.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace InventoryTracker.Tests.Api;

public class CategoriesControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CategoriesControllerTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();

        _factory.ResetDatabase();
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryContainsItems_ReturnsBadRequest()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<InventoryTrackerDbContext>();

        var category = new Category
        {
            Name = "Test Category"
        };

        var item = new InventoryItem
        {
            Name = "Test Item",
            Quantity = 5,
            ReorderLevel = 2,
            IsActive = true,
            Category = category
        };

        context.InventoryItems.Add(item);
        await context.SaveChangesAsync();

        var categoryId = category.CategoryId;

        // Act
        var response = await _client.DeleteAsync(
            $"/api/Categories/{categoryId}");

        var responseBody =
            await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);

        Assert.Contains(
            "still contains Inventory Items",
            responseBody);
    }

    [Fact]
    public async Task DeleteCategory_WhenCategoryIsEmpty_ReturnsNoContent()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();

        var context =
            scope.ServiceProvider
                .GetRequiredService<InventoryTrackerDbContext>();

        var category = new Category
        {
            Name = "Empty Test Category"
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        var categoryId = category.CategoryId;

        // Act
        var response = await _client.DeleteAsync(
            $"/api/Categories/{categoryId}");

        var getResponse = await _client.GetAsync(
            $"/api/Categories/{categoryId}");

        // Assert
        Assert.Equal(
            HttpStatusCode.NoContent,
            response.StatusCode);

        Assert.Equal(
            HttpStatusCode.NotFound,
            getResponse.StatusCode);
    }
}