using System;

namespace LFI.Sync.DataManager
{
	public class CountTransaction : BaseTransaction<int>
	{
		private string customWhere;
		private string specificColumn = "*";
		private readonly bool isDistinct;

		//----------------------------------------------------------------------
		/// <summary>
		/// Perpares a transaction for a standard Count(*) FROM tableName query
		/// </summary>
		/// <param name="tableName">Name of table where records will be counted</param>
		public CountTransaction(string tableName)
			: base(tableName, String.Empty)
		{
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Prepares a transaction to get count of a specific column
		/// </summary>
		/// <param name="tableName">Name of table containing the column</param>
		/// <param name="specificColumn">Specific column to be counted</param>
		/// <param name="whereSql">Custom where sql to be evaluated in the query</param>
		public CountTransaction(string tableName, string whereSql)
			: base(tableName, String.Empty)
		{
			customWhere = whereSql;
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Prepares a transaction to get count of a specific column
		/// </summary>
		/// <param name="tableName">Name of table containing the column</param>
		/// <param name="specificColumn">Specific column to be counted</param>
		/// <param name="whereSql">Custom where sql to be evaluated in the query</param>
		public CountTransaction(string tableName, string specificColumn, string whereSql)
			: base(tableName, String.Empty)
		{
			customWhere = whereSql;
			this.specificColumn = specificColumn;
		}

		//----------------------------------------------------------------------
		/// <summary>
		/// Prepares a transaction to get count of a specific column
		/// </summary>
		/// <param name="tableName">Name of table containing the column</param>
		/// <param name="specificColumn">Specific column to be counted</param>
		/// <param name="isDistinct">Set to only count specific occurances of this column</param>
		public CountTransaction(string tableName, string specificColumn, bool isDistinct)
			: base(tableName, String.Empty)
		{
			this.isDistinct = isDistinct;
			this.specificColumn = specificColumn;
		}

		//----------------------------------------------------------------------
		public void SetCustomWhere(string whereSql)
		{
			customWhere = whereSql;
		}

		//----------------------------------------------------------------------
		protected internal override void RegisterSelectColumns()
		{
			if (this.isDistinct)
				specificColumn = "DISTINCT " + specificColumn;
			AddColumn("COUNT(" + specificColumn + ")", "Count");
		}

		//----------------------------------------------------------------------
		protected override string BuildWhere()
		{
			if (!String.IsNullOrEmpty(customWhere))
				return customWhere;
			
			return base.BuildWhere();
		}

		//----------------------------------------------------------------------
		public override int BuildFromReader(TransactionReader reader)
		{
			return reader.TryReadInt("Count");
		}
	}
}
