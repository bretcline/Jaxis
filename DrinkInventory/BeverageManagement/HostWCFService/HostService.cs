using System;
using System.Collections.Generic;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;
using Jaxis.MessageLibrary.POS;
using Jaxis.Util.Log4Net;
using SubSonic.Query;

namespace HostWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PourEngineService : IPourEngineService
    {
        object m_Locker = new object();

        #region IPourEngineService Members

        public bool Ping( )
        {
            return true;
        }

        public void PushPourData(DataPour data)
        {
            Log.Debug(string.Format("{0} - {1}", data.TagID, data.Volume));
            LiveDataStore.Get().Push(data);
        }

        public void PushCalcPourData(CalcPour data)
        {
            Log.Debug(string.Format("{0} - {1}", data.TagID, data.PourAmount));
            LiveDataStore.Get().Push(data);
        }

        public void PushTagActivity(DataTagActivity data)
        {
            LiveDataStore.Get( ).Push( data );
        }

        public void PushTagAlert( DataTagAlert data )
        {
            LiveDataStore.Get( ).Push( data );
        }

        public void PushTagMove( DataTagMove data )
        {
            LiveDataStore.Get( ).Push( data );
        }

        public void PushActivityLog( DataActivityLog data )
        {
            LiveDataStore.Get( ).Push( data );
        }

        public void PushTicket( DataPOSTicket data )
        {
            LiveDataStore.Get( ).Push( data );
        }

        public void PushDeviceAlert( DataDeviceAlert data )
        {
            LiveDataStore.Get( ).Push( data );
        }

        //public string GetSettings( string deviceID )
        //{
        //    return string.Empty;
        //    //throw new NotImplementedException( );
        //}

        //public string GetLocationID( string deviceID )
        //{
        //    string SQL =
        //        "select L.LocationID from Devices D JOIN Locations L on D.LocationID = L.LocationID";

        //    CodingHorror Sql = new CodingHorror( SQL );
        //    return Sql.ExecuteScalar<string>();
        //}

        #endregion
    }

}
