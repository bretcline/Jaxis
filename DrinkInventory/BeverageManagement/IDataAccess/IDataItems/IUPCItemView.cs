using System;

namespace Jaxis.Inventory.Data
{
    public interface IUPCItemView : INamedObject, IDataObject<IUPCItem>
    {
        Guid UPCID { get; set; }
        Guid CategoryID { get; set; }
        string ItemNumber { get; set; }
        string Manufacturer { get; set; }
        int Size { get; set; }
        IStandardNozzle Nozzle { get; set; }
        //ISizeType SizeType { get; set; }
        //ICategory Category { get; set; }
        Guid SizeTypeID { get; set; }
        Guid RootCategoryID { get; set; }
    }


}