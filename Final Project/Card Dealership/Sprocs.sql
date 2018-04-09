USE Spark;
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'SetsSelectAll')
	    DROP PROCEDURE SetsSelectAll
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'ArtistsSelectAll')
	    DROP PROCEDURE ArtistsSelectAll
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'ColorsSelectAll')
	    DROP PROCEDURE ColorsSelectAll
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'CardInfoInsert')
	    DROP PROCEDURE CardInfoInsert
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'CardInfoUpdate')
	    DROP PROCEDURE CardInfoUpdate
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'CardInfoDelete')
	    DROP PROCEDURE CardInfoDelete
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'CardInfoSelect')
	    DROP PROCEDURE CardInfoSelect
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'CardInfoSelectMSRP')
	    DROP PROCEDURE CardInfoSelectMSRP
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'CardInfoSelectDetails')
	    DROP PROCEDURE CardInfoSelectDetails
GO

CREATE PROCEDURE CardInfoSelectDetails(
	@CardId int
) AS
BEGIN

CREATE TABLE #Temp
(
    CardID int,
    ColorName varchar(50)
)
INSERT INTO #Temp
SELECT i.CardId, c.ColorName
FROM CardInfo i
    JOIN CardColors cc
        ON cc.CardId = i.CardId
    JOIN Colors c
        ON c.ColorId = cc.ColorId

CREATE TABLE #ColorString
(
    CardID int,
    ColorName varchar(50)
)
INSERT INTO #ColorString

Select Main.CardID,
       Left(Main.Colors,Len(Main.Colors)-1) As "Students"
From
    (
        Select distinct T2.CardId, 
            (
                Select T1.ColorName + ', ' AS [text()]
                From #Temp T1
                Where T1.CardId = T2.CardID
                ORDER BY T1.CardID
                For XML PATH ('')
            ) [Colors]
        From #Temp T2
    ) [Main]



	SELECT CardInfo.CardId, CardName, CmcAmount, CardArtURL, CardText, [Power], Toughness, ArtistName, CardNumber, MSRP, SalePrice, SetName, TypeName, #ColorString.ColorName
	FROM CardInfo
		INNER JOIN CardSet ON CardInfo.CardId = CardSet.CardId
		INNER JOIN [Set] ON CardSet.SetId = [Set].SetId
		INNER JOIN CMC ON CardInfo.CmcId = CMC.CmcId
		INNER JOIN Artist ON CardInfo.ArtistId = Artist.ArtistId
		INNER JOIN CardType ON CardInfo.CardId = CardType.CardId
		INNER JOIN [Type] ON CardType.TypeId = [Type].TypeId
		INNER JOIN CardColors ON CardInfo.CardId = CardColors.CardId 
		INNER JOIN Colors ON CardColors.ColorId = Colors.ColorId
		INNER JOIN #ColorString ON CardInfo.CardId = #ColorString.CardID
	WHERE CardInfo.CardId = @CardId
END
GO

CREATE PROCEDURE CardInfoSelectMSRP AS
BEGIN
	SELECT TOP 6 CardInfo.CardId, CardName, CardNumber, SetName, CardArtURL, MSRP
	FROM CardInfo
		LEFT JOIN CardSet ON CardInfo.CardId = CardSet.CardId
		LEFT JOIN [Set] ON CardSet.SetId = [Set].SetId
	ORDER BY MSRP DESC
END
GO

CREATE PROCEDURE CardInfoSelect(
	@CardId int
)AS
BEGIN
	SELECT CardId, CardName, CmcId, CardArtURL, CardText, [Power], Toughness, ArtistId, CardNumber, MSRP, SalePrice
	FROM CardInfo
	WHERE CardId = @CardId
END
GO

CREATE PROCEDURE CardInfoDelete(
	@CardId int
)AS
BEGIN
	BEGIN TRANSACTION
	DELETE FROM CardColors Where CardId = @CardId;
	DELETE FROM CardSet Where CardId = @CardId;
	DELETE FROM CardType Where CardId = @CardId;
	DELETE FROM CardInfo Where CardId = @CardId;

	COMMIT TRANSACTION
END
GO

CREATE PROCEDURE CardInfoInsert(
	@CardId int output,
	@CardName varchar(30),
	@CmcId int,
	@CardArtURL varchar(30),
	@CardText varchar(500),
	@Power int,
	@Toughness int,
	@ArtistId int,
	@CardNumber varchar(3),
	@MSRP int,
	@SalePrice int,
	@SetId varchar(3),
	@SetName varchar(30)
)AS
BEGIN
	INSERT INTO CardInfo(CardName, CmcId, CardArtUrl, CardText, [Power], Toughness, ArtistId, CardNumber, MSRP, SalePrice)
	VALUES (@CardName, @CmcId, @CardArtURL, @CardText, @Power, @Toughness, @ArtistId, @CardNumber, @MSRP, @SalePrice);
	SET @CardId = SCOPE_IDENTITY();


	INSERT INTO [Set](SetName, SetId)
	VALUES (@SetName, @SetId)

	INSERT INTO CardSet(CardId, SetId)
	VALUES(@CardId, @SetId)

END
GO

CREATE PROCEDURE CardInfoUpdate(
	@CardId int,
	@CardName varchar(30),
	@CmcId int,
	@CardArtURL varchar(30),
	@CardText varchar(500),
	@Power int,
	@Toughness int,
	@ArtistId int,
	@CardNumber varchar(3),
	@MSRP int,
	@SalePrice int
)AS
BEGIN
	UPDATE CardInfo SET
	CardName = @CardName, 
	CmcId = @CmcId, 
	CardArtUrl = @CardArtURL, 
	CardText = @CardText, 
	[Power] = @Power,
	Toughness = @Toughness, 
	ArtistId = @ArtistId, 
	CardNumber = @CardNumber, 
	MSRP = @MSRP, 
	SalePrice = @SalePrice
	WHERE CardId = @CardId
END
GO

CREATE PROCEDURE SetsSelectAll AS
BEGIN 
	SELECT SetId, SetName
	FROM [Set]
END
GO

CREATE PROCEDURE ArtistsSelectAll AS
BEGIN 
	SELECT ArtistId, ArtistName
	FROM Artist
END
GO

CREATE PROCEDURE ColorsSelectAll AS
BEGIN 
	SELECT ColorId, ColorName
	FROM Colors
END
GO