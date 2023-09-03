using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class DungeonExitObject : BaseObject
    {
        public DungeonExitObject() : base()
        {
            Width = 300;
            Height = 350;

            BoxCollider.Height = Height;
            BoxCollider.Width = Width;
            BoxCollider.BackgroundColor = "rgba(255, 255, 0, 0.5)";
			BoxCollider.ID = "HitBox" + ID;

			BackgroundImage = "url(https://localhost:44319/images/Dungeon/DungeonExit.png)";
            BackgroundSize = "300px 350px";
            Position = "absolute";
            Left = 0;
            Bottom = 0;
            Class = "Exit";
            ObjectType = "Exit";

		}
    }
}
