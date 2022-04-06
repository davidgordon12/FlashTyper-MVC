using FlashTyper_MVC.Models;
using FlashTyperLibrary.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlashTyper_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Leaderboards()
        {
            //Get the current top 5 users with highest WPM
            List<FlashTyperLibrary.Model.UserModel> leaderboard = UserLogic.GetLeaderboard();

            return View(leaderboard);
        }

        public IActionResult Account()
        {
            try
            {
                var user = JsonConvert.DeserializeObject<FlashTyper_MVC.Models.UserModel>(HttpContext.Session.GetString("UserSession"));

                FlashTyperLibrary.Model.UserModel userModel = new() { Username = user.Username, WPM = UserLogic.GetWPM(user.Username) };

                ViewBag.Username = user.Username;

                return View("Profile", userModel);
            }
            catch(ArgumentNullException)
            {
                return View("Account");
            }
        }

        public IActionResult Submit()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
