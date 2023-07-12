using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Model;
using HealthyLifeStyle.Types.Data;
using HealthyLifeStyle.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Services
{
    public class NeedForBloodService : INeedForBloodService
    {
        private readonly HealthyLifeStyleDbContext _dbContext;

        public NeedForBloodService(HealthyLifeStyleDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<NeedForBlood> GetAll()
        {
            return _dbContext.NeedForBlood.ToList();
        }
        public List<NeedForBloodWithDetail> GetAllWithDetailByHospitalId(int hospitalId)
        {
            var query = from needForBlood in _dbContext.Set<NeedForBlood>()
                        join bloodGroup in _dbContext.Set<BloodGroup>()
                            on needForBlood.BloodGroupId equals bloodGroup.Id
                        where needForBlood.HospitalId == hospitalId
                        select new NeedForBloodWithDetail()
                        {
                            BloodGroupId =needForBlood.BloodGroupId,
                            HospitalId = needForBlood.HospitalId,
                            Id = needForBlood.Id,
                            Count = needForBlood.Count,
                            BloodGroup_Name = bloodGroup.Name
                        };
            return query.ToList();

            //return _dbContext.NeedForBlood.Where(x=>x.HospitalId == hospitalId).ToList();
        }

        public NeedForBlood GetById(int id)
        {
            return _dbContext.NeedForBlood.FirstOrDefault(x => x.Id == id);
        }

        public NeedForBlood GetByHospitalIdAndBloodGroupId(int hospitalId, int bloodGroupId)
        {
            return _dbContext.NeedForBlood.FirstOrDefault(x => x.HospitalId == hospitalId && x.BloodGroupId == bloodGroupId);
        }

        public int Add(NeedForBlood needForBlood)
        {
            _dbContext.NeedForBlood.Add(needForBlood);
            return _dbContext.SaveChanges();
        }

        public int Update(NeedForBlood needForBlood)
        {
            _dbContext.NeedForBlood.Update(needForBlood);
            return _dbContext.SaveChanges();
        }

        public List<Hospital> GetHospitalListByBloodId(int bloodId)
        {
            var query = from hospital in _dbContext.Set<Hospital>()
                        join needForBlood in _dbContext.Set<NeedForBlood>()
                            on hospital.Id equals needForBlood.HospitalId
                        where needForBlood.BloodGroupId == bloodId
                        select hospital;
            return query.ToList();
        }
    }
}
