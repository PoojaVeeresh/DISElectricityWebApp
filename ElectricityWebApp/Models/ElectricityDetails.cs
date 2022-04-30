using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectricityWebApp.Models
{
    public class ElectricityDetails
    {
        [Key]
        public Guid ID { get; set; }
        public string CountryID { get; set; }
        public string Year { get; set; }
        public int UnitsConsumption { get; set; }
       
    }

}
