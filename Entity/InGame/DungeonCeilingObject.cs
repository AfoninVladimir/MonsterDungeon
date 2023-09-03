using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class DungeonCeilingObject : BaseObject
    {
        public DungeonCeilingObject() : base()
        {
            Width = 300;
            Height = 150;
            BackgroundImage = "url(https://localhost:44319/images/Dungeon/DungeonCeiling.png)";
            BackgroundSize = "contain";
            Position = "absolute";
            Class  = "Ceiling";
            ObjectType = "Ceiling";
		}
    }
}
