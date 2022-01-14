CREATE PROCEDURE [dbo].[spTour_Get]
	@id uniqueidentifier
AS
BEGIN
	SELECT TOP 1 [Id], [Couuntry], [Region], [District], [City], [MinimalPrice], [MaximumPrice], [DaysCount], [BeginDate]
	FROM dbo.Tour
	WHERE Id = @id
END
