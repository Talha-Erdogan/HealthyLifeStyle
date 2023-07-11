using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Types.Entity
{
    public class NeedForBlood
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BloodGroupId { get; set; }

        [Required]
        public int HospitalId { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
