using Entity.InGame;
using Quartz;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BL
{
	public class GameLogic
	{
		public static int Gravitation = 1;  // гравитация

		public static ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.InGame.BaseObject>> AllGames = new ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.InGame.BaseObject>>();
		public static ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.InGame.BaseObject>>> UserInterface = new ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<string, Entity.InGame.BaseObject>>>();
		public static ConcurrentDictionary<int, ConcurrentDictionary<int, bool>> UsersInGame = new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>();
		public static ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>> AllButtons = new ConcurrentDictionary<int, ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>>();

		public static void Initialization(Entity.Game game)
		{
			AllButtons.TryAdd(game.GameId, new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>());
			UsersInGame.TryAdd(game.GameId, new ConcurrentDictionary<int, bool>());
			UserInterface.TryAdd(game.GameId, new ConcurrentDictionary<int, ConcurrentDictionary<string, BaseObject>>());

			var arrayObj = new ConcurrentDictionary<string, BaseObject>();

			// Потолок
			var DungeonCeiling = new DungeonCeilingObject();
			DungeonCeiling.Left = 0;
			DungeonCeiling.Top = 0;
			DungeonCeiling.Width = 1950;
			DungeonCeiling.Height = 150;
			arrayObj.TryAdd(DungeonCeiling.ID, DungeonCeiling);

			// Пол
			var DungeonGround = new DungeonGroundObject();
			DungeonGround.Left = 0;
			DungeonGround.Bottom = -60;
			DungeonGround.Width = 1950;
			DungeonGround.Height = 150;
			DungeonGround.BoxCollider.Width = DungeonGround.Width;
			arrayObj.TryAdd(DungeonGround.ID, DungeonGround);


			// Выход
			var DungeonExit = new DungeonExitObject();
			arrayObj.TryAdd(DungeonExit.ID, DungeonExit);

			// Враг(слизь)
			var Slime = new SlimeObject();
			Slime.BackgroundRepeat = "no-repeat";
			Slime.Left = 1000;
			Slime.Bottom = 65;
			arrayObj.TryAdd(Slime.ID, Slime);

			AllGames.TryAdd(game.GameId, arrayObj);
		}
		public static void JoinUser(int gameID, int userID)
		{
			AllButtons[gameID].TryAdd(userID, new ConcurrentDictionary<int, bool>());
			UsersInGame[gameID].TryAdd(userID, true);

			var ArrayInterface = new ConcurrentDictionary<string, BaseObject>();

			//Head-Up Display
			/*var HUD = new Entity.InGame.HUDObject();
			ArrayInterface.TryAdd(HUD.ID, HUD);*/

			// Игрок
			var Player = new PlayerObject();
			Player.UserId = userID;
			Player.Left = 500;
			Player.Bottom = 100;
			Player.ObjectType = "Player";
			ArrayInterface.TryAdd(Player.ID, Player);

			//Интерфейс
			//var Book = new Entity.InGame.BookObject();

			UserInterface[gameID].TryAdd(userID, ArrayInterface);
			AllGames[gameID].TryAdd(Player.ID, Player);
		}
		public static bool Intersects(PlayerObject Obj1, BaseObject Obj2)
		{
			double? axl = Obj1.Left + Obj1.BoxCollider.Left;
			double? axr = axl + Obj1.BoxCollider.Width;
			double? ayd = Obj1.Bottom + Obj1.BoxCollider.Bottom;
			double? ayu = ayd + Obj1.BoxCollider.Height;

			double? bxl = Obj2.Left + Obj2.BoxCollider.Left;
			double? bxr = bxl + Obj2.BoxCollider.Width;
			double? byd = Obj2.Bottom + Obj2.BoxCollider.Bottom;
			double? byu = byd + Obj2.BoxCollider.Height;

			double? ax = axl;
			double? ay = ayd;
			double? ax1 = axr;
			double? ay1 = ayu;

			double? bx = bxl;
			double? by = byd;
			double? bx1 = bxr;
			double? by1 = byu;

			bool result = (
		(
		  (
			(ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
		  ) && (
			(ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
		  )
		) || (
		  (
			(bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
		  ) && (
			(by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
		  )
		)
	  ) || (
		(
		  (
			(ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
		  ) && (
			(by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
		  )
		) || (
		  (
			(bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
		  ) && (
			(ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
		  )
		)
	  );

			return result;
		}
		public static void IntersectAttack(PlayerObject player, ICollection<BaseObject> arrayObj)
		{
			foreach (var item in arrayObj)
			{
				if (item.ObjectType == "Enemy")
				{
					//double? axl = player.Left + player.BoxAttack.Left;
					//double? axr = axl + player.BoxAttack.Width;
					//double? ayd = player.Bottom + player.BoxAttack.Bottom;
					//double? ayu = ayd + player.BoxAttack.Height;

					double? axl;
					double? axr;
					double? ayd;
					double? ayu;

					if (player.Orientation == "Right")
					{
						axl = player.Left + player.BoxAttack.Left;
						axr = axl + player.BoxAttack.Width;
						ayd = player.Bottom + player.BoxAttack.Bottom;
						ayu = ayd + player.BoxAttack.Height;
					}
					else // Left
					{
						axl = player.Left + player.BoxAttack.Left - player.BoxCollider.Width - player.BoxAttack.Width;
						axr = axl + player.BoxAttack.Width;
						ayd = player.Bottom + player.BoxAttack.Bottom;
						ayu = ayd + player.BoxAttack.Height;
					}

					double? bxl = item.Left + item.BoxCollider.Left;
					double? bxr = bxl + item.BoxCollider.Width;
					double? byd = item.Bottom + item.BoxCollider.Bottom;
					double? byu = byd + item.BoxCollider.Height;

					double? ax = axl;
					double? ay = ayd;
					double? ax1 = axr;
					double? ay1 = ayu;

					double? bx = bxl;
					double? by = byd;
					double? bx1 = bxr;
					double? by1 = byu;

					bool result = (((
					(ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
				  ) && (
					(ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
				  )) || ((
					(bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
				  ) && (
					(by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
				  ))) || (((
					(ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
				  ) && (
					(by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
				  )) || ((
					(bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
				  ) && (
					(ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
				  )));
					if (result && player.CanAttack)
					{
						player.CanAttack = false;
						if ((item.HP - player.Damage) > 0)
						{
							item.HP -= player.Damage;
						}
						else
						{
							item.HP = 0;
							item.Status = "dead";
						}					 
					}
				}

				if (item.ObjectType == "Enemy")
				{
					double? axl = player.Left + player.BoxCollider.Left;
					double? axr = axl + player.BoxCollider.Width;
					double? ayd = player.Bottom + player.BoxCollider.Bottom;
					double? ayu = ayd + player.BoxCollider.Height;

					double? bxl = item.Left + item.BoxCollider.Left;
					double? bxr = bxl + item.BoxCollider.Width;
					double? byd = item.Bottom + item.BoxCollider.Bottom;
					double? byu = byd + item.BoxCollider.Height;

					double? ax = axl;
					double? ay = ayd;
					double? ax1 = axr;
					double? ay1 = ayu;

					double? bx = bxl;
					double? by = byd;
					double? bx1 = bxr;
					double? by1 = byu;

					bool result = (((
					(ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
				  ) && (
					(ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
				  )) || ((
					(bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
				  ) && (
					(by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
				  ))) || (((
					(ax >= bx && ax <= bx1) || (ax1 >= bx && ax1 <= bx1)
				  ) && (
					(by >= ay && by <= ay1) || (by1 >= ay && by1 <= ay1)
				  )) || ((
					(bx >= ax && bx <= ax1) || (bx1 >= ax && bx1 <= ax1)
				  ) && (
					(ay >= by && ay <= by1) || (ay1 >= by && ay1 <= by1)
				  )));

					if (result)
					{
						if (player.HP - item.Damage < 0)
						{
							player.HP = 0;
							player.Animation = "Dead";
						}
						else
						{
							player.HP -= item.Damage;
						}
					}
				}
			}
		}
		public static void MoveX(PlayerObject player)
		{
			if (player.xPos != 0)
			{
				player.xPos -= player.xDelta;
				bool canMove = true;

				/*foreach (var item in arrayObj)
				{
					if (item.ObjectType == "Player") continue;

					if (Intersects(player, item))
					{
						canMove = false;
					}
				}*/

				if (canMove)
				{
					player.Left += player.xDelta;
				}
			}
		}
		public static void MoveY(PlayerObject player, ICollection<BaseObject> arrayObj, Dictionary<string, bool> boolAnimation)
		{
			bool canFall = false;

			foreach (var item in arrayObj)
			{
				if (item.ObjectType == "Ground")
				{
					if (Intersects(player, item))
					{
						canFall = false;
					}
					else
					{
						canFall = true;
					}
				}
			}

			if (player.yPos != 0)
			{
				player.yPos -= player.yDelta;
				player.Bottom += player.yDelta;
				boolAnimation["Idle"] = false;
				boolAnimation["Jump"] = true;
			}
			else
			{
				if (canFall)
				{

					player.Bottom -= player.yDelta + Gravitation;
					boolAnimation["Idle"] = false;
					boolAnimation["Fall"] = true;
				}
			}
		}
		public static void Inter(PlayerObject player, ICollection<BaseObject> arrayObj)
		{
			player.Intersector = null;
			foreach (var item in arrayObj)
			{
				if (item.ObjectType == "Player" || item.ObjectType == "Ceiling") continue;
				if (item.ObjectType == "Exit")
				{
					if (Intersects(player, item))
					{
						player.Intersector = item.ObjectType;
					}
				}
				if (Intersects(player, item))
				{
					player.Intersector = item.ObjectType;
				}
			}
		}
		public static void Animation(PlayerObject player, Dictionary<string, bool> boolAnimation, Dictionary<string, bool> boolKey)
		{
			// Анимация покоя
			if (boolAnimation["Idle"])
			{
				player.Animation = "Idle";
				player.BackgroundImage = "url(https://localhost:44319/images/Player/Idle.png)";
				if (player.BackgroundPositionX + 360 < 3600)
				{
					player.CountAnimation++;
				}
				else
				{
					player.CountAnimation = -1;
				}
				player.BackgroundPositionX = 360 * player.CountAnimation;
			}
			// Анимация бега
			if (boolAnimation["Run"])
			{
				player.BackgroundImage = "url(https://localhost:44319/images/Player/Run.png)";
				if (boolKey["A"])
				{
					player.BackgroundImage = "url(https://localhost:44319/images/Player/Run.png)";
					player.Animation = "Run Left";
					player.Transform = "scale(-1, 1)";
				}
				if (boolKey["D"])
				{
					player.BackgroundImage = "url(https://localhost:44319/images/Player/Run.png)";
					player.Animation = "Run Right";
					player.Transform = "scale(1, 1)";
				}
				if (player.BackgroundPositionX + 360 < 3600)
				{
					player.CountAnimation++;
				}
				else
				{
					player.CountAnimation = 0;
				}
				player.BackgroundPositionX = -360 * player.CountAnimation;
			}
			// Анимация прыжка
			if (boolAnimation["Jump"])
			{
				player.Animation = "Jump";
				player.BackgroundImage = "url(https://localhost:44319/images/Player/Jump.png)";
				if (player.BackgroundPositionX + 360 < 1080)
				{
					player.CountAnimation++;
				}
				else
				{
					player.CountAnimation = -1;
				}
				player.BackgroundPositionX = -360 * player.CountAnimation;
			}
			// Анимация атаки
			if (boolAnimation["Attack"])
			{
				player.Animation = "Attack";
				if (player.CountAttack == -1)
				{
					player.BoxCollider.Left += 45;
					player.BoxAttack.Left += 45;
					player.BoxAttack.Display = "flex";
				}
				player.BackgroundImage = "url(https://localhost:44319/images/Player/Attack.png)";

				if (player.CountAttack < 3)
				{
					player.CountAttack++;
				}
				else
				{
					player.BoxAttack.Display = "none";
					player.BoxCollider.Left -= 45;
					player.BoxAttack.Left -= 45;
					player.CountAttack = -1;
				}


				player.BackgroundPositionX = -360 * player.CountAttack;
			}
			else
			{
				player.BoxAttack.Display = "none";
			}
			// Анимация падения
			if (boolAnimation["Fall"])
			{
				player.Animation = "Fall";
				player.BackgroundImage = "url(https://localhost:44319/images/Player/Fall.png)";
				if (player.BackgroundPositionX + 360 < 1080)
				{
					player.CountAnimation++;
				}
				else
				{
					player.CountAnimation = -1;
				}
				player.BackgroundPositionX = -360 * player.CountAnimation;
			}
			// Анимация смерти
			if (boolAnimation["Death"])
			{
				player.BackgroundImage = "url(https://localhost:44319/images/Player/Death.png)";
				if (player.BackgroundPositionX + 360 < 3600)
				{
					player.CountAnimation++;
				}
				else
				{
					player.Status = "dead";
				}
				player.BackgroundPositionX = -360 * player.CountAnimation;
			}
		}
	}
}
