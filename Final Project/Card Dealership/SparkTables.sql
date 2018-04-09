USE Spark
GO 

--Bridge Tables first
IF EXISTS(SELECT * FROM sys.tables WHERE name='CardColors')
	DROP TABLE CardColors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CardType')
	DROP TABLE CardType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CardSet')
	DROP TABLE CardSet
GO

--Then the main table with FK
IF EXISTS(SELECT * FROM sys.tables WHERE name='CardInfo')
	DROP TABLE CardInfo
GO

--Then the rest
IF EXISTS(SELECT * FROM sys.tables WHERE name='Colors')
	DROP TABLE Colors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='CMC')
	DROP TABLE CMC
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Artist')
	DROP TABLE Artist
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='[Type]')
	DROP TABLE [Type]
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='[Set]')
	DROP TABLE [Set]
GO


IF EXISTS(SELECT * FROM sys.tables WHERE name='SalesInfo')
	DROP TABLE SalesInfo
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
	DROP TABLE PurchaseType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='[State]')
	DROP TABLE [State]
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='City')
	DROP TABLE City
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

--IF EXISTS(SELECT * FROM sys.tables WHERE name='UserInfo')
--	DROP TABLE UserInfo
--GO

--IF EXISTS(SELECT * FROM sys.tables WHERE name='[Role]')
--	DROP TABLE [Role]
--GO




--IF EXISTS(SELECT * FROM sys.tables WHERE name='Toughness')
--	DROP TABLE Toughness
--GO

--IF EXISTS(SELECT * FROM sys.tables WHERE name='[Power]')
--	DROP TABLE [Power]
--GO






CREATE TABLE [Set] (
	SetId varchar(3) NOT NULL Primary key,
	SetName varchar(30) NOT NULL
)
GO



CREATE TABLE [Type] (
	TypeId INT primary key identity (1,1),
	TypeName varchar(30) NOT NULL
)
GO



CREATE TABLE Artist (
	ArtistId INT Primary key identity (1,1),
	ArtistName varchar(30) NOT NULL
)
GO



CREATE TABLE CMC (
	CmcId INT Primary key identity (1,1),
	CmcAmount varchar(2) NOT NULL
)
GO



CREATE TABLE Colors (
	ColorId INT Primary key identity (1,1),
	ColorName varchar(10) NULL
)
GO



--CREATE TABLE [Power] (
--	PowerId INT Primary key identity (1,1),
--	PowerNumber varchar(2) NULL
--)
--GO



--CREATE TABLE Toughness (
--	ToughnessId INT Primary key identity (1,1),
--	ToughnessNumber varchar(2) NULL
--)
--GO



CREATE TABLE CardInfo (
	CardId INT Primary key identity (1,1),
	CardName varchar(30) NOT NULL,
	CmcId INT NOT NULL Foreign Key References CMC(CmcId),
	CardArtURL varchar(30) NOT NULL,
	CardText varchar(500) NULL,
	[Power] INT NULL, --Foreign Key References [Power](PowerId),
	Toughness INT NULL, -- Foreign Key References Toughness(ToughnessId),
	ArtistId INT NOT NULL Foreign Key References Artist(ArtistId),
	CardNumber varchar(3) NOT NULL,
	MSRP INT NOT NULL,
	SalePrice INT NOT NULL
)
GO



CREATE TABLE CardSet (
	CardId INT,
	SetId varchar(3),
	constraint PK_CardSet primary key (CardId, SetId),
	constraint FK_CardId
		foreign key (CardId) references CardInfo (CardId),
	constraint FK_SetId
		foreign key (SetId) references [Set] (SetId)
)
GO



CREATE TABLE CardType (
	CardId INT,
	TypeId INT,
	constraint PK_CardType primary key (CardId, TypeId),
	constraint FK_CardId2
		foreign key (CardId) references CardInfo (CardId),
	constraint FK_TypeId
		foreign key (TypeId) references [Type] (TypeId)
)
GO



CREATE TABLE CardColors (
	CardId INT,
	ColorId INT,
	constraint PK_CardColors primary key (CardId, ColorId),
	constraint FK_CardId3
		foreign key (CardId) references CardInfo (CardId),
	constraint FK_ColorId
		foreign key (ColorId) references Colors (ColorId)
)
GO



--CREATE TABLE [Role] (
--	RoleId INT Primary key identity (1,1),
--	RoleName varchar(15) NOT NULL
--)
--GO



--CREATE TABLE UserInfo (
--	FirstName varchar(20) NOT NULL,
--	LastName varchar(20) NULL,
--	Email varchar(30) NULL,
--	[Password] varchar(30) NOT NULL,
--	RoleId INT NOT NULL Foreign Key References [Role](RoleId)
--)
--GO



CREATE TABLE Specials (
	SpecialId INT Primary key identity (1,1),
	[Name] varchar(30) NOT NULL,
	[Description] varchar(500) NULL
)
GO



CREATE TABLE City (
	CityId INT Primary key identity (1,1),
	[Name] varchar(20) NOT NULL
)
GO



CREATE TABLE [State] (
	StateId varchar(2) Primary key,
	[Name] varchar(20) NOT NULL
)
GO



CREATE TABLE PurchaseType (
	PurchaseTypeId INT Primary key identity (1,1),
	[Type] varchar(20) NOT NULL
)
GO



CREATE TABLE SalesInfo (
	SalesId INT Primary key identity (1,1),
	[Name] varchar(40) NOT NULL,
	Phone varchar(10) NOT NULL,
	Email varchar(30) NOT NULL,
	Street1 varchar(30) NOT NULL,
	Street2 varchar(30) NULL,
	CityId INT NOT NULL Foreign key references City(CityId),
	StateId varchar(2) NOT NULL Foreign key references [State](StateId),
	Zipcode varchar(5) NOT NULL,
	PurchasePrice INT NOT NULL,
	CardId INT NOT NULL 
)
GO