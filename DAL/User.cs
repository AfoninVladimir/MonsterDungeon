using System;
using System.Collections.Generic;

namespace DAL;

public partial class User
{
    public User()
    {
        Games = new HashSet<Game>();
        Sessions = new HashSet<Session>();
    }
    public int UserId { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; }
    public virtual ICollection<Session> Sessions { get; set; }
}
