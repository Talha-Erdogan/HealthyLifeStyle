using HealthyLifeStyle.Business.Model;
using HealthyLifeStyle.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Interfaces
{
    public interface INeedForBloodService
    {
        List<NeedForBlood> GetAll();
        List<NeedForBloodWithDetail> GetAllWithDetailByHospitalId(int hospitalId);
        NeedForBlood GetById(int id);
        NeedForBlood GetByHospitalIdAndBloodGroupId(int hospitalId, int bloodGroupId);
        int Add(NeedForBlood needForBlood);
        int Update(NeedForBlood needForBlood);
        List<Hospital> GetHospitalListByBloodId(int bloodId);
    }
}
