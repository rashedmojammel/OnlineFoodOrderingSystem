using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Food
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Image { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
