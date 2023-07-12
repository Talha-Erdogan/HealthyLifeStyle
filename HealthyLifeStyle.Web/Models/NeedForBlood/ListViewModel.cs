using System.ComponentModel.DataAnnotations;

namespace HealthyLifeStyle.Web.Models.NeedForBlood
{
    public class ListViewModel
    {
        public int Id { get; set; }
      
        public string BloodGroup_Name { get; set; }
       
        public int HospitalId { get; set; }
       
        public int Count { get; set; }
    }
}
