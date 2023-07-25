using System.Runtime.Serialization;

namespace Jaxis.DrinkInventory.Reporting.WcfService.DataContracts
{
    [DataContract]
    [KnownType(typeof(ServiceResult))]
    [KnownType(typeof(SuccessResult))]
    [KnownType(typeof(ExceptionResult))]
    [KnownType(typeof(LogOnResult))]
    [KnownType(typeof(GetAreasResult))]
    [KnownType(typeof(GetReportsResult))]
    [KnownType(typeof(GetDataTableResult))]
    [KnownType(typeof(GetReportResult))]
    [KnownType(typeof(GetOrganizationResult))]
    [KnownType(typeof(GetParametersResult))]
    [KnownType(typeof(GetOrganizationsResult))]
    [KnownType(typeof(GetAreaResult))]
    [KnownType(typeof(GetUsersResult))]
    [KnownType(typeof(GetUserResult))]
    [KnownType(typeof(GetUserGroupsResult))]
    [KnownType(typeof(GetVisibleWidgetIdsResult))]
    [KnownType(typeof(GetUserGroupResult))]
    [KnownType(typeof(GetReportsResult))]
    [KnownType(typeof(GetUpcsResult))]
    [KnownType(typeof(GetUpcResult))]
    public class ServiceResult
    {
    }
}
