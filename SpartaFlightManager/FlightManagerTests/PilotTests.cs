using NUnit.Framework;
using Database;
using Manager;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FlightManagerTests
{
    public class PilotTests
    {
        private PilotManager _pilotManager = new PilotManager();

        [SetUp]
        public void Setup()
        {
            var newPilot = new Pilot() { Title = "Mr", FirstName = "Dummy", LastName = "Data" };
            using(var db = new SpartaFlightContext())
            {
                db.Pilots.Add(newPilot);
                db.SaveChanges();
            }
        }
        [Test]
        public void CheckIfNewPilotAddedDatabaseIncreasesBy1()
        {
            using (var db = new SpartaFlightContext())
            {
                var initialCount = db.Pilots.Count();
                _pilotManager.Create( "Dummy","Data", "Mr");
                var finalCount = db.Pilots.Count();
                Assert.That(finalCount - initialCount, Is.EqualTo(1));
            }
        }

        [TearDown]
        public void TearDown()
        {
            using(var db = new SpartaFlightContext())
            {
                var removePilots = db.Pilots.Where(p => p.FirstName == "Dummy" && p.LastName == "Data");
                foreach (var removePilot in removePilots)
                {
                    db.Pilots.RemoveRange(removePilot);
                }
                db.SaveChanges();
            }
        }
    }
}