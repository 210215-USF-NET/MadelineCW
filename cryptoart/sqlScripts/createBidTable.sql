DROP TABLE IF EXISTS Bids;
CREATE TABLE Bids(
Id SERIAL PRIMARY KEY,
	auctionid int,
	collectorid int,
	amount money,
	timeOfBid timestamp,
	CONSTRAINT FK_Bid_Auction FOREIGN KEY (AuctionId) REFERENCES Auction(Id),
	CONSTRAINT FK_Bid_Collector FOREIGN KEY (CollectorId) REFERENCES Collectors(Id)
);