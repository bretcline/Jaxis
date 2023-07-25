DECLARE @Interval INT

SELECT @Interval = 30

-- By Manufacturer
select TI.POSTicketItemID 
	, U.Name
	, A.Description
	, R.Description
	, P.Volume
	, SP.PourStandard
	, SP.StandardVariance
	, L.Name
	, T.TicketDate
	, P.PourTime
from POSTicketItems TI
	JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
	JOIN TicketItemAliases A ON TI.Description = A.Description
	JOIN Recipe R ON A.RecipeID = R.RecipeID
	JOIN Ingredients I ON A.RecipeID = I.RecipeID
	JOIN StandardPours SP ON I.StandardPourID = SP.StandardPourID
	JOIN UPCs U ON I.ManufacturerID = U.ManufacturerID AND I.Quality = U.Quality AND U.ChildUPCID IS NULL
	JOIN Locations L ON T.Establishment = L.POSAlias
	JOIN Pours P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
where 1 = 1 
	AND P.Volume > ( SP.PourStandard - (SP.PourStandard * SP.StandardVariance ))
	AND P.Volume < ( SP.PourStandard + (SP.PourStandard * SP.StandardVariance ))
	AND P.PourTime < DATEADD( MINUTE, @Interval, T.TicketDate )
	AND P.PourTime > DATEADD( MINUTE, -1 * @Interval, T.TicketDate )
	AND P.POSTicketItemID IS NULL
	AND TI.Status <> 4

UNION
-- By Category
select TI.POSTicketItemID 
	, U.Name
	, A.Description
	, R.Description
	, P.Volume
	, SP.PourStandard
	, SP.StandardVariance
	, L.Name
	, T.TicketDate
	, P.PourTime
from POSTicketItems TI
	JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
	JOIN TicketItemAliases A ON TI.Description = A.Description
	JOIN Recipe R ON A.RecipeID = R.RecipeID
	JOIN Ingredients I ON A.RecipeID = I.RecipeID
	JOIN StandardPours SP ON I.StandardPourID = SP.StandardPourID
	JOIN UPCs U ON I.CategoryID = U.CategoryID AND I.Quality = U.Quality AND U.ChildUPCID IS NULL
	JOIN Locations L ON T.Establishment = L.POSAlias
	JOIN Pours P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
where 1 = 1 
	AND P.Volume > ( SP.PourStandard - (SP.PourStandard * SP.StandardVariance ))
	AND P.Volume < ( SP.PourStandard + (SP.PourStandard * SP.StandardVariance ))
	AND P.PourTime < DATEADD( MINUTE, @Interval, T.TicketDate )
	AND P.PourTime > DATEADD( MINUTE, -1 * @Interval, T.TicketDate )
	AND P.POSTicketItemID IS NULL
	AND TI.Status <> 4

UNION
-- By UPC
select TI.POSTicketItemID 
	, U.Name
	, A.Description
	, R.Description
	, P.Volume
	, SP.PourStandard
	, SP.StandardVariance
	, L.Name
	, T.TicketDate
	, P.PourTime
from POSTicketItems TI
	JOIN TicketItemAliases A ON TI.Description = A.Description
	JOIN Ingredients I ON A.RecipeID = I.RecipeID
	JOIN StandardPours SP ON I.StandardPourID = SP.StandardPourID
	JOIN Recipe R ON A.RecipeID = R.RecipeID
	JOIN UPCs U ON I.UPCID = U.UPCID
	JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
	JOIN Locations L ON T.Establishment = L.POSAlias
	JOIN Pours P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
where 1 = 1 
	AND P.Volume > ( SP.PourStandard - (SP.PourStandard * SP.StandardVariance ))
	AND P.Volume < ( SP.PourStandard + (SP.PourStandard * SP.StandardVariance ))
	AND P.PourTime < DATEADD( MINUTE, @Interval, T.TicketDate )
	AND P.PourTime > DATEADD( MINUTE, -1 * @Interval, T.TicketDate )
	AND P.POSTicketItemID IS NULL
	AND TI.Status <> 4
	
union
-- By Manufacturer
--select TI.POSTicketItemID 
--	, U.Name
--	, A.Description
--	, R.Description
--	, P.Volume + P2.Volume AS Volume
--	, SP.PourStandard6
--	, SP.StandardVariance
--	, L.Name
--	, T.TicketDate
--	, P.PourTime
----	,* 
--from POSTicketItems TI
--	JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
--	JOIN TicketItemAliases A ON TI.Description = A.Description
--	JOIN Recipe R ON A.RecipeID = R.RecipeID
--	JOIN Ingredients I ON A.RecipeID = I.RecipeID
--	JOIN StandardPours SP ON I.StandardPourID = SP.StandardPourID
--	JOIN UPCs U ON I.ManufacturerID = U.ManufacturerID AND I.Quality = U.Quality AND U.ChildUPCID IS NULL
--	JOIN Locations L ON T.Establishment = L.POSAlias
--	JOIN Pours P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
--	JOIN Pours P2 ON P.TagID = P2.TagID AND P.PourID <> P2.PourID AND U.UPCID = P2.UPCID
--where 1 = 1 
--	AND P.Volume + P2.Volume > ( SP.PourStandard - (SP.PourStandard * SP.StandardVariance ))
--	AND P.Volume + P2.Volume < ( SP.PourStandard + (SP.PourStandard * SP.StandardVariance ))
--	AND P.PourTime < DATEADD( MINUTE, 15, T.TicketDate )
--	AND P.PourTime > DATEADD( MINUTE, -15, T.TicketDate )
--	AND DATEADD( SECOND, 6, P2.PourTime ) > P.PourTime
--	AND P2.PourTime < P.PourTime
--	AND P.LocationID = P2.LocationID

--union

-- OVER/UNDER POURS
-- By Manufacturer
select TI.POSTicketItemID 
	, U.Name
	, A.Description
	, R.Description
	, P.Volume
	, SP.PourStandard
	, SP.StandardVariance
	, L.Name
	, T.TicketDate
	, P.PourTime
from POSTicketItems TI
	JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
	JOIN TicketItemAliases A ON TI.Description = A.Description
	JOIN Recipe R ON A.RecipeID = R.RecipeID
	JOIN Ingredients I ON A.RecipeID = I.RecipeID
	JOIN StandardPours SP ON I.StandardPourID = SP.StandardPourID
	JOIN UPCs U ON I.ManufacturerID = U.ManufacturerID AND I.Quality = U.Quality AND U.ChildUPCID IS NULL
	JOIN Locations L ON T.Establishment = L.POSAlias
	JOIN Pours P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
where 1 = 1 
	AND ( P.Volume > ( SP.PourStandard - (SP.PourStandard * SP.StandardVariance )) OR
		P.Volume < ( SP.PourStandard + (SP.PourStandard * SP.StandardVariance )) )
	AND P.PourTime < DATEADD( MINUTE, @Interval, T.TicketDate )
	AND P.PourTime > DATEADD( MINUTE, -1 * @Interval, T.TicketDate )
	AND P.POSTicketItemID IS NULL
	AND TI.Status <> 4

UNION
-- By Category
select TI.POSTicketItemID 
	, U.Name
	, A.Description
	, R.Description
	, P.Volume
	, SP.PourStandard
	, SP.StandardVariance
	, L.Name
	, T.TicketDate
	, P.PourTime
from POSTicketItems TI
	JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
	JOIN TicketItemAliases A ON TI.Description = A.Description
	JOIN Recipe R ON A.RecipeID = R.RecipeID
	JOIN Ingredients I ON A.RecipeID = I.RecipeID
	JOIN StandardPours SP ON I.StandardPourID = SP.StandardPourID
	JOIN UPCs U ON I.CategoryID = U.CategoryID AND I.Quality = U.Quality AND U.ChildUPCID IS NULL
	JOIN Locations L ON T.Establishment = L.POSAlias
	JOIN Pours P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
where 1 = 1 
	AND ( P.Volume > ( SP.PourStandard - (SP.PourStandard * SP.StandardVariance )) OR
		P.Volume < ( SP.PourStandard + (SP.PourStandard * SP.StandardVariance )) )
	AND P.PourTime < DATEADD( MINUTE, @Interval, T.TicketDate )
	AND P.PourTime > DATEADD( MINUTE, -1 * @Interval, T.TicketDate )
	AND P.POSTicketItemID IS NULL
	AND TI.Status <> 4

UNION
-- By UPC
select TI.POSTicketItemID 
	, U.Name
	, A.Description
	, R.Description
	, P.Volume
	, SP.PourStandard
	, SP.StandardVariance
	, L.Name
	, T.TicketDate
	, P.PourTime
--	,* 
from POSTicketItems TI
	JOIN TicketItemAliases A ON TI.Description = A.Description
	JOIN Ingredients I ON A.RecipeID = I.RecipeID
	JOIN StandardPours SP ON I.StandardPourID = SP.StandardPourID
	JOIN Recipe R ON A.RecipeID = R.RecipeID
	JOIN UPCs U ON I.UPCID = U.UPCID
	JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
	JOIN Locations L ON T.Establishment = L.POSAlias
	JOIN Pours P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
where 1 = 1 
	AND ( P.Volume > ( SP.PourStandard - (SP.PourStandard * SP.StandardVariance )) OR
		P.Volume < ( SP.PourStandard + (SP.PourStandard * SP.StandardVariance )) )
	AND P.PourTime < DATEADD( MINUTE, @Interval, T.TicketDate )
	AND P.PourTime > DATEADD( MINUTE, -1 * @Interval, T.TicketDate )
	AND P.POSTicketItemID IS NULL
	AND TI.Status <> 4



order by P.PourTime




--select P.PourTime, P2.PourTime, P.Volume, P2.Volume, P.Volume + P2.Volume, U.Name, U2.Name
--from Pours P
--	JOIN Pours P2 ON P.TagID = P2.TagID AND P.PourID <> P2.PourID
--	JOIN UPCs U ON P.UPCID = U.UPCID
--	JOIN UPCs U2 ON P2.UPCID = U2.UPCID
--WHERE 1=1 
--	AND DATEADD( SECOND, 6, P2.PourTime ) > P.PourTime
--	AND P2.PourTime < P.PourTime
--	AND P.LocationID = P2.LocationID
--order by p.PourTime, P.PourID
