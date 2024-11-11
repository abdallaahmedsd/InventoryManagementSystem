using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Web.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
