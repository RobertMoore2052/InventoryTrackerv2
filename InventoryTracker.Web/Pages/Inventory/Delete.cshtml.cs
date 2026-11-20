using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages.Inventory;

public class DeleteModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InventoryItemViewModel? InventoryItem { get; set; }

    public DeleteModel(IHttpClientFactory httpClientFactory)
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

        InventoryItem =
            await response.Content
                .ReadFromJsonAsync<InventoryItemViewModel>();

        if (InventoryItem == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        var response = await client.DeleteAsync(
            $"api/InventoryItems/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                string.Empty,
                "Unable to delete inventory item.");

            return Page();
        }

        return RedirectToPage("./Index");
    }
}