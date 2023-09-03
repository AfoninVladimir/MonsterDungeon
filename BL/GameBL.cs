using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class GameBL
    {
        public static int AddOrUpdate(Entity.Game game)
        {
            return DAL.GameDAL.AddOrUpdate(game);
        }
        public static Entity.Game Get(int id)
        {
            return DAL.GameDAL.Get(id);
        }
        static public Entity.SearchOut.SearchOutGame Get(Entity.SearchIn.SearchingGame query)
        {
            return DAL.GameDAL.Get(query);
        }
    }
}
