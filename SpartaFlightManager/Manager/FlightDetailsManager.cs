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
        public List<string> ReturnFlightDetailIDStrings()
        {
            var outputList = new List<string>();
            using (var db = new SpartaFlightContext())
            {
                var query =
                    from fd in db.FlightDetails
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
                        fd.FlightDuration
                    };
                foreach (var item in query)
                {
                    outputList.Add(item.FlightId.ToString());
                    outputList.Add($"{item.Title} {item.FirstName} {item.LastName}");
                    outputList.Add(item.AirlineName);
                    outputList.Add(item.PlaneModel);
                    outputList.Add(item.Capacity.ToString());
                    outputList.Add(item.PassengerNumber.ToString());
                    outputList.Add(item.FlightDuration.ToString());
                }
                return outputList;
            }
        }
        public bool Update(int flightDetailsId,int flightId, int pilotId, 
            int airlineId, int planeId, int passengerNumber, int flightDuration, bool archive = false)
        {
            using (var db = new SpartaFlightContext())
            {
                var updateFDetails = db.FlightDetails.Where(f => f.FlightDetailsId == flightDetailsId).FirstOrDefault();
                if (updateFDetails == null)
                {
                    return false;
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

