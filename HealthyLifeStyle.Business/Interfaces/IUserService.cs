using HealthyLifeStyle.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthyLifeStyle.Business.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        User GetByUserNameAndPassword(string userName, string password);
        int Add(User user);
        int Update(User user);  
    }
}
