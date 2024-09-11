CREATE PROCEDURE GetHotelRooms (@Hotel uniqueidentifier)
AS

BEGIN

SELECT RoomId
FROM [dbo].[HotelRooms] AS HR
WHERE HR.HotelId = @Hotel;
END
