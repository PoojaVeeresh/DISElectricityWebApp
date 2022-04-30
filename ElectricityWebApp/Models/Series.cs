using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectricityWebApp.Models
{
    public class Series
    {
        [Key]
        public Guid ID { get; set; }

        public string series_id { get; set; }
        public string name { get; set; }
        public string units { get; set; }
        public string f { get; set; }
        public string description { get; set; }
        public string source { get; set; }
        public string iso3166 { get; set; }
        public string latlon { get; set; }
        public string latlon2 { get; set; }
        public string geography { get; set; }
        public string geography2 { get; set; }
        public string lastHistoricalPeriod { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string updated { get; set; }

    }

}
