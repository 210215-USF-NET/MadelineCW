DROP TABLE IF EXISTS Auction;
CREATE TABLE Auction(
Id SERIAL PRIMARY KEY,
	sellerid int,
	artid int,
	minimumamount money,
	closingDate timestamp
);