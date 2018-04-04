USE master
GO
if exists (select * from sysdatabases where name='DVDLibrary')
		drop database DVDLibrary
go

CREATE DATABASE DVDLibrary
GO

USE DVDLibrary
GO 

CREATE TABLE ReleaseYear (
	ReleaseYearID INT Primary key identity (1,1),
	ReleaseYear smallint NOT NULL
)

CREATE TABLE Rating(
	RatingID INT Primary key identity (1,1),
	RatingName varchar(5)
)

CREATE TABLE Director(
	DirectorID INT Primary key identity (1,1),
	DirectorName varchar(30)
)

CREATE TABLE DvdInfo(
	DvdID INT Primary key identity (1,1),
	DvdTitle varchar(70) NOT NULL,
	ReleaseYearID INT NOT NULL FOREIGN KEY REFERENCES ReleaseYear(ReleaseYearID),
	RatingID INT NOT NULL FOREIGN KEY REFERENCES Rating(RatingID),
	DirectorID INT NOT NULL FOREIGN KEY REFERENCES Director(DirectorID),
	Notes varchar(1000) NULL
)

--SET IDENTITY_INSERT ReleaseYear ON;
--GO
--insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (1, 1997); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (2, 2012); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (3, 2002); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (4, 2004); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (5, 1993); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (6, 2002); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (7, 1986); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (8, 2003); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (9, 2009); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (10, 1994); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (11, 2006); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (12, 1988); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (13, 2010); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (14, 1984); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (15, 2001); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (16, 2010); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (17, 2006); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (18, 2011); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (19, 2006); insert into ReleaseYear (ReleaseYearID, ReleaseYear) values (20, 2009);
--SET IDENTITY_INSERT ReleaseYear OFF;
--GO

--SET IDENTITY_INSERT Rating ON;
--GO
--insert into Rating (RatingID, RatingName) values (1, 'G'); insert into Rating (RatingID, RatingName) values (2, 'PG'); insert into Rating (RatingID, RatingName) values (3, 'PG13'); insert into Rating (RatingID, RatingName) values (4, 'R');
--SET IDENTITY_INSERT Rating OFF;
--GO

--SET IDENTITY_INSERT Director ON;
--GO
--insert into Director (DirectorID, DirectorName) values (1, 'Myer Thomasset'); insert into Director (DirectorID, DirectorName) values (2, 'Jolynn Crunkhorn'); insert into Director (DirectorID, DirectorName) values (3, 'Eldon Jedrzejczyk'); insert into Director (DirectorID, DirectorName) values (4, 'Ediva MacDermott'); insert into Director (DirectorID, DirectorName) values (5, 'Lief Alman'); insert into Director (DirectorID, DirectorName) values (6, 'Ebenezer Fidge'); insert into Director (DirectorID, DirectorName) values (7, 'Thorndike Gillfillan'); insert into Director (DirectorID, DirectorName) values (8, 'Gertrud Bendley'); insert into Director (DirectorID, DirectorName) values (9, 'Gretchen Verheijden'); insert into Director (DirectorID, DirectorName) values (10, 'Carmine Lease'); insert into Director (DirectorID, DirectorName) values (11, 'Jeri Yeldham'); insert into Director (DirectorID, DirectorName) values (12, 'Vale Valeri'); insert into Director (DirectorID, DirectorName) values (13, 'Chrotoem Djorvic'); insert into Director (DirectorID, DirectorName) values (14, 'Hyatt Peacey'); insert into Director (DirectorID, DirectorName) values (15, 'Rhody Winston'); insert into Director (DirectorID, DirectorName) values (16, 'Brantley Bolesworth'); insert into Director (DirectorID, DirectorName) values (17, 'Oates Dockwray'); insert into Director (DirectorID, DirectorName) values (18, 'Marlo Tumbridge'); insert into Director (DirectorID, DirectorName) values (19, 'Sena Perell'); insert into Director (DirectorID, DirectorName) values (20, 'Corine Barabich');
--SET IDENTITY_INSERT Director OFF;
--GO

--SET IDENTITY_INSERT DvdInfo ON;
--GO
--insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (1, 'McKenna Shoots for the Stars', 12, 3, 15, 'quam sapien varius ut blandit non interdum in ante vestibulum'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (2, 'Starman', 16, 1, 3, 'quis odio consequat varius integer ac leo pellentesque ultrices mattis odio'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (3, 'Hours', 5, 3, 20, 'convallis nulla neque libero convallis eget eleifend luctus ultricies eu nibh quisque id'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (4, '300: Rise of an Empire', 1, 2, 12, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (5, 'Case of You, A', 16, 4, 12, 'amet lobortis sapien sapien non mi integer ac neque duis bibendum morbi non quam nec dui luctus rutrum nulla tellus in sagittis dui vel nisl'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (6, 'Terminal Velocity', 14, 4, 1, 'mi in porttitor pede justo eu massa donec dapibus duis at velit eu est congue elementum in hac habitasse platea dictumst morbi vestibulum velit'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (7, 'Altered', 11, 3, 14, 'volutpat in congue etiam justo etiam pretium iaculis justo in hac habitasse platea dictumst etiam faucibus cursus urna ut tellus nulla ut erat id mauris vulputate elementum nullam varius nulla'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (8, 'There Goes My Heart', 1, 3, 4, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (9, 'Entr''acte', 1, 4, 12, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (10, 'Happy New Year', 9, 2, 4, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (11, 'Pete Seeger: The Power of Song', 16, 4, 7, 'volutpat dui maecenas tristique est et tempus semper est quam pharetra magna ac consequat metus sapien ut nunc vestibulum ante ipsum primis in'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (12, 'Mona Lisa Smile', 5, 4, 5, 'faucibus cursus urna ut tellus nulla ut erat id mauris vulputate elementum nullam varius nulla facilisi cras'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (13, 'Fort Apache', 20, 1, 5, 'id sapien in sapien iaculis congue vivamus metus arcu adipiscing molestie hendrerit at vulputate vitae'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (14, 'Duellists, The', 19, 3, 4, 'est risus auctor sed tristique in tempus sit amet sem fusce consequat nulla nisl nunc nisl duis bibendum felis sed interdum venenatis turpis enim blandit mi in porttitor pede justo'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (15, 'Love Affair', 1, 1, 4, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (16, 'Only God Forgives', 5, 3, 7, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (17, 'Laid To Rest', 9, 1, 5, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (18, 'My Avatar and Me (Min Avatar og mig)', 7, 3, 18, 'nisl duis bibendum felis sed interdum venenatis turpis enim blandit mi in porttitor pede'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (19, 'Sweet Karma', 4, 3, 4, 'consectetuer adipiscing elit proin risus praesent lectus vestibulum quam sapien varius ut blandit non interdum in ante vestibulum ante ipsum primis'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (20, 'Hot Pursuit', 5, 1, 17, 'interdum in ante vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae duis'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (21, 'Addicted', 4, 3, 4, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (22, 'Ass Backwards', 1, 1, 13, 'fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam quis turpis eget elit sodales scelerisque mauris sit'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (23, 'Tokyo-Ga', 16, 4, 9, 'faucibus accumsan odio curabitur convallis duis consequat dui nec nisi volutpat eleifend donec ut dolor morbi vel lectus in quam fringilla rhoncus mauris enim leo rhoncus sed vestibulum sit'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (24, 'I Accuse!', 7, 2, 14, 'orci luctus et ultrices posuere cubilia curae mauris viverra diam vitae quam suspendisse potenti nullam porttitor lacus at turpis donec posuere metus vitae ipsum aliquam non mauris morbi'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (25, 'Trucker', 4, 2, 18, 'vulputate luctus cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus vivamus vestibulum sagittis sapien cum sociis natoque penatibus et magnis'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (26, 'Safe House', 7, 4, 1, null); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (27, 'Soul Men', 18, 1, 2, 'imperdiet et commodo vulputate justo in blandit ultrices enim lorem ipsum dolor sit amet consectetuer adipiscing elit proin interdum mauris'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (28, 'Not Reconciled', 19, 3, 3, 'praesent blandit lacinia erat vestibulum sed magna at nunc blandit nam nulla integer pede justo lacinia eget tincidunt eget tempus'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (29, 'Popular Music (Populärmusik från Vittula)', 6, 3, 20, 'potenti cras in purus eu magna vulputate luctus cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus vivamus'); insert into DvdInfo (DvdID, DvdTitle, ReleaseYearID, RatingID, DirectorID, Notes) values (30, 'Sadist, The', 17, 2, 5, 'sapien a libero nam dui proin leo odio porttitor id consequat in consequat ut nulla');
--SET IDENTITY_INSERT DvdInfo OFF;
--GO
