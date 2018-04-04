USE DVDLibrary
GO

SELECT DvdID, DvdTitle, ReleaseYear, RatingName, DirectorName, Notes
FROM DvdInfo
	INNER JOIN ReleaseYear ON
		DvdInfo.ReleaseYearID = ReleaseYear.ReleaseYearID
	INNER JOIN Rating ON
		DvdInfo.RatingID = Rating.RatingID
	INNER JOIN Director ON
		DvdInfo.DirectorID = Director.DirectorID