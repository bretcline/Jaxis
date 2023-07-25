using System;
using Jaxis.Inventory.Data.IBLDataItems;

namespace BeverageManagement.Forms.Reconcile
{
    public enum TicketItemAliasStatus
    {
        Modified,
        Deleted,
        Unchanged,
    }

    public class TicketItemAliasDisplay : ITicketItemAliasDisplay
    {
        public TicketItemAliasDisplay()
        {
            DescriptionOnTicket = "NEW ITEM";
            Modified = TicketItemAliasStatus.Modified;
        }

        public TicketItemAliasDisplay(IBLTicketItemAlias _alias)
        {
            TicketItemAliasId = _alias.ObjectID;
            DescriptionOnTicket = _alias.Description;
            AssignedDrinkRecipe = _alias.RecipeID;
            Price = _alias.Price;
            Modified = TicketItemAliasStatus.Unchanged;
        }

        public Guid TicketItemAliasId { get; set; }
        public string DescriptionOnTicket { get; set; }
        public Guid? AssignedDrinkRecipe { get; set; }
        public decimal Price { get; set; }
        public TicketItemAliasStatus Modified { get; set; }

        public void UpdateAlias(IBLTicketItemAlias _alias)
        {
            _alias.Description = DescriptionOnTicket;
            _alias.RecipeID = AssignedDrinkRecipe;
            _alias.Price = Price;
            Modified = TicketItemAliasStatus.Modified;
        }
    }
}
