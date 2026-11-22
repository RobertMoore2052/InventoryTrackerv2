using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages.Categories;

public class DeleteModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryViewModel? Category { get; set; }
    
    public DeleteModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        var response = await client.GetAsync(
            $"api/Categories/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        response.EnsureSuccessStatusCode();

        Category =
            await response.Content
                .ReadFromJsonAsync<CategoryViewModel>();

        if (Category == null)
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
            $"api/Categories/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage =
                await response.Content.ReadAsStringAsync();

            var categoryResponse =
                await client.GetAsync($"api/Categories/{id}");

            if (categoryResponse.IsSuccessStatusCode)
            {
                Category =
                    await categoryResponse.Content
                        .ReadFromJsonAsync<CategoryViewModel>();
            }

            ModelState.AddModelError(
                string.Empty,
                string.IsNullOrWhiteSpace(errorMessage)
                    ? "Unable to delete category."
                    : errorMessage);

            return Page();
        }

        return RedirectToPage("./Index");
    }
}
