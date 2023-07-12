using HealthyLifeStyle.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Model
{
    public  class NeedForBloodWithDetail : NeedForBlood
    {
        public string BloodGroup_Name { get; set; }
    }
}
