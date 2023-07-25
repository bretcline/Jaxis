namespace LFI.Sync.DataManager
{
	internal static class ParamFactory
	{
		internal static System.Data.IDbDataParameter CreateParam(string key, object value)
		{
#if PocketPC
			return new System.Data.SqlServerCe.SqlCeParameter(key, value);
#else
			return new System.Data.SqlClient.SqlParameter(key, value);
#endif
		}
	}
}
