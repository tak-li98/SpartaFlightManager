CREATE TABLE FlightPaths(
flightId int NOT NULL FOREIGN KEY REFERENCES Flights(flightId),
airportId varchar(5) NOT NULL FOREIGN KEY REFERENCES Airports(airportId))