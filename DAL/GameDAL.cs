using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GameDAL
    {
        public static int AddOrUpdate(Entity.Game game)
        {
            using (MonsterDungeonContext data  = new MonsterDungeonContext())
            {
                var DataBaseGame = data.Games.FirstOrDefault(x => x.GameId == game.GameId);

                if (DataBaseGame == null)
                {
                    DataBaseGame = new Game();
                    data.Games.Add(DataBaseGame);
                }

                DataBaseGame.GameId = game.GameId;
                DataBaseGame.Host = game.Host;
                DataBaseGame.Time = game.Time;

                data.SaveChanges();
                
                return DataBaseGame.GameId;
            }
        }
        public static Entity.Game Get(int id)
        {
            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var DataBaseGame = data.Games.FirstOrDefault(x => x.GameId == id);
                var game = new Entity.Game();
                game.GameId = id;
                game.Host = DataBaseGame.Host;
                game.Time = DataBaseGame.Time;

                return game;
            }
        }
        static public Entity.SearchOut.SearchOutGame Get(Entity.SearchIn.SearchingGame query)
        {
            var result = new Entity.SearchOut.SearchOutGame();

            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var temp = data.Games.Where(x => !query.Host.HasValue || x.Host.Equals(query.Host)); // ?
                result.Total = temp.Count();
                if (query.Top.HasValue)
                {
                    temp = temp.Take(query.Top.Value);
                }
                if (query.Skip.HasValue)
                {
                    temp = temp.Skip(query.Skip.Value);
                }
                result.Games = temp.Select(x => new Entity.Game()
                {
                    GameId = x.GameId,
                    Time = x.Time,
                    Host = x.Host,
                }).ToList();
            }

            return result;
        }
    }
}
