DBCC CHECKIDENT ('FlightStatuses',RESEED,0)
GO

INSERT INTO FlightStatuses(status)
VALUES
('Scheduled'),
('Delayed'),
('Departed'),
('Arrived'),
('Cancelled')