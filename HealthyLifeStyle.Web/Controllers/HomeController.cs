using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Services;
using HealthyLifeStyle.Web.Models;
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
            var result = _bloodGroupService.GetAll();
            return View(result);
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