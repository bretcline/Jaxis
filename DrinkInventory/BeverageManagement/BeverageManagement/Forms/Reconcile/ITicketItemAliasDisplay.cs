using System;

namespace BeverageManagement.Forms.Reconcile
{
    public interface ITicketItemAliasDisplay
    {
        string DescriptionOnTicket { get; set; }
        Guid? AssignedDrinkRecipe { get; set; }
        decimal Price { get; set; }
    }
}
