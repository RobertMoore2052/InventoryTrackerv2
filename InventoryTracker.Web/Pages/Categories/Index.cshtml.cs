using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public List<CategoryViewModel> Categories { get; set; } = [];

    public IndexModel(IHttpClientFactory httpClientFactory)
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
}