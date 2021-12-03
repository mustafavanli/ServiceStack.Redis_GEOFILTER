using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStack.Redis_GEOFILTER.Models
{
    public class GeoResult
    {
        public string Member { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public long Hash { get; set; }
        public string Unit { get; set; }
        public double Distance { get; set; }

        // Additional info
        public string EndUpdate { get; set; }
    }
}
