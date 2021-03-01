DROP TABLE IF EXISTS Auction;
CREATE TABLE Auction(
Id SERIAL PRIMARY KEY,
	sellerid int,
	artid int,
	minimumamount money,
	closingDate timestamp,
	CONSTRAINT FK_Auction_Seller FOREIGN KEY (SellerId) REFERENCES Seller(Id),
	CONSTRAINT FK_Auction_Art FOREIGN KEY (ArtId) REFERENCES Art(Id)
);
