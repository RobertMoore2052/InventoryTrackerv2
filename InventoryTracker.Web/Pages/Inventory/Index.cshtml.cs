using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages.Inventory;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public List<InventoryItemViewModel> InventoryItems { get; set; } = [];

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("InventoryTrackerApi");

        InventoryItems =
            await client.GetFromJsonAsync<List<InventoryItemViewModel>>(
                "api/InventoryItems")
            ?? [];
    }
}