using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;
}
