using System.ComponentModel.DataAnnotations;

namespace HealthyLifeStyle.Web.Models.User
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
