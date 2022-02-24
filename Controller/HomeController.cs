using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rand_Pass.Models;
using Microsoft.AspNetCore.Http;

namespace Rand_Pass.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public static int count = 1;
        public static string Passcode = "0123456789ABcdeFGHijKLMNopqRsTUVWxyZ";

        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("number") == null)
            {
                HttpContext.Session.SetInt32("number", count);
            }
            ViewBag.count = HttpContext.Session.GetInt32("number");
            Random rand = new Random();
            string RandomPassword ="";
            for(int i = 0; i < 14; i++)
            {
                var newCode = Passcode[rand.Next(0, Passcode.Length)];
                RandomPassword += newCode;
                
            }
            ViewBag.RandPass = RandomPassword;
            return View();
        }

        [HttpPost("generateNew")]
        public IActionResult GenerateNew()
        {
            int? count = HttpContext.Session.GetInt32("number");
            HttpContext.Session.SetInt32("number", (int)++count);
            return RedirectToAction("Index");
        }
    }
}
