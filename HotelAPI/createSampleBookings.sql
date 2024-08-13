﻿DROP TABLE BookingsTestTable;

CREATE TABLE Bookings (
	Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
	RoomID int,
	CustomerID int,
	CheckInDate Date,
	CheckOutDate Date,
	FOREIGN KEY (RoomId) REFERENCES Rooms(RoomId),
);

INSERT INTO BookingsTestTable VALUES (1,1,100,'2024-03-03','2024-03-05');
INSERT INTO BookingsTestTable VALUES (2,1,200,'2024-03-05','2024-03-08');
INSERT INTO BookingsTestTable VALUES (3,2,300,'2024-03-03','2024-03-06');
INSERT INTO BookingsTestTable VALUES (4,2,400,'2024-03-06','2024-03-07');
INSERT INTO BookingsTestTable VALUES (5,3,500,'2024-03-07','2024-03-10');
INSERT INTO BookingsTestTable VALUES (6,8,600,'2024-03-02','2024-03-12');
INSERT INTO BookingsTestTable VALUES (7,9,700,'2024-03-04','2024-03-05');
INSERT INTO BookingsTestTable VALUES (8,9,800,'2024-03-05','2024-03-06');
INSERT INTO BookingsTestTable VALUES (9,9,900,'2024-03-06','2024-03-08');
INSERT INTO BookingsTestTable VALUES (10,10,1000,'2024-03-08','2024-03-09');

SELECT * FROM BookingsTestTable;