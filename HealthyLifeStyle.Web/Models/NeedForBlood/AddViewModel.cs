using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HealthyLifeStyle.Web.Models.NeedForBlood
{
    public class AddViewModel
    {
        public int Id { get; set; }

        [Required]
        public int BloodGroupId { get; set; }

        [Required]
        public int Count { get; set; }

        //select list
        public List<SelectListItem>? BloodGroupSelectList { get; set; }
    }
}
