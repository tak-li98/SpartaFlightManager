DBCC CHECKIDENT ('Regions',RESEED,0)
GO

INSERT INTO Regions(regionName)
VALUES
('Africa/MiddleEast'),
('Asia/Pacific'),
('Europe'),
('Latin America/Caribbean'),
('North America')