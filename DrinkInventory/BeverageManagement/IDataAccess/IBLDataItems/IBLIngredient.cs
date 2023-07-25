using System;
namespace Jaxis.Inventory.Data.IBLDataItems
{
    public interface  IBLIngredient : IIngredient
    {
        IBLRecipe Recipe { get; set; }
        int Number { get; set; }
        int Type { get; set; }
        Guid? UPCID { get; set; }
        Guid? ManufacturerID { get; set; }
        Guid? CategoryID { get; set; }
        int? Quality { get; set; }
        Guid StandardPourID { get; set; }
    }
}