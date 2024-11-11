﻿namespace InventoryManagementSystem.Web.DTOs
{
    public class UpdateItemDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }

}
