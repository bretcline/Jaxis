using System;
using System.Collections.Generic;

namespace LFI.Sync.DataManager
{
	public interface ITransaction
	{
		TransactionType TransactionType { get; }
		string Table { get; }
		string Select { get; }
		string Update { get; }
		string Insert { get; }
		Dictionary<string, object> Params { get; }
		object PrimaryKey { get; }
		string IDColumn { get; }

		Func<string> KeyGen { get; set; }

		void Compile();
        void Compile(CompileCondition condition);
        
        void SetTransactionType(TransactionType type);
#if !PocketPC
		System.Data.DataTable BuildDataTable();
		void CreateColumnMapping(System.Data.SqlClient.SqlBulkCopyColumnMappingCollection mappingCollection);
		void AddDataRow(System.Data.DataTable table);
	    void RegisterParams();
        IDictionary<string, object> GetData();
#else
        void RegisterColumnOrdinals();
		void PerpareResultSet(System.Data.SqlServerCe.SqlCeResultSet resultSet, System.Data.SqlServerCe.SqlCeUpdatableRecord record);
#endif
	}
}