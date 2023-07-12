using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HealthyLifeStyle.Web.Models.User
{
    public class AddViewModel
    {
   
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int BloodGroupId { get; set; }
   
        public string GSM { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public int? HospitalId { get; set; }

        //select list
        public List<SelectListItem>? BloodGroupSelectList { get; set; }
        public List<SelectListItem>? HospitalSelectList { get; set; }
    }
}
