using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class FlightStatus
    {
        public FlightStatus()
        {
            Flights = new HashSet<Flight>();
        }

        public int FlightStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
