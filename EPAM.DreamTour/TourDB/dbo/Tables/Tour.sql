CREATE TABLE [dbo].[Tour]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Country] NVARCHAR(50) NULL, 
    [Region] NVARCHAR(50) NULL, 
    [District] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [MinimalPrice] INT NULL, 
    [MaximumPrice] INT NULL, 
    [DaysCount] SMALLINT NULL, 
    [BeginDate] DATE NULL
)
