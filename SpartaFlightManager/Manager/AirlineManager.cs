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
    }
}
