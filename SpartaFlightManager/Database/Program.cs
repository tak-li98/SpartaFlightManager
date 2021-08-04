using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Database
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReturnInfoFromFlight();
        }
        public static void ReturnInfoFromFlight()
        {

            using (var db = new SpartaFlightContext())
            {
                var queryDepart =
                    from f in db.Flights
                    join fp in db.FlightPaths on f.FlightId equals fp.FlightId
                    join ap in db.Airports on fp.AirportId equals ap.AirportId
                    join fd in db.FlightDetails on f.FlightId equals fd.FlightId
                    join al in db.Airlines on fd.AirlineId equals al.AirlineId
                    where (fp.IsDepartElseArrival == true)
                    select new
                    {
                        f.FlightId,
                        f.FlightDate,
                        ap.AirportId,
                        ap.City,
                        ap.Country,
                        al.AirlineName,
                        al.AirlineCode
                    };
                var queryArrival =
                    from f in db.Flights
                    join fp in db.FlightPaths on f.FlightId equals fp.FlightId
                    join ap in db.Airports on fp.AirportId equals ap.AirportId
                    where (fp.IsDepartElseArrival == false)
                    select new
                    {
                        ap.AirportId,
                        ap.City,
                        ap.Country,
                    };
                var info = new string[queryDepart.Count(), 10];
                var count = 0;
                foreach (var i in queryDepart)
                {
                    info[count, 0] = i.AirlineCode;
                    info[count, 1] = i.FlightId.ToString();
                    info[count, 2] = i.AirlineName;
                    info[count, 3] = i.City;
                    info[count, 4] = i.Country;
                    info[count, 5] = i.AirportId;
                    info[count, 6] = i.FlightDate.ToString();
                    count++;
                }
                var countArrival = 0;
                foreach (var i in queryArrival)
                {
                    info[countArrival, 7] = i.City;
                    info[countArrival, 8] = i.Country;
                    info[countArrival, 9] = i.AirportId;
                    countArrival++;
                }

                for (int i = 0; i < queryDepart.Count(); i++)
                {
                    Console.WriteLine($"Flight {info[i, 0]}0{info[i, 1]} ({info[i, 2]})" +
                        $"; departing from {info[i, 3]}, {info[i, 4]} ({info[i, 5]}) on {info[i, 6]}" +
                        $" arriving at {info[i, 7]}, {info[i, 8]} ({info[i, 9]}).");
                }
            }
        }
    }
}
