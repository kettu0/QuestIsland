using QuestStore_C_of_thieves.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        public List<Team> Entities { get; set; }
        public Team Entity { get; set; }

        public TeamDAO TeamDao = new TeamDAO();

        public List<int> IdNumbers { get; set; }
        public List<User> Users { get; set; }
        public List<Quest> Quests { get; set; }


        public bool EntityExists(Team entity)
        {
            throw new NotImplementedException();
        }

        public void SeedAllTeams()
        {
            this.Entities = TeamDao.ReadAllEntities();
        }

        public void SeedUsersById(int id)
        {
            this.IdNumbers = TeamDao.ReadUsersIdByTeamId(id);
            this.Users = TeamDao.ReadUsersByUserId(IdNumbers);
        }

        public void SeedQuestsById(int id)
        {
            this.IdNumbers = TeamDao.ReadQuestsIdByTeamId(id);
            this.Quests = TeamDao.ReadQuestsById(IdNumbers);
        }

        public void SeedTeamById(int id)
        {
            this.Entity = TeamDao.ReadEntityByID(id);
        }
    }
}
