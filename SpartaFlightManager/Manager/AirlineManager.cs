using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Manager
{
    public class AirlineManager
    {
        public List<Airline> RetrieveAll()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Airlines.ToList();
            }
        }

        public int ReturnAirlineID(string str)
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Airlines.Where(a => a.AirlineName == str).FirstOrDefault().AirlineId;
            }
        }

        public List<string> ReturnAirlinesGivenRegion(string airportStr)
        {
            using (var db = new SpartaFlightContext())
            {
                var regionId = db.Airports.Where(a => airportStr.Contains(a.AirportId) && airportStr.Contains(a.City)).FirstOrDefault().RegionId;
                return db.Airlines.Where(a => a.RegionId == regionId).Select(i => i.AirlineName).ToList();
            }
        }

        public List<AirlineRegion> ReturnAirlineAndRegion()
        {
            var list = new List<AirlineRegion>();
            using (var db = new SpartaFlightContext())
            {
                
                var query =
                    from a in db.Airlines
                    join r in db.Regions on a.RegionId equals r.RegionId
                    select new
                    {
                        a.AirlineName,
                        a.AirlineCode,
                        r.RegionName
                    };
                foreach (var item in query)
                {
                    AirlineRegion airlineRegion = new AirlineRegion();
                    airlineRegion.AirlineName = item.AirlineName;
                    airlineRegion.AirlineCode = item.AirlineCode;
                    airlineRegion.RegionName = item.RegionName;
                    list.Add(airlineRegion);
                }
                return list;
            }
        }

    }
}
