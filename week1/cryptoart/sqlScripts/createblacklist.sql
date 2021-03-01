DROP TABLE IF EXISTS BlackList;
CREATE TABLE BlackList(
Id SERIAL PRIMARY KEY,
	location varchar,
	artid int,
	CONSTRAINT FK_BlackList_Art FOREIGN KEY (ArtId) REFERENCES Art(Id)
);