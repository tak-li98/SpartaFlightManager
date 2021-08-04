using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class Airport
    {
        public Airport()
        {
            FlightPaths = new HashSet<FlightPath>();
        }

        public string AirportId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<FlightPath> FlightPaths { get; set; }
    }
}
