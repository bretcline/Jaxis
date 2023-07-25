using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LFI.Sync.DataManager;
using WFT.PS.Shared;

namespace WFT.PSService.ServiceLibrary
{
	[DataContract]
	public class XRData<T> : BaseData<T>
	{
	    private Guid id = Guid.Empty;

        public virtual string TablePrimaryKey { get; protected set; }

		[DataMember]
		public Guid ID
		{
            get
            {
                Guid? primaryKey = PrimaryKey as Guid?;

                if (primaryKey.HasValue)
                {
                    id = primaryKey.Value;
                }

                return id;
            }
		    set
			{
				id = value;
				if (value == Guid.Empty)
					PrimaryKey = null;
				else
					PrimaryKey = id;
			}
		}

		[DataMember]
		public virtual string XRefID { get; set; }

		[DataMember]
		public Guid SourceDB { get; set; }

		public override string ToString()
		{
			if (!String.IsNullOrEmpty(this.XRefID))
				return this.Name + " " + this.XRefID;
			else
				return this.Name + " " + this.ID.ToString();
		}

		public virtual SelectTransaction GetExistsTransaction(string foreignKeyUpdateColumn)
		{
			const string whereSQL = "WHERE SourceDB = '{0}' AND {1} = '{2}'";
			SelectTransaction existsIDTransaction = new SelectTransaction(
                DataTagMapping.DataTagTables[BaseData<T>.DataTag], 
                new List<string>() { DataTagMapping.DataTagIDColumns[BaseData<T>.DataTag] });
			existsIDTransaction.SetWhereSQL(String.Format(whereSQL, SourceDB, foreignKeyUpdateColumn, XRefID));

			return existsIDTransaction;
		}

        public virtual void AdjustDates( TimeSpan _offset )
        {
            this.LastModified = DateTime.Now + _offset;
        }
	}
}
