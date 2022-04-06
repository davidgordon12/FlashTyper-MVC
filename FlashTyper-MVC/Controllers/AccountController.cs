using FlashTyper_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using FlashTyperLibrary.Logic;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FlashTyper_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Account(UserModel user)
        {
            if (UserLogic.AddUser(user.Username, user.Password))
            {
                ViewBag.errorMessage = null;
                _logger.LogInformation("New user '{0}' created at {1}", user.Username, DateTime.Now);

                return View("Index");
            }
            else
            {
                ViewBag.signupError = "This username is already taken";

                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            if (UserLogic.ValidLogin(user.Username, user.Password))
            {
                _logger.LogInformation("User '{0}' logged in at {1}", user.Username, DateTime.Now);

                HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(user));

                return View("Index");
            }
            else
            {
                ViewBag.loginError = "Incorrect username and/or password";

                return View("Account");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View("Index");
        }
    }
}
