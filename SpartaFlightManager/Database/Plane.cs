using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class Plane
    {
        public Plane()
        {
            FlightDetails = new HashSet<FlightDetail>();
        }

        public int PlaneId { get; set; }
        public string PlaneModel { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<FlightDetail> FlightDetails { get; set; }
    }
}
