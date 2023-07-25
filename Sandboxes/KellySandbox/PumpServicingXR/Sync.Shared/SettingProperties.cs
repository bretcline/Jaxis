using System;
using System.Collections.Generic;
using LFI.Sync.DataManager;

namespace LFI.Sync.Shared
{
    public class SettingProperties
    {
        //----------------------------------------------------------------------
        public static string Get( LFI.Sync.DataManager.DataManager wsDataMgr, string key )
        {
            if( wsDataMgr == null ) throw new ArgumentNullException( "wsDataMgr" );
            SelectTransaction trans = new SelectTransaction( "SettingProperties", new List<string>( ) { "PropertyValue" } );
            trans.SetWhereSQL( String.Format( "WHERE PropertyKey = '{0}'", key ) );
            List<Dictionary<string, object>> results = wsDataMgr.GetData( trans );

            if( results.Count == 0 )
                return null;

            return results[ 0 ][ "PropertyValue" ].ToString( );
        }

        //----------------------------------------------------------------------
        public static TransactionResult Put( LFI.Sync.DataManager.DataManager wsDataMgr, string key, string value )
        {
            if( wsDataMgr == null ) throw new ArgumentNullException( "wsDataMgr" );
            TransactionResult result = new TransactionResult( );
            if( Get( wsDataMgr, key ) == null )
            {
                string insertSql = String.Format( "INSERT INTO SettingProperties (PropertyKey, PropertyValue) VALUES ('{0}', '{1}')", key, value );
                result = wsDataMgr.InvokeRawNonQuery( insertSql );
            }
            else
            {
                string updateSql = String.Format( "UPDATE SettingProperties SET PropertyValue = '{0}' WHERE PropertyKey = '{1}'", value, key );
                result = wsDataMgr.InvokeRawNonQuery( updateSql );
            }

            return result;
        }

        //----------------------------------------------------------------------
        public static DateTime GetDateTime( LFI.Sync.DataManager.DataManager wsDataMgr, string key, DateTime minValue )
        {
            if( wsDataMgr == null ) throw new ArgumentNullException( "wsDataMgr" );
            string dateStr = Get( wsDataMgr, key );
            DateTime outDate = minValue;

            if( dateStr != null )
            {
                DateTime resultDate = DateTime.Parse( dateStr );
                if( resultDate >= outDate )
                    outDate = resultDate;
            }

            return outDate;
        }
    }
}
