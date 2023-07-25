update SizeTypes SET ConversionMultiplier = 0.001 WHeRE SizeTypeID = '69329439-93B8-467F-BF0D-F8F2A4F4576D'
update Inventories set Amount = Amount * 1000 WHERE Amount = 1

update UPCs SET Size = Size * 1000 where Size < 2
update UPCs SET Size = Size / 0.0338140227 where Size < 25
update Inventories set Amount = '354.88235476934' where Amount = '0.4057682724'

alter table Pours ADD IngredientID UNIQUEIDENTIFIER, Status INT

ALTER TABLE Pours ADD CONSTRAINT FK_Pours_Ingredients FOREIGN KEY (IngredientID) REFERENCES Ingredients (IngredientID)

delete from TicketItemAliases
delete from Ingredients
delete from Recipe

-- Load Recipes here

update POSTicketItems SET Status = 2, Reconciled = 0
update Pours SET POSTicketItemID = null, IngredientID = null, Status = 2
update Ingredients SET CategoryID = NULL WHERE ManufacturerID IS NOT NULL

DELETE FROM Pours WHERE PourID IN (
select P.PourID 
from Pours P
	JOIN UPCs U ON P.UPCID = U.UPCID
	JOIN Categories C ON U.CategoryID = C.CategoryID
	JOIN Pours P2 ON P.TagID = P2.TagID AND P.PourID <> P2.PourID
	JOIN Locations L ON P.LocationID = L.LocationID
	JOIN Locations L2 ON P2.LocationID = L2.LocationID
WHERE 1=1 
	AND DATEADD( MILLISECOND, 500, P2.PourTime ) > P.PourTime
	AND P2.PourTime < P.PourTime
	AND ABS( P.Volume - P2.Volume ) < 1
	AND P.DeviceID <> P2.DeviceID )

UPDATE POSTickets SET Establishment = 'Lobby Bar' WHERE Establishment = '3125'
UPDATE POSTickets SET Establishment = 'Pool Bar' WHERE Establishment = '002'
UPDATE POSTickets SET Establishment = 'Pelican Bar' WHERE Establishment = '003'

UPDATE Locations SET POSAlias = 'Pelican Bar' where Name like 'Pelican Bar'
UPDATE Locations SET POSAlias = 'Lobby Bar' where Name like 'Lobby Bar'
UPDATE Locations SET POSAlias = 'Pool Bar' where Name like 'Pool Bar'


