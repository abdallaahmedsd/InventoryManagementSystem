using InventoryManagementSystem.DataAccess.Repository.IRepository;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Web.Controllers
{
    /// <summary>
    /// Controller for managing item-related operations.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for performing CRUD operations on items,
    /// including retrieving all items, getting an item by ID, creating, updating, and deleting items.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves all items along with their associated categories.
        /// </summary>
        /// <returns>A list of item DTOs.</returns>
        /// <response code="200">Returns the list of items.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _unitOfWork.Item.GetAllAsync(includeProperties: "Category");

                var itemDtos = items.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category.Name
                });

                return Ok(itemDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific item by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The requested item DTO.</returns>
        /// <response code="200">Returns the requested item.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                var item = await _unitOfWork.Item.GetByIdAsync(id, includeProperties: "Category");

                if (item == null)
                    return NotFound();

                var itemDto = new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    CategoryId = item.CategoryId,
                    CategoryName = item.Category.Name
                };

                return Ok(itemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="itemDto">The item data to create.</param>
        /// <returns>The created item.</returns>
        /// <response code="201">Item created successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TbItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var item = new TbItem
                {
                    Name = itemDto.Name,
                    Description = itemDto.Description,
                    Price = itemDto.Price,
                    Quantity = itemDto.Quantity,
                    CategoryId = itemDto.CategoryId
                };

                await _unitOfWork.Item.AddAsync(item);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="itemDto">The updated item data.</param>
        /// <response code="204">Item updated successfully.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemDto itemDto)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var item = await _unitOfWork.Item.GetByIdAsync(id);
                if (item == null)
                    return NotFound();

                item.Name = itemDto.Name;
                item.Description = itemDto.Description;
                item.Price = itemDto.Price;
                item.Quantity = itemDto.Quantity;
                item.CategoryId = itemDto.CategoryId;

                _unitOfWork.Item.Update(item);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        /// <summary>
        /// Deletes an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <response code="204">Item deleted successfully.</response>
        /// <response code="404">Item not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await _unitOfWork.Item.GetByIdAsync(id);
                if (item == null)
                    return NotFound();

                _unitOfWork.Item.Remove(item);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
