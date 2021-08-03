CREATE TABLE FlightDetails(
flightDetailsId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
flightId int NOT NULL FOREIGN KEY REFERENCES Flights,
pilotId int FOREIGN KEY REFERENCES Pilots,
airlineId int NOT NULL FOREIGN KEY REFERENCES Airlines,
planeId int FOREIGN KEY REFERENCES Planes,
passengerNumber int,
flightDuration int,
archive bit)