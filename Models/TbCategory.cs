using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class TbCategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = null!;
        public ICollection<TbItem> Items { get; set; } = new List<TbItem>();
    }
}
