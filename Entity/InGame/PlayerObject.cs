using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entity.InGame
{
    public class PlayerObject : BasePlayerObject
    {
        public PlayerObject() : base()
        {
            Width = 360;
            Height = 240;
            BackgroundImage = "url(https://localhost:44319/images/Player/Idle.png)";
            Position = "absolute";
            z_index = "1000";
            Transform = "scale(1, 1)";
			Status = "live";


			BoxCollider.Width = 64;
            BoxCollider.Height = 114;
            BoxCollider.Left = Width/3 + 11;
            BoxCollider.Bottom = 0;
			BoxCollider.BackgroundColor = "rgba(0, 255, 0, 0.5)";
            BoxCollider.ID = "HitBox" + ID;

            BoxAttack.Display = "none";
			BoxAttack.Width = 115;
            BoxAttack.Height = BoxCollider.Height;
            BoxAttack.Left = BoxCollider.Left + BoxCollider.Width;
            BoxAttack.Bottom = BoxCollider.Bottom;
			BoxAttack.BackgroundColor = "rgba(0, 0, 255, 0.4)";

			BoxAttack.ID = "BoxAttack" + ID;
			//-----------------------------------------------------------------------------//
			Inventory = new int[MaxQuantity];       // инвентарь игрока
            Equipment = new int[6];                 // экипировка игрока
            Weight = 0;                             // текущая загруженность
            MaxWeight = 50;                         // грузоподъемность
            Quantity = 0;                           // текущая вместительность
            MaxQuantity = 50;			            // вместительность
            Crystals = 0;			                // кристаллы
            Coins = 0;		                        // денег
            //-----------------------------------------------------------------------------//
            Power = 1;                              // сила
            Dexterity = 1;                          // ловкость
            Durability = 1;                         // стойкость
            Vitality = 1;                           // живучесть
            Endurance = 1;                          // выносливость
            Swordplay = 1;                          // фехтование
            //-----------------------------------------------------------------------------//
            DamageMultiplier = 1;                                   // множитель урона
            MaxHP = Vitality * 10 + Durability * 2;                 // максимальное значение здоровья
            Armor =  Durability + Vitality / 10 + Endurance / 10;   // показатель брони
            Damage = (int)(Power * (DamageMultiplier / 2) + Dexterity + Swordplay * DamageMultiplier); // показатель урона
            HP = MaxHP;                     // здоровье
            SP = 100;                       // выносливость
            //-----------------------------------------------------------------------------//
            Speed = 4;
            JumpPower = 5;
            //-----------------------------------------------------------------------------//
            ObjectType = "Player";
        }
    }
}
