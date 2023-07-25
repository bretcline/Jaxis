using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A Business Logic interface which represents the SizeTypes table in the BevMetMobile Database.
    /// </summary>
    public interface IUISizeType
    {
        //Guid SizeTypeID { get; set; }
        string Name { get; set; }
        string Abbreviation { get; set; }
    }
}