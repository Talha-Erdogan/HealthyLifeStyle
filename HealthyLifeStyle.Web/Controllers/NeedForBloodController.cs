using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Services;
using HealthyLifeStyle.Types.Entity;
using HealthyLifeStyle.Web.Models.Common;
using HealthyLifeStyle.Web.Models.NeedForBlood;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthyLifeStyle.Web.Controllers
{
    public class NeedForBloodController : Controller
    {
        private readonly INeedForBloodService _needForBloodService;
        private readonly IBloodGroupService _bloodGroupService;

        public NeedForBloodController(INeedForBloodService needForBloodService, IBloodGroupService bloodGroupService)
        {
            _needForBloodService = needForBloodService;
            _bloodGroupService = bloodGroupService;
        }
        public IActionResult List()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_User_Id")))
            {
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.Hospital)
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            var hospitalId = HttpContext.Session.GetString("HealthyLifeStyle_User_HospitalId");
            if (string.IsNullOrEmpty(hospitalId))
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            List<ListViewModel> model = new List<ListViewModel>();

            var needForBloodList = _needForBloodService.GetAllWithDetailByHospitalId(Convert.ToInt32(hospitalId));
            if (needForBloodList != null)
            {
                foreach (var item in needForBloodList)
                {
                    ListViewModel needForBlood = new ListViewModel()
                    {
                        Id = item.Id,
                        BloodGroup_Name = item.BloodGroup_Name,
                        Count = item.Count,
                        HospitalId = item.HospitalId
                    };
                    model.Add(needForBlood);
                }
            }
            return View(model);
        }

        public IActionResult Add()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_User_Id")))
            {
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.Hospital)
            {
                return RedirectToAction("NotAuthorized", "User");
            }
            var hospitalId = HttpContext.Session.GetString("HealthyLifeStyle_User_HospitalId");
            if (string.IsNullOrEmpty(hospitalId))
            {
                return RedirectToAction("NotAuthorized", "User");
            }

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
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            var hospitalId = HttpContext.Session.GetString("HealthyLifeStyle_User_HospitalId");
            if (string.IsNullOrEmpty(hospitalId))
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            var isExist = _needForBloodService.GetByHospitalIdAndBloodGroupId(Convert.ToInt32(hospitalId), model.BloodGroupId);
            if (isExist !=null)
            {
                ViewBag.ErrorMessage = "Kayıt zaten mevcut.Lütfen mevcut kaydın sayısını güncelleyiniz.";
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                return View(model);
            }

            NeedForBlood needForBlood = new NeedForBlood()
            {
                HospitalId = Convert.ToInt32(hospitalId),
                BloodGroupId = model.BloodGroupId,
                Count = model.Count
            };

            try
            {
                _needForBloodService.Add(needForBlood);
                return RedirectToAction(nameof(NeedForBloodController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }


        public IActionResult Edit(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_User_Id")))
            {
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.Hospital)
            {
                return RedirectToAction("NotAuthorized", "User");
            }
            var hospitalId = HttpContext.Session.GetString("HealthyLifeStyle_User_HospitalId");
            if (string.IsNullOrEmpty(hospitalId))
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            AddViewModel model = new AddViewModel();
            model.BloodGroupSelectList = GetBloodGroupSelectList();

            try
            {
                var result = _needForBloodService.GetById(id);
                if (result == null)
                {
                    return View("_ErrorNotExist");
                }
                model.Id = result.Id;
                model.BloodGroupId = result.BloodGroupId;
                model.Count = result.Count;
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Record Not Found";
                return View("_ErrorNotExist");
            }
        }

        [HttpPost]
        public IActionResult Edit(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.BloodGroupSelectList = GetBloodGroupSelectList();
                return View(model);
            }
            model.BloodGroupSelectList = GetBloodGroupSelectList();
            var hospitalId = HttpContext.Session.GetString("HealthyLifeStyle_User_HospitalId");
            if (string.IsNullOrEmpty(hospitalId))
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            var needForBlood = _needForBloodService.GetById(model.Id);
            if (needForBlood == null)
            {
                return View("_ErrorNotExist");
            }

            needForBlood.Count = model.Count;
            
            try
            {
                _needForBloodService.Update(needForBlood);
                return RedirectToAction(nameof(NeedForBloodController.List));
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
    }
}
