use drinkreporting











SELECT *
FROM OrgUnits ou





SELECT *
FROM Users

SELECT ou.*
FROM [Sessions] s
JOIN Users u ON s.UserId = u.Id
JOIN UserGroupMemberships ugm ON u.Id = ugm.UserId
JOIN UserGroups ug ON ugm.UserGroupId = ug.Id
JOIN OrgUnits ou ON ug.OrgUnitId = ou.Id
WHERE s.Id = '79328748-49CB-419F-8130-0974B8AC542C' AND s.ExpirationTime > GETDATE()



SELECT *
FROM UserGroupMemberships

SELECT *
FROM UserGroups ug

SELECT 

select * from orgunits

select * from areas


select * from parameters








select * from [users]




select * from usergroups



select * from areas


select * from sections


select *
from sessions
where expirationtime < getdate()





delete usergroups


