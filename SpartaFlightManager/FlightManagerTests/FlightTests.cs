using NUnit.Framework;
using Database;
using Manager;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FlightManagerTests
{
    public class FlightTests
    {
        private FlightManager _flightManager = new FlightManager();
        [SetUp]
        public void Setup()
        {
            var newFlight = new Flight() { FlightStatusId = (int)Status.SCHEDULED, FlightDate = new DateTime(2025, 12, 25, 12, 0, 0) };

            using (var db = new SpartaFlightContext())
            {
                db.Flights.Add(newFlight);
                db.SaveChanges();
                var lastId = db.Flights.Select(i => i.FlightId).Max();
                var newDepature = new FlightPath() { FlightId = lastId, AirportId = "LHR", IsDepartElseArrival = true };
                var newArrival = new FlightPath() { FlightId = lastId, AirportId = "DAL", IsDepartElseArrival = false };
                db.SaveChanges();
            }
        }

        [Test]
        [Category("HAPPY")]
        public void CheckIfNewFlightAddedDatabaseIncreasesBy1()
        {
            using (var db = new SpartaFlightContext())
            {
                var initialCount = db.Flights.Count();
                _flightManager.Create(new DateTime(2025, 12, 25, 12, 0, 0), "LHR", "DAL");
                var finalCount = db.Flights.Count();
                Assert.That(finalCount - initialCount, Is.EqualTo(1));
            }
        }
        [Test]
        [Category("HAPPY")]
        public void CheckIfFlightIsUpdated()
        {
            using (var db = new SpartaFlightContext())
            {
                var flightId = db.Flights.Where(f => f.FlightDate == new DateTime(2025, 12, 25, 12, 0, 0)).FirstOrDefault().FlightId;
                var flightUpdate = _flightManager.Update(flightId, Status.DELAYED);
                var updateFlightId =
                    from f in db.Flights
                    where f.FlightDate == new DateTime(2025, 12, 25, 12, 0, 0)
                    select new { f.FlightStatusId };
                var updateStatus = 0;
                foreach (var item in updateFlightId)
                {
                    updateStatus = item.FlightStatusId;
                }
                Assert.That(flightUpdate, Is.True);
                Assert.That(updateStatus, Is.EqualTo((int)Status.DELAYED));
            }
        }
        [Test]
        [Category("HAPPY")]
        public void CheckIfFlightIsDeleted()
        {
            using (var db = new SpartaFlightContext())
            {
                var flightId = db.Flights.Where(f => f.FlightDate == new DateTime(2025, 12, 25, 12, 0, 0) || f.FlightDate == new DateTime(2026, 12, 25, 12, 0, 0));
                foreach (var item in flightId)
                {
                    var delFlight = _flightManager.Delete(item.FlightId);
                    Assert.That(delFlight, Is.True);
                }
            }
        }

 
        [Test]
        public void ReturnsTheRightStatusFromStatusID()
        {
            using (var db = new SpartaFlightContext())
            {
                var statusId = db.FlightStatuses.Where(s => s.Status == "Scheduled").FirstOrDefault().FlightStatusId;
                Assert.That(statusId, Is.EqualTo(1));
            }
        }

        [TearDown]

        public void Teardown()
        {
            using (var db = new SpartaFlightContext())
            {
                var removeFlight = db.Flights.Where(f =>
                f.FlightDate == new DateTime(2025, 12, 25, 12, 0, 0) || f.FlightDate == new DateTime(2026, 12, 25, 12, 0, 0));

                var idList = new List<int>();

                foreach (var item in removeFlight)
                {
                    idList.Add(item.FlightId);
                }


                if (removeFlight != null)
                {
                    foreach (var item in idList)
                    {
                        db.FlightPaths.RemoveRange(db.FlightPaths.Where(i => i.FlightId == item));
                    }
                    db.SaveChanges();
                }
                if (removeFlight != null)
                {
                    foreach (var item in removeFlight)
                    {
                        db.Flights.RemoveRange(item);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}