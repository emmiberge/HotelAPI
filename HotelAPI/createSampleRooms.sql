DROP TABLE RoomsTestTable;

CREATE TABLE RoomsTestTable (
	ID int PRIMARY KEY,
	AmountOfBeds int NOT NULL,
	PricePerNight decimal(10,2)
);




SELECT * FROM RoomsTestTable;


-------------- TABLES ------------------

CREATE TABLE Rooms (
	RoomId uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Title varchar(50),
	Summary varchar(255),
	Capacity tinyint CHECK (Capacity BETWEEN 1 AND 8),
	BedType varchar(50)
);

CREATE TABLE Categorys(
	CategoryId uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
	CategoryName varchar(50)
);

CREATE TABLE Features (
	FeatureId uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
	CategoryId uniqueidentifier,
	FeatureDescription varchar(100),
	Priority tinyint CHECK (Priority BETWEEN 1 AND 2),
	FOREIGN KEY (CategoryId) REFERENCES Categorys(CategoryId),
);

CREATE TABLE Hotels(
	HotelId uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
	HotelLocation varchar(255),
	Contact varchar(255)
);



CREATE TABLE RoomFeatures (
    RoomId uniqueidentifier,
    FeatureId uniqueidentifier,
    PRIMARY KEY (RoomId, FeatureId),
    FOREIGN KEY (RoomId) REFERENCES Rooms(RoomId),
    FOREIGN KEY (FeatureId) REFERENCES Features(FeatureId)
);

CREATE TABLE HotelFeatures(
	HotelId uniqueidentifier,
	FeatureId uniqueidentifier,
	PRIMARY KEY(HotelId, FeatureId),
	FOREIGN KEY (HotelId) REFERENCES Hotels(HotelId),
	FOREIGN KEY (FeatureId) REFERENCES Features(FeatureId),
);

CREATE TABLE HotelRooms(
	HotelId uniqueidentifier,
	RoomId uniqueidentifier,
	PRIMARY KEY(HotelId, RoomId),
	FOREIGN KEY (HotelId) REFERENCES Hotels(HotelId),
	FOREIGN KEY (RoomId) REFERENCES Rooms(RoomId),
);