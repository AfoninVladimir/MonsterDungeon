using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SessionDAL
    {
        public static int AddOrUpdate(Entity.Session session)
        {
            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var DaraBaseSession = data.Sessions.FirstOrDefault(x => x.SessionId == session.SessionId);

                if (DaraBaseSession == null)
                {
                    DaraBaseSession = new Session();
                    data.Sessions.Add(DaraBaseSession);
                }

                DaraBaseSession.SessionId = session.SessionId;
                DaraBaseSession.GameId = session.GameId;
                DaraBaseSession.UserId = session.UserId;
                
                data.SaveChanges();

                return DaraBaseSession.SessionId;
            }
        }
        public static Entity.Session Get(int id)
        {
            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var DataBaseSession = data.Sessions.FirstOrDefault(x => x.SessionId == id);
                var session = new Entity.Session();

                session.SessionId = id;
                session.GameId = DataBaseSession.GameId;
                session.UserId = DataBaseSession.UserId;

                return session;
            }
        }
        static public Entity.SearchOut.SearchOutSession Get(Entity.SearchIn.SearchingSession query)
        {
            var result = new Entity.SearchOut.SearchOutSession();

            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var temp = data.Sessions.Where(x => x.GameId.Equals(query.Game)); // ?
                result.Total = temp.Count();
                if (query.Top.HasValue)
                {
                    temp = temp.Take(query.Top.Value);
                }
                if (query.Skip.HasValue)
                {
                    temp = temp.Skip(query.Skip.Value);
                }
                result.Sessions = temp.Select(x => new Entity.Session()
                {
                    SessionId = x.SessionId,
                    GameId = x.GameId,
                    UserId = x.UserId,
                }).ToList();
            }

            return result;
        }
    }
}
