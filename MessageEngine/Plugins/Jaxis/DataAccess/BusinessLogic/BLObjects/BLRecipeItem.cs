using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;

namespace Jaxis.Inventory.Data.BusinessLogic.BLObjects
{
    public class BLRecipeItem : IBLRecipeItem, IUIRecipeItem
    {
        #region Private Properties
        private string m_Name;
        private string m_TicketItemAlias;
        private List<IBLIngredientItem> m_Ingredients;
        #endregion Private Properties

        #region Public Properties
        public string Name
        {
            get
            {
                if (m_Name == null)
                {
                    m_Name = string.Empty;
                }
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public string TicketItemAlias
        {
            get
            {
                if (m_TicketItemAlias == null)
                {
                    m_TicketItemAlias = string.Empty;
                }
                return m_TicketItemAlias;
            }
            set
            {
                m_TicketItemAlias = value;
            }
        }

        public List<IBLIngredientItem> Ingredients
        {
            get
            {
                if (m_Ingredients == null)
                {
                    m_Ingredients = new List<IBLIngredientItem>();
                }
                return m_Ingredients;
            }
            set
            {
                m_Ingredients = value;
            }
        }
        #endregion Public Properties
    }
}
