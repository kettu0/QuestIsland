using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStore_C_of_thieves.Models;
using QuestStore_C_of_thieves.Models.Repositories;
using System.Text.Json.Serialization;

namespace QuestStore_C_of_thieves.Controllers
{
    public class QuestController : BaseController
    {
        public static QuestRepository QuestRepository = new QuestRepository();

        private UserRepository _userRepository = new UserRepository();

        public IActionResult Index()
        {
            UpdateControllerProperties("Index");

            QuestRepository.SeedAllQuests();
            return View(QuestRepository);
        }

        public IActionResult Index2()
        {
            UpdateControllerProperties("Index");
            UpdateWalletFromDB(_userRepository.UserDao);
            QuestRepository.SeedAllQuests();
            return View(QuestRepository);
        }

        public IActionResult Details(int id)
        {
            UpdateControllerProperties("Details");
            UpdateWalletFromDB(_userRepository.UserDao);
            QuestRepository.SeedAllQuests();
            var questToCheck = QuestRepository.Entities.FirstOrDefault(s => s.Id == id);
            return View(questToCheck);
        }

        public IActionResult Create()
        {
            UpdateControllerProperties("Create");
            return View();
        }

        public IActionResult Delete(int id)
        {
            UpdateControllerProperties("Delete");

            QuestRepository.SeedAllQuests();
            var questToDelete = QuestRepository.Entities.FirstOrDefault(s => s.Id == id);
            return View(questToDelete);
        }

        public IActionResult Edit(int id)
        {
            UpdateControllerProperties("Edit");

            QuestRepository.SeedAllQuests();
            var questToEdit = QuestRepository.Entities.FirstOrDefault(s => s.Id == id);
            return View(questToEdit);
        }

        public IActionResult GoOnQuest(int id)
        {
            UpdateControllerProperties("Details");
            var codecoolerQuestToAdd = new CodecoolerQuest(id, ActiveUser.UserId);
            QuestRepository.CodecoolerQuestDAO.CreateEntity(codecoolerQuestToAdd);
            return RedirectToAction("MyQuests");
        }

        public IActionResult CompleteQuest(int id)
        {
            UpdateControllerProperties("My Quests");

            var questToComplete = QuestRepository.CodecoolersQuestList.FirstOrDefault(q => q.Id == id);
            questToComplete.IsCompleted = true;
            questToComplete.CompletionDate = DateTime.UtcNow;
            QuestRepository.CodecoolerQuestDAO.UpdateEntity(questToComplete);

            return RedirectToAction("MyQuests");
        }

        public IActionResult MyQuests()
        {
            UpdateControllerProperties("My Quests");

            QuestRepository.SeedCodecoolersQuests(ActiveUser.UserId);

            return View(QuestRepository);
        }

        public IActionResult ApproveQuest(int id)
        {
            UpdateControllerProperties("Pending");

            var questToApprove = QuestRepository.CodecoolersQuestList.FirstOrDefault(q => q.Id == id);
            questToApprove.ApproverId = ActiveUser.UserId;
            QuestRepository.CodecoolerQuestDAO.UpdateEntity(questToApprove);

            int codecoolerIdToUpdate = questToApprove.CodecoolerId;
            int coolpointsToAdd = QuestRepository.QuestDAO.ReadEntityByID(questToApprove.QuestId).CoolpointsValue;
            int experienceToAdd = QuestRepository.QuestDAO.ReadEntityByID(questToApprove.QuestId).ExpValue;

            Codecooler codecoolerData = new Codecooler() { UserId = codecoolerIdToUpdate, CoolpointsWallet = coolpointsToAdd, Exp = experienceToAdd };

            TempData["codecoolerdata"] = JsonSerializer.Serialize(codecoolerData);
            return RedirectToAction("UpdateCodecoolerAfterQuestApproval", "User");
        }

        public IActionResult Pending()
        {
            UpdateControllerProperties("Pending Requests");

            QuestRepository.SeedCodecoolersQuests();

            return View(QuestRepository);
        }


        [HttpPost]
        public IActionResult Create(Quest quest)
        {
            QuestRepository.QuestDAO.CreateEntity(quest);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Quest quest)
        {
            var questToDelete = QuestRepository.Entities.FirstOrDefault(s => s.Id == quest.Id);

            if (questToDelete != null)
            {
                QuestRepository.QuestDAO.DeleteEntity(questToDelete);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Quest quest)
        {
            QuestRepository.QuestDAO.UpdateEntity(quest);
            return RedirectToAction("Index");
        }

        public IActionResult Search(string keyword)
        {
            UpdateControllerProperties("Search");

            if (keyword != null)
            {
                QuestRepository.SeedQuestsByKeyword(keyword.ToLower());
                return View(QuestRepository);
            }

            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Search2(string keyword)
        {
            UpdateControllerProperties("Search");

            if (keyword != null)
            {
                QuestRepository.SeedQuestsByKeyword(keyword.ToLower());
                return View(QuestRepository);
            }

            else
            {
                return RedirectToAction("Index2");
            }
        }


        //public void CheckUser()
        //{
        //    ActiveUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("activeUser"));
        //    if (ActiveUser.UserRole == UserRole.C.ToString())
        //    {
        //        UserWallet = _userRepository.UserDao.GetUserWalletById(ActiveUser.UserId);
        //        HttpContext.Session.SetString("userWallet", UserWallet.ToString());
        //    }
        //    else if (ActiveUser.UserRole == UserRole.M.ToString())
        //    {

        //    }
        //}
    }
}
