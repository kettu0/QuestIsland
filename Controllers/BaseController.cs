using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestStore_C_of_thieves.DAO;
using QuestStore_C_of_thieves.Models;
using QuestStore_C_of_thieves.Models.Repositories;

namespace QuestStore_C_of_thieves.Controllers
{
    public abstract class BaseController : Controller
    {
        [ViewData]
        public string Title { get; set; }
        [ViewData]
        public User ActiveUser { get; set; }

        [ViewData] public int UserWallet { get; set; }

        internal void UpdateControllerProperties(string title)
        {
            Title = title;
            ActiveUser = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("activeUser"));
            var _userWallet = HttpContext.Session.GetString("userWallet");
            if (ActiveUser.UserRole == UserRole.C.ToString() && _userWallet != null)
            {
                UserWallet = int.Parse(_userWallet);
            }
            else
            {
                UserWallet = 0;
            }
        }

        internal void UpdateWalletFromDB(UserDAO userDao)
        {
            if (ActiveUser.UserRole == UserRole.C.ToString())
            {
                UserWallet = userDao.GetUserWalletById(ActiveUser.UserId);
                HttpContext.Session.SetString("userWallet", UserWallet.ToString());
            }
            else
            {
                UserWallet = 0;
            }
        }
    }
}
