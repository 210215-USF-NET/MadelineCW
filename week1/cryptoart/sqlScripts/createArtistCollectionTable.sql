DROP TABLE IF EXISTS ArtistCollection;


CREATE TABLE ArtistCollection (
ArtistId int NOT NULL,
ArtId int NOT NULL,
CONSTRAINT PK_ArtistCollection PRIMARY KEY (ArtistID, ArtId),
CONSTRAINT FK_ArtistCollection_Artists FOREIGN KEY (ArtistId) REFERENCES Artist(Id),
CONSTRAINT FK_ArtistCollection_Art FOREIGN KEY (ArtId) REFERENCES Art(Id)
);
