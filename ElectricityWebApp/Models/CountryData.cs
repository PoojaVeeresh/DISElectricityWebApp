using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectricityWebApp.Models
{
    public class CountryData
    {
        [Key]
        public Guid ID { get; set; }

        public string CountryName { get; set; }

        
    }

}
