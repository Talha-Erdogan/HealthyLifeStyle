using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Services;
using HealthyLifeStyle.Types.Entity;
using HealthyLifeStyle.Web.Models;
using HealthyLifeStyle.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthyLifeStyle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBloodGroupService _bloodGroupService;

        public HomeController(ILogger<HomeController> logger, IBloodGroupService bloodGroupService)
        {
            _logger = logger;
            _bloodGroupService = bloodGroupService;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetString("HealthyLifeStyle_User_Id");
            var userType = HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_User_Id")))
            {
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.User)
            {
                return RedirectToAction("NotAuthorized", "User");
            }
                            
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
    }
}