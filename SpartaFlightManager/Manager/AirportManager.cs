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
        public List<Airport> RetrieveAll()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Airports.ToList();
            }
        }
        public string ReturnRegion(string airportStr)
        {
            using (var db = new SpartaFlightContext())
            {
                var regionId = db.Airports.Where(a => airportStr.Contains(a.AirportId) && airportStr.Contains(a.City)).FirstOrDefault().RegionId;
                return db.Regions.Where(r => r.RegionId == regionId).FirstOrDefault().RegionName;
            }
        }
    }
}
