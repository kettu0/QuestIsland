using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuestStore_C_of_thieves.DAO;

namespace QuestStore_C_of_thieves.Models.Repositories
{
    public class UserViewModelRepo : IRepository<User>
    {
        public List<User> Entities { get; set; }
        public UserDAO UserDao;
        public MentorDAO MentorDao;
        public CodecoolerDAO CodecoolerDao;

        public bool EntityExists(User entity)
        {
            throw new NotImplementedException();
        }

        public UserViewModelRepo()
        {
            this.UserDao = new UserDAO();
            this.MentorDao = new MentorDAO();
            this.CodecoolerDao = new CodecoolerDAO();
        }
    }
}
