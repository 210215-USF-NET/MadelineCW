DROP TABLE IF EXISTS Bids;
CREATE TABLE Bids(
Id SERIAL PRIMARY KEY,
	auctionid int,
	collectorid int,
	amount money,
	timeOfBid timestamp
);