using System;
using System.Collections.Generic;

namespace DAL;

public partial class Session
{
    public int SessionId { get; set; }
    public int? GameId { get; set; }
    public int? UserId { get; set; }

    public virtual Game Game { get; set; }
    public virtual User User { get; set; }
}
