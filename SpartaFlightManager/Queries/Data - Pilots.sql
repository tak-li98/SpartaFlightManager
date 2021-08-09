DBCC CHECKIDENT ('Pilots',RESEED,0)
GO


INSERT INTO Pilots(title, firstName, lastName, photoLinks)
VALUES
('Mr.','Christopher','Rafferty','PilotPics/Chris Rafferty.jpg'),
('Mr.','Sabir','Nisar','PilotPics/Sabir Nisar.jpg'),
('Mr.','Atchuthan','Soorasangaram','PilotPics/Atchu Soorasangaram.jpg'),
('Mr.','Denzel','Estacio','PilotPics/Denzel Estacio.jpg'),
('Mr.','Keagan','Gonsalves','PilotPics/Keagan Gonsalves.png'),
('Mr.','Tak','Li','PilotPics/Tak Li.jpg'),
('Mr.','Jacob','Chan','PilotPics/Jacob Chan.jpg'),
('Mr.','Ronil','Goldenwalla','PilotPics/Ronil Goldenwalla.jpg'),
('Mr.','Samuel','Adedina','PilotPics/Samuel Adedina.jpg'),
('Mr.','Umar','Majid','PilotPics/Umar Majid.jpg'),
('Mr.','Nishant','Mandal','PilotPics/Default.jpg'),
('Mrs.','Cathy','French','PilotPics/Default.jpg'),
('Mrs.','Jane','Austen','PilotPics/Jane Austen.jpg'),
('Mrs.','Sakura','Haruno','PilotPics/Sakura Haruno.jpg'),
('Mrs.','Hinata','Hyuga','PilotPics/Hinata Hyuga.jpg'),
('Mrs.','Mary','Poppins','PilotPics/Mary Poppins.jpg'),
('Mrs.','Sarah','Connor','PilotPics/Sarah Connor.jpg'),
('Miss','Nina','Tucker','PilotPics/Nina Tucker.png'),
('Miss','Irina','Jelavić','PilotPics/Irina Jelavic.jpg'),
('Miss','Rosita','Espinosa','PilotPics/Rosita Espinosa.jpg'),
('Miss','Katniss','Everdeen','PilotPics/Katniss Everdeen.jpg'),
('Miss','Lisa','Simpson','PilotPics/Lisa Simpson.jpg'),
('Mrs.','Lara','Croft','PilotPics/Lara Croft.jpg')
