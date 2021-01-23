using LoginData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proje.Filters;
using Proje.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [LoginAtribute]
        public IActionResult Index()
        {
            LoginData.Services.KampBilgiServices kampBilgiServices = new LoginData.Services.KampBilgiServices();
            kampBilgiServices.GetAll(true);

            ViewData["class_1"] = "bg_warning";
            ViewData["class_2"] = "bg_danger";

            return View();
        }

        public IActionResult Hakkimda()
        {
            return View();
        }
        public IActionResult Oneriler()
        {
            return View();
        }
        public IActionResult Iletisim()
        {
            return View();
        }
        
        public IActionResult Login()
        { 
            return View("~/Views/Home/Login.cshtml"); 
        }
        [HttpPost]
        public IActionResult Login(LoginData.Models.User user)
        {
            
            bool SıgnIn = Giris(user);
            if (SıgnIn == true)
            {
                return RedirectToAction("Index", "Home");
            }           
            return View();

        }
        public bool Giris(LoginData.Models.User user) 
        {
            bool result = false;
            LoginData.Services.UserServices servis = new LoginData.Services.UserServices();
           
            var deneme = servis.Login(user.Email, user.Password);
            HttpContext.Session.SetString("Id", deneme.ToString());
            if (deneme>0)
            {
                result = true;
            }
            return result;
        }
        public IActionResult Kayit(int? Id)
        {
            var result = new LoginData.Models.User();
            if (Id.HasValue && Id > 0) 
            {
                LoginData.Services.UserServices services = new LoginData.Services.UserServices();
                result = services.GetById(Id.Value);
            }
            return View(result);
        }           
        [HttpPost]
        public IActionResult Kayit(LoginData.Models.User data)
        {
            LoginData.Services.UserServices services = new LoginData.Services.UserServices();
            var result = services.Save(data);
            if (result>0)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Delete(int userId) 
        {
            if (userId >0)
                {
                LoginData.Services.UserServices services = new LoginData.Services.UserServices();
                var result = services.Delete(userId);
                if ((bool)result) {
                    return RedirectToAction("Login");

                }
            
            }
            return RedirectToAction("Kayit");
        }
        public IActionResult Cikis()
        {
            return View("~/Views/Home/Login.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
