DBCC CHECKIDENT ('Planes',RESEED,0)
GO

INSERT INTO Planes(planeModel, capacity)
VALUES
('Airbus A320',170),
('Boeing 777-300',550),
('Airbus A330',290),
('Boeing 787 Dreamliner',248),
('Boeing 747',416),
('Airbus A300',247),
('Boeing 737 Max', 172)