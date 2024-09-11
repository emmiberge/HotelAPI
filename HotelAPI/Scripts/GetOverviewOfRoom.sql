CREATE PROCEDURE GetOverviewsOfRoomsForHotel (@Hotel uniqueidentifier)
AS

BEGIN

-- Rooms for this hotel
WITH Tmp AS(
	SELECT RoomId
	FROM [dbo].[HotelRooms] AS HR
	WHERE HR.HotelId = @Hotel)

-- Overview of room
SELECT Title, Capacity, BedType
FROM Tmp
LEFT JOIN 
Rooms
ON Tmp.RoomId = Rooms.RoomId;


END