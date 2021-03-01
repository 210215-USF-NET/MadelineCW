DROP TABLE IF EXISTS SellersInventory;
CREATE TABLE SellersInventory(


SellerId int NOT NULL,
ArtId int NOT NULL,
CONSTRAINT PK_SellersInventory PRIMARY KEY (SellerId, ArtId),
CONSTRAINT FK_SellersInventory_Seller FOREIGN KEY (SellerId) REFERENCES Seller(Id),
CONSTRAINT FK_SellersInventory_Art FOREIGN KEY (ArtId) REFERENCES Art(Id)
);


