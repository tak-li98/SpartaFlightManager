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
        public Flight SelectedFlight { get; set; }

        public void SetFlightCustomer(object selectedItem)
        {
            SelectedFlight = (Flight)selectedItem;
        }

        public List<Flight> RetrieveAll()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.Flights.ToList();
            }
        }

        public void Create(Status flightStatus, DateTime flightDate, string departureId,string arrivalId)
        {

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
        public bool Update(int flightId, Status flightStatus, DateTime flightDate)
        {
            using (var db = new SpartaFlightContext())
            {
                var updateFlight = db.Flights.Where(f => f.FlightId == flightId).FirstOrDefault();
                if (updateFlight == null)
                {
                    return false;
                }
                updateFlight.FlightId = flightId;
                updateFlight.FlightStatusId = (int)flightStatus;
                updateFlight.FlightDate = flightDate;

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
                if(delFlightPath == null)
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
