using NUnit.Framework;
using Database;
using Manager;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FlightManagerTests
{
    public class FlightDetailsTests
    {
        FlightDetailsManager _flightDetailsManager = new FlightDetailsManager();
        [SetUp]
        public void Setup()
        {
            using (var db = new SpartaFlightContext())
            {
                var newFlight = new Flight() { FlightStatusId = (int)Status.SCHEDULED, FlightDate = new DateTime(2025, 12, 25, 12, 0, 0) };
                db.Flights.Add(newFlight);
                db.SaveChanges();
                var lastFlightId = db.Flights.Select(i => i.FlightId).Max();
                var newDepature = new FlightPath() { FlightId = lastFlightId, AirportId = "LHR", IsDepartElseArrival = true };
                var newArrival = new FlightPath() { FlightId = lastFlightId, AirportId = "DAL", IsDepartElseArrival = false };

                var newPilot = new Pilot() { Title = "Mr", FirstName = "Test", LastName = "Man" }; //new pilot
                var lastPilotId = db.Pilots.Select(i => i.PilotId).Max();
                db.Pilots.Add(newPilot);
                db.SaveChanges();
                var newRegion = new Region() { RegionName = "Test Region" }; //new region
                var lastRegionId = db.Regions.Select(i => i.RegionId).Max();
                db.Regions.Add(newRegion);
                db.SaveChanges();
                var newAirline = new Airline() { AirlineName = "Test Airline", AirlineCode = "TA", RegionId = lastRegionId }; //new airline
                var lastAirlineId = db.Airlines.Select(i => i.AirlineId).Max();
                db.Airlines.Add(newAirline);
                db.SaveChanges();
                var newPlane = new Plane() { PlaneModel = "Sparta3000", Capacity = 250 }; //new plane
                var lastPlaneId = db.Planes.Select(i => i.PlaneId).Max();
                db.Planes.Add(newPlane);
                db.SaveChanges();
                var newFlightDetails = new FlightDetail() { FlightId = lastFlightId, PilotId = lastPilotId, AirlineId = lastAirlineId, PassengerNumber = 200, PlaneId = lastPlaneId, FlightDuration = 5, Archive = false };
                db.FlightDetails.Add(newFlightDetails);
                db.SaveChanges();
                
            }
        }
        [Test]
        public void CheckIfLatestFlightIdIsReturned()
        {
            using (var db = new SpartaFlightContext())
            {
                var flightId = db.Flights.Select(i => i.FlightId).Max();
                Assert.That(_flightDetailsManager.ReturnLatestFlightId, Is.EqualTo(flightId));
            }
        }
        [TearDown]
        public void Teardown()
        {
            using (var db = new SpartaFlightContext())
            {
                var lastFlightDetailId = db.FlightDetails.Select(i => i.FlightDetailsId).Max();
                db.FlightDetails.RemoveRange(db.FlightDetails.Where(f => f.FlightDetailsId == lastFlightDetailId));
                db.SaveChanges();

                var lastPilotId = db.Pilots.Select(i => i.PilotId).Max();
                db.Pilots.RemoveRange(db.Pilots.Where(f => f.PilotId == lastPilotId));
                db.SaveChanges();

                var lastRegionId = db.Regions.Select(i => i.RegionId).Max();
                db.Regions.RemoveRange(db.Regions.Where(r => r.RegionId == lastRegionId));
                db.SaveChanges();

                var lastAirlineId = db.Airlines.Select(i => i.AirlineId).Max();
                db.Airlines.RemoveRange(db.Airlines.Where(a => a.AirlineId == lastAirlineId));
                db.SaveChanges();

                var lastPlaneId = db.Planes.Select(i => i.PlaneId).Max();
                db.Planes.RemoveRange(db.Planes.Where(a => a.PlaneId == lastPlaneId));
                db.SaveChanges();

                var lastFlightId = db.Flights.Select(i => i.FlightId).Max();
                db.Flights.RemoveRange(db.Flights.Where(i => i.FlightId == lastFlightId));
                db.SaveChanges();



            }
        }
    }
}
