using System.ComponentModel.DataAnnotations;

namespace HealthyLifeStyle.Web.Models.User
{
    public class ListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
        
        public string GSM { get; set; }

        public string UserName { get; set; }

        public int UserType { get; set; }


        public string BloodGroup_Name { get; set; }
        public string Hospital_Name { get; set; }
    }
}
