CREATE PROCEDURE [dbo].[spTour_GetBest]
AS
BEGIN
	SELECT TOP 3 [Id], [Country], [Region], [District], [City], [MinimalPrice], [MaximumPrice], [DaysCount], [BeginDate]
	FROM dbo.Tour
	ORDER BY NEWID()
END
