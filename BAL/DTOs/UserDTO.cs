using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
