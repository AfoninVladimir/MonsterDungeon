using System;
using System.Collections.Generic;

namespace DAL;

public partial class Game
{
    public Game() 
    {
        Sessions = new HashSet<Session>();
    }
    public int GameId { get; set; }
    public int? Host { get; set; }
    public DateTime Time { get; set; }
    public virtual User HostNavigation { get; set; } = null!;
    public virtual ICollection<Session> Sessions { get; set; }
}
