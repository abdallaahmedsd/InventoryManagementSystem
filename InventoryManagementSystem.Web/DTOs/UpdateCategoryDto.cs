using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Web.DTOs
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        [MaxLength(150, ErrorMessage = "Category name cannot exceed 150 characters.")]
        public string Name { get; set; } = null!;
    }

}
