using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class SessionBL
    {
        public static int AddOrUpdate(Entity.Session session)
        {
            return DAL.SessionDAL.AddOrUpdate(session);
        }

        public static Entity.Session Get(int id)
        {
            return DAL.SessionDAL.Get(id);
        }
        static public Entity.SearchOut.SearchOutSession Get(Entity.SearchIn.SearchingSession query)
        {
            return DAL.SessionDAL.Get(query);
        }
    }
}
