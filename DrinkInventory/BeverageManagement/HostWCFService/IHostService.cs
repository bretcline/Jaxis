using System;
using System.Collections.Generic;
using System.ServiceModel;
using Jaxis.Inventory.Data;
using Jaxis.MessageLibrary;

namespace HostWCFService
{
    //Outbound Web Calls
    [ServiceContract]
    public interface IPourEngineService
    {
        [OperationContract]
        bool Ping( );

        [OperationContract]
        void PushCalcPourData(CalcPour data);
        
        [OperationContract]
        void PushPourData( DataPour data );

        [OperationContract]
        void PushTagActivity( DataTagActivity data );

        [OperationContract]
        void PushTagAlert( DataTagAlert data );

        [OperationContract]
        void PushDeviceAlert( DataDeviceAlert data );

        [OperationContract]
        void PushTagMove( DataTagMove ticket );

        [OperationContract]
        void PushActivityLog( DataActivityLog ticket );

        [OperationContract]
        void PushTicket( DataPOSTicket ticket );

        //[OperationContract]
        //string GetSettings( string deviceID );

        //[OperationContract]
        //string GetLocationID( string deviceID );
    }
}