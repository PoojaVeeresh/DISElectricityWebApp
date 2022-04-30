using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectricityWebApp.Models
{
    public class datasets
    {
        public string label { get; set; }
        public IList<int> data = new List<int>();
        public string backgroundColor { get; set; }
    }

    public class data
    {
        public data()
        {
            xLabels.Add("x1");
            xLabels.Add("xT");
            xLabels.Add("xW");
            xLabels.Add("xT");
            xLabels.Add("xF");
            xLabels.Add("xS");
            xLabels.Add("xS");
        }

        public IList<string> xLabels = new List<string>();
        public IList<datasets> datasets = new List<datasets>();
    }
}
