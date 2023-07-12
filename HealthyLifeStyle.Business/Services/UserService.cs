using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Business.Model;
using HealthyLifeStyle.Types.Data;
using HealthyLifeStyle.Types.Entity;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Services
{
    public class UserService : IUserService
    {
        private readonly HealthyLifeStyleDbContext _dbContext;

        public UserService(HealthyLifeStyleDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<User> GetAll()
        {
            return _dbContext.User.ToList();
        }

        public List<UserWithDetail> GetAllWithDetailForOtherUser()
        {
            //return _dbContext.User.ToList();
            var query = from user in _dbContext.Set<User>()
                        join bloodGroup in _dbContext.Set<BloodGroup>()
                            on user.BloodGroupId equals bloodGroup.Id
                        join hospital in _dbContext.Set<Hospital>()
                        on user.HospitalId equals hospital.Id
                        where user.HospitalId == null && user.UserType != 1
                        select new UserWithDetail()
                        {
                           Id = user.Id,
                           Name = user.Name,
                           LastName = user.LastName,
                           BloodGroupId = user.BloodGroupId,
                           GSM = user.GSM,
                           UserName = user.UserName,
                           Password = user.Password,
                           UserType = user.UserType,
                           HospitalId = user.HospitalId,

                           BloodGroup_Name = bloodGroup.Name,
                           Hospital_Name = hospital.Name
                        };
            return query.ToList();

        }

        public List<UserWithDetail> GetAllWithDetailForHospitalUser()
        {
            //return _dbContext.User.ToList();
            var query = from user in _dbContext.Set<User>()
                        join bloodGroup in _dbContext.Set<BloodGroup>()
                            on user.BloodGroupId equals bloodGroup.Id
                        join hospital in _dbContext.Set<Hospital>()
                        on user.HospitalId equals hospital.Id
                        where user.HospitalId != null
                        select new UserWithDetail()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            LastName = user.LastName,
                            BloodGroupId = user.BloodGroupId,
                            GSM = user.GSM,
                            UserName = user.UserName,
                            Password = user.Password,
                            UserType = user.UserType,
                            HospitalId = user.HospitalId,

                            BloodGroup_Name = bloodGroup.Name,
                            Hospital_Name = hospital.Name
                        };
            return query.ToList();

        }



        public User GetById(int id)
        {
            return _dbContext.User.FirstOrDefault(x => x.Id == id);
        }

        public User GetByUserNameAndPassword(string userName, string password)
        {
            return _dbContext.User.FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }

        public User GetByUserName(string userName)
        {
            return _dbContext.User.FirstOrDefault(x => x.UserName == userName );
        }

        public int Add(User user)
        {
            _dbContext.User.Add(user);
            return _dbContext.SaveChanges();
        }

        public int Update(User user)
        {
            _dbContext.User.Update(user);
            return _dbContext.SaveChanges();
        }
    }
}
