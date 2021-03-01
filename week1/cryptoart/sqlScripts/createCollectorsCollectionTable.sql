DROP TABLE IF EXISTS CollectorsInventory;
CREATE TABLE CollectorsInventory(
collectorId int NOT NULL,
ArtId int NOT NULL,
CONSTRAINT PK_CollectorsInventory PRIMARY KEY (collectorID, ArtId),
CONSTRAINT FK_CollectorsInventory_Collector FOREIGN KEY (collectorId) REFERENCES Collector(Id),
CONSTRAINT FK_CollectorsInventory_Art FOREIGN KEY (ArtId) REFERENCES Art(Id)
);



