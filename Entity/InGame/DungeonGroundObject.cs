using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class DungeonGroundObject : BaseObject
    {
		public DungeonGroundObject() : base()
        {
            Width = 300;
            Height = 150;

            BoxCollider.Height = Height - 25;
			BoxCollider.Width = Width;
			BoxCollider.Left = 0;
			BoxCollider.Bottom = 0;
            //BoxCollider.BackgroundColor = "rgba(255, 150, 0, 0.5)";
			BoxCollider.ID = "HitBox" + ID;

			BackgroundImage = "url(https://localhost:44319/images/Dungeon/DungeonGround.png)";
            BackgroundSize = "contain";
            Position = "absolute";
            Left = 0;
            Bottom = 0;
            Class = "Ground";
			ObjectType = "Ground";
            z_index = "1";

		}
    }
}
