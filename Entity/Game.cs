using System;
using System.Collections.Generic;

namespace Entity
{
    public class Game
    {
        public int GameId { get; set; }

        public int? Host { get; set; }

        public DateTime Time { get; set; }
    }
}
