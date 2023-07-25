UPDATE UPCs SET Size = 750 WHERE RootCategoryID = '4E7F65BB-34B5-49F5-9AFB-5789EC439275' and Size = 0
UPDATE UPCs SET Size = 1000 WHERE RootCategoryID = '5285A1ED-1782-43B6-93EE-DF3EDD1EC8C6' and Size = 0
UPDATE UPCs SET Size = 354.88235476934 WHERE RootCategoryID = '7A1344CB-6762-400A-874D-D68C80BAB93C' and Size = 0

DELETE FROM ReportParameters 
DELETE FROM Reports 

DELETE FROM Reports WHERE Name like 'Par Levels'
GO

DROP FUNCTION dbo.StatusName 
GO

CREATE FUNCTION dbo.StatusName 
(
	-- Add the parameters for the function here
	@Status int
)
RETURNS nvarchar(256)
AS
BEGIN
	--Status = 4 THEN ''Matches''
	--Status = 2 THEN ''Unmatched''
	--Status = 8 THEN ''Void''
	--Status = 64 THEN ''Under pour''
	--Status = 128 THEN ''Over pour''
	--Status = 256 THEN ''Substitution''
	--Status = 512 THEN ''Topoff''
	DECLARE @rc NVARCHAR(256)
	select @rc = ''

	if( @Status & 2 = 2 )
		select @rc = @rc + 'Unmatched/'
	if( @Status & 4 = 4 )
		select @rc = @rc + 'Matched Exactly/'
	if( @Status & 8 = 8 )
		select @rc = @rc + 'Void/'
	if( @Status & 64 = 64 )
		select @rc = @rc + 'Matched - Under pour/'
	if( @Status & 128 = 128 )
		select @rc = @rc + 'Matched - Over pour/'
	if( @Status & 256 = 256 )
		select @rc = @rc + 'Matched - Substitution/'
	if( @Status & 512 = 512 )
		select @rc =  @rc + 'Matched - Topoff/'
	
	return  SUBSTRING( @rc, 1, LEN( @rc ) - 1 )

END
GO



-----------------------------------------------------------------------------------------------------------------------------------------------
-- Cost Analysis
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[rptCostAnalysis]
GO
CREATE VIEW [dbo].[rptCostAnalysis] AS
SELECT P.PourTime, U.Name, T.TagNumber, 
	dbo.ConvertVolume(SPour.PourStandard) AS PourStandard, dbo.ConvertVolume(P.Volume) AS Volume,
	CASE WHEN P.Volume > (SPour.PourStandard + (SPour.StandardVariance * SPour.PourStandard))
	THEN 'Over Pour' WHEN P.Volume < (SPour.PourStandard - (SPour.StandardVariance * SPour.PourStandard))
	THEN 'Under Pour' ELSE 'Single Pour' END AS SinglePourType, 
	CAST( ROUND( SP.SinglePrice, U.UnitPrice / U.Size * SPour.PourStandard, 2) AS DECIMAL ) AS IdealCost,
	ROUND( U.UnitPrice / U.Size * P.Volume, 2) AS PourCost, ROUND( SP.SinglePrice - U.UnitPrice / U.Size * P.Volume, 2 ) AS ActualProfit,
	ROUND( SP.SinglePrice / SPour.PourStandard * P.Volume, 2) AS TheoreticalProfit, ROUND( SP.SinglePrice - SP.SinglePrice / SPour.PourStandard * P.Volume, 2) AS LostProfit, U.Size,
	C.Name AS Category, U.ItemNumber 
FROM
	dbo.Pours AS P INNER JOIN
	dbo.Tags AS T ON P.TagID = T.TagID INNER JOIN
	dbo.Inventories I ON T.TagID = I.TagID AND I.ExitDate IS NULL INNER JOIN
	dbo.UPCs AS U ON I.UPCID = U.UPCID INNER JOIN
	dbo.Categories AS C ON U.RootCategoryID = C.CategoryID LEFT OUTER JOIN
	dbo.StandardPrices AS SP ON U.StandardPriceID = SP.StandardPriceID INNER JOIN
	dbo.StandardPours AS SPour ON C.CategoryID = SPour.CategoryID 
WHERE     (U.UnitPrice IS NOT NULL) AND SinglePrice IS NOT NULL 
 
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Cost Analysis'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM rptCostAnalysis ORDER BY PourTime DESC', null, null, 0, 0



-----------------------------------------------------------------------------------------------------------------------------------------------
-- Disconnect With Volume
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[rptDisconnectWithVolume]
GO
CREATE VIEW [dbo].[rptDisconnectWithVolume] AS
SELECT L.Name, A.Message, A.AlertTime 
FROM TagAlerts A 
	JOIN Tags T ON A.TagID = T.TagID 
	JOIN Locations L ON A.LocationID = L.LocationID 
WHERE AlertType = 127 

GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DROP PROC [dbo].[procGetDisconnectWithVolume]
GO

CREATE PROCEDURE [dbo].[procGetDisconnectWithVolume]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM [rptDisconnectWithVolume] WHERE AlertTime <= @EndTime AND AlertTime >= @StartTime
	ORDER BY AlertTime Desc
END
GO

-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Disconnect With Volume'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'ALT-Disconnect With Volume'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, '[procGetDisconnectWithVolume]', null, null, 1, 1


-----------------------------------------------------------------------------------------------------------------------------------------------
-- Full Bottle Inventory 
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwFullBottleInventory]
GO
CREATE VIEW [dbo].[vwFullBottleInventory]
AS
select U.ItemNumber
	, U.Name AS UPCName
	, COUNT(1) AS BottleCount
	, COUNT(T.TagID) AS TaggedBottles
	, ROUND( AVG(I.Amount), 2 ) AS Quantity
	, L.Name AS Location
	, ROUND(AVG(I.Cost),2) AS BottleCost
	, CASE 
		WHEN MIN( U.Size ) <> 0 THEN ROUND(SUM( I.Cost/ U.Size * I.Amount ), 2 ) 
		ELSE SUM ( 0 )
		END AS TotalCost 
	, C.Name AS Category --, I.Cost, U.Size, I.Amount
from Inventories I 
	JOIN UPCs U ON I.UPCID = U.UPCID
	JOIN Locations L ON I.LocationID = L.LocationID
	JOIN Categories C ON U.RootCategoryID = C.CategoryID
	LEFT JOIN Tags T ON I.TagID = T.TagID
WHERE I.ExitDate IS NULL
	AND I.Amount = U.Size
GROUP BY U.ItemNumber, U.Name, L.Name, C.Name

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Full Bottle Inventory'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Full Bottle Inventory'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM vwFullBottleInventory', null, null, 0, 1
GO


-----------------------------------------------------------------------------------------------------------------------------------------------
-- Inventory
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[rptInventory]
GO

CREATE VIEW [dbo].[rptInventory]
AS
SELECT     Data.Name, ROUND(SUM(Data.StockQuantity), 2) AS Stock
	, ROUND(SUM(Data.TagQuantity), 2) AS Tagged
	, ROUND(SUM(Data.StockQuantity), 2) + ROUND(SUM(Data.TagQuantity), 2) AS TotalQuantity
	, U.MinimumParLevel
	, dbo.ConvertVolume(SUM(Data.TotalVolume)) AS TotalVolume
	, ROUND( U.UnitPrice, 2 ) AS AverageBottleCost -- needs to average cost from actual Inventory
	--, SUM(Data.StockQuantity) + SUM(Data.TagQuantity) * U.UnitPrice AS TotalBottleCost
	, SUM(Data.TotalVolume) * (U.UnitPrice / AVG(U.Size)) AS TotalInventoryCost  -- needs to 
	, C.Name AS Category 
FROM         
(
	SELECT     U.UPCID, U.Name, COUNT(1) AS StockQuantity, 0 AS TagQuantity, MAX(U.MinimumParLevel) AS MinimumParLevel, SUM(I.Amount) AS TotalVolume
	FROM          dbo.Inventories AS I INNER JOIN
		dbo.UPCs AS U ON I.UPCID = U.UPCID
	WHERE I.TagID IS NULL
	GROUP BY U.UPCID, U.Name
		UNION
	SELECT     U.UPCID, U.Name, 0 AS StockQuantity, COUNT(1) AS TagQuantity, MAX(U.MinimumParLevel) AS MinimumParLevel, SUM(I.Amount) AS TotalVolume
	FROM         dbo.Tags AS T 
		JOIN dbo.Inventories I on T.TagID = I.TagID
		JOIN dbo.UPCs AS U ON I.UPCID = U.UPCID
	WHERE     (I.UPCID <> '00000000-0000-0000-0000-000000000000' AND I.ExitDate IS NULL)
	GROUP BY U.UPCID, U.Name
) AS Data 
	JOIN dbo.UPCs AS U ON Data.UPCID = U.UPCID 
	JOIN dbo.Categories AS C ON U.RootCategoryID = C.CategoryID 
	LEFT JOIN dbo.StandardPrices AS P ON U.StandardPriceID = P.StandardPriceID 
GROUP BY Data.Name, U.MinimumParLevel, U.UnitPrice, P.SinglePrice, C.Name 

GO


DROP INDEX inx_InventoryView ON dbo.Inventories
GO

CREATE NONCLUSTERED INDEX inx_InventoryView
ON [dbo].[Inventories] ([ExitDate],[UPCID])

GO

-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Inventory'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Inventory Detail'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM rptInventory ORDER BY Name ', null, null, 0, 1

-----------------------------------------------------------------------------------------------------------------------------------------------
-- Inventory Movement
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP PROC [dbo].[procGetInventoryMovement]
GO

CREATE PROCEDURE [dbo].[procGetInventoryMovement]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	SELECT T.TagNumber, U.Name, O.Name [From], D.Name [To], COUNT( 1 ) BottleCount, MAX(M.MoveTime) [Time]
	FROM Moves M
		JOIN Inventories I ON M.InventoryID = I.InventoryID
		JOIN UPCs U ON I.UPCID = U.UPCID
		JOIN Locations O ON M.OldLocation = O.LocationID
		JOIN Locations D ON M.NewLocation = D.LocationID
		LEFT JOIN Tags T ON I.TagID = T.TagID
	WHERE M.MoveTime >= @StartTime AND M.MoveTime <= @EndTime
	GROUP BY T.TagNumber, U.Name, O.Name, D.Name
	UNION

	SELECT null, U.Name, 'New Inventory', L.Name [To], COUNT( 1 ) BottleCount, MAX(I.EnterDate) [Time]
	FROM Inventories I 
		JOIN UPCs U ON I.UPCID = U.UPCID
		JOIN Locations L ON I.LocationID = L.LocationID
	WHERE I.InventoryID NOT IN 
	(SELECT M.InventoryID FROM Moves M)
		AND I.EnterDate >= @StartTime AND I.EnterDate <= @EndTime
	GROUP BY U.Name, L.Name
	ORDER BY [Time] 

END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Inventory Movement'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Inventory Movement'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetInventoryMovement', null, null, 1, 1


-----------------------------------------------------------------------------------------------------------------------------------------------
-- Par Level Issues
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwParLevelIssues]
GO
CREATE VIEW [dbo].[vwParLevelIssues]
AS
SELECT *
	, CASE 
		WHEN ParLevel < BottleCount THEN 'OVER'
		WHEN ParLevel > BottleCount THEN 'UNDER'
	  END AS [Over/Under]
	, ABS( ParLevel - BottleCount ) AS OffBy
FROM (	
SELECT L.Name AS Location, R.Name AS Type, C.Name AS Category, U.Name AS UPCName, M.Name AS Manufacturer, P.BottleCount as ParLevel, COUNT( 1 ) [BottleCount] 
FROM Tags T 
	JOIN Inventories I ON T.TagID = I.TagID AND I.ExitDate IS NULL 
	JOIN Locations L ON T.LocationID = L.LocationID 
	JOIN UPCs U ON I.UPCID = U.UPCID 
	JOIN ParLevels P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID
	JOIN Manufacturers M ON U.ManufacturerID = M.ManufacturerID 
	LEFT JOIN dbo.Categories AS C ON U.CategoryID = C.CategoryID 
	LEFT JOIN dbo.Categories AS R ON U.RootCategoryID = R.CategoryID 
WHERE I.Amount = U.Size
GROUP BY L.Name, R.Name, C.Name, U.Name, M.Name, P.BottleCount ) AS A
WHERE ParLevel > 0 AND BottleCount <> ParLevel

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Par Level Issues'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Par Level Issues'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM vwParLevelIssues', null, null, 0, 1
GO

-----------------------------------------------------------------------------------------------------------------------------------------------
-- Par Level Counts
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwParCounts]
GO

CREATE VIEW [dbo].[vwParCounts]
AS
SELECT     L.Name AS Location, R.Name AS Type, C.Name AS Category, U.Name, M.Name AS Manufacturer, ISNULL(P.BottleCount, 0) AS ParLevel, COUNT(1) AS BottleCount
FROM         dbo.Tags AS T INNER JOIN
                      dbo.Inventories AS I ON T.TagID = I.TagID AND I.ExitDate IS NULL INNER JOIN
                      dbo.Locations AS L ON T.LocationID = L.LocationID INNER JOIN
                      dbo.UPCs AS U ON I.UPCID = U.UPCID INNER JOIN
                      dbo.Manufacturers AS M ON U.ManufacturerID = M.ManufacturerID LEFT OUTER JOIN
                      dbo.ParLevels AS P ON U.UPCID = P.UPCID AND L.LocationID = P.LocationID LEFT OUTER JOIN
                      dbo.Categories AS C ON U.CategoryID = C.CategoryID LEFT OUTER JOIN
                      dbo.Categories AS R ON U.RootCategoryID = R.CategoryID
WHERE     (I.Amount > 0)
GROUP BY L.Name, R.Name, C.Name, U.Name, M.Name, P.BottleCount
 
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Par Level Counts'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Par Level Counts'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM vwParCounts', null, null, 0, 1



-----------------------------------------------------------------------------------------------------------------------------------------------
-- Partial Bottle Inventory
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwPartialBottleInventory]
GO
CREATE VIEW [dbo].[vwPartialBottleInventory]
AS 
select U.ItemNumber, U.Name AS UPCName, T.TagNumber, U.Size
	, CASE 
		WHEN I.Amount > 0 THEN ROUND(I.Amount, 2 ) 
		ELSE 0
	  END AS Quantity
	, L.Name AS Location
	, ROUND(I.Cost, 2) AS [BottleCost ($)]
	, CASE 
		WHEN I.Amount > 0 THEN ROUND( I.Cost/U.Size * I.Amount, 2 )
		ELSE 0
	  END AS [Partial Inventory ($)]
	, C.Name AS Category --, I.Cost, U.Size, I.Amount
from Inventories I 
	JOIN UPCs U ON I.UPCID = U.UPCID
	JOIN Locations L ON I.LocationID = L.LocationID
	JOIN Categories C ON U.RootCategoryID = C.CategoryID
	JOIN Tags T ON T.TagID = I.TagID
WHERE I.ExitDate IS NULL
	AND I.Amount <> U.Size

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Partial Bottle Inventory'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Partial Bottle Inventory'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM vwPartialBottleInventory', null, null, 0, 1
GO


-----------------------------------------------------------------------------------------------------------------------------------------------
-- Pours
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwPours]
GO
CREATE VIEW [dbo].[vwPours]
AS 
select L.Name AS [Location Name], T.TagNumber, dbo.ConvertVolume( P.Volume ) as Volume
	, U.Name, dbo.StatusName( P.Status ) AS Status, CONVERT( nvarchar(64), P.PourTime, 101 ) + ' ' + CONVERT( nvarchar(64), P.PourTime, 108 ) AS PourTime 
FROM Pours P 
	JOIN Tags T ON P.TagID = T.TagID 
	JOIN UPCs U ON P.UPCID = U.UPCID 
	JOIN Locations L ON P.LocationID = L.LocationID
	
GO

-----------------------------------------------------------------------------------------------------------------------------------------------

DROP PROC [dbo].[procGetPours]
GO

CREATE PROCEDURE [dbo].[procGetPours]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM vwPours WHERE PourTime <= @EndTime AND PourTime >= @StartTime
END
GO

-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Pours'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Pours'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetPours', null, null, 1, 1
GO

-----------------------------------------------------------------------------------------------------------------------------------------------
-- Reader List
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwReaders]
GO
CREATE VIEW [dbo].[vwReaders]
AS 

SELECT D.HardwareID, D.Name, D.Description, L.Name AS Location 
FROM Devices D
	LEFT JOIN Locations L ON D.DeviceID = L.DeviceID
WHERE D.DeviceID <> '00000000-0000-0000-0000-000000000000'

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Reader List'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'ADM-Reader List'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM vwReaders', null, null, 0, 1
GO

-----------------------------------------------------------------------------------------------------------------------------------------------
-- Summary by Group
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[rptSummaryByGroup]
GO
CREATE VIEW [dbo].[rptSummaryByGroup]
AS
SELECT     TOP (100) PERCENT Category, COUNT(1) AS NumberOfPours
	, ROUND(AVG(PourStandard),2) AS PourStandard, dbo.ConvertVolume(AVG(Volume)) AS AveragePour
	, dbo.ConvertVolume(MIN(Volume)) AS MinPour, dbo.ConvertVolume(MAX(Volume)) AS MaxPour
	, dbo.ConvertVolume(SUM(Volume)) AS TotalVolume, ROUND(SUM(IdealCost),2) AS IdealCostTotal
	, ROUND(SUM(PourCost),2) AS TotalPourCost, ROUND(SUM(IdealCost) - SUM(PourCost),2) AS TotalProfit
	, ROUND(SUM(IdealCost),2) AS TotalSales, ROUND(1 - SUM(PourCost) / SUM(IdealCost),2) AS Average
FROM         dbo.rptCostAnalysis
GROUP BY Category

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Summary by Group'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Summary by Group'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM rptSummaryByGroup', null, null, 0, 1
GO


-----------------------------------------------------------------------------------------------------------------------------------------------
-- Summary by Tag
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[rptSummaryByTag]
GO
CREATE VIEW [dbo].[rptSummaryByTag]
AS
SELECT     TOP (100) PERCENT Name, TagNumber, COUNT(1) AS NumberOfPours
	, dbo.ConvertVolume(AVG(PourStandard)) AS PourStandard, dbo.ConvertVolume(AVG(Volume)) AS AveragePour
	, dbo.ConvertVolume(MIN(Volume)) AS MinPour, dbo.ConvertVolume(MAX(Volume)) AS MaxPour
	, dbo.ConvertVolume(SUM(Volume)) AS TotalVolume, ROUND(SUM(IdealCost),2) AS IdealCostTotal
	, ROUND(SUM(PourCost),2) AS TotalPourCost, ROUND(SUM(IdealCost) - SUM(PourCost),2) AS TotalProfit
	, ROUND(SUM(IdealCost),2) AS TotalSales, ROUND(1 - SUM(PourCost) / SUM(IdealCost),2) AS Average
FROM         dbo.rptCostAnalysis
GROUP BY Name, TagNumber

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Summary by Tag'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Summary by Tag'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM rptSummaryByTag', null, null, 0, 1
GO



-----------------------------------------------------------------------------------------------------------------------------------------------
-- Summary by UPC
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[rptSummaryByUPC]
GO
CREATE VIEW [dbo].[rptSummaryByUPC]
AS
SELECT     TOP (100) PERCENT ItemNumber, Name, COUNT(1) AS NumberOfPours, AVG(PourStandard) AS PourStandard
	, dbo.ConvertVolume( AVG(Volume)) AS AveragePour, dbo.ConvertVolume( MIN(Volume)) AS MinPour
	, dbo.ConvertVolume( MAX(Volume)) AS MaxPour, dbo.ConvertVolume( SUM(Volume)) AS TotalVolume
	, ROUND( SUM(IdealCost), 2) AS IdealCostTotal, SUM(PourCost) AS TotalPourCost
	, ROUND( SUM(IdealCost) - SUM(PourCost), 2) AS TotalProfit
	, ROUND( SUM(IdealCost), 2) AS TotalSales, ROUND( 1 - SUM(PourCost) / SUM(IdealCost),2) AS Average
FROM dbo.rptCostAnalysis
WHERE IdealCost > 0
GROUP BY Name, ItemNumber

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Summary by UPC'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Summary by UPC'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM rptSummaryByUPC', null, null, 0, 1
GO

-----------------------------------------------------------------------------------------------------------------------------------------------
-- Tagged Inventory
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[rptTaggedInventory]
GO
CREATE VIEW [dbo].[rptTaggedInventory]
AS
SELECT 
	L.Name as Location
	, M.Name AS Manufacturer
	, U.Name as 'UPC Name'
	, T.TagNumber 
	, CASE 
		WHEN I.Amount < 0 THEN 0
		ELSE ROUND(I.Amount, 2 ) --dbo.ConvertVolume(I.Amount) 
	  END AS Quantity		
	, ROUND( dbo.[NozzleDiameter](COALESCE( N.Length, UN.Length, CN.Length, RN.Length, SN.Length)
	, COALESCE( N.Width, UN.Width, CN.Width, RN.Width, SN.Width )
	, COALESCE( N.Shape, UN.Shape, CN.Shape, RN.Shape, SN.Shape ) ), 3, 0 )AS NozzleArea
FROM Tags T 
	JOIN Inventories I ON T.TagID = I.TagID AND I.ExitDate IS NULL 
	JOIN Locations L ON I.LocationID = L.LocationID 
	JOIN UPCs U ON I.UPCID = U.UPCID 
	LEFT JOIN dbo.Categories AS C ON U.CategoryID = C.CategoryID 
	JOIN Manufacturers M ON U.ManufacturerID = M.ManufacturerID 
	LEFT JOIN dbo.Categories AS R ON U.RootCategoryID = R.CategoryID 
	LEFT JOIN StandardNozzles N ON T.StandardNozzleID = N.StandardNozzleID 
	LEFT JOIN StandardNozzles UN ON T.StandardNozzleID = UN.StandardNozzleID 
	LEFT JOIN StandardNozzles CN ON C.StandardNozzleID = CN.StandardNozzleID 
	LEFT JOIN StandardNozzles RN ON R.StandardNozzleID = RN.StandardNozzleID 
	, StandardNozzles SN 
WHERE SN.StandardNozzleID = '00000000-0000-0000-0000-000000000000'

GO
-----------------------------------------------------------------------------------------------------------------------------------------------
DECLARE @Name NVARCHAR(512), @ReportID UNIQUEIDENTIFIER
SELECT @Name = 'Tagged Inventory'
SELECT TOP 1 @ReportID = ReportID
FROM Reports 
WHERE Name like 'Tagged Inventory'

DELETE FROM ReportParameters where ReportID = @ReportID
DELETE FROM Reports WHERE Name like @Name

DECLARE @Name NVARCHAR(512), @ReportID UNIQUEIDENTIFIER
SELECT @Name = 'INV-Tagged Inventory'
SELECT TOP 1 @ReportID = ReportID
FROM Reports 
WHERE Name like 'INV-Tagged Inventory'

DELETE FROM ReportParameters where ReportID = @ReportID
DELETE FROM Reports WHERE Name like @Name

SELECT @ReportID = NEWID()
insert into Reports
SELECT @ReportID, @Name, @Name, 'SELECT * FROM rptTaggedInventory', null, null, 0, 1

INSERT INTO ReportParameters 
SELECT NEWID( ), @ReportID, 'Quantity', 'string', 'Quantity', '0'
GO
-----------------------------------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------------------------------------------
-- POS Pour Matches
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP PROC [dbo].[procGetPOSPourMatches]
GO

CREATE PROCEDURE [dbo].[procGetPOSPourMatches]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	--Total Reconciled, unreconciled, and pours
	select 'Matched' AS Status, COUNT(1) AS Count from Pours where Status <> 2 AND PourTime > @StartTime AND PourTime <= @EndTime
	union
	select 'Unmatched' AS Status, COUNT(1) AS Count from Pours where Status = 2 AND PourTime > @StartTime AND PourTime <= @EndTime
	union
	select 'Total Pours' AS Status, COUNT(1) AS Count from Pours WHERE PourTime > @StartTime AND PourTime <= @EndTime

END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'POS-Pour Matches'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetPOSPourMatches', null, null, 1, 1


-----------------------------------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------------------------------------------
-- POS Pour Matches
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP PROC [dbo].[procGetPOSStatusCounts]
GO

CREATE PROCEDURE [dbo].[procGetPOSStatusCounts]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	select 
		CASE 
			WHEN Status = 4 THEN 'Matched Exactly'
			WHEN Status = 2 THEN 'Unmatched'
			WHEN Status = 8 THEN 'Matched - Void'
			WHEN Status = 64 THEN 'Matched - Under pour'
			WHEN Status = 128 THEN 'Matched - Over pour'
			WHEN Status = 256 THEN 'Matched - Substitution'
			WHEN Status = 512 THEN 'Matched - Topoff'
			ELSE CAST( Status AS NVARCHAR(256) )
		END AS PourStatus
		, COUNT( 1 ) AS Count
	from Pours 
	WHERE PourTime > @StartTime AND PourTime <= @EndTime
	GROUP BY Status

END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'POS-Pour Status Counts'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetPOSStatusCounts', null, null, 1, 1



-----------------------------------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------------------------------------------
-- POS Pour Matches
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP PROC [dbo].[procGetPOSMissingAliases]
GO

CREATE PROCEDURE [dbo].[procGetPOSMissingAliases]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	select TI.Description AS [POS Ticket Item], COUNT(1 ) AS Count
	from TicketItemAliases TIA
		RIGHT JOIN POSTicketItems TI ON TIA.Description = TI.Description
		JOIN POSTickets T ON TI.POSTicketID = T.POSTicketID
	WHERE TIA.Description IS NULL 
		AND  T.TicketDate > @StartTime AND T.TicketDate <= @EndTime
	GROUP BY TI.Description
	ORDER BY COUNT( 1 ) DESC



END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'POS-Missing Aliases'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetPOSMissingAliases', null, null, 1, 1




-----------------------------------------------------------------------------------------------------------------------------------------------


-----------------------------------------------------------------------------------------------------------------------------------------------
-- POS Pour Matches
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP PROC [dbo].[procGetPOSMissingTickets]
GO

CREATE PROCEDURE [dbo].[procGetPOSMissingTickets]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	-- pours with no tickets associated
	select L.Name AS Location
		, U.Name AS Name
		-- , COUNT( 1 ) AS Pours
		, dbo.ConvertVolume( SUM( P.Volume ) ) AS Volume
	from Pours P
		JOIN UPCs U ON P.UPCID = U.UPCID
		JOIN Locations L ON P.LocationID = L.LocationID
	WHERE P.POSTicketItemID IS NULL 
		AND PourTime > @StartTime AND PourTime <= @EndTime
	GROUP BY L.Name, U.Name
	order by L.Name, Volume DESC
--	order by L.Name, Pours DESC



END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
-- Renamed the report
SELECT @Name = 'POS Missing Tickets'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'POS Pours without matching Ticket'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'POS-Pours without matching Ticket'
DELETE FROM Reports WHERE Name like @Name

SELECT @Name = 'POS-Pours without matching POS Ticket'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetPOSMissingTickets', null, null, 1, 1

-----------------------------------------------------------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------------------------------------------------------
-- POS Tickets
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP PROC [dbo].[procGetPOSTicketItems]
GO

CREATE PROCEDURE [dbo].[procGetPOSTicketItems]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	-- pours with no tickets associated
	select T.CheckNumber
		, TI.Description
		, TI.Quantity
		, dbo.StatusName( Status ) AS Status
		, T.TicketDate
	from POSTickets T
		JOIN POSTicketItems TI ON T.POSTicketID = TI.POSTicketID
	WHERE T.TicketDate > @StartTime AND T.TicketDate <= @EndTime
	ORDER BY T.TicketDate



END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'POS-Tickets'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetPOSTicketItems', null, null, 1, 1


-----------------------------------------------------------------------------------------------------------------------------------------------
-- Quantity by Item
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwItemsCategoryQuantity]
GO

CREATE VIEW [dbo].[vwItemsCategoryQuantity]
AS
SELECT L.Name AS LocationName, C.Name AS Category, S.Name as Subcategory, U.ItemNumber, U.Name, COUNT(I.InventoryID) - COUNT(T .TagID) AS StockQuantity, 
     COUNT(T .TagID) AS TaggedQuantity, COUNT(I.InventoryID) AS TotalQuantity
FROM dbo.Inventories AS I INNER JOIN
     dbo.UPCs AS U ON I.UPCID = U.UPCID INNER JOIN
     dbo.Locations AS L ON I.LocationID = L.LocationID LEFT OUTER JOIN
     dbo.Tags AS T ON I.TagID = T .TagID INNER JOIN
     dbo.Categories C ON U.RootCategoryID = C.CategoryID INNER JOIN
     dbo.Categories S ON U.CategoryID = S.CategoryID
WHERE (I.ExitDate IS NULL)
GROUP BY U.ItemNumber, U.Name, L.Name, C.Name, S.Name

GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Quantity by Category'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Quantity by Category'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM vwItemsCategoryQuantity', null, null, 0, 1




-----------------------------------------------------------------------------------------------------------------------------------------------
-- Missing Inventory
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP VIEW [dbo].[vwMissingInventory]
GO

CREATE VIEW [dbo].[vwMissingInventory]
AS

select U.Name, max( I.ExitDate) AS ExitDate
from Inventories I 
       JOIN UPCs U ON I.UPCID = U.UPCID
GROUP BY U.Name, I.UPCID HAVING SUM(
       CASE 
              WHEN I.ExitDate IS NOT NULL THEN 0
              ELSE 1
       END ) = 0
--ORDER BY ExitDate DESC

GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'Missing Inventory'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Missing Inventory'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-Out of Stock Items'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'SELECT * FROM vwMissingInventory', null, null, 0, 1


-----------------------------------------------------------------------------------------------------------------------------------------------
-- Get Highest Volume
-----------------------------------------------------------------------------------------------------------------------------------------------
DROP PROC [dbo].[procGetHighestVolume]
GO

CREATE PROCEDURE [dbo].[procGetHighestVolume]
	@StartTime DATETIME, 
	@EndTime DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT CategoryName, Name, ROUND( dbo.ConvertVolume( Volume), 2) 
FROM (

select TOP 5 C.Name AS CategoryName,  U.Name, SUM( P.Volume) AS Volume
from Pours P 
	JOIN Inventories I ON P.TagID = I.TagID
	JOIN UPCs U ON I.UPCID = U.UPCID
	JOIN Categories C ON U.RootCategoryID = C.CategoryID
WHERE U.RootCategoryID = '5285A1ED-1782-43B6-93EE-DF3EDD1EC8C6'
	AND PourTime <= @EndTime AND PourTime >= @StartTime
GROUP BY C.Name, U.Name

UNION

select TOP 5 C.Name AS CategoryName,  U.Name, SUM( P.Volume) AS Volume
from Pours P 
	JOIN Inventories I ON P.TagID = I.TagID
	JOIN UPCs U ON I.UPCID = U.UPCID
	JOIN Categories C ON U.RootCategoryID = C.CategoryID
WHERE U.RootCategoryID = '4E7F65BB-34B5-49F5-9AFB-5789EC439275'
	AND PourTime <= @EndTime AND PourTime >= @StartTime
GROUP BY C.Name, U.Name

UNION

select TOP 5 C.Name AS CategoryName,  U.Name, SUM( P.Volume) AS Volume
from Pours P 
	JOIN Inventories I ON P.TagID = I.TagID
	JOIN UPCs U ON I.UPCID = U.UPCID
	JOIN Categories C ON U.RootCategoryID = C.CategoryID
WHERE U.RootCategoryID = '7A1344CB-6762-400A-874D-D68C80BAB93C'
	AND PourTime <= @EndTime AND PourTime >= @StartTime
GROUP BY C.Name, U.Name ) T

ORDER BY CategoryName, Volume DESC

END

GO
-----------------------------------------------------------------------------------------------------------------------------------------------

DECLARE @Name NVARCHAR(512)
SELECT @Name = 'High Volume'
DELETE FROM Reports WHERE Name like @Name
SELECT @Name = 'INV-High Volume'
DELETE FROM Reports WHERE Name like @Name
insert into Reports
SELECT NEWID(), @Name, @Name, 'procGetHighestVolume', null, null, 1, 1










-------------------------------------------------------------------------------------------------------------------------------------------------
---- Missing Inventory
-------------------------------------------------------------------------------------------------------------------------------------------------
--DROP VIEW [dbo].[vwMissingInventory]
--GO

--CREATE VIEW [dbo].[vwMissingInventory]
--AS

--SELECT * FROM Inventories WHERE ExitDate = NULL AND TagID IN (
--       SELECT T.TagID 
--       FROM (SELECT a.TagID, a.LocationID FROM TagActivities a
--                WHERE ActivityTime = 
--                     (SELECT MAX(ActivityTime)
--                     FROM TagActivities
--                     WHERE TagID = a.TagID)) AS LA
--              JOIN Tags T ON LA.TagID = T.TagID AND LA.LocationID != T.LocationID )


--GO
-------------------------------------------------------------------------------------------------------------------------------------------------

--DECLARE @Name NVARCHAR(512)
--SELECT @Name = 'Missing Inventory'
--DELETE FROM Reports WHERE Name like @Name
--insert into Reports
--SELECT NEWID(), @Name, @Name, 'vwMissingInventory', null, null, 1










