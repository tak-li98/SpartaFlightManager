using NUnit.Framework;
using Database;
using Manager;
using System;
using System.Linq;
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
                db.Add(newFlight);
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
                _flightManager.Create(Status.SCHEDULED, new DateTime(2025, 12, 25, 12, 0, 0));
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
                var flightUpdate = _flightManager.Update(flightId, Status.DELAYED, new DateTime(2026, 12, 25, 12, 0, 0));
                var updateFlightId =
                    from f in db.Flights
                    where f.FlightDate == new DateTime(2026, 12, 25, 12, 0, 0)
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
                var flightId = db.Flights.Where(f => f.FlightDate == new DateTime(2025, 12, 25, 12, 0, 0)).FirstOrDefault().FlightId;
                var delFlight = _flightManager.Delete(flightId);
                Assert.That(delFlight, Is.True);
            }
        }
        [TearDown]
        public void Teardown()
        {
            using (var db = new SpartaFlightContext())
            {
                var removeFlight1 = db.Flights.Where(f => f.FlightDate == new DateTime(2025, 12, 25, 12, 0, 0));
                var removeFlight2 = db.Flights.Where(f => f.FlightDate == new DateTime(2026, 12, 25, 12, 0, 0));
                if (removeFlight1 != null)
                {
                    foreach (var item in removeFlight1)
                    {
                        db.Flights.RemoveRange(item);

                    }
                }
                if (removeFlight2 != null)
                {
                    foreach (var item in removeFlight2)
                    {
                        db.Flights.RemoveRange(item);
                    }
                }
                db.SaveChanges();
            }

        }
    }
}