using InventoryTracker.Api.Dtos;
using InventoryTracker.Data;
using InventoryTracker.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly InventoryTrackerDbContext _context;

    public CategoriesController(InventoryTrackerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var categories = await _context.Categories
            .OrderBy(category => category.Name)
            .Select(category => new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                InventoryItemCount = category.InventoryItems.Count
            })
            .ToListAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Where(category => category.CategoryId == id)
            .Select(category => new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                InventoryItemCount = category.InventoryItems.Count
            })
            .FirstOrDefaultAsync();

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(
        CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var result = new CategoryDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            InventoryItemCount = category.InventoryItems.Count
        };

        return CreatedAtAction(
            nameof(GetCategory),
            new { id = category.CategoryId },
            result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(
        int id,
        UpdateCategoryDto dto)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.CategoryId == id);

        if (category == null)
        {
            return NotFound();
        }

        category.Name = dto.Name;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoryDto>> DeleteCategory(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.CategoryId == id);

        if (category == null)
        {
            return NotFound();
        }

        var containsItems = await _context.InventoryItems
            .AnyAsync(item => item.CategoryId == id);

        if (containsItems)
        {
            return BadRequest($"Category {id} still contains Inventory Items.");
        }

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}