USE Spark;
GO

IF EXISTS (
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE ROUTINE_NAME = 'DbReset'
)
BEGIN
    DROP PROCEDURE DbReset
END
GO

CREATE PROCEDURE DbReset AS
BEGIN 

	
	DELETE FROM CardSet;
	DELETE FROM CardColors;
	DELETE FROM CardType;
	DELETE FROM CardInfo;
	DELETE FROM Artist;
	DELETE FROM Specials;
	DELETE FROM [Set];
	DELETE FROM [Type];
	DELETE FROM CMC;
	DELETE FROM Colors;
	DELETE FROM AspNetUsers WHERE id = '00000000-0000-0000-0000-000000000000';	
	



	DBCC CHECKIDENT ('cardInfo', RESEED, 1)
	
	--	Insert into [Set] (SetId, SetName)
	--Values ('CMD', 'Commander'),
	--('WWK', 'Worldwake')

	SET IDENTITY_INSERT [Type] ON;
	INSERT INTO [Type] (TypeId, TypeName)
	Values (1, 'Legendary Creature'),
	(2, 'Instant'),
	(3, 'Planeswalker'),
	(4, 'Land')
	SET IDENTITY_INSERT [Type] OFF;

	INSERT INTO [Set] (SetId, SetName)
	Values ('RTR', 'Return to Ravnica'),
	('M25', 'Masters 25'),
	('P3K', 'Portal Three Kingdoms'),
	('CMD', 'Commander'),
	('WWK', 'Worldwake')

	SET IDENTITY_INSERT Colors ON;
	INSERT INTO Colors (ColorId, ColorName)
	Values (1, 'White'),
	(2, 'Blue'),
	(3, 'Black'),
	(4, 'Red'),
	(5, 'Green'),
	(6, 'Colorless')
	SET IDENTITY_INSERT Colors OFF;

	INSERT INTO AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	Values('00000000-0000-0000-0000-000000000000', 0, 0, 'test@test.com', 0, 0, 0, 'test');

	SET IDENTITY_INSERT Specials ON;
	INSERT INTO Specials (SpecialId, [Name], [Description])
	Values(1, 'Creatures%Discount', '10% off all creatures')
	SET IDENTITY_INSERT Specials OFF;

	SET IDENTITY_INSERT CMC ON;
	Insert into CMC (CmcId, CmcAmount)
	Values(0, '0'),
	(1, '1'),
	(2, '2'),
	(3, '3'),
	(4, '4'),
	(5, '5')
	SET IDENTITY_INSERT CMC OFF;

	SET IDENTITY_INSERT Artist ON;
	Insert into Artist (ArtistId, ArtistName)
	Values (1, 'Peter Morbacher')
	SET IDENTITY_INSERT Artist OFF;

	SET IDENTITY_INSERT CardInfo ON;
	Insert into CardInfo(CardId, CardName, CmcId, CardArtUrl, CardText, [Power], Toughness, ArtistId, CardNumber, MSRP, SalePrice)
	Values(1, 'Animar, Soul of the Elements', 3, 'animar.jpg', 'Protection from white and from black.', 1, 1, 1, '186', 25, 20), 
	(2, 'Underground Sea', 0, 'sea.jpg', null, null, null, 1, '110', 425, 420), 
	(3, 'Kiki-Jiki Mirror Breaker', 5, 'kiki.jpg', 'Copy non-legendary boi.', 2, 2, 1, '105', 125, 120), 
	(4, 'Jace, The Mind Sculpter', 4, 'jace.jpg', 'Planeswaler.', null, null, 1, '86', 225, 220), 
	(5, 'Llanowar Elf', 1, 'llanowar.jpg', 'Tap for G.', 1, 1, 1, '16', 5, 2), 
	(6, 'Lightning Bolt', 1, 'bolt.jpg', '3 damage.', null, null, 1, '63', 15, 10) 
	SET IDENTITY_INSERT CardInfo OFF;

	Insert into CardSet (CardId, SetId)
	Values (1, 'CMD'),
	(2, 'M25'),
	(3, 'RTR'),
	(4, 'WWK'),
	(5, 'CMD'),
	(6, 'WWK')

	Insert into CardType (CardId, TypeId)
	Values (1, 1),
	(2, 4),
	(3, 1),
	(4, 3),
	(5, 1),
	(6, 2)

	Insert into CardColors (CardId, ColorId)
	Values (1, 2),
	(1, 4),
	(1, 5),
	(2, 6),
	(3, 4),
	(4, 2),
	(5, 5),
	(6, 4)

END

--exec DbReset
--Select * from CardInfo INNER JOIN CardSet ON CardInfo.CardId = CardSet.CardID INNER JOIN [Set] ON CardSet.SetId = [Set].SetId INNER JOIN CardColors ON CardInfo.CardId = CardColors.CardId INNER JOIN Colors ON CardColors.ColorId = Colors.ColorId
