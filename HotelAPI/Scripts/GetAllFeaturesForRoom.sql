CREATE PROCEDURE SelectFeaturesForRoomByPriority (@Room uniqueidentifier, @Prio int)
AS

IF (@Prio IN (1,2))
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


	-- Get details about features with the specified priority
	SELECT DISTINCT FeatureDescription, CategoryName
	FROM FeatureIds 
	INNER JOIN(
		SELECT FeatureId, FeatureDescription, CategoryName  -- Pick out Category name instead of ID
		FROM [dbo].[Features] AS F
		INNER JOIN [dbo].[Categorys] AS C ON F.CategoryId = C.CategoryId
		WHERE Info.Priority = @Prio
	) AS Info ON FeatureIds.FeatureId = Info.FeatureId

	  
	END
ELSE
	


DROP PROCEDURE SelectAllFeaturesForRoom;



