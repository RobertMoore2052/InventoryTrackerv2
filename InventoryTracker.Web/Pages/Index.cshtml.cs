using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DashboardViewModel Dashboard { get; set; } = new();

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        Dashboard =
            await client.GetFromJsonAsync<DashboardViewModel>(
                "api/Dashboard")
            ?? new();
    }
}
