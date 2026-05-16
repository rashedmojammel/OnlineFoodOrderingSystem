using System;
using System.Collections.Generic;
using System.Text;
using BAL;
using BAL.DTOs;
using BAL.DTOs.BAL.DTOs;

namespace BAL.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Status { get; set; }
        public decimal? Total { get; set; }
        public string? PaymentMethod { get; set; }   
        public string? PaymentStatus { get; set; }   
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}
