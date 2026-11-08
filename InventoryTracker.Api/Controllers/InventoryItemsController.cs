using InventoryTracker.Data;
using InventoryTracker.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryTracker.Domain;

namespace InventoryTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemsController : ControllerBase
    {
        private readonly InventoryTrackerDbContext _context;

        public InventoryItemsController(InventoryTrackerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryItemDto>>> GetInventoryItems()
        {
            var items = await _context.InventoryItems
                .OrderBy(item => item.Name)
                .Select(item => new InventoryItemDto
                {
                    InventoryItemId = item.InventoryItemId,
                    Name = item.Name,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    ReorderLevel = item.ReorderLevel,
                    IsActive = item.IsActive,
                    CategoryId = item.CategoryId,
                    CategoryName = item.Category.Name
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItemDto>> GetInventoryItem(int id)
        {
            var item = await _context.InventoryItems
                .Where(item => item.InventoryItemId == id)
                .Select(item => new InventoryItemDto
                {
                    InventoryItemId = item.InventoryItemId,
                    Name = item.Name,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    ReorderLevel = item.ReorderLevel,
                    IsActive = item.IsActive,
                    CategoryId = item.CategoryId,
                    CategoryName = item.Category.Name
                })
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItemDto>> CreateInventoryItem(
            CreateInventoryItemDto dto)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(category => category.CategoryId == dto.CategoryId);

            if (!categoryExists)
            {
                return BadRequest("Category does not exist.");
            }

            var item = new InventoryItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Quantity = dto.Quantity,
                ReorderLevel = dto.ReorderLevel,
                IsActive = dto.IsActive,
                CategoryId = dto.CategoryId
            };

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();

            var result = new InventoryItemDto
            {
                InventoryItemId = item.InventoryItemId,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                ReorderLevel = item.ReorderLevel,
                IsActive = item.IsActive,
                CategoryId = item.CategoryId,
                CategoryName = await _context.Categories
                    .Where(category => category.CategoryId == item.CategoryId)
                    .Select(category => category.Name)
                    .FirstAsync()
            };

            return CreatedAtAction(
                nameof(GetInventoryItem),
                new { id = item.InventoryItemId },
                result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryItem(
            int id,
            UpdateInventoryItemDto dto)
        {
            var item = await _context.InventoryItems
                .FirstOrDefaultAsync(item => item.InventoryItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            var categoryExists = await _context.Categories
                .AnyAsync(category => category.CategoryId == dto.CategoryId);

            if (!categoryExists)
            {
                return BadRequest("Category does not exist.");
            }

            item.Name = dto.Name;
            item.Description = dto.Description;
            item.Quantity = dto.Quantity;
            item.ReorderLevel = dto.ReorderLevel;
            item.IsActive = dto.IsActive;
            item.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var item = await _context.InventoryItems
                .FirstOrDefaultAsync(item => item.InventoryItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            _context.InventoryItems.Remove(item);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
