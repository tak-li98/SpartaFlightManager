using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using System.Diagnostics;

namespace Manager
{
    public class FlightDetailsManager
    {
        public int SelectedFlightDetail { get; set; }

        public void SetFlightDetail(int selectedItem)
        {
            SelectedFlightDetail = selectedItem;
        }

        public List<FlightDetail> RetrieveAll()
        {
            using (var db = new SpartaFlightContext())
            {
                return db.FlightDetails.ToList();
            }
        }
        public void Create(int flightId, int pilotId, int airlineId, int planeId, int passengerNumber, int flightDuration, bool archive=false)
        {
            var newFDetails = new FlightDetail() {FlightId = flightId,PilotId = pilotId, AirlineId=airlineId
                ,PlaneId=planeId,PassengerNumber=passengerNumber,FlightDuration=flightDuration, Archive = archive };
            using (var db = new SpartaFlightContext())
            {
                db.FlightDetails.Add(newFDetails);
                db.SaveChanges();
            }
        }
        public string[] ReturnDepartureStr(int flightId)
        {
            using (var db = new SpartaFlightContext())
            {
                var airports = new string[2];
                var query = db.FlightPaths.Where(fp => fp.FlightId == flightId && fp.IsDepartElseArrival == true);
                foreach (var item in query)
                {
                    if(item.IsDepartElseArrival == true)
                    {
                        airports[0] = db.Airports.Where(i=>i.AirportId == item.AirportId).ToString();
                    }
                    airports[1] = db.Airports.Where(i=>i.AirportId == item.AirportId).ToString();
                }
                return airports;
            }
        }
        public List<string> ReturnFlightDetailIDStrings()
        {
            var outputList = new List<string>();
            using (var db = new SpartaFlightContext())
            {
                var query =
                    from fd in db.FlightDetails
                    join f in db.Flights on fd.FlightId equals f.FlightId
                    join fs in db.FlightStatuses on f.FlightStatusId equals fs.FlightStatusId
                    join pl in db.Pilots on fd.PilotId equals pl.PilotId
                    join p in db.Planes on fd.PlaneId equals p.PlaneId
                    join a in db.Airlines on fd.AirlineId equals a.AirlineId
                    where fd.FlightId == SelectedFlightDetail
                    select new
                    {
                        fd.FlightId,
                        pl.Title,
                        pl.FirstName,
                        pl.LastName,
                        a.AirlineName,
                        p.PlaneModel,
                        p.Capacity,
                        fd.PassengerNumber,
                        fd.FlightDuration,
                        f.FlightStatus
                        
                    };
                foreach (var item in query)
                {
                    outputList.Add(item.FlightId.ToString()); // 0 element 
                    outputList.Add($"{item.Title} {item.FirstName} {item.LastName}"); // 1 element 
                    outputList.Add(item.AirlineName); // 2 element 
                    outputList.Add(item.PlaneModel); // 3 element 
                    outputList.Add(item.Capacity.ToString()); // 4 element 
                    outputList.Add(item.PassengerNumber.ToString()); // 5 element 
                    outputList.Add(item.FlightDuration.ToString()); // 6 element 
                    //outputList.Add(item.FlightStatus.ToString()); // 7 element 
                   // outputList.Add(item.FlightDate.ToShortDateString()); // 8 element 
                    //outputList.Add(item.FlightDate.ToShortTimeString()); // 9 element 
                }
                return outputList;
            }
        }
        public bool Update(int flightDetailsId,int flightId, int pilotId, 
            int airlineId, int planeId, int passengerNumber, int flightDuration,int capacity, bool archive = false)
        {
            using (var db = new SpartaFlightContext())
            {
                var updateFDetails = db.FlightDetails.Where(f => f.FlightDetailsId == flightDetailsId).FirstOrDefault();
                if (updateFDetails == null)
                {
                    return false;
                }
                if (passengerNumber > capacity)
                {
                    throw new ArgumentException("Passenger number is greater than capacity!");
                }
                updateFDetails.FlightId = flightId;
                updateFDetails.PilotId = pilotId;
                updateFDetails.AirlineId = airlineId;
                updateFDetails.PlaneId = planeId;
                updateFDetails.PassengerNumber = passengerNumber;
                updateFDetails.FlightDuration = flightDuration;
                updateFDetails.Archive = archive;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                   
                    Debug.WriteLine($"Error updating flight detail {flightDetailsId}");
                    return false;
                }
            }
            return true;
        }

        public bool Delete(int flightDetailsId)
        {
            using (var db = new SpartaFlightContext())
            {
                var delFDetails = db.FlightDetails.Where(f => f.FlightDetailsId == flightDetailsId);
                if (delFDetails == null)
                {
                    return false;
                }
                foreach (var delFDetail in delFDetails)
                {
                    db.FlightDetails.RemoveRange(delFDetail);
                }
                db.SaveChanges();
            }
            return true;
        }
    }
}

