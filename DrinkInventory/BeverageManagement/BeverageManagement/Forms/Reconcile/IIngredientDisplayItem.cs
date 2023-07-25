using System;
using Jaxis.Interfaces;

namespace BeverageManagement.Forms.Reconcile
{
    public interface IIngredientDisplayItem
    {
        // ReSharper disable InconsistentNaming
        Guid? UPC { get; set; }
        Guid? CategoryID { get; set; }
        Guid? ManufacturerID { get; set; }
        int? Quality { get; set; }
        // ReSharper restore InconsistentNaming
        
        int Group { get; set; }
        IngredientRequirementType Type { get; set; }
    }
}
