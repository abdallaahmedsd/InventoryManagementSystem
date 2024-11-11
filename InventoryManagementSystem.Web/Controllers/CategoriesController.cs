using InventoryManagementSystem.DataAccess.Repository.IRepository;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.Category.GetAllAsync(includeProperties: "Items");

            var categoryDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Items = c.Items.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    CategoryId = i.CategoryId,
                    CategoryName = c.Name
                }).ToList()
            });
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id, includeProperties: "Items");
            if (category == null)
                return NotFound();

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Items = category.Items.Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    CategoryId = i.CategoryId,
                    CategoryName = category.Name
                }).ToList()
            };

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest();

            var category = new TbCategory { Name = categoryDto.Name };

            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            category.Name = categoryDto.Name;

            _unitOfWork.Category.Update(category);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _unitOfWork.Category.Remove(category);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
