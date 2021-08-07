using HangfireExample.BackgroundJobs;
using HangfireExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireExample.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Gerçek anlamda bir kullanıcı kaydı yapmayacağız.Job u denemek için oluşturduk
        public IActionResult RegisterUser()
        {
            //Hemen çalışmaya başlayacaktır
            FireAndForgetJob.EmailSendToUserJob("5","Test Mesajı");

            return RedirectToAction("Index");
        }
    }
}
