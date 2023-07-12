﻿using HealthyLifeStyle.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace HealthyLifeStyle.Web.Controllers
{
    public class HospitalController : Controller
    {
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





            return View();
        }
    }
}
