using InventoryManagementSystem.DataAccess.Repository.IRepository;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
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

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDto itemDto)
        {
            if (itemDto == null)
                return BadRequest();

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemDto itemDto)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _unitOfWork.Item.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            _unitOfWork.Item.Remove(item);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
