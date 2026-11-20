using InventoryTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace InventoryTracker.Web.Pages.Categories;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory httpClientFactory;

    [BindProperty]
    public EditCategoryViewModel Category { get; set; } = new();

    public EditModel(IHttpClientFactory httpClientFactory)
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

        var category =
            await response.Content
                .ReadFromJsonAsync<CategoryViewModel>();

        if (category == null)
        {
            return NotFound();
        }

        Category = new EditCategoryViewModel
        {
            CategoryId = category.CategoryId,
            Name = category.Name
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client =
            _httpClientFactory.CreateClient("InventoryTrackerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Categories/{Category.CategoryId}",
            Category);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                string.Empty,
                "Unable to update category.");

            return Page();
        }

        return RedirectToPage("./Index");
    }
}
