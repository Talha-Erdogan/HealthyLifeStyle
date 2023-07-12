using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Services;
using HealthyLifeStyle.Types.Entity;
using HealthyLifeStyle.Web.Models;
using HealthyLifeStyle.Web.Models.Common;
using HealthyLifeStyle.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthyLifeStyle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly INeedForBloodService _needForBloodService;

        public HomeController(ILogger<HomeController> logger, IBloodGroupService bloodGroupService, INeedForBloodService needForBloodService)
        {
            _logger = logger;
            _bloodGroupService = bloodGroupService;
            _needForBloodService = needForBloodService;
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
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_BloodGroupId") == null || HttpContext.Session.GetInt32("HealthyLifeStyle_User_BloodGroupId") ==0)
            {
                return RedirectToAction("NotAuthorized", "User");
            }
            List<ListViewModel> model = new List<ListViewModel>();
            try
            {
                var currentUserBloodGroupId = HttpContext.Session.GetInt32("HealthyLifeStyle_User_BloodGroupId");
                var needForBloodList = _needForBloodService.GetHospitalListByBloodId(currentUserBloodGroupId.Value);
                if (needForBloodList != null)
                {
                    foreach (var item in needForBloodList)
                    {
                        ListViewModel needForBlood = new ListViewModel()
                        {
                            Address = item.Address,
                            Name = item.Name,
                            Phone = item.Phone
                        };
                        model.Add(needForBlood);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
                            
            return View(model);
        }

        
    }
}