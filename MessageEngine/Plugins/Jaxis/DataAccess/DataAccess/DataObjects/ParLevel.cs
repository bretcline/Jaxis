using System.Collections.Generic;

namespace Jaxis.Inventory.Data
{
    public partial class ParLevel : IParLevel, IBLParLevel
    {
        public IEnumerable<IParLevel> GetAll()
        {
            return ParLevel.All();
        }
    }
}