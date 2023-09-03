using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class SlimeObject : BaseMonsterObject
    {
        public SlimeObject() : base()
        {
            Width = 90;
            Height = 50;
            BackgroundSize = "90px 50px";
			BackgroundImage = "url(https://localhost:44319/images/Monsters/slime2.png)";
            Position = "absolute";

            Left = 0;
            Bottom = 0;
            //Inventory = new string[2]{ "items.Materials.SlimeCore", "items.Token.Crystal"}; // инвентарь слизня
            MaxHP = 10;     // Максимальное здоровье
            HP = MaxHP;     // Текущее здоровье
            Damage = 2;     // урон
            Class  = "Enemy";
            ObjectType = "Enemy";
			Status = "live";

			BoxCollider.Width = Width;
            BoxCollider.Height = Height;
            BoxCollider.Left = 0;
            BoxCollider.Bottom = 0;
            BoxCollider.BackgroundColor = "rgba(255, 0, 0, 0.5)";

		}
    }
}
