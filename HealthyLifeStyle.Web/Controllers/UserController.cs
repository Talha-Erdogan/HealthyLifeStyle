using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Web.Models.Common;
using HealthyLifeStyle.Web.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthyLifeStyle.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByUserNameAndPassword(model.UserName, model.Password);
                if (user == null)
                {
                    ViewBag.Error = "Kullanıcı bulunamadı.";
                    return View(model);
                }

                //todo :  session ekleme işlemleri yapılacak.
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_UserId")))
                {
                    HttpContext.Session.SetString("HealthyLifeStyle_User_Id", user.Id.ToString());
                    HttpContext.Session.SetString("HealthyLifeStyle_User_Name", user.Name);
                    HttpContext.Session.SetString("HealthyLifeStyle_User_LastName", user.LastName);
                    HttpContext.Session.SetInt32("HealthyLifeStyle_User_BloodGroupId", user.BloodGroupId);
                    HttpContext.Session.SetString("HealthyLifeStyle_User_UserName", user.UserName);
                    HttpContext.Session.SetInt32("HealthyLifeStyle_User_UserType", user.UserType);
                    if (user.HospitalId !=null)
                    {
                        HttpContext.Session.SetString("HealthyLifeStyle_User_HospitalId", user.HospitalId.Value.ToString());
                    }
                    else
                    {
                        HttpContext.Session.SetString("HealthyLifeStyle_User_HospitalId", "");
                    }
                }

                if (user.UserType == (int)UserType.Admin) 
                {
                    return RedirectToAction("List", "Hospital"); // admin profili
                }
                else if (user.UserType == (int)UserType.User)
                {
                    return RedirectToAction("Index", "Home"); // kullanıcı profili
                }
                else if (user.UserType == (int)UserType.Hospital)
                {
                    return RedirectToAction("List", "NeedForBlood"); // hastane kullanıcı profili
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Bilgileri kontrol ediniz.";
                return View(model);
            }
        }

        [AllowAnonymous]
        public IActionResult NotAuthorized()
        {
            
            return View();
        }

    }
}
