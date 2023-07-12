using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Services;
using HealthyLifeStyle.Types.Entity;
using HealthyLifeStyle.Web.Models.Common;
using HealthyLifeStyle.Web.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthyLifeStyle.Web.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly IHospitalService _hospitalService;

        public UserController(IUserService userService, IBloodGroupService bloodGroupService, IHospitalService hospitalService)
        {
            _userService = userService;
            _bloodGroupService = bloodGroupService;
            _hospitalService = hospitalService;
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
                    if (user.HospitalId != null)
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

        [AllowAnonymous]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                return View(model);
            }
            var existUser = _userService.GetByUserName(model.UserName);
            if (existUser != null)
            {
                ViewBag.ErrorMessage = "Mevcut UserName. Farklı bir username girebilir misiniz ?";
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                return View(model);
            }
            model.BloodGroupSelectList = GetBloodGroupSelectList();

            HealthyLifeStyle.Types.Entity.User user = new Types.Entity.User()
            {
                Name = model.Name,
                LastName = model.LastName,
                BloodGroupId = model.BloodGroupId,
                GSM = model.GSM,
                UserName = model.UserName,
                Password = model.Password,
                UserType = (int)UserType.User
            };

            try
            {
                _userService.Add(user);
                return RedirectToAction(nameof(UserController.Login));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }


        public IActionResult ListForHospitalUser()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_User_Id")))
            {
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.Admin)
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            List<ListViewModel> model = new List<ListViewModel>();

            try
            {
                var userList = _userService.GetAllWithDetailForHospitalUser();
                if (userList != null && userList.Count > 0)
                {
                    foreach (var item in userList)
                    {
                        ListViewModel user = new ListViewModel()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            UserType = item.UserType,
                            Hospital_Name = item.Hospital_Name,
                            BloodGroup_Name = item.BloodGroup_Name,
                            GSM = item.GSM,
                            LastName = item.LastName,
                            UserName = item.UserName
                        };
                        model.Add(user);
                    }
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        public IActionResult AddForHospitalUser()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_User_Id")))
            {
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.Admin)
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            AddViewModel model = new AddViewModel();
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            model.HospitalSelectList = GetHospitalSelectList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddForHospitalUser(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                model.HospitalSelectList = GetHospitalSelectList();
                return View(model);
            }
            if (model.HospitalId == null || model.HospitalId == 0)
            {
                ViewBag.ErrorMessage = "Hastane Seçilmelidir.";
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                model.HospitalSelectList = GetHospitalSelectList();
                return View(model);
            }
            var existUser = _userService.GetByUserName(model.UserName);
            if (existUser !=null)
            {
                ViewBag.ErrorMessage = "Mevcut UserName. Farklı bir username girebilir misiniz ?";
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                model.HospitalSelectList = GetHospitalSelectList();
                return View(model);
            }
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            model.HospitalSelectList = GetHospitalSelectList();

            HealthyLifeStyle.Types.Entity.User user = new Types.Entity.User()
            {
                Name = model.Name,
                LastName = model.LastName,
                BloodGroupId = model.BloodGroupId,
                GSM = model.GSM,
                UserName = model.UserName,
                Password = model.Password,
                UserType = (int)UserType.Hospital,
                HospitalId = model.HospitalId,
            };

            try
            {
                _userService.Add(user);
                return RedirectToAction(nameof(UserController.ListForHospitalUser));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }



        [NonAction]
        private List<SelectListItem> GetBloodGroupSelectList()
        {
            // blood group select list 
            List<SelectListItem> resultList = new List<SelectListItem>();
            try
            {
                resultList = _bloodGroupService.GetAll().OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            }
            catch
            {
                resultList = new List<SelectListItem>();
            }
            return resultList;
        }

        [NonAction]
        private List<SelectListItem> GetHospitalSelectList()
        {
            // blood group select list 
            List<SelectListItem> resultList = new List<SelectListItem>();
            try
            {
                resultList = _hospitalService.GetAll().OrderBy(r => r.Name).Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }).ToList();
            }
            catch
            {
                resultList = new List<SelectListItem>();
            }
            return resultList;
        }
    }
}
