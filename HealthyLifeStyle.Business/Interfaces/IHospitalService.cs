using HealthyLifeStyle.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Interfaces
{
    public interface IHospitalService
    {
        List<Hospital> GetAll();
        Hospital GetById(int id);
        int Add(Hospital hospital);
        int Update(Hospital hospital);
    }
}
