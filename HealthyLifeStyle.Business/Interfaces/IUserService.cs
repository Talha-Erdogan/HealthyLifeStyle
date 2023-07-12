using HealthyLifeStyle.Business.Model;
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
        List<UserWithDetail> GetAllWithDetailForOtherUser();
        List<UserWithDetail> GetAllWithDetailForHospitalUser();
        User GetById(int id);
        User GetByUserNameAndPassword(string userName, string password);
        User GetByUserName(string userName);
        int Add(User user);
        int Update(User user);  
    }
}
