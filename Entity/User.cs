using System;
using System.Collections.Generic;

namespace Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
