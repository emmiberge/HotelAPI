DROP TABLE RoomsTestTable;

CREATE TABLE RoomsTestTable (
	ID int PRIMARY KEY,
	AmountOfBeds int NOT NULL,
	PricePerNight decimal(10,2)
);

INSERT INTO RoomsTestTable VALUES (1,1,150.0);
INSERT INTO RoomsTestTable VALUES (2,2,300.0);
INSERT INTO RoomsTestTable VALUES (3,2,350.0);
INSERT INTO RoomsTestTable VALUES (4,4,650.0);

SELECT * FROM RoomsTestTable;