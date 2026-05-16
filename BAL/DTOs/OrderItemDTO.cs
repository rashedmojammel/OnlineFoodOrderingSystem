using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTOs
{
    namespace BAL.DTOs
    {
        public class OrderItemDTO
        {
            public int Id { get; set; }
            public int? OrderId { get; set; }
            public int? FoodId { get; set; }
            public string? FoodName { get; set; }
            public int? Quantity { get; set; }
            public decimal? Price { get; set; }
        }
    }
}
