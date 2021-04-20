using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestStore_C_of_thieves.DAO;
using QuestStore_C_of_thieves.Models;
using QuestStore_C_of_thieves.Models.Repositories;

namespace QuestStore_C_of_thieves.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserRepository _userRepository = new UserRepository();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("/Index")]
        public IActionResult Index()

        {
            UpdateControllerProperties("Frontpage");
            UpdateWalletFromDB(_userRepository.UserDao);

            return View();
        }
        public IActionResult About()
        {
            UpdateControllerProperties("About");
            UpdateWalletFromDB(_userRepository.UserDao);

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string message, string nickname, string email)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            Title = "Login";
            return View();
        }

        [HttpPost]
        public IActionResult Login(User foreignUser)
        {
            if(_userRepository.UserDao.LoginUser(foreignUser).Equals(true))
            {
                var loggedUser = _userRepository.UserDao.NewUser;
                    if(loggedUser.Password == foreignUser.Password)
                    {
                        loggedUser.Password = "";
                        ActiveUser = loggedUser;
                        string userAsJson = JsonSerializer.Serialize(loggedUser);
                        HttpContext.Session.SetString("activeUser", userAsJson);
                        return RedirectToAction("Index");
                    }
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");

        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            return View();
        }

        public IActionResult Logout()
        {
            Title = "Logout";
            ActiveUser = null;
            HttpContext.Session.Remove("activeUser");
            return View();
        }
    }

}
