using System.Runtime.Serialization;

namespace Jaxis.DrinkInventory.Reporting.WcfService.Results
{
    [DataContract]
    [KnownType(typeof(ServiceResult))]
    [KnownType(typeof(SuccessResult))]
    [KnownType(typeof(ExceptionResult))]
    [KnownType(typeof(LogOnResult))]
    [KnownType(typeof(GetAreasResult))]
    [KnownType(typeof(GetSectionsResult))]
    [KnownType(typeof(GetViewsResult))]
    [KnownType(typeof(GetDataTableResult))]
    [KnownType(typeof(GetViewResult))]
    [KnownType(typeof(GetLocationResult))]
    [KnownType(typeof(AddLocationResult))]
    [KnownType(typeof(GetAreaForSectionResult))]
    [KnownType(typeof(GetParametersResult))]
    [KnownType(typeof(GetLocationsResult))]
    [KnownType(typeof(GetAreaResult))]
    [KnownType(typeof(GetUsersResult))]
    [KnownType(typeof(GetUserResult))]
    [KnownType(typeof(GetUserGroupsResult))]
    [KnownType(typeof(GetVisibleWidgetIdsResult))]
    [KnownType(typeof(GetUserGroupResult))]
    public class ServiceResult
    {
    }
}
