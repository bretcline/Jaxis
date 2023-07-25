using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Inventory.Data.IUIDataItems;
using Jaxis.Interfaces;

namespace Jaxis.Inventory.Data.BusinessLogic.BLObjects
{
    public class BLIngredientItem : IBLIngredientItem, IUIIngredientItem
    {
        #region Private Fields
        private string m_TicketItemAlias;
        private string m_RecipeName;
        private string m_Name;
        private Guid m_IngredientID;
        private Guid m_StandardPourID;
        private int m_Quality;
        private double m_PourStandard;
        private double m_StandardVariance;
        private Guid? m_PourID;
        #endregion Private Fields

        #region Public Properties
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

        public string RecipeName
        {
            get
            {
                if (m_RecipeName == null)
                {
                    m_RecipeName = string.Empty;
                }
                return m_RecipeName;
            }
            set
            {
                m_RecipeName = value;
            }
        }

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

        public Guid IngredientID
        {
            get
            {
                if (m_IngredientID == null)
                {
                    m_IngredientID = Guid.Empty;
                }
                return m_IngredientID;
            }
            set
            {
                m_IngredientID = value;
            }
        }

        public Guid StandardPourID
        {
            get
            {
                if (m_StandardPourID == null)
                {
                    m_StandardPourID = Guid.Empty;
                }
                return m_StandardPourID;
            }
            set
            {
                m_StandardPourID = value;
            }
        }

        public int Quality
        {
            get
            {
                if (m_Quality == null)
                {
                    m_Quality = 0;
                }
                return m_Quality;
            }
            set
            {
                m_Quality = value;
            }
        }

        public double PourStandard
        {
            get
            {
                if (m_PourStandard == null)
                {
                    m_PourStandard = 0;
                }
                return m_PourStandard;
            }
            set
            {
                m_PourStandard = value;
            }
        }

        public double StandardVariance
        {
            get
            {
                if (m_StandardVariance == null)
                {
                    m_StandardVariance = 0;
                }
                return m_StandardVariance;
            }
            set
            {
                m_StandardVariance = value;
            }
        }

        public Guid? PourID
        {
            get
            {
                return m_PourID;
            }
            set
            {
                m_PourID = value;
            }
        }

        public string QualityName
        {
            get
            {
                var qualityName = string.Empty;
                var quality = BLManagerFactory.Get().ManageQuality().GetAll().FirstOrDefault(q => q.QualityLevel == m_Quality);
                if (quality != null)
                {
                    qualityName = quality.Name;
                }
                return qualityName;
            }
        }
        #endregion Public Properties
    }
}
