CREATE TABLE Flights(
flightId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
flightDate datetime NOT NULL,
flightStatus varchar(15) NOT NULL,
