using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.InGame
{
    public class BasePlayerObject: BaseObject
    {
		public int? UserId { get; set; }
		public int[] Inventory { get; set; }        // инвентарь игрока !!!
        public int[] Equipment { get; set; }        // экипировка игрока !!!
        public int MaxWeight { get; set; }          // грузоподъемность
        public int Weight { get; set; }             // текущая загруженность
        public int MaxQuantity { get; set; }        // вместительность
        public int Quantity { get; set; }           // текущая вместительность
        public int Crystals { get; set; }           // кристаллы
        public int Coins { get; set; }              // денег
        public double DamageMultiplier { get; set; }  // множитель урона
        public int Power { get; set; }              // сила
        public int Dexterity { get; set; }          // ловкость
        public int Durability { get; set; }         // стойкость
        public int Vitality { get; set; }           // живучесть
        public int Endurance { get; set; }          // выносливость
        public int Swordplay { get; set; }          // фехтование (Владение оружием)
        public int MaxHP { get; set; }              // максимальное значение здоровья vitality * 10 + durability * 2
        public int Armor { get; set; }              // показатель брони durability +  vitality / 10 + endurance / 10
        public int Damage { get; set; }             // показатель урона power * (damage_multiplier / 2) + dexterity + (swordplay * damage_multiplier)
        public int HP { get; set; }                 // здоровье
        public int SP { get; set; }                 // выносливость
        public int Speed { get; set; }              // скорость
        public int JumpPower { get; set; }          // сила прыжка

		public BoxColliderObject BoxAttack { get; set; } = new BoxColliderObject(); // хитбокс атаки

		public string Orientation { get; set; } = "Right";
		public string? Intersector { get; set; }
		public int? CountAnimation { get; set; } = -1;
		public string? Animation { get; set; }
		public int CountSec { get; set; } = 0;
		public int CountAttack { get; set; } = -1;
        public bool CanAttack { get; set; }
        public int xDelta { get; set; } = 0;
		public int yDelta { get; set; } = 0;
		public int xPos { get; set; } = 0;
		public int yPos { get; set; } = 0;
	}
}
