using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStore_C_of_thieves.DAO;
using QuestStore_C_of_thieves.Models;
using QuestStore_C_of_thieves.Models.Repositories;

namespace QuestStore_C_of_thieves.Controllers
{
    public class GroupController : BaseController
    {
        public static ClassRepository ClassRepository = new ClassRepository();
        public static TeamRepository TeamRepository = new TeamRepository();
        public static Team TempTeam { get; set; }
        public static Class TempClass { get; set; }
        public static List<int> IdNumbersOfMentors { get; set; }
        public UserDAO UserDAO = new UserDAO();
        public IActionResult Classes()
        {
            UpdateControllerProperties("Classes");

            ClassRepository.SeedAllClasses();
            return View(ClassRepository);
        }


        public IActionResult ClassDetails(int id)
        {
            UpdateControllerProperties("ClassDetails");

            ClassRepository.SeedClassById(id);
            ClassRepository.SeedCodecoolersOfClass(ClassRepository.Entity);
            ClassRepository.SeedMentorsByClassId(id);
            return View(ClassRepository);
        }

        public IActionResult DeleteTeam(int id)
        {
            UpdateControllerProperties("Delete");

            var teamToDelete = TeamRepository.Entities.FirstOrDefault(a => a.TeamId == id);
            if (teamToDelete == null)
            {
                TeamRepository.SeedAllTeams();
                return RedirectToAction("Index", "Home");
            }
            TempTeam = teamToDelete;
            return View(teamToDelete);
        }

        [HttpPost]
        public IActionResult DeleteTeam()
        {
            var teamToDelete = TempTeam;
            if (teamToDelete != null)
            {
                TeamRepository.TeamDao.DeleteEntity(teamToDelete);
            }

            return RedirectToAction("Teams");
        }

        public IActionResult DeleteClass(int id)
        {
            UpdateControllerProperties("Delete");

            var classToDelete = ClassRepository.Entities.FirstOrDefault(a => a.Id == id);
            if (classToDelete == null)
            {
                ClassRepository.SeedAllClasses();
                return RedirectToAction("Index", "Home");
            }
            TempClass = classToDelete;
            return View(classToDelete);
        }

        [HttpPost]
        public IActionResult DeleteClass()
        {
            var classToDelete = TempClass;
            if (classToDelete != null)
            {
                ClassRepository.ClassDAO.DeleteEntity(classToDelete);
            }

            return RedirectToAction("Classes");
        }


        public IActionResult Teams()
        {
            UpdateControllerProperties("Teams");

            TeamRepository.SeedAllTeams();
            return View(TeamRepository);
        }

        public IActionResult TeamDetails(int id)
        {
            UpdateControllerProperties("Details");
            TeamRepository.SeedTeamById(id);
            TeamRepository.SeedUsersById(id);
            TeamRepository.SeedQuestsById(id);
            return View(TeamRepository);
        }

        [HttpGet]
        public IActionResult CreateClass()
        {
            UpdateControllerProperties("CreateClass");

            var mentors = UserDAO.ReadAllMentors();
            var viewModel = new NewClassViewModel { Mentors = mentors };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateClass(NewClassViewModel newClass)
        {
            UpdateControllerProperties("CreateClass");

            var newClassName = newClass.NewClassName;
            newClass.NewClass.ClassName = newClassName;

            var startDate = DateTime.Now;
            newClass.NewClass.StartDate = startDate;

            ClassRepository.CreateNewClass(newClass.NewClass);
            int newClassId = ClassRepository.GetClassId(newClass.NewClass);



            foreach (var mentor in newClass.Mentors)
            {
                if (mentor.IsAssigned.Equals(true))
                {
                    int mentorId = ClassRepository.GetMentorId(mentor.UserId);
                    ClassRepository.AssignMentorsToClass(mentorId, newClassId, startDate);
                }
            }

            return (RedirectToAction("Classes"));
        }

    }
}
