using FlashTyper_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using FlashTyperLibrary.Logic;
using Microsoft.Extensions.Logging;
using System;

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
            UserLogic.AddUser(user.Username, user.Password);
            _logger.LogInformation($"New user '{user.Username}' created at {DateTime.Now}");

            return View("Index");
        }
    }
}
