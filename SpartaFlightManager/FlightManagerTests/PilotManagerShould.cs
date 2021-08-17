using Moq;
using Manager;
using Database;
using Database.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FlightManagerTests
{
    public class PilotManagerShould
    {
        private PilotManager _sut;

        [Test]
        [Category("SAD")]
        public void ThrowAnException_WhenPilotServiceIsNull()
        {
            Assert.That(() => new PilotManager(null), Throws.ArgumentException.With.Message.Contains("IPilotService object cannot be null"));
        }

        [Test]
        [Category("HAPPY")]
        public void BeAbleToBeConstructed()
        {
            var mockPilotService = new Mock<IPilotService>();
            _sut = new PilotManager(mockPilotService.Object);
            Assert.That(_sut, Is.InstanceOf<PilotManager>());
        }

        [Test]
        [Category("HAPPY")]
        public void UpdateTheSelectedPilot_WhenUpdateIsCalled_WithValidId()
        {
            //Arrange
            var mockPilotService = new Mock<IPilotService>();
            var originalPilot = new Pilot()
            {
                PilotId = 1,
                Title = "Mr.",
                FirstName = "Tak",
                LastName = "Li"
            };
            mockPilotService.Setup(ps => ps.GetPilotById(1)).Returns(originalPilot);
            _sut = new PilotManager(mockPilotService.Object);

            //Act
            _sut.Update(1, "Alan", It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.That(_sut.SelectedPilot.FirstName, Is.EqualTo("Alan"));
        }

        [Test]
        [Category("SAD")]
        public void UpdateTheSelectedPilot_ReturnsFalse_WhenUpdateIsCalled_WithInvalidId()
        {
            //Arrange
            var mockPilotService = new Mock<IPilotService>();      
            mockPilotService.Setup(ps => ps.GetPilotById(1)).Returns((Pilot)null);
            _sut = new PilotManager(mockPilotService.Object);

            //Act
            var result = _sut.Update(1, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("SAD")]
        public void UpdateTheSelectedPilot_ReturnsFalse_WhenADatabaseExceptionIsThrown()
        {
            //Arrange
            var mockPilotService = new Mock<IPilotService>();
            mockPilotService.Setup(ps => ps.GetPilotById(It.IsAny<int>())).Returns(new Pilot());
            mockPilotService.Setup(ps => ps.SavePilotChanges()).Throws<DbUpdateConcurrencyException>();
            _sut = new PilotManager(mockPilotService.Object);

            //Act
            var result = _sut.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("SAD")]
        public void DeleteSelectedCustomer_ReturnsFalse_WhenDeleteIsCalledWithInvalidId()
        {
            //Arrange
            var mockPilotService = new Mock<IPilotService>();
            mockPilotService.Setup(ps => ps.GetPilotById(1)).Returns((Pilot)null);
            _sut = new PilotManager(mockPilotService.Object);

            //Act
            var result = _sut.Delete(1);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        [Category("HAPPY")]
        public void DeleteTheSelectedPilot_WhenDeleteIsCalled_WithValidId()
        {
            //Arrange
            var mockPilotService = new Mock<IPilotService>();
            var originalPilot = new Pilot()
            {
                PilotId = 1,
                Title = "Mr.",
                FirstName = "Tak",
                LastName = "Li"
            };
            mockPilotService.Setup(ps => ps.GetPilotById(1)).Returns(originalPilot);
            _sut = new PilotManager(mockPilotService.Object);

            //Act
            var result = _sut.Delete(1);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        [Category("HAPPY")]
        public void TheSelectedPilotIsNull_WhenDeleteIsCalled_WithValidId()
        {
            //Arrange
            var mockPilotService = new Mock<IPilotService>();
            var originalPilot = new Pilot()
            {
                PilotId = 1,
                Title = "Mr.",
                FirstName = "Tak",
                LastName = "Li"
            };
            mockPilotService.Setup(ps => ps.GetPilotById(1)).Returns(originalPilot);
            _sut = new PilotManager(mockPilotService.Object);
            _sut.SelectedPilot = originalPilot;

            //Act
            _sut.Delete(1);

            //Assert
            Assert.That(_sut.SelectedPilot, Is.Null);
        }
    }
}
