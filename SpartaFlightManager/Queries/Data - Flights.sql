DBCC CHECKIDENT ('Flights',RESEED,0)
GO

INSERT INTO Flights (flightStatusId,flightDate,departureId, arrivalId)
VALUES
(1, '2021-08-10 15:00:00', 'ATL','BJS')