CREATE TABLE Character (
	Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CreatedOn datetime NOT NULL
	)

CREATE TABLE CharacterDetail(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Name varchar(50) NOT NULL,
	PlayerName varchar(50) NOT NULL,
	CharacterId int NOT NULL,
	CONSTRAINT FK_CharacterId
	FOREIGN KEY (CharacterId)
	REFERENCES Character(Id)
	ON DELETE CASCADE
	)

CREATE TABLE Attributes(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Strength int NOT NULL DEFAULT 0,
	Dexterity int NOT NULL DEFAULT 0,
	Constitution int NOT NULL DEFAULT 0,
	Intelligence int NOT NULL DEFAULT 0,
	Wisdom int NOT NULL DEFAULT 0,
	Charisma int NOT NULL DEFAULT 0,
	CharacterId int NOT NULL,
	CONSTRAINT FK_CharacterId_Attributes
	FOREIGN KEY (CharacterId)
	REFERENCES Character(Id)
	ON DELETE CASCADE
	)


INSERT INTO Character (CreatedOn) Values (GETUTCDATE())
INSERT INTO Character (CreatedOn) Values (GETUTCDATE())
INSERT INTO Character (CreatedOn) Values (GETUTCDATE())

DECLARE @unique uniqueidentifier
SET @unique = NEWID()
INSERT INTO CharacterDetail (id, Name, PlayerName, CharacterId) VALUES(@unique, 'Bob', 'Jon', 1)

SET @unique = NEWID()
INSERT INTO CharacterDetail (id, Name, PlayerName, CharacterId) VALUES(@unique, 'Dave', 'Tony', 2)

SET @unique = NEWID()
INSERT INTO CharacterDetail (id, Name, PlayerName, CharacterId) VALUES(@unique, 'Steve', 'Sam', 3)

SET @unique = NEWID()
INSERT INTO Attributes (id, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, CharacterId) VALUES (@unique,16,13,11,10,6,8,1)
SET @unique = NEWID()
INSERT INTO Attributes (id, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, CharacterId) VALUES (@unique,16,15,11,10,18,8,2)
SET @unique = NEWID()
INSERT INTO Attributes (id, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, CharacterId) VALUES (@unique, 16,13,9,18,6,8,3)

SELECT c.*, d.Name, d.PlayerName, a.*  FROM Character c INNER JOIN CharacterDetail d on c.Id=d.CharacterId INNER JOIN Attributes a on c.Id = a.CharacterId ORDER BY c.Id

ALTER TABLE CharacterDetail
DROP CONSTRAINT FK_CharacterId

DROP TABLE Character
DROP TABLE CharacterDetail