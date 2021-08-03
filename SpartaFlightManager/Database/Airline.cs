using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class Airline
    {
        public Airline()
        {
            FlightDetails = new HashSet<FlightDetail>();
        }

        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public int RegionId { get; set; }
        public string AirlineCode { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<FlightDetail> FlightDetails { get; set; }
    }
}
