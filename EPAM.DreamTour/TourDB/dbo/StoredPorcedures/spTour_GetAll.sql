CREATE PROCEDURE [dbo].[spTour_GetAll]

AS
BEGIN
	SELECT [Id], [Country], [Region], [District], [City], [MinimalPrice], [MaximumPrice], [DaysCount], [BeginDate]
	FROM dbo.Tour
END
