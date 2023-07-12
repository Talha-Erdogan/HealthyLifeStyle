using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Web.Models.Common;
using HealthyLifeStyle.Web.Models.Hospital;
using Microsoft.AspNetCore.Mvc;

namespace HealthyLifeStyle.Web.Controllers
{
    public class HospitalController : Controller
    {

        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }
        public IActionResult List()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("HealthyLifeStyle_User_Id")))
            {
                return RedirectToAction("Login", "User");
            }
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.Admin)
            {
                return RedirectToAction("NotAuthorized", "User");
            }

            List<ListViewModel> model =  new List<ListViewModel>();
           
            try
            {
                var hospitalList = _hospitalService.GetAll();
                if (hospitalList !=null && hospitalList.Count>0)
                {
                    foreach (var item in hospitalList)
                    {
                        ListViewModel hospital = new ListViewModel()
                        {
                            Address = item.Address,
                            Id = item.Id,
                            Name = item.Name,
                            Phone = item.Phone
                        };
                        model.Add(hospital);
                    }
                }
            }
            catch
            {
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        public IActionResult Add()
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
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HealthyLifeStyle.Types.Entity.Hospital hospital = new Types.Entity.Hospital ();
            hospital.Name = model.Name; 
            hospital.Phone = model.Phone;
            hospital.Address = model.Address;

            try
            {
                _hospitalService.Add(hospital);
                return RedirectToAction(nameof(HospitalController.List));
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
            if (HttpContext.Session.GetInt32("HealthyLifeStyle_User_UserType") != (int)UserType.Admin)
            {
                return RedirectToAction("NotAuthorized", "User");
            }
            AddViewModel model = new AddViewModel();
            try
            {
                var result = _hospitalService.GetById(id);
                if (result == null)
                {
                    return View("_ErrorNotExist");
                }
                model.Id = result.Id;
                model.Name = result.Name;
                model.Phone = result.Phone;
                model.Address = result.Address;
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
                return View(model);
            }

            try
            {
                var hospital = _hospitalService.GetById(model.Id);
                if (hospital == null)
                {
                    return View("_ErrorNotExist");
                }

                hospital.Name = model.Name; 
                hospital.Phone = model.Phone;
                hospital.Address = model.Address;
               
                _hospitalService.Update(hospital);
                return RedirectToAction(nameof(HospitalController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }
    }
}
