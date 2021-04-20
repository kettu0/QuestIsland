using QuestStore_C_of_thieves.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models.Repositories
{
    public class QuestRepository : IRepository<Quest>
    {
        public List<Quest> Entities { get; set; }
        public Quest Entity { get; set; }
        public List<CodecoolerQuest> CodecoolersQuestList { get; set; }
        public IBaseDAO<Quest> QuestDAO { get; set; }

        public IBaseDAO<CodecoolerQuest> CodecoolerQuestDAO {get; set; }

        public IBaseDAO<User> UserDAO { get; set; }

        public QuestRepository()
        {
            this.QuestDAO = new QuestDAO();
            this.CodecoolerQuestDAO = new CodecoolerQuestDAO();
            this.UserDAO = new UserDAO();
        }

        public void SeedAllQuests()
        {
            this.Entities = QuestDAO.ReadAllEntities();
        }

        public void SeedQuestsByKeyword(string keyword)
        {
            this.Entities = QuestDAO.ReadEntitiesByKeyword(keyword);
        }

        public void SeedQuestById(int id)
        {
            this.Entity = QuestDAO.ReadEntityByID(id);
        }

        public void SeedCodecoolersQuests()
        {
            CodecoolerQuestDAO codecoolersQuestsDAO = (CodecoolerQuestDAO)CodecoolerQuestDAO;
            var allCodecoolersQuests = codecoolersQuestsDAO.ReadAllEntities();

            var allCodecoolersQuestsWithNames = new List<CodecoolerQuest>();

            foreach (var quest in allCodecoolersQuests)
            {
                quest.CodecoolerNames = UserDAO.ReadEntityByID(quest.CodecoolerId).FirstName + " " + UserDAO.ReadEntityByID(quest.CodecoolerId).LastName;
                quest.QuestName = QuestDAO.ReadEntityByID(quest.QuestId).Name;           
                allCodecoolersQuestsWithNames.Add(quest);
            }

            this.CodecoolersQuestList = allCodecoolersQuests;
        }

        public void SeedCodecoolersQuests(int id)
        {
            CodecoolerQuestDAO codecoolersQuestsDAO = (CodecoolerQuestDAO)CodecoolerQuestDAO;
            var allCodecoolersQuests = codecoolersQuestsDAO.ReadEntitiesByCodecoolerID(id);

            var allCodecoolersQuestsWithNames = new List<CodecoolerQuest>();

            foreach (var quest in allCodecoolersQuests)
            {
               quest.QuestName = QuestDAO.ReadEntityByID(quest.QuestId).Name;
                if (quest.ApproverId != null)
                {
                    quest.AprroverNames = UserDAO.ReadEntityByID((int)quest.ApproverId).FirstName + " " + UserDAO.ReadEntityByID((int)quest.ApproverId).LastName;
                }
                allCodecoolersQuestsWithNames.Add(quest);
            }

            this.CodecoolersQuestList = allCodecoolersQuests;

        }
        public bool EntityExists(Quest entity)
        {
            throw new NotImplementedException();
        }
    }
}
