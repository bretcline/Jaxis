using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LFI.RFID.Format
{
    public class DataRowDef
    {
        public DataRowDef()
        {
            ElementDefs = new BindingList<DataElementDef>();
        }

        public DataRowDef(DataRowDef src)
        {
            ElementDefs = new BindingList<DataElementDef>( );
            foreach (DataElementDef elemDef in src.ElementDefs)            
                ElementDefs.Add(new DataElementDef(elemDef));            
        }

        public BindingList<DataElementDef> ElementDefs { get; set; }

        public bool HasChanges()
        {
            foreach (DataElementDef elementDef in ElementDefs)
            {
                if (elementDef.HasChanges())
                    return true;
            }
            return false;
        }

        public void AcceptChanges()
        {
            foreach (DataElementDef elementDef in ElementDefs)
            {
                elementDef.AcceptChanges();
            }
        }

        public void RevertChanges()
        {
            IList<DataElementDef> newElementDefs = new List<DataElementDef>();
            foreach (DataElementDef elementDef in ElementDefs)
            {
                if (elementDef.IsNew)
                    newElementDefs.Add(elementDef);
                else
                    elementDef.RevertChanges();
            }
            foreach (DataElementDef newElementDef in newElementDefs)
                ElementDefs.Remove(newElementDef);
        }
    }
}
