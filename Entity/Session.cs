using System;
using System.Collections.Generic;

namespace Entity
{
    public class Session
    {
        public int SessionId { get; set; }
        public int? GameId { get; set; }
        public int? UserId { get; set; }
    }
}
