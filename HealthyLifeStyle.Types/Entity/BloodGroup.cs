﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Types.Entity
{
    public class BloodGroup
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    
    }
}
