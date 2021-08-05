using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Manager
{
    public class AirportManager
    {
        public List<Airport> ReturnAirports()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Airports.ToList();
            }
        }
    }
}
