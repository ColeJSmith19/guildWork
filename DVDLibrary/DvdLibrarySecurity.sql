USE master
GO

CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO


USE DVDLibrary
GO
 
CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO


GRANT EXECUTE ON GetAllDvds TO DvdLibraryApp
GRANT EXECUTE ON GetDvdByID TO DvdLibraryApp
GRANT EXECUTE ON GetDvdByTitle TO DvdLibraryApp
GRANT EXECUTE ON GetDvdByReleaseYear TO DvdLibraryApp
GRANT EXECUTE ON GetDvdByRating TO DvdLibraryApp
GRANT EXECUTE ON GetDvdByDirector TO DvdLibraryApp
GRANT EXECUTE ON AddDvd TO DvdLibraryApp
GRANT EXECUTE ON UpdateDvd TO DvdLibraryApp
GRANT EXECUTE ON DeleteDvd TO DvdLibraryApp
GO


GRANT SELECT ON DvdInfo TO DvdLibraryApp
GRANT INSERT ON DvdInfo TO DvdLibraryApp
GRANT UPDATE ON DvdInfo TO DvdLibraryApp
GRANT DELETE ON DvdInfo TO DvdLibraryApp
GO

GRANT SELECT ON Director TO DvdLibraryApp
GRANT INSERT ON Director TO DvdLibraryApp
GRANT UPDATE ON Director TO DvdLibraryApp
GRANT DELETE ON Director TO DvdLibraryApp
GO

GRANT SELECT ON Rating TO DvdLibraryApp
GRANT INSERT ON Rating TO DvdLibraryApp
GRANT UPDATE ON Rating TO DvdLibraryApp
GRANT DELETE ON Rating TO DvdLibraryApp
GO

GRANT SELECT ON ReleaseYear TO DvdLibraryApp
GRANT INSERT ON ReleaseYear TO DvdLibraryApp
GRANT UPDATE ON ReleaseYear TO DvdLibraryApp
GRANT DELETE ON ReleaseYear TO DvdLibraryApp
GO


