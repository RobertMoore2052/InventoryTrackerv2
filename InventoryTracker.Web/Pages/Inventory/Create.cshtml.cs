using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace InventoryTracker.Web.Pages.Inventory;

public class CreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    [BindProperty]
    public CreateInventoryItemViewModel InventoryItem { get; set; } = new();

    public List<CategoryViewModel> Categories { get; set; } = [];

    public CreateModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        Categories =
            await client.GetFromJsonAsync<List<CategoryViewModel>>(
                "api/Categories")
            ?? [];
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        if (!ModelState.IsValid)
        {
            Categories =
                await client.GetFromJsonAsync<List<CategoryViewModel>>(
                    "api/Categories")
                ?? [];

            return Page();
        }

        var response = await client.PostAsJsonAsync(
            "api/InventoryItems",
            InventoryItem);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage =
                await response.Content.ReadAsStringAsync();

            Categories =
                await client.GetFromJsonAsync<List<CategoryViewModel>>(
                    "api/Categories")
                ?? [];

            ModelState.AddModelError(
                string.Empty,
                string.IsNullOrWhiteSpace(errorMessage)
                    ? "Unable to create inventory item."
                    : errorMessage);

            return Page();
        }

        return RedirectToPage("./Index");
    }
}