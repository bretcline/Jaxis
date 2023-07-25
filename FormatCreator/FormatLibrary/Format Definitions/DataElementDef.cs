using System;
using System.Collections.Generic;

namespace LFI.RFID.Format
{
    public class DataElementDef
    {
        private bool hasChanges = false;
        private bool newElement = true;
        private DataElementDef originalElementDef = null;

        public DataElementDef() {}
        public DataElementDef(DataElementDef src) : this(src.Name, src.DataType, src.Required, src.Constraints) { this.hasChanges = src.hasChanges; }
        public DataElementDef(string name, DataType dataType, bool required, string constraints)
        {
            this.name = name;
            this.dataType = dataType;
            this.required = required;
            this.constraints = constraints;            
        }

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.Compare(name, value) != 0)
                {
                    name = value;
                    hasChanges = true;
                }
            }
        }

        private DataType dataType;
        public DataType DataType
        {
            get
            {
                return dataType;
            }
            set
            {
                if (dataType != value)
                {
                    dataType = value;
                    hasChanges = true;
                }
            }
        }

        private bool required = false;
        public bool Required
        {
            get
            {
                return required;
            }
            set
            {
                if (required != value)
                {
                    required = value;
                    hasChanges = true;
                }
            }
        }

        private string constraints = string.Empty;
        public string Constraints
        {
            get
            {
                return constraints;
            }
            set
            {
                if (string.Compare(constraints, value) != 0)
                {
                    constraints = value;
                    hasChanges = true;
                }
            }
        }        

        public bool HasChanges()
        {
            return hasChanges;
        }

        public void AcceptChanges()
        {
            hasChanges = false;
            if (originalElementDef == null)
                originalElementDef = new DataElementDef();
            originalElementDef.constraints = constraints;
            originalElementDef.name = name;
            originalElementDef.dataType = dataType;
            originalElementDef.required = required;
            newElement = false;
        }

        public void RevertChanges()
        {
            this.hasChanges = false;
            if (originalElementDef != null)
            {
                this.constraints = originalElementDef.constraints;
                this.dataType = originalElementDef.dataType;
                this.name = originalElementDef.name;
                this.required = originalElementDef.required;
            }
        }

        public bool IsNew { get { return newElement; } }
    }
}
