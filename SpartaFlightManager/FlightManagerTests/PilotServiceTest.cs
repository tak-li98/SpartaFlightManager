using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Database;
using Database.Services;

namespace FlightManagerTests
{
    public class PilotServiceTest
    {
        private PilotService _sut;
        private SpartaFlightContext _context;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<SpartaFlightContext>().UseInMemoryDatabase(databaseName: "Example_DB").Options;
            _context = new SpartaFlightContext(options);
            _sut = new PilotService(_context);

            //seed the database
            _sut.CreatePilot(new Pilot { PilotId = 1, FirstName = "Tak", LastName = "Li", Title = "Mr." });
            _sut.CreatePilot(new Pilot { PilotId = 2, FirstName = "Jacky", LastName = "Chan", Title = "Mrs." });
        }

        [Test]
        public void GivenAValidId_CorrectPilotIsReturned()
        {
            var result = _sut.GetPilotById(1);
            Assert.That(result.FirstName, Is.EqualTo("Tak"));
            Assert.That(result.LastName, Is.EqualTo("Li"));
            Assert.That(result.Title, Is.EqualTo("Mr."));
        }

        [Test]
        public void ReturnCorrectPilotId_GivenPilotDetails()
        {
            var result1 = _sut.GetPilotIdByDetails("Mr. Tak Li");
            var result2 = _sut.GetPilotIdByDetails("Mrs. Jacky Chan");
            Assert.That(result1, Is.EqualTo(1));
            Assert.That(result2, Is.EqualTo(2));
        }

        [Test]
        public void GivenANewPilot_CreatePilotAddsItToTheDatabase()
        {
            //arrange
            var numberOfPilotsBefore = _context.Pilots.Count();
            var newPilot = new Pilot { PilotId = 3, FirstName = "Dez", LastName = "Estacio", Title = "Dr." };

            //act
            _sut.CreatePilot(newPilot);
            var result = _sut.GetPilotById(3);

            //assert 
            Assert.That(result, Is.TypeOf<Pilot>());
            Assert.That(result.FirstName, Is.EqualTo("Dez"));
            Assert.That(result.LastName, Is.EqualTo("Estacio"));
            Assert.That(result.Title, Is.EqualTo("Dr."));
            Assert.That(_context.Pilots.Count(), Is.EqualTo(numberOfPilotsBefore + 1));

            //Clean up
            _context.Pilots.Remove(newPilot);
            _context.SaveChanges();
        }

        [Test]
        public void ReturnCorrectListOfAllPilots_InOrderOfFirstName()
        {
            var pilotList = _sut.GetPilotList();
            Assert.That(pilotList, Is.TypeOf<List<Pilot>>());
            Assert.That(pilotList.Count(), Is.EqualTo(2));
            Assert.That(pilotList[0].FirstName, Is.EqualTo("Jacky"));
            Assert.That(pilotList[0].LastName, Is.EqualTo("Chan"));
            Assert.That(pilotList[0].FlightDetails, Is.Empty);
            Assert.That(pilotList[1].FirstName, Is.EqualTo("Tak"));
            Assert.That(pilotList[1].LastName, Is.EqualTo("Li"));
        }

        [Test]
        public void GivenPilotId_DeletePilotFromDatabase()
        {
            var numberOfPilotsBefore = _context.Pilots.Count();
            var delPilot = _sut.GetPilotById(1);
            _sut.RemovePilot(delPilot);

            Assert.That(_context.Pilots.Count(), Is.EqualTo(numberOfPilotsBefore - 1));

            _sut.CreatePilot(new Pilot { PilotId = 1, FirstName = "Tak", LastName = "Li", Title = "Mr." });
        }
    }
}
