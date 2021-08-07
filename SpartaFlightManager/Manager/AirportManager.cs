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
        public List<AirportRegion> ReturnAirportAndRegion()
        {
            var list = new List<AirportRegion>();
            using (var db = new SpartaFlightContext())
            {
                var query =
                    from a in db.Airports
                    join r in db.Regions on a.RegionId equals r.RegionId
                    orderby r.RegionName
                    select new
                    {
                        a.AirportId,
                        a.City,
                        a.Country,
                        r.RegionName
                    };
                foreach (var item in query)
                {
                    AirportRegion airportRegion = new AirportRegion();
                    airportRegion.AirportID = item.AirportId;
                    airportRegion.City = item.City;
                    airportRegion.Country = item.Country;
                    airportRegion.RegionName = item.RegionName;
                    list.Add(airportRegion);
                }
                return list;
            }
        }

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
        public string ReturnAirportIdGivenAirportStr(string airportStr)
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Airports.Where(a => airportStr.Contains(a.AirportId)&&airportStr.Contains(a.Country) && airportStr.Contains(a.City)).FirstOrDefault().AirportId;
            }
        }
        public string ReturnRegionGivenFlightId(int flightId)
        {
            using (var db = new SpartaFlightContext())
            {
                var airportId = db.FlightPaths.Where(fp => fp.FlightId == flightId && fp.IsDepartElseArrival == true).FirstOrDefault();
                var regionId = db.Airports.Where(a => a.AirportId == airportId.AirportId).FirstOrDefault();
                return db.Regions.Where(r => r.RegionId == regionId.RegionId).FirstOrDefault().RegionName;
            }
        }

        
    }
}
