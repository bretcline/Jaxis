use [DrinkReporting]
go

delete [parameters]
delete [sessions]
delete pours
delete [columns]
delete [reports]
delete [areamemberships]
delete [usergroupmemberships]
delete areas
delete [users]
delete [usergroupxorganizations]
delete [usergroups]
delete [organizations]
go

-- create root organization
declare @rootId uniqueidentifier
set @rootId = 'C182A31B-E419-4E77-AEA9-DB5DA1F9DBB4'
insert Organizations (OrganizationId, ParentId, ShortName, Name, Path) values (@rootId, null, 'ROOT', 'Root Oranization', cast(@rootId as nvarchar(36)) + '/')

-- create root user
insert users (username, [password], [organizationId]) values ('root', 'root', @rootId)
go

-- create areas
insert areas ([order],controller,name,shortname) values (10, 'Dashboard', 'Dashboard', 'Dashboard')
insert areas ([order],controller,name,shortname) values (20, 'Report', 'Reports', 'Reports')
insert areas ([order],controller,name,shortname) values (30, 'UpcManager', 'UPCs', 'UpcMgr')
insert areas ([order],controller,name,shortname) values (40, 'RecipeManager', 'Recipes', 'RecipeMgr')
insert areas ([order],controller,name,shortname) values (50, 'OrganizationManager', 'Organizations', 'Organization')
insert areas ([order],controller,name,shortname) values (60, 'UserGroupManager', 'User Groups', 'UserGroupMgr')
insert areas ([order],controller,name,shortname) values (70, 'UserManager', 'Users', 'UserMgr')
go



---- Sections for the Dashboard Area
--declare @areaid uniqueidentifier
--set @areaid = (select AreaId from areas where shortname='Reports')
--insert sections (SectionId, [order], areaid, name, shortname) values ('A8A77887-D413-4433-94F0-C504F805053D', 10, @areaid, 'Recent Activity', 'RecentActivity')
--insert sections ([order], areaid, name, shortname) values (20, @areaid, 'Inventory Levels', 'InventoryLevels')
--insert sections ([order], areaid, name, shortname) values (30, @areaid, 'Alerts', 'Alerts')
--go

-- Sections for the History Area
--declare @areaid uniqueidentifier
--set @areaid = (select id from areas where shortname='History')
--insert sections ([order], areaid, name, shortname) values (10, @areaid, 'Moves', 'Moves')
--go




-- create reports
insert [reports] ([reportid], [order], name, [type], ShortName, SelectCommand, ReportClassName, modifiedon) 
values (NewId( ), 10, 'Recent Pours', 'Table', 'RecentPours',	'
SELECT
	pour.PourTime AS [Time],
	org.ShortName AS Location,
	pour.ItemNumber AS [UPC],
	pour.Volume   
FROM Pours pour
	JOIN SecurityView sv ON pour.OrganizationId = sv.OrganizationId  
	JOIN Organizations org ON pour.OrganizationId = org.OrganizationId   
WHERE sv.SessionId = @sessionId AND pour.ItemNumber LIKE ''%'' + @Upc + ''%''
ORDER BY pour.PourTime DESC',
	 'RecentPours.repx', GetDate( ) )
go

declare @reportId uniqueidentifier
set @reportId = (select reportId from reports where shortname = 'RecentPours')
insert [columns] (DisplayName, reportId) values ('Time Poured', @reportId)
insert [columns] (DisplayName, reportId) values ('Quantity', @reportId)
insert [columns] (DisplayName, reportId) values ('Product', @reportId)
insert [columns] (DisplayName, reportId) values ('Location', @reportId)

insert [parameters] (reportId, [Type], [Name], Label, [Order]) values (@reportId, 'Guid', 'SessionId', NULL, 10)
insert [parameters] (reportId, [Type], [Name], Label, [Order]) values (@reportId, 'String', 'ItemNumber', 'UPC', 20)

declare @rootId uniqueidentifier
set @rootId = 'C182A31B-E419-4E77-AEA9-DB5DA1F9DBB4'

-- create Acme organization
declare @acmeId uniqueidentifier
set @acmeId = NEWID()
insert Organizations (OrganizationId, ParentId, ShortName, Name, Path) 
values (@acmeId, @rootId, 'ACME', 'ACME Inc.', cast(@rootId as nvarchar(36)) + '/' + cast(@acmeId as nvarchar(36)) + '/')

insert into Devices( DeviceId, DeviceNumber, ModifiedOn, OrganizationId, Status, LocationName )
	values( NEWID( ), SUBSTRING( CAST( NEWID( ) AS VARCHAR(37) ), 1, 12 ), GETDATE( ),@acmeId, 0, '' )
insert into Devices( DeviceId, DeviceNumber, ModifiedOn, OrganizationId, Status, LocationName )
	values( NEWID( ), SUBSTRING( CAST( NEWID( ) AS VARCHAR(37) ), 1, 12 ), GETDATE( ),@acmeId, 0, '' )
insert into Devices( DeviceId, DeviceNumber, ModifiedOn, OrganizationId, Status, LocationName )
	values( NEWID( ), SUBSTRING( CAST( NEWID( ) AS VARCHAR(37) ), 1, 12 ), GETDATE( ),@acmeId, 0, '' )
insert into Devices( DeviceId, DeviceNumber, ModifiedOn, OrganizationId, Status, LocationName )
	values( NEWID( ), SUBSTRING( CAST( NEWID( ) AS VARCHAR(37) ), 1, 12 ), GETDATE( ),@acmeId, 0, '' )
insert into Devices( DeviceId, DeviceNumber, ModifiedOn, OrganizationId, Status, LocationName )
	values( NEWID( ), SUBSTRING( CAST( NEWID( ) AS VARCHAR(37) ), 1, 12 ), GETDATE( ),@acmeId, 0, '' )

declare @deviceNumber varchar(12)
select top 1 @deviceNumber = devicenumber from devices

insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:00:00', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Don Julio', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:10:23', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Crown Royal', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:15:32', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Don Julio', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:23:47', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Chivas Regal', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:31:05', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Crown Royal', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:45:54', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Crown Royal', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:47:13', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Don Julio', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 8:55:29', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Crown Royal', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 9:10:15', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Don Julio', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 9:19:10', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Chivas Regal', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 9:19:10', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Don Julio', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 9:19:10', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Don Julio', @acmeId, GetDate( ), null, 0, 0)
insert Pours (PourId, TagNumber, DeviceNumber, PourTime, Volume, Duration, AmountLeft, Temperature, BatteryVoltage, LocationName, ItemNumber, OrganizationId, ModifiedOn, POSTicketItemId, Status, Cost) 
	values( newID( ), newID( ), @devicenumber, '11/28/2011 9:19:10', 5.89, 2, 0, 72, 5, 'Lobby Bar', 'Chivas Regal', @acmeId, GetDate( ), null, 0, 0)


-- make a default user group for
insert usergroups (Name, OrganizationId)
select 'SYSTEM ADMIN', @rootId
go

-- put users in the group
insert UserGroupXOrganizations (UserGroupXOrganizationId, ModifiedOn, UserGroupId, OrganizationId)
select newID( ), GetDate( ), ug.UserGroupId, o.OrganizationId
from UserGroups ug
cross join Organizations o
where o.ShortName = 'ROOT'

-- make an area membership for all groups
insert AreaMemberships (AreaId, UserGroupId)
select a.AreaId, ug.UserGroupId from Areas a, UserGroups ug

-- make a user group membership for all users
insert UserGroupMemberships (UserId, UserGroupId)
select u.UserId, ug.UserGroupId from Users u, UserGroups ug

