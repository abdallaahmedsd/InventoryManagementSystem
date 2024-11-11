using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Web.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;
        public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();
    }
}
