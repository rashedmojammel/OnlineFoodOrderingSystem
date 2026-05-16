using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class OrderItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? FoodId { get; set; }

    public string? FoodName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Food? Food { get; set; }

    public virtual Order? Order { get; set; }
}
