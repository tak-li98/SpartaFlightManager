using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class Pilot
    {
        public Pilot()
        {
            FlightDetails = new HashSet<FlightDetail>();
        }

        public int PilotId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoLink { get; set; }

        public virtual ICollection<FlightDetail> FlightDetails { get; set; }
    }
}
