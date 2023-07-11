using Microsoft.AspNetCore.Mvc;

namespace HealthyLifeStyle.Web.Controllers
{
    public class HospitalController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
