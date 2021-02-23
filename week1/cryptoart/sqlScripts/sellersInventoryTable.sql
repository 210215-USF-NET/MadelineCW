DROP TABLE IF EXISTS SellersInventory;
CREATE TABLE SellersInventory(
Id SERIAL PRIMARY KEY,
	auctionid int,
	sellerid int
);