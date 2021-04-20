using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestStore_C_of_thieves.Models;
using QuestStore_C_of_thieves.Models.Repositories;


namespace QuestStore_C_of_thieves.Controllers
{
    public class ArtifactController : BaseController
    {
        public static ArtifactsRepository ArtifactRepository = new ArtifactsRepository();
        private UserRepository _userRepository = new UserRepository();

        public IActionResult Index(string sortOrder)
        {
            UpdateControllerProperties("Booties to purchase!");
            UpdateWalletFromDB(_userRepository.UserDao);

            ViewBag.PriceSortParm = sortOrder == "Price_asc" ? "Price_desc" : "Price_asc";
            ViewBag.NameSortParm = sortOrder == "Name_asc" ? "Name_desc" : "Name_asc";


            switch (sortOrder)
            {
                case "Name_desc":
                    ArtifactRepository.SeedArtifactsbyName("DESC");
                    break;
                case "Name_asc":
                    ArtifactRepository.SeedArtifactsbyName("ASC");
                    break;
                case "Price_desc":
                    ArtifactRepository.SeedArtifactByPrice("DESC");
                    break;
                case "Price_asc":
                    ArtifactRepository.SeedArtifactByPrice("ASC");
                    break;
                default:
                    ArtifactRepository.SeedArtifacts();
                    break;
            }

            return View(ArtifactRepository);
        }

        public IActionResult Index2()
        {
            UpdateControllerProperties("Booties for purchase!");
            UpdateWalletFromDB(_userRepository.UserDao);

            ArtifactRepository.SeedArtifacts();
            return View(ArtifactRepository);
        }

        public IActionResult Details(int ID)
        {
            UpdateControllerProperties("Details");
            UpdateWalletFromDB(_userRepository.UserDao);
            ArtifactRepository.SeedArtifacts();
            var artifactToDisplay = ArtifactRepository.Entities.FirstOrDefault(a => a.Id == ID);
            return View(artifactToDisplay);
        }


        public IActionResult MyArtifacts()
        {
            UpdateControllerProperties("My Artifacts");
            ArtifactRepository.SeedCodecoolerArtifactsById(ActiveUser.UserId);

            return View(ArtifactRepository);

        }

        public IActionResult CheckArtifactValue(int id)
        {
            UpdateControllerProperties("CheckArtifactValue");
            var artifactToBuy = ArtifactRepository.Entities.FirstOrDefault(a => a.Id == id);
            int artifactValue = ArtifactRepository.ArtifactDAO.ReadEntityByID(artifactToBuy.Id).Price;

            Codecooler codecoolerData = new Codecooler() { UserId = ActiveUser.UserId, CoolpointsWallet = artifactValue };
            Artifact artifactData = new Artifact() { Id = artifactToBuy.Id };
            TempData["codecoolerData"] = JsonSerializer.Serialize(codecoolerData);
            TempData["artifactData"] = JsonSerializer.Serialize(artifactData);

            return RedirectToAction("CheckWallet", "User");
        }

        public IActionResult BuyArtifact()
        {
            UpdateControllerProperties("Buy Artifact");
            UpdateWalletFromDB(_userRepository.UserDao);
            Artifact artifactData = JsonSerializer.Deserialize<Artifact>(TempData["artifactData"].ToString());
            var artifactToUpdate = ArtifactRepository.Entities.FirstOrDefault(a => a.Id == artifactData.Id);
            artifactToUpdate.Amount -= 1;
            ArtifactRepository.ArtifactDAO.UpdateEntity(artifactToUpdate);
            var artifactToBuy = new CodecoolerArtifact(artifactData.Id, ActiveUser.UserId, DateTime.UtcNow, false);
            ArtifactRepository.CodecoolerArtifactDAO.CreateEntity(artifactToBuy);
            return RedirectToAction("MyArtifacts");
        }

        public IActionResult NotEnoughCC()
        {
            UpdateControllerProperties("NotEnoughCC");
            return View();
        }

        public IActionResult UseArtifact(int id)
        {
            UpdateControllerProperties("Use Artifact");
            var artifactToUse = ArtifactRepository.CodecoolersArtifactsList.FirstOrDefault(a => a.Id == id);
            artifactToUse.IsUsed = true;
            ArtifactRepository.CodecoolerArtifactDAO.UpdateEntity(artifactToUse);
            return RedirectToAction("MyArtifacts");
        }


        [HttpGet]
        public IActionResult Create()
        {
            UpdateControllerProperties("Create an artifact");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Artifact artifact)
        {
            ArtifactRepository.ArtifactDAO.CreateEntity(artifact);
            return RedirectToAction("Index2");
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            ArtifactRepository.SeedArtifacts();
            var artifactToEdit = ArtifactRepository.Entities.FirstOrDefault(a => a.Id == ID);

            UpdateControllerProperties("Edit the artifact");

            return View(artifactToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Artifact artifact)
        {
            ArtifactRepository.ArtifactDAO.UpdateEntity(artifact);
            return RedirectToAction("Index2");
        }

        [HttpGet]
        public IActionResult Delete(int ID)
        {
            ArtifactRepository.SeedArtifacts();
            var artifactToDelete = ArtifactRepository.Entities.FirstOrDefault(a => a.Id == ID);
            UpdateControllerProperties(artifactToDelete.Name);

            return View(artifactToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Artifact artifact)
        {
            var artifactToDelete = ArtifactRepository.Entities.FirstOrDefault(a => a.Id == artifact.Id);
            if (artifactToDelete != null)
            {
                ArtifactRepository.ArtifactDAO.DeleteEntity(artifactToDelete);
            }

            return RedirectToAction("Index2");
        }


        public IActionResult Search(string keyword)
        {
            UpdateControllerProperties("Search results");

            ArtifactRepository.SeedArtifactsByKeyword(keyword);
            return View(ArtifactRepository);
        }

        public IActionResult Search2(string keyword)
        {
            UpdateControllerProperties("Search result");

            ArtifactRepository.SeedArtifactsByKeyword(keyword);
            return View(ArtifactRepository);
        }

        [HttpGet]
        public IActionResult DisplayBasic(string sortOrder)
        {
            UpdateControllerProperties("Booties to purchase!");
            ViewBag.PriceSortParm = sortOrder == "Price_asc" ? "Price_desc" : "Price_asc";
            ViewBag.NameSortParm = sortOrder == "Name_asc" ? "Name_desc" : "Name_asc";


            switch (sortOrder)
            {
                case "Name_desc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("basic", "artifact_name", "DESC");
                    break;
                case "Name_asc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("basic", "artifact_name", "ASC");
                    break;
                case "Price_desc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("basic", "price", "DESC");
                    break;
                case "Price_asc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("basic", "price", "ASC");
                    break;
                default:
                    ArtifactRepository.SeedArtifactsByType("basic");
                    break;
            }

            return View(ArtifactRepository);
        }

        [HttpGet]
        public IActionResult DisplayMagic(string sortOrder)
        {
            UpdateControllerProperties("Booties to purchase!");
            ViewBag.PriceSortParm = sortOrder == "Price_asc" ? "Price_desc" : "Price_asc";
            ViewBag.NameSortParm = sortOrder == "Name_asc" ? "Name_desc" : "Name_asc";

            switch (sortOrder)
            {
                case "Name_desc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("magic", "artifact_name", "DESC");
                    break;
                case "Name_asc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("magic", "artifact_name", "ASC");
                    break;
                case "Price_desc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("magic", "price", "DESC");
                    break;
                case "Price_asc":
                    ArtifactRepository.SeedArtifactsByTypeAndSort("magic", "price", "ASC");
                    break;
                default:
                    ArtifactRepository.SeedArtifactsByType("magic");
                    break;
            }
            return View(ArtifactRepository);
        }
    }
}
