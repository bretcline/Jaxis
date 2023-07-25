using System;

namespace Jaxis.Inventory.Data
{
    /// <summary>
    /// A class which represents the mobPours table in the BevMetMobile Database.
    /// </summary>
    public interface IIngredient : IDataObject<IIngredient>
    {
        Guid RecipeID { get; set; }
        int Number { get; set; }
        int Type { get; set; }
        Guid? UPCID { get; set; }
        Guid? ManufacturerID { get; set; }
        Guid? CategoryID { get; set; }
        int? Quality { get; set; }
        Guid StandardPourID { get; set; }
    }
}
