using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using System.Diagnostics;

namespace Manager
{
    public enum Status
    {
        SCHEDULED = 1, DELAYED = 2, DEPARTED = 3, ARRIVED = 4, CANCELLED = 5
    }
    public class FlightManager
    {
        public int SelectedFlight { get; set; }

        public void SetSelectedFlight(int selectedItem)
        {
            SelectedFlight = selectedItem;
        }

        public List<Flight> RetrieveAll()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Flights.ToList();
            }
        }

        public int ReturnStatusId(string statusStr)
        {
            using (var db = new SpartaFlightContext())
            {
                return db.FlightStatuses.Where(s => s.Status == statusStr).FirstOrDefault().FlightStatusId;
            }
        }

        public string[,] ReturnFlightBoardInfoFromFlights()
        {
            using (var db = new SpartaFlightContext())
            {
                var queryDepart =
                    from f in db.Flights
                    join fs in db.FlightStatuses on f.FlightStatusId equals fs.FlightStatusId
                    join fp in db.FlightPaths on f.FlightId equals fp.FlightId
                    join ap in db.Airports on fp.AirportId equals ap.AirportId
                    join fd in db.FlightDetails on f.FlightId equals fd.FlightId
                    join al in db.Airlines on fd.AirlineId equals al.AirlineId
                    where fp.IsDepartElseArrival == true
                    //orderby f.FlightDate
                    select new
                    {
                        f.FlightId,
                        f.FlightDate,
                        fs.Status,
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
                    where fp.IsDepartElseArrival == false
                    // orderby f.FlightDate descending
                    select new
                    {
                        ap.AirportId,
                        ap.City,
                        ap.Country,
                    };
                var info = new string[queryDepart.Count(), 12];
                var count = 0;
                foreach (var i in queryDepart)
                {
                    info[count, 0] = i.AirlineCode;
                    info[count, 1] = i.FlightId.ToString();
                    info[count, 2] = i.AirlineName;
                    info[count, 3] = i.City;
                    info[count, 4] = i.Country;
                    info[count, 5] = i.AirportId;
                    info[count, 6] = i.FlightDate.ToShortDateString();
                    info[count, 7] = i.FlightDate.ToShortTimeString();
                    info[count, 8] = i.Status.ToString();
                    count++;
                }
                var countArrival = 0;
                foreach (var i in queryArrival)
                {
                    info[countArrival, 9] = i.City;
                    info[countArrival, 10] = i.Country;
                    info[countArrival, 11] = i.AirportId;
                    countArrival++;
                }
                return info;
            }
        }
        public void Create(DateTime flightDate, string departureId, string arrivalId)
        {
            var flightStatus = Status.SCHEDULED;
            var newFlight = new Flight() { FlightStatusId = (int)flightStatus, FlightDate = flightDate };
            using (var db = new SpartaFlightContext())
            {
                db.Flights.Add(newFlight);
                db.SaveChanges();
                var lastId = db.Flights.Select(i => i.FlightId).Max();
                CreateDeparture(lastId, departureId);
                CreateArrival(lastId, arrivalId);
            }
        }
        public static void CreateDeparture(int flightId, string departureId)
        {

            var newFlightPath = new FlightPath() { FlightId = flightId, AirportId = departureId, IsDepartElseArrival = true };
            using (var db = new SpartaFlightContext())
            {
                db.FlightPaths.Add(newFlightPath);
                db.SaveChanges();
            }
        }
        public static void CreateArrival(int flightId, string arrivalId)
        {

            var newFlightPath = new FlightPath() { FlightId = flightId, AirportId = arrivalId, IsDepartElseArrival = false };
            using (var db = new SpartaFlightContext())
            {
                db.FlightPaths.Add(newFlightPath);
                db.SaveChanges();
            }
        }
        public bool UpdateFlightDeparture(int flightId, string departureId)
        {
            using (var db = new SpartaFlightContext())
            {
                var updateFlightPath = db.FlightPaths.Where(f => f.FlightId == flightId && f.IsDepartElseArrival == true).FirstOrDefault();
                if (updateFlightPath == null)
                {
                    return false;
                }
                updateFlightPath.AirportId = departureId;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error updating flight {flightId}");
                }
            }
            return true;
        }
        public bool UpdateFlightArrival(int flightId, string arrivalId)
        {
            using (var db = new SpartaFlightContext())
            {
                var updateFlightPath = db.FlightPaths.Where(f => f.FlightId == flightId && f.IsDepartElseArrival == false).FirstOrDefault();
                if (updateFlightPath == null)
                {
                    return false;
                }
                updateFlightPath.AirportId = arrivalId;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error updating flight {flightId}");
                }
            }
            return true;
        }
        public bool Update(int flightId, Status flightStatus)
        {
            using (var db = new SpartaFlightContext())
            {
                var updateFlight = db.Flights.Where(f => f.FlightId == flightId).FirstOrDefault();
                if (updateFlight == null)
                {
                    return false;
                }
                if (updateFlight.FlightDate > DateTime.Today && flightStatus == Status.ARRIVED)
                {
                    throw new ArgumentException("Flight can't arrive if it hasn't left!");
                }
                updateFlight.FlightId = flightId;
                updateFlight.FlightStatusId = (int)flightStatus;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error updating flight {flightId}");
                    return false;
                }
            }
            return true;
        }

        public bool Delete(int flightId)
        {
            using (var db = new SpartaFlightContext())
            {
                var delFlightPath = db.FlightPaths.Where(fp => fp.FlightId == flightId);
                var delFlight = db.Flights.Where(f => f.FlightId == flightId);
                if (delFlightPath == null)
                {
                    return false;
                }
                if (delFlight == null)
                {
                    return false;
                }
                foreach (var item in delFlightPath)
                {
                    db.FlightPaths.RemoveRange(item);
                }
                db.SaveChanges();
                foreach (var item in delFlight)
                {
                    db.Flights.RemoveRange(item);
                }
                db.SaveChanges();
            }
            return true;
        }
    }
}
