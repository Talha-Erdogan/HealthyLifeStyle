﻿using System.ComponentModel.DataAnnotations;

namespace HealthyLifeStyle.Web.Models.Hospital
{
    public class ListViewModel
    {

        public int Id { get; set; }
       
        public string Name { get; set; }
        
        public string Phone { get; set; }
       
        public string Address { get; set; }
    }
}
