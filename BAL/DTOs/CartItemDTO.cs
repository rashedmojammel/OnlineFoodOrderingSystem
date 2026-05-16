using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTOs
{
    public class CartItemDTO
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}
