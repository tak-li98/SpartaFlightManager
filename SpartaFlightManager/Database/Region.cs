using System;
using System.Collections.Generic;

#nullable disable

namespace Database
{
    public partial class Region
    {
        public Region()
        {
            Airlines = new HashSet<Airline>();
            Airports = new HashSet<Airport>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Airline> Airlines { get; set; }
        public virtual ICollection<Airport> Airports { get; set; }
    }
}
