using FlashTyper_MVC.Models;
using FlashTyperLibrary.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlashTyper_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
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

                var stats = UserLogic.GetStats(user.Username);

                FlashTyperLibrary.Model.UserModel userModel = new() { Username = stats.Username, WPM = stats.WPM, Accuracy = stats.Accuracy};

                ViewBag.Username = user.Username;

                return View("Profile", userModel);
            }
            catch(ArgumentNullException)
            {
                return View("Account");
            }
        }

        [HttpPost]
        public IActionResult Submit(GameModel gameModel)
        {
            int wpm = GameLogic.CalculateWPM(gameModel.Input);
            float acc = GameLogic.CalculateAccuracy(gameModel.Input.ToLower(), gameModel.Words.ToLower());
            ViewBag.WPM = wpm;
            ViewBag.ACC = acc;

            try
            {
                var user = JsonConvert.DeserializeObject<FlashTyper_MVC.Models.UserModel>(HttpContext.Session.GetString("UserSession"));

                GameLogic.UpdateStats(user.Username, wpm, acc);

                return View("Score", new FlashTyperLibrary.Model.UserModel { Username = user.Username, WPM = wpm, Accuracy = acc });
            }
            catch(ArgumentNullException)
            {
                return View("Score", new FlashTyperLibrary.Model.UserModel { Username = "Login to save", WPM = wpm, Accuracy = acc });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
