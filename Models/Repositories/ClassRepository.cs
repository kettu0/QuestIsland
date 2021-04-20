using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Emit;
using QuestStore_C_of_thieves.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestStore_C_of_thieves.Models.Repositories
{
    public class ClassRepository : IRepository<Class>
    {
        public List<Class> Entities { get; set; }

        public ClassDAO ClassDAO = new ClassDAO();
        public Class Entity { get; set; }
        public List<Codecooler> Codecoolers { get; set; }
        public List<User> UsersCodecoolers { get; set; }

        public List<User> UsersMentors { get; set; }
        public List<Mentor> Mentors { get; set; }
        public List<int> IdNumbers { get; set; }


        public void SeedAllClasses()
        {
            this.Entities = ClassDAO.ReadAllEntities();
        }

        public void SeedClassByName (string className)
        {
            this.Entity = ClassDAO.ReadClassByName(className);
        }

        public void SeedClassById (int id)
        {
            this.Entity = ClassDAO.ReadClassById(id);
        }
        public void SeedCodecoolersOfClass(Class pickedClass)
        {
            this.Codecoolers = ClassDAO.ReadAllCodecoolersFromClass(pickedClass);
            this.UsersCodecoolers = ClassDAO.ReadUsersFromList(Codecoolers);
        }

        public void SeedMentorsByClassId(int id)
        {
            this.IdNumbers = ClassDAO.ReadMentorsIdByClassId(id);
            this.IdNumbers = ClassDAO.ReadUsersIdByMentorsId(IdNumbers);
            this.UsersMentors = ClassDAO.ReadUsersById(IdNumbers);
        }

        public bool EntityExists(Class entity)
        {
            throw new NotImplementedException();
        }

        public void CreateNewClass(Class newClass)
        {
            ClassDAO.CreateEntity(newClass);
        }

        public int GetClassId(Class newClass)
        {
            return ClassDAO.ReadClassId(newClass);
        }

        public int GetMentorId(int userId)
        {
            return ClassDAO.ReadMentorIdByUserId(userId);
        }

        public void AssignMentorsToClass(int mentorId, int newClassId,  DateTime assignmentDate)
        {
            ClassDAO.AssignMentorsToClass(mentorId, newClassId, assignmentDate);
        }
    }
}
