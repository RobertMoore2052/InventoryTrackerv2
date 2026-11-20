using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages.Inventory;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    [BindProperty]
    public EditInventoryItemViewModel InventoryItem { get; set; } = new();

    public List<CategoryViewModel> Categories { get; set; } = [];
    
    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        var response = await client.GetAsync(
            $"api/InventoryItems/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        response.EnsureSuccessStatusCode();

        var item =
            await response.Content
                .ReadFromJsonAsync<InventoryItemViewModel>();

        if (item == null)
        {
            return NotFound();
        }

        InventoryItem = new EditInventoryItemViewModel
        {
            InventoryItemId = item.InventoryItemId,
            Name = item.Name,
            Description = item.Description,
            Quantity = item.Quantity,
            ReorderLevel = item.ReorderLevel,
            IsActive = item.IsActive,
            CategoryId = item.CategoryId
        };

        Categories =
            await client.GetFromJsonAsync<List<CategoryViewModel>>(
                "api/Categories")
            ?? [];

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        var response = await client.PutAsJsonAsync(
            $"api/InventoryItems/{InventoryItem.InventoryItemId}",
            InventoryItem);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            Categories =
                await client.GetFromJsonAsync<List<CategoryViewModel>>(
                    "api/Categories")
                ?? [];

            ModelState.AddModelError(
                string.Empty,
                $"Unable to update inventory item. Status: {(int)response.StatusCode}. {error}");

            return Page();
        }

        return RedirectToPage("./Index");
    }
}