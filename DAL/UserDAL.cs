using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        public static int AddOrUpdate(Entity.User user)
        {
            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var DataBaseUser = data.Users.FirstOrDefault(x => x.Login == user.Login);
                if (DataBaseUser == null)
                {
                    DataBaseUser = new User();
                    data.Users.Add(DataBaseUser);
                }

                DataBaseUser.UserId = user.UserId;
                DataBaseUser.Login = user.Login;
                DataBaseUser.Password = user.Password;

                data.SaveChanges();

                return DataBaseUser.UserId;
            }
        }
        public static Entity.User Get(int id)
        {
            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var DataBaseUser = data.Users.FirstOrDefault(x => x.UserId == id);
                var user = new Entity.User();

                user.UserId = id;
                user.Login = DataBaseUser.Login;
                user.Password = DataBaseUser.Password;

                return user;
            }
        }
        public static Entity.User Get(string login)
        {
            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var DataBaseUser = data.Users.FirstOrDefault(x => x.Login == login);

                if (DataBaseUser != null)
                {
                    var user = new Entity.User();

                    user.UserId = DataBaseUser.UserId;
                    user.Login = DataBaseUser.Login;
                    user.Password = DataBaseUser.Password;

                    return user;
                }
                return null;

            }
        }
        static public Entity.SearchOut.SearchOutUser Get(Entity.SearchIn.SearchingUser query)
        {
            var result = new Entity.SearchOut.SearchOutUser();

            using (MonsterDungeonContext data = new MonsterDungeonContext())
            {
                var temp = data.Users.Where(x => x.Login.StartsWith(query.Login));
                result.Total = temp.Count();
                if (query.Top.HasValue)
                {
                    temp = temp.Take(query.Top.Value);
                }
                if (query.Skip.HasValue)
                {
                    temp = temp.Skip(query.Skip.Value);
                }
                result.Users = temp.Select(x => new Entity.User()
                {
                    UserId = x.UserId,
                    Login = x.Login,
                    Password = x.Password,
                }).ToList();
            }

            return result;
        }
    }
}
