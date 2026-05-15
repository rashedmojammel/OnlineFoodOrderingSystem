using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTOs
{
    public class FoodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
