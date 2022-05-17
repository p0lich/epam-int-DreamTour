IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name = 'IIS APPPOOL\DreamTour')
BEGIN
    CREATE LOGIN [IIS APPPOOL\DreamTour] 
      FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
      DEFAULT_LANGUAGE=[us_english]
END
GO
CREATE USER [DreamTourUser] 
  FOR LOGIN [IIS APPPOOL\DreamTour]
GO
EXEC sp_addrolemember 'db_owner', 'DreamTourUser'
GO