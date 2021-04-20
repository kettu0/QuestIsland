using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestStore_C_of_thieves.DAO;

namespace QuestStore_C_of_thieves.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public List<User> Entities { get; set; }
        public UserDAO UserDao;

        public bool EntityExists(User entity)
        {
            throw new NotImplementedException();
        }

        public UserRepository()
        {
            this.UserDao = new UserDAO();
        }
    }
}
