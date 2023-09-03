using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Concurrent;
using Quartz;
using Quartz.Impl;

namespace Monster_Dungeon.Controllers
{

	public class GameController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var query = new Entity.SearchIn.SearchingGame();
            return View(BL.GameBL.Get(query));
        }

		ConcurrentDictionary<int, IScheduler> gameFlows = new ConcurrentDictionary<int, IScheduler>();

		StdSchedulerFactory factory = new StdSchedulerFactory();

		[Authorize]
        public async Task<IActionResult> CreateNewGame()
        {
            var game = new Entity.Game();
            game.Host = BL.UserBL.Get(User.Identity.Name).UserId;
            game.Time = DateTime.Now;
            game.GameId = BL.GameBL.AddOrUpdate(game);

            var hostSession = new Entity.Session();
            hostSession.UserId = game.Host;
            hostSession.GameId = game.GameId;
            BL.SessionBL.AddOrUpdate(hostSession);

			BL.GameLogic.Initialization(game);

			ITrigger trigger = TriggerBuilder.Create()      // создаем триггер
		  .WithIdentity(game.GameId.ToString(), "group1")     // идентифицируем триггер с именем и группой
		  .StartNow()                                     // запуск сразу после начала выполнения
		  .WithSimpleSchedule(x => x                      // настраиваем выполнение действия
			  .WithInterval(new TimeSpan(0, 0, 0, 0, 10)) // каждые 10 мс
			  .RepeatForever())                           // бесконечное повторение
		  .Build();

			IJobDetail job = JobBuilder.Create<BL.GamePlay>()
				.UsingJobData("GameId", game.GameId)
				.Build();

			gameFlows.TryAdd(game.GameId, await factory.GetScheduler());

			await gameFlows[game.GameId].ScheduleJob(job, trigger);

			await gameFlows[game.GameId].Start();

			BL.GameLogic.JoinUser(game.GameId, game.Host ?? 0);

			return RedirectToAction("Start", "Game", new { gameId = game.GameId, userId = game.Host });
        }

        [Authorize]
        public IActionResult Start()
        {
            return View();
        }

        [Authorize]
        public IActionResult Join(int gameID)
        {
            var session = new Entity.Session();
            session.UserId = BL.UserBL.Get(User.Identity.Name).UserId;
            session.GameId = gameID;
			BL.GameLogic.JoinUser(session.GameId ?? 0, session.UserId ?? 0);
			BL.SessionBL.AddOrUpdate(session);
			return RedirectToAction("Start", "Game", new { gameId = session.GameId, userId = session.UserId });
		}

		public IActionResult PressButton(int keyCode, bool pressed, int gameId, int userId)
		{
			if (pressed)
			{
				if (!BL.GameLogic.AllButtons.ContainsKey(gameId)) BL.GameLogic.AllButtons.TryAdd(gameId, new ConcurrentDictionary<int, ConcurrentDictionary<int, bool>>());
				if (!BL.GameLogic.AllButtons[gameId].ContainsKey(userId)) BL.GameLogic.AllButtons[gameId].TryAdd(userId, new ConcurrentDictionary<int, bool>());
				if (!BL.GameLogic.AllButtons[gameId][userId].ContainsKey(keyCode))
				{
					BL.GameLogic.AllButtons[gameId][userId].TryAdd(keyCode, pressed);
				}
			}
			else
			{
				if (BL.GameLogic.AllButtons.ContainsKey(gameId) && BL.GameLogic.AllButtons[gameId].ContainsKey(userId))
				{
					if (BL.GameLogic.AllButtons[gameId][userId].ContainsKey(keyCode))
					{
						bool success;
						BL.GameLogic.AllButtons[gameId][userId].Remove(keyCode, out success);
					}
				}
			}
			return Ok(Json(true));

		}

		public IActionResult GetElements(int gameId, int userId)
		{
			if (BL.GameLogic.AllGames.ContainsKey(gameId))
			{
				Entity.InGame.BaseObject[] temp = new Entity.InGame.BaseObject[BL.GameLogic.AllGames[gameId].Count];
				BL.GameLogic.AllGames[gameId].Values.CopyTo(temp, 0);
				var json = JsonSerializer.Serialize((object[])temp);
				return Ok(json);
			}
			else return Ok();
		}

		public IActionResult GetInterface(int gameId, int userId)
		{
			if (BL.GameLogic.UserInterface.ContainsKey(gameId))
			{
				Entity.InGame.BaseObject[] temp = new Entity.InGame.BaseObject[BL.GameLogic.UserInterface[gameId][userId].Count];
				BL.GameLogic.UserInterface[gameId][userId].Values.CopyTo(temp, 0);
				var json = JsonSerializer.Serialize((object[])temp);
				return Ok(json);
			}
			else return Ok();
		}
	}
}
