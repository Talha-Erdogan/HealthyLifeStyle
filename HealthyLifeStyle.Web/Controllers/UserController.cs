using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Web.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
                    HttpContext.Session.SetString("HealthyLifeStyle_UserId", user.Id.ToString());
                    HttpContext.Session.SetString("HealthyLifeStyle_UserName", user.Name.ToString());
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Bilgileri kontrol ediniz.";
                return View(model);
            }
        }

    }
}
