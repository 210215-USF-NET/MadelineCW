DROP TABLE IF EXISTS Artist;
CREATE TABLE Artist(
Id SERIAL PRIMARY KEY,
	Name VARCHAR,
	Location VarChar,
	Signature varchar,
	ArtistStatement text,
	Biography text,
	DigitalSignature bytea,
	Country varchar
);