using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Web.DTOs
{
    public class CreateItemDto
    {
        [Required(ErrorMessage = "Item name is required.")]
        [MaxLength(150, ErrorMessage = "Item name cannot exceed 150 characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }
    }

}
