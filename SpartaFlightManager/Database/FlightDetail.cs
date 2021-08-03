using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class FlightDetail
    {
        public int FlightDetailsId { get; set; }
        public int FlightId { get; set; }
        public int? PilotId { get; set; }
        public int AirlineId { get; set; }
        public int? PlaneId { get; set; }
        public int? PassengerNumber { get; set; }
        public int? FlightDuration { get; set; }
        public bool? Archive { get; set; }

        public virtual Airline Airline { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Pilot Pilot { get; set; }
        public virtual Plane Plane { get; set; }
    }
}
