using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages.Inventory;

public class SearchModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    [BindProperty(SupportsGet = true)]
    public string? Term { get; set; }

    public List<InventoryItemViewModel> InventoryItems { get; set; } = [];

    public SearchModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
        if (string.IsNullOrWhiteSpace(Term))
        {
            return;
        }

        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        InventoryItems =
            await client.GetFromJsonAsync<List<InventoryItemViewModel>>(
                $"api/InventoryItems/search?term={Uri.EscapeDataString(Term)}")
            ?? [];
    }
}
