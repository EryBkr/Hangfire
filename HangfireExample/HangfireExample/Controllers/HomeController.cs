using HangfireExample.BackgroundJobs;
using HangfireExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public IActionResult PictureSave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PictureSave(IFormFile image)
        {
            string newFileName = String.Empty;

            if (image!=null && image.Length>0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newFileName);

                using (var stream=new FileStream(path,FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                //Hangfire Delayed Job Execute
                string jobID = BackgroundJobs.DelayedJob.AddWaterMarkJob(newFileName, "Eray Bakır");

                //Continuations Job Execute
                //Resim işlemi Delayed Job ile oluştuktan sonra ondan geri dönen Job id yi ContinuationsJob a veriyorum ve hemen ardından çalışmasını bekliyorum
                BackgroundJobs.ContinuationsJob.WriteWatermarkInformation(jobID, newFileName);
            }
            return RedirectToAction("Index");
        }
    }
}
