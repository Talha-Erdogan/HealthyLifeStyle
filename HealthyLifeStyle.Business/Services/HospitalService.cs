using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Types.Data;
using HealthyLifeStyle.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly HealthyLifeStyleDbContext _dbContext;

        public HospitalService(HealthyLifeStyleDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<Hospital> GetAll()
        {
            return _dbContext.Hospital.ToList();
        }

        public Hospital GetById(int id)
        {
            return _dbContext.Hospital.FirstOrDefault(x => x.Id == id);
        }

        public int Add(Hospital hospital)
        {
             _dbContext.Hospital.Add(hospital);
             return _dbContext.SaveChanges();
        }

        public int Update(Hospital hospital)
        {
            _dbContext.Hospital.Update(hospital);
            return _dbContext.SaveChanges();
        }

    }
}
