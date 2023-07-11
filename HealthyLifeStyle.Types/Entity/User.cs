using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Types.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string LastName { get; set; }

        [Required]
        public int BloodGroupId { get; set; }
     
        [StringLength(50)]
        public string GSM { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        public int UserType { get; set; }

        public int? HospitalId { get; set; }

    }
}
