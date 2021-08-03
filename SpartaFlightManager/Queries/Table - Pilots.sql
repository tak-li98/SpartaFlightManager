CREATE TABLE Pilots(
pilotId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
title varchar(6),
firstName varchar(20) NOT NULL,
lastName varchar(20) NOT NULL)