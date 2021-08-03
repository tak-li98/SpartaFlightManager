CREATE TABLE Flights(
flightId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
flightDate datetime NOT NULL,
flightStatus varchar(15) NOT NULL,
flightNumber varchar(7) NOT NULL,
departureId varchar(3) NOT NULL,
arrivalId varchar(3) NOT NULL)