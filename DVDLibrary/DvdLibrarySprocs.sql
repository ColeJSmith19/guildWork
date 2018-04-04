Use DVDLibrary
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'GetAllDvds'
)
BEGIN
    DROP PROCEDURE GetAllDvds
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'GetDvdByID'
)
BEGIN
    DROP PROCEDURE GetDvdByID
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'GetDvdByTitle'
)
BEGIN
    DROP PROCEDURE GetDvdByTitle
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'GetDvdByReleaseYear'
)
BEGIN
    DROP PROCEDURE GetDvdByReleaseYear
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'GetDvdByRating'
)
BEGIN
    DROP PROCEDURE GetDvdByRating
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'GetDvdByDirector'
)
BEGIN
    DROP PROCEDURE GetDvdByDirector
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'AddDvd'
)
BEGIN
    DROP PROCEDURE AddDvd
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'UpdateDvd'
)
BEGIN
    DROP PROCEDURE UpdateDvd
END
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'DeleteDvd'
)
BEGIN
    DROP PROCEDURE DeleteDvd
END
GO

CREATE PROCEDURE GetAllDvds
AS
SELECT DvdID, DvdTitle, ReleaseYear, DirectorName, RatingName, Notes
FROM DvdInfo
	INNER JOIN ReleaseYear ON
		DvdInfo.ReleaseYearID = ReleaseYear.ReleaseYearID
	INNER JOIN Rating ON
		DvdInfo.RatingID = Rating.RatingID
	INNER JOIN Director ON
		DvdInfo.DirectorID = Director.DirectorID
ORDER BY DvdTitle
GO



CREATE PROCEDURE GetDvdByID(
	@DvdID int
)
AS
SELECT DvdID, DvdTitle, ReleaseYear, DirectorName, RatingName, Notes
FROM DvdInfo
	INNER JOIN ReleaseYear ON
		DvdInfo.ReleaseYearID = ReleaseYear.ReleaseYearID
	INNER JOIN Rating ON
		DvdInfo.RatingID = Rating.RatingID
	INNER JOIN Director ON
		DvdInfo.DirectorID = Director.DirectorID
WHERE DvdID = @DvdID
GO



CREATE PROCEDURE GetDvdByTitle(
	@DvdTitle varchar
)
AS
SELECT DvdID, DvdTitle, ReleaseYear, DirectorName, RatingName, Notes
FROM DvdInfo
	INNER JOIN ReleaseYear ON
		DvdInfo.ReleaseYearID = ReleaseYear.ReleaseYearID
	INNER JOIN Rating ON
		DvdInfo.RatingID = Rating.RatingID
	INNER JOIN Director ON
		DvdInfo.DirectorID = Director.DirectorID
WHERE DvdTitle = @DvdTitle
GO



CREATE PROCEDURE GetDvdByReleaseYear(
	@DvdReleaseYear smallint
)
AS
SELECT DvdID, DvdTitle, ReleaseYear, DirectorName, RatingName, Notes
FROM DvdInfo
	INNER JOIN ReleaseYear ON
		DvdInfo.ReleaseYearID = ReleaseYear.ReleaseYearID
	INNER JOIN Rating ON
		DvdInfo.RatingID = Rating.RatingID
	INNER JOIN Director ON
		DvdInfo.DirectorID = Director.DirectorID
WHERE ReleaseYear = @DvdReleaseYear
ORDER BY ReleaseYear
GO



CREATE PROCEDURE GetDvdByDirector(
	@DvdDirector varchar
)
AS
SELECT DvdID, DvdTitle, ReleaseYear, DirectorName, RatingName, Notes
FROM DvdInfo
	INNER JOIN ReleaseYear ON
		DvdInfo.ReleaseYearID = ReleaseYear.ReleaseYearID
	INNER JOIN Rating ON
		DvdInfo.RatingID = Rating.RatingID
	INNER JOIN Director ON
		DvdInfo.DirectorID = Director.DirectorID
WHERE DirectorName = @DvdDirector
ORDER BY DvdTitle
GO


CREATE PROCEDURE GetDvdByRating(
	@DvdRating varchar
)
AS
SELECT DvdID, DvdTitle, ReleaseYear, DirectorName, RatingName, Notes
FROM DvdInfo
	INNER JOIN ReleaseYear ON
		DvdInfo.ReleaseYearID = ReleaseYear.ReleaseYearID
	INNER JOIN Rating ON
		DvdInfo.RatingID = Rating.RatingID
	INNER JOIN Director ON
		DvdInfo.DirectorID = Director.DirectorID
WHERE RatingName = @DvdRating
ORDER BY DvdTitle
GO


CREATE PROCEDURE AddDvd(

	@DvdTitle varchar(70),
	@DvdReleaseYearID int output,
	@DvdDirectorID int output,
	@DvdRatingID int output,
	@DvdNotes varchar(500),
	@ReleaseYear smallint,
	@DirectorName varchar(30),
	@RatingName varchar(5)
	--@DvdID int output
)
AS
	begin
		if not exists(
			select * from Director
			where DirectorName = @DirectorName
		)
		begin
			insert into Director (DirectorName)
				values (@DirectorName)
				set @DvdDirectorID = SCOPE_IDENTITY()
			end
		else
			set @DvdDirectorID = (select DirectorID from Director
									where DirectorName = @DirectorName)
			set @DvdRatingID = (select RatingID from Rating
									where RatingName = @RatingName)
		if not exists(
			select * from ReleaseYear
			where ReleaseYear = @ReleaseYear
		)
		begin
			insert into ReleaseYear(ReleaseYear)
				values (@ReleaseYear)
				set @DvdReleaseYearID = SCOPE_IDENTITY()
			end
		else
			set @DvdReleaseYearID = (select ReleaseYearID from ReleaseYear
										where ReleaseYear = @ReleaseYear)
		insert into DvdInfo (DirectorID, RatingID, DvdTitle, ReleaseYearID, Notes)
		values (@DvdDirectorID, @DvdRatingID, @DvdTitle, @DvdReleaseYearID, @DvdNotes)
		--set @DvdID = SCOPE_IDENTITY()
		
end
GO


CREATE PROCEDURE UpdateDvd(
	@DvdID int,
	@DvdTitle varchar(70),
	@DvdReleaseYearID int output,
	@DvdDirectorID int output,
	@DvdRatingID int output,
	@DvdNotes varchar(500),
	@ReleaseYear smallint,
	@DirectorName varchar(30),
	@RatingName varchar(5)
)
AS
	begin
		if exists(
			select * from DvdInfo
			where DvdInfo.DvdID = @DvdID
		) 
	begin
		if not exists(
			select * from Director
			where DirectorName = @DirectorName
		)
		begin
			insert into Director (DirectorName)
				values (@DirectorName)
				set @DvdDirectorID = SCOPE_IDENTITY()
			end
		else
			set @DvdDirectorID = (select DirectorID from Director
									where DirectorName = @DirectorName)
			set @DvdRatingID = (select RatingID from Rating
									where RatingName = @RatingName)
		if not exists(
			select * from ReleaseYear
			where ReleaseYear = @ReleaseYear
		)
		begin
			insert into ReleaseYear(ReleaseYear)
				values (@ReleaseYear)
				set @DvdReleaseYearID = SCOPE_IDENTITY()
			end
		else
			set @DvdReleaseYearID = (select ReleaseYearID from ReleaseYear
										where ReleaseYear = @ReleaseYear)
	UPDATE DvdInfo
		SET DvdTitle = @DvdTitle,
		ReleaseYearID = @DvdReleaseYearID,
		DirectorID = @DvdDirectorID,
		RatingID = @DvdRatingID,
		Notes = @DvdNotes
	WHERE DvdID = @DvdID
end
end
GO

CREATE PROCEDURE DeleteDvd(
	@DvdID int
)
AS
	DELETE FROM DvdInfo
	WHERE DvdID = @DvdID
GO