using InventoryManagementSystem.DataAccess.Repository.IRepository;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Web.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Web.Controllers
{
    /// <summary>
    /// Controller for managing category-related operations.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for performing CRUD operations on categories,
    /// including retrieving all categories, getting a category by ID, creating, updating, and deleting categories.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>A list of all categories with their item count.</returns>
        /// <response code="200">Returns the list of categories</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _unitOfWork.Category.GetAllAsync();

                var categoryDtos = categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                });

                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category if found, otherwise a NotFound result.</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="404">If the category is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(id);
                if (category == null)
                    return NotFound();

                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="categoryDto">The category data to create.</param>
        /// <returns>A CreatedAtAction result with the newly created category.</returns>
        /// <response code="201">If the category is successfully created</response>
        /// <response code="400">If the category data is invalid</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPost]
        [ProducesResponseType(typeof(TbCategory), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var category = new TbCategory { Name = categoryDto.Name };

                await _unitOfWork.Category.AddAsync(category);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The updated category data.</param>
        /// <returns>A NoContent result if the update was successful, or a NotFound result if the category does not exist.</returns>
        /// <response code="204">If the category was successfully updated</response>
        /// <response code="400">If the category data is invalid</response>
        /// <response code="404">If the category is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(id);
                if (category == null)
                    return NotFound();

                category.Name = categoryDto.Name;

                _unitOfWork.Category.Update(category);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A NoContent result if the category was deleted, or a NotFound result if the category does not exist.</returns>
        /// <response code="204">If the category was successfully deleted</response>
        /// <response code="404">If the category is not found</response>
        /// <response code="500">If an internal server error occurs</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(id);
                if (category == null)
                    return NotFound();

                _unitOfWork.Category.Remove(category);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
