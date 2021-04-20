using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuestStore_C_of_thieves.Models;
using QuestStore_C_of_thieves.Models.Repositories;

namespace QuestStore_C_of_thieves.Controllers
{
    public class UserController : BaseController
    {
        private UserViewModelRepo userViewModelRepo = new UserViewModelRepo();
        public UserViewModel UserViewModelObj { get; set; } = new UserViewModel();

        public IActionResult Index()
        {
            UpdateControllerProperties("Index");
            UserViewModelObj.UsersList = userViewModelRepo.UserDao.ReadAllEntities();
            UserViewModelObj.CodecoolersList = userViewModelRepo.CodecoolerDao.ReadAllEntities();
            UserViewModelObj.MentorsList = userViewModelRepo.MentorDao.ReadAllEntities();
            return View(UserViewModelObj);
        }

        [Route("/User/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            UpdateControllerProperties("Details");
            PrepareViewForUserId(id);
            return View(UserViewModelObj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            UpdateControllerProperties("Create");
            UpdateWalletFromDB(userViewModelRepo.UserDao);

            return View();
        }
        [HttpPost]
        public IActionResult Create(UserViewModel userViewModel)
        {
            var _user = userViewModel.LoadedUser;
            _user.DateOfRegistration = DateTime.Now;
            _user.Password = _user.Email;
            userViewModelRepo.UserDao.CreateEntity(_user);
            var _createdUserEmail = _user.Email;
            userViewModel.LoadedUser = userViewModelRepo.UserDao.ReadEntitiesByStringPair("email", _createdUserEmail)[0];
            if (userViewModel.LoadedUser.UserRole == "M")
            {
                userViewModel.LoadedMentor.UserId = userViewModel.LoadedUser.UserId;
                userViewModelRepo.MentorDao.CreateEntity(userViewModel.LoadedMentor);
            }
            else if (userViewModel.LoadedUser.UserRole == "C")
            {
                userViewModel.LoadedCodecooler.UserId = userViewModel.LoadedUser.UserId;
                userViewModelRepo.CodecoolerDao.CreateEntity(userViewModel.LoadedCodecooler);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/User/Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            UpdateControllerProperties("Edit User");
            UpdateWalletFromDB(userViewModelRepo.UserDao);

            PrepareViewForUserId(id);
            return View(UserViewModelObj);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            //Need data validation
            var _editedUser = userViewModel.LoadedUser;
            var _id = _editedUser.UserId;
            if (_editedUser.UserRole == UserRole.C.ToString())
            {
                userViewModel.LoadedCodecooler.UserId = _id;
                var _codecooler =
                    userViewModelRepo.CodecoolerDao.ReadEntitiesByStringIntPair("user_id", _id)[0];
                userViewModel.LoadedCodecooler.ClassId = _codecooler.ClassId;
                userViewModel.LoadedCodecooler.TeamId = _codecooler.TeamId;
                userViewModel.LoadedCodecooler .CodecoolerId = _codecooler.CodecoolerId;
                userViewModelRepo.CodecoolerDao.UpdateEntity(userViewModel.LoadedCodecooler);
            }
            else if (_editedUser.UserRole == UserRole.M.ToString())
            {
                userViewModel.LoadedMentor.UserId = _id;
                userViewModel.LoadedMentor.MentorId =
                    userViewModelRepo.MentorDao.ReadEntitiesByStringIntPair("user_id", _id)[0].MentorId;
                userViewModelRepo.MentorDao.UpdateEntity(userViewModel.LoadedMentor);
            }
            userViewModelRepo.UserDao.UpdateEntity(userViewModel.LoadedUser);

            return RedirectToAction("Details", new {id = _id }); //$"User/{{{id}}}");
        }
        [HttpGet]
        [Route("/User/Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            UpdateControllerProperties("Delete User");
            UpdateWalletFromDB(userViewModelRepo.UserDao);

            var _user = UserViewModelObj.LoadedUser = userViewModelRepo.UserDao.ReadEntityByID(id);
            if (_user == null)
            {
                return RedirectToAction("Index");
            }
            if (_user.UserRole == UserRole.M.ToString())
            {
                UserViewModelObj.LoadedMentor = userViewModelRepo.MentorDao.ReadEntitiesByStringIntPair("user_id", id)[0];
            }
            else if (_user.UserRole == UserRole.C.ToString())
            {
                UserViewModelObj.LoadedCodecooler = userViewModelRepo.CodecoolerDao.ReadEntitiesByStringIntPair("user_id", id)[0];
            }

            return View(UserViewModelObj);
        }

        [HttpPost]
        [Route("/User/Delete/id:int")]
        public IActionResult DeleteUser(int id)
        {
            UpdateControllerProperties("Delete");
            UpdateWalletFromDB(userViewModelRepo.UserDao);
            var _user = userViewModelRepo.UserDao.ReadEntityByID(id);

            var _deleted = false;
            if (_user.UserRole == UserRole.M.ToString())
            {
                userViewModelRepo.MentorDao.DeleteEntitiesByStringIntPair("user_id", _user.UserId);
                _deleted = true;
            }
            else if (_user.UserRole == UserRole.C.ToString())
            {
                userViewModelRepo.CodecoolerDao.DeleteEntitiesByStringIntPair("user_id", _user.UserId);
                _deleted = true;
            }
            if (_deleted)
            {
                userViewModelRepo.UserDao.DeleteEntityById(_user.UserId);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCodecoolerAfterQuestApproval()
        {
            Codecooler codecoolerData = JsonSerializer.Deserialize<Codecooler>(TempData["codecoolerdata"].ToString());
            Codecooler codecoolerToUpdate = userViewModelRepo.CodecoolerDao.ReadEntityByUserID(codecoolerData.UserId);
            codecoolerToUpdate.CoolpointsWallet += codecoolerData.CoolpointsWallet;
            codecoolerToUpdate.Exp += codecoolerData.Exp;
            userViewModelRepo.CodecoolerDao.UpdateEntity(codecoolerToUpdate);

            return RedirectToAction("Pending", "Quest");
        }


        public IActionResult CheckWallet()
        {
            Codecooler codecollerData = JsonSerializer.Deserialize<Codecooler>(TempData["codecoolerData"].ToString());

            Codecooler codecoolerToUpdate = userViewModelRepo.CodecoolerDao.ReadEntityByUserID(codecollerData.UserId);

            if (codecoolerToUpdate.CoolpointsWallet >= codecollerData.CoolpointsWallet)
            {
                codecoolerToUpdate.CoolpointsWallet -= codecollerData.CoolpointsWallet;
                userViewModelRepo.CodecoolerDao.UpdateEntity(codecoolerToUpdate);
            
                return RedirectToAction("BuyArtifact", "Artifact");
            }
            
            else
            {
                return RedirectToAction("NotEnoughCC", "Artifact");
            }
        }

        private void PrepareViewForUserId(int id)
        {
            var _user = userViewModelRepo.UserDao.ReadEntityByID(id);
            UserViewModelObj.LoadedUser = _user;
            if (_user.UserRole == UserRole.M.ToString())
            {
                UserViewModelObj.LoadedMentor =
                    userViewModelRepo.MentorDao.ReadEntitiesByStringIntPair("user_id", _user.UserId)[0];
            }
            else if (_user.UserRole == UserRole.C.ToString())
            {
                UserViewModelObj.LoadedCodecooler =
                    userViewModelRepo.CodecoolerDao.ReadEntitiesByStringIntPair("user_id", _user.UserId)[0];
            }
        }

        [HttpGet]
        public IActionResult Password(int id)
        {
            UpdateControllerProperties("Password");
            var userToEdit = userViewModelRepo.UserDao.ReadEntityByID(id);
            return View(userToEdit);
            //UpdateControllerProperties("Password");
            //var userToEdit = userViewModelRepo.UserDao.ReadEntityByID(id);

            //PrepareViewForUserId(id);
            //return View(userToEdit);
        }

        [HttpPost]
        public IActionResult Password(User userToUpdate)
        {

            UpdateControllerProperties("Password");
            userViewModelRepo.UserDao.UpdatePassword(userToUpdate);
            return RedirectToAction("Password");

        }

        //UpdateControllerProperties("Use Artifact");
        //var artifactToUse = ArtifactRepository.CodecoolersArtifactsList.FirstOrDefault(a => a.Id == id);
        //artifactToUse.IsUsed = true;
        //    ArtifactRepository.CodecoolerArtifactDAO.UpdateEntity(artifactToUse);
        //    return RedirectToAction("MyArtifacts");

        //ArtifactRepository.ArtifactDAO.UpdateEntity(artifact);
        //    return RedirectToAction("Index2");
    }
}
