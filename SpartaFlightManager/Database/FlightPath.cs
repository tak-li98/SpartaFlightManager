using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class FlightPath
    {
        public int FlightId { get; set; }
        public string AirportId { get; set; }
        public bool IsDepartElseArrival { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
