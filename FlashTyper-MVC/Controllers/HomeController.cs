using FlashTyper_MVC.Models;
using FlashTyperLibrary.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            HttpContext.Session.GetString("UserSession");

            return View("Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
