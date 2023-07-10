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
    public class BloodGroupService: IBloodGroupService
    {
        private readonly HealthyLifeStyleDbContext _dbContext;

        public BloodGroupService(HealthyLifeStyleDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<BloodGroup> GetAll()
        {
            return _dbContext.BloodGroup.ToList();
        }
    }
}
