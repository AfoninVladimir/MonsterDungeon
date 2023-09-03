using Entity.InGame;
using Quartz;
using System.Collections.Concurrent;

namespace BL
{
	public class GamePlay : IJob
	{
		public Task Execute(IJobExecutionContext context)
		{
			JobKey key = context.JobDetail.Key;
			JobDataMap dataMap = context.JobDetail.JobDataMap;

			Dictionary<int, string> Keys = new Dictionary<int, string>()
			{
				{65, "A"},
				{68, "D"},
				{87, "W"},
				{83, "S"},
				{67, "C"},
				{69, "E"},
				{73, "I"},
				{74, "J"},
				{75, "K"},
				{76, "L"},
				{70, "F"},
				{9, "Tab"},
				{18, "Alt"},
				{17, "Ctrl"},
				{32, "Space"},
				{16, "Shift"},
				{1011, "LKM"},


			};

			int gameId = dataMap.GetInt("GameId");
			foreach (int userId in GameLogic.UsersInGame[gameId].Keys)
			{
				foreach (var i in GameLogic.AllGames[gameId].Values)
				{
					if (i.ObjectType == "Player" && i.Status == "live")
					{
						var player = (PlayerObject)i;
						if (player.UserId == userId)
						{
							Dictionary<string, bool> boolKey = new Dictionary<string, bool>()
			{
				{"A", false},
				{"D", false},
				{"C", false},
				{"W", false},
				{"S", false},
				{"E", false},
				{"I", false},
				{"J", false},
				{"K", false},
				{"L", false},
				{"F", false},
				{"Tab", false},
				{"Alt", false},
				{"Ctrl", false},
				{"Space", false},
				{"Shift", false},
				{"LKM", false},
			};
							Dictionary<string, bool> boolAnimation = new Dictionary<string, bool>()
			{
				{"Idle", true},
				{"Run", false},
				{"Jump", false},
				{"Death", false},
				{"Roll", false},
				{"Crouch", false},
				{"Attack", false},
				{"Fall", false}
			};

							foreach (var Key in Keys)
							{
								if (GameLogic.AllButtons[gameId][userId].ContainsKey(Key.Key))
								{
									boolKey[Key.Value] = !boolKey[Key.Value];
								}
							}

							if (boolKey["A"])
								{
									player.xPos = -5;
									player.xDelta = -player.Speed;
									boolAnimation["Run"] = true;
									player.Orientation = "Left";
									GameLogic.MoveX(player);
								}
							if (boolKey["D"])
								{
									player.xPos = 5;
									player.xDelta = player.Speed;
									boolAnimation["Run"] = true;
									player.Orientation = "Right";
									GameLogic.MoveX(player);
								}
							if (boolKey["Space"])
								{
									player.yPos = 100;
									player.yDelta = player.JumpPower;
									boolAnimation["Jump"] = true;
								}
							if (boolKey["LKM"])
								{
									boolAnimation["Attack"] = true;
									player.CanAttack = true;
								}
							if (player.CountAttack > -1)
								{
									boolAnimation["Attack"] = true;
								}
							if (player.Animation == "Dead")
							{
								boolAnimation["Death"] = true;
							}
							GameLogic.Inter(player, GameLogic.AllGames[gameId].Values);
							GameLogic.MoveY(player, GameLogic.AllGames[gameId].Values, boolAnimation);

							if (player.CountSec < 10)
							{
								player.CountSec++;
							}
							else
							{
								player.CountSec = 0;
								GameLogic.Animation(player, boolAnimation, boolKey);
								GameLogic.IntersectAttack(player, GameLogic.AllGames[gameId].Values);
							}
						}
					}

					if (i.ObjectType == "Enemy")
					{
                        if (i.Status == "dead")
                        {
							BL.GameLogic.AllGames[gameId].TryRemove(i.ID, out BaseObject value);
						}
                    }
				}
			}
			return Task.CompletedTask;
		}
	}
}
