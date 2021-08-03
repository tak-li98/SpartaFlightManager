using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class Flight
    {
        public Flight()
        {
            FlightDetails = new HashSet<FlightDetail>();
        }

        public int FlightId { get; set; }
        public int FlightStatusId { get; set; }
        public DateTime FlightDate { get; set; }

        public virtual FlightStatus FlightStatus { get; set; }
        public virtual ICollection<FlightDetail> FlightDetails { get; set; }
    }
}
