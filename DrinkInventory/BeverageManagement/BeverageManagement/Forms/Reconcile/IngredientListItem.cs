using System;
using Jaxis.Interfaces;
using Jaxis.Inventory.Data;

namespace BeverageManagement.Forms.Reconcile
{
    public class IngredientListItem : IIngredientDisplayItem
    {
        public IngredientListItem()
        {
            Group = 0;
            Type = IngredientRequirementType.Required;
        }

        public IngredientListItem(IIngredient _ingredient)
        {
            IngredientId = _ingredient.ObjectID;
            UPC = _ingredient.UPCID ?? Guid.Empty;
            Group = _ingredient.Number;
            Type = (IngredientRequirementType)_ingredient.Type;
        }

        // ReSharper disable InconsistentNaming
        public Guid? UPC { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? ManufacturerID { get; set; }
        public int? Quality { get; set; }
        // ReSharper restore InconsistentNaming
        public int Group { get; set; }
        public IngredientRequirementType Type { get; set; }
        public Guid IngredientId { get; private set; }

        public void UpdateIngredient(IIngredient _ingredient)
        {
            _ingredient.Number = Group;
            _ingredient.UPCID = UPC;
            _ingredient.Type = (int)Type;
        }
    }
}
