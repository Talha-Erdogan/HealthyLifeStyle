using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Types.Data;
using HealthyLifeStyle.Types.Entity;
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


       

        public User GetById(int id)
        {
            return _dbContext.User.FirstOrDefault(x => x.Id == id);
        }

        public User GetByUserNameAndPassword(string userName, string password)
        {
            return _dbContext.User.FirstOrDefault(x => x.UserName == userName && x.Password == password);
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
