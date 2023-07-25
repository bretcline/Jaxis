using System;

namespace Jaxis.Inventory.Data
{
    public interface INameDescription : INamedObject    
    {
        string Description { get; set; }
    }
}