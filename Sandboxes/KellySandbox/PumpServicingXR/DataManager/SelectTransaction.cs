using System;
using System.Collections.Generic;
using System.Text;

namespace LFI.Sync.DataManager
{
	public class SelectTransaction : BaseTransaction<Dictionary<string, object>>
	{
		private string where = String.Empty;

		//----------------------------------------------------------------------
		public SelectTransaction(string tableName)
			: base(tableName, String.Empty)
		{
		}

		//----------------------------------------------------------------------
		public SelectTransaction(string tableName, bool isDistinct)
			: base(tableName, String.Empty)
		{
			distinct = isDistinct;
		}

		//----------------------------------------------------------------------
		public SelectTransaction(string tableName, List<string> selectColumns)
			: base(tableName, String.Empty)
		{
			AddSelectColumns(selectColumns);
		}

		//----------------------------------------------------------------------
		public SelectTransaction(string tableName, List<string> selectColumns, bool isDistinct)
			: this(tableName, selectColumns)
		{
			distinct = isDistinct;
		}

		//----------------------------------------------------------------------
		public void AddSelectColumn(string colName)
		{
			AddColumn(colName);
		}

		//----------------------------------------------------------------------
		public void AddSelectColumns(List<string> columns)
		{
			foreach (string column in columns)
			{
				AddColumn(column);
			}
		}

		//----------------------------------------------------------------------
        public override Dictionary<string, object> BuildFromReader(TransactionReader reader)
		{
			Dictionary<string, object> outList = new Dictionary<string, object>(20);
			
			int fieldCount = reader.GetColumnCount();
			for (int i = 0; i < fieldCount; ++i)
			{
				outList.Add(reader.GetColumnName(i), reader.GetColumnValue(i));
			}

			return outList;
		}

		//----------------------------------------------------------------------
		public void SetWhereSQL(string where)
		{
			this.where = where;
		}

		//----------------------------------------------------------------------
		protected override string BuildWhere()
		{
		    return where == String.Empty ? base.BuildWhere() : where;
		}
	}
}
