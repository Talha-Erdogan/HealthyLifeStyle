using HealthyLifeStyle.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Model
{
    public class UserWithDetail: User
    {
        public string BloodGroup_Name { get; set; }
        public string Hospital_Name { get; set; }
    }
}
