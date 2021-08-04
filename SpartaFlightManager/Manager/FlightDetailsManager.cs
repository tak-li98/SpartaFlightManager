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
        public FlightDetail SelectedFlightDetail { get; set; }

        public void SetFlightDetail(object selectedItem)
        {
            SelectedFlightDetail = (FlightDetail)selectedItem;
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

