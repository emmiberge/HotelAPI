DROP TABLE RoomsTestTable;

CREATE TABLE RoomsTestTable (
	ID int PRIMARY KEY,
	AmountOfBeds int NOT NULL,
	PricePerNight decimal(10,2)
);


INSERT INTO Rooms VALUES (NEWID(),'Double bedroom', 'A beautiful room with view of the ocean', 2, 'King size bed');
INSERT INTO Rooms VALUES (NEWID(),'Single bedroom', 'A small but cosy apartment', 1, 'Single bed');



INSERT INTO Categorys VALUES (NEWID(), 'Bathroom');
INSERT INTO Categorys VALUES (NEWID(), 'Accessibility');
INSERT INTO Categorys VALUES (NEWID(), 'Connectivity');



INSERT INTO Features VALUES (NEWID(), 'b2ec9cb9-c83f-48ca-8d80-8bec7b5cf03f', 'Free toiletries', 2);
INSERT INTO Features VALUES (NEWID(), 'b2ec9cb9-c83f-48ca-8d80-8bec7b5cf03f', 'Towels', 2);
INSERT INTO Features VALUES (NEWID(), '3d17ea52-9352-49f1-96b0-f5de3c9fd2fb', 'Free Wifi', 1);


INSERT INTO Hotels VALUES (NEWID(), 'Las Vegas', 'hotelVegas@gmail.com', 'Grand Hotel Las Vegas');
INSERT INTO Hotels VALUES (NEWID(), 'Florida', 'floridaBeach@gmail.com', 'Beach Vibes Hotel');

INSERT INTO RoomFeatures VALUES('65a4bdca-c43a-4960-9905-ce266cfda87f', 'd690485e-32d0-4d07-bb35-2060a062e6b8');
INSERT INTO RoomFeatures VALUES('ed247584-91a3-49c1-b1c5-dcc7af554de4', 'd690485e-32d0-4d07-bb35-2060a062e6b8');
INSERT INTO RoomFeatures VALUES('ed247584-91a3-49c1-b1c5-dcc7af554de4', '5a5e2f9f-5d4e-4857-8d11-352cb1a35075');
INSERT INTO RoomFeatures VALUES('ed247584-91a3-49c1-b1c5-dcc7af554de4', '378e0520-eac1-406b-ae9a-3286e80212f3');


INSERT INTO HotelFeatures VALUES ('2eca0a49-2346-473a-bbb0-1aacab56c0e9', '378e0520-eac1-406b-ae9a-3286e80212f3');

INSERT INTO HotelRooms VALUES ('2eca0a49-2346-473a-bbb0-1aacab56c0e9','ed247584-91a3-49c1-b1c5-dcc7af554de4');
INSERT INTO HotelRooms VALUES ('e39fb6fd-89ea-466a-847e-685e9979efba','65a4bdca-c43a-4960-9905-ce266cfda87f');





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



-------------- STORED PROCEDURES -------------------
CREATE PROCEDURE SelectAllFeaturesForRoom (@Room uniqueidentifier)
AS

BEGIN

-- Find hotel id
DECLARE @Hotel uniqueidentifier;
SELECT @Hotel = HotelId FROM HotelRooms WHERE RoomId = @Room;

-- Feature for this room (incluing the features of its hotel)
WITH FeatureIds AS (
    (SELECT FeatureId FROM [dbo].[HotelFeatures] WHERE HotelId = @Hotel)
	UNION ALL
	(SELECT FeatureId FROM [dbo].[RoomFeatures] WHERE RoomId = @Room)
)


-- Get details about features
SELECT DISTINCT FeatureDescription, CategoryName, Priority
FROM FeatureIds 
INNER JOIN(
	SELECT FeatureId, FeatureDescription, CategoryName, Priority  -- Pick out Category name instead of ID
	FROM [dbo].[Features] AS F
	INNER JOIN [dbo].[Categorys] AS C ON F.CategoryId = C.CategoryId
) AS Info ON FeatureIds.FeatureId = Info.FeatureId;
	  
END


DROP PROCEDURE SelectAllFeaturesForRoom;