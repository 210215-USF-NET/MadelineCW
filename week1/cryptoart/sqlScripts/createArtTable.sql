DROP TABLE IF EXISTS Art;
CREATE TABLE Art(
Id SERIAL PRIMARY KEY,
	Name VARCHAR,
	Location VarChar,
	Description VARCHAR,
	ArtistCommentary TEXT,
	ArtPiece bytea,
	thumbnail bytea,
	seriesNumber int,
	maxSeries int,
	buynowprice money,
	currentValue money
);