using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class UserBL
    {
        public static int AddOrUpdate(Entity.User user)
        {
            return DAL.UserDAL.AddOrUpdate(user);
        }
        public static bool Authorization(Entity.User user)
        {
            var temp = DAL.UserDAL.Get(user.Login);
            if (temp == null)
            {
                return false;
            }
            return temp.Password == user.Password;
        }
        public static Entity.User Get(int id)
        {
            return DAL.UserDAL.Get(id);
        }
        public static Entity.User Get(string login)
        {
            return DAL.UserDAL.Get(login);
        }
        static public Entity.SearchOut.SearchOutUser Get(Entity.SearchIn.SearchingUser query)
        {
            return DAL.UserDAL.Get(query);
        }

    }
}
