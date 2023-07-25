using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using System.Data.SqlServerCe;

namespace Jaxis.Readers.Identec
{
    public class ClientData
    {
        public class MissingEventException : Exception
        {
            public MissingEventException( string _EventID ) : base( string.Format( "Missing Event #{0}", _EventID ) ) { }
        }

        public const string DEFAULT = "Default";

        private static ClientData m_Data = new ClientData( );
        private Dictionary<string, IEngineSettings> m_EngineSettings = new Dictionary<string,IEngineSettings>( );
        protected Dictionary<string, List<UPCData>> m_UPCData = new Dictionary<string, List<UPCData>>( );
        private Mutex m_Mutex = new Mutex( );
        private Dictionary<string, string> m_EventIDs = new Dictionary<string,string>( );
        private List<string> m_DeviceList = new List<string>( );

        public string DefaultEventID { set { SetEventID( DEFAULT, value ); } }

        public static ClientData GetClientData( )
        {
            if( null == m_Data )
            {
                m_Data = new ClientData( );
            }
            return m_Data;
        }


        public void AddDevice( string _DeviceID )
        {
            m_DeviceList.Add( _DeviceID );
        }

        //public string DeviceID
        //{
        //    get
        //    {
        //        string rc = null;
        //        try
        //        {
        //            m_Mutex.WaitOne( );
        //            rc = m_DeviceID;
        //        }
        //        catch( Exception exp )
        //        {
        //            Log.WriteException( "ClientData::GetDeviceID", exp );
        //        }
        //        finally
        //        {
        //            m_Mutex.ReleaseMutex( );
        //        }

        //        return rc;
        //    }
        //    set
        //    {
        //        try
        //        {
        //            m_Mutex.WaitOne( );
        //            m_DeviceID = value;
        //        }
        //        catch( Exception exp )
        //        {
        //            Log.WriteException( "ClientData::SetDeviceID", exp );
        //        }
        //        finally
        //        {
        //            m_Mutex.ReleaseMutex( );
        //        }
        //    }
        //}

        public string GetEventID( string _DeviceID )
        {
            {
                string rc = string.Empty;

                try
                {
                    foreach( string Key in m_EventIDs.Keys )
                    {
                        Log.Debug( string.Format( "GetEventID - DeviceID {0} EventID {1}", Key, m_EventIDs[Key] ) );
                    }

                    m_Mutex.WaitOne( );
                    if( m_EventIDs.ContainsKey( _DeviceID ) )
                    {
                        rc = m_EventIDs[_DeviceID];
                    }
                    else
                    {
                        //PullEventsByDeviceFromDB( );
                        rc = m_EventIDs[_DeviceID];
                    }
                }
                catch( Exception exp )
                {
                    Log.WriteException( string.Format( "ClientData::GetEventID {0}", _DeviceID ), exp );
                }
                finally
                {
                    m_Mutex.ReleaseMutex( );
                }

                if( string.IsNullOrWhiteSpace( rc ) )
                {
                    rc = m_EventIDs[DEFAULT];
                }
                return rc;
            }
        }
        public void SetEventID( string _DeviceID, string _EventID )
        {
            try
            {
                Log.Debug( string.Format( "Device {0} Event {1}", _DeviceID, _EventID ) );
                m_Mutex.WaitOne( );
                if( !m_UPCData.ContainsKey( _EventID ) )
                {
                    m_UPCData[_EventID] = new List<UPCData>( );
                }
                m_EventIDs[_DeviceID] = _EventID;
                if( _DeviceID.Equals( DEFAULT ) )
                {
                    foreach( string DeviceID in m_DeviceList )
                    {
                        Log.Debug( string.Format( "Device {0} Event {1}", DeviceID, _EventID ) );
                        m_EventIDs[DeviceID] = _EventID;
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::SetEventID", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
        }

        public EngineSettings GetEngineSettings( string _DeviceID )
        {
            EngineSettings rc = null;
            try
            {
                m_Mutex.WaitOne( );
                if( m_EngineSettings.ContainsKey( _DeviceID ) )
                {
                    rc = m_EngineSettings[_DeviceID] as EngineSettings;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::GetEngineSettings", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }

            return rc;
        }

        public void SetEngineSettings( string _DeviceID, IEngineSettings _EngineSettings )
        {
            try
            {
                m_Mutex.WaitOne( );
                m_EngineSettings[_DeviceID] = _EngineSettings;
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::SetEngineSettings", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
        }

        public void InitUPCList( string _EventID, List<UPCData> _UPCList )
        {
            try
            {
                Log.Debug( string.Format( "Adding {1} items to {0}", _EventID, _UPCList.Count ) );
                foreach( UPCData U in _UPCList )
                {
                    Log.Debug( string.Format( "TagID: {0}  Nozzle {1} Amount {2} Size {3}", U.TagID, U.NozzleDiameter, U.AmountInBottle, U.BottleSize ) );
                }
                m_Mutex.WaitOne( );
                m_UPCData[_EventID] = _UPCList;
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::InitUPCList", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
        }

        public void AddUPC( string _EventID, UPCData _UPC )
        {
            try
            {
                Log.Debug( string.Format( "AddUPC {0} {1}", _EventID, _UPC.TagID ) );
                m_Mutex.WaitOne( );
                if( m_UPCData.ContainsKey( _EventID ) )
                {
                    Log.Debug( string.Format( "Tag {0} Nozzle {1}", _UPC.TagID, _UPC.NozzleDiameter ) );
                    m_UPCData[_EventID].Add( _UPC );
                }
                else
                {
                    
                    throw new MissingEventException( _EventID );
                }
            }
            catch( MissingEventException )
            {
                Log.Write( string.Format( "ClientData::AddUPC - Missing EventID {0}", _EventID ), LogType.Error );
                throw;
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::AddUPC", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
        }

        public void RemoveUPC( string _TagID )
        {
            try
            {
                m_Mutex.WaitOne( );

                foreach( string Key in m_UPCData.Keys )
                {
                    if( true == RemoveUPC( Key, _TagID ) )
                    {
                        break;
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::RemoveUPC", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
        }

        public bool RemoveUPC( string _EventID, string _TagID )
        {
            bool rc = false;
            try
            {
                m_Mutex.WaitOne( );
                if( m_UPCData.ContainsKey( _EventID ) )
                {
                    List<UPCData> Tags = m_UPCData[_EventID].Where( U => U.TagID.Equals( _TagID ) ).ToList( );
                    foreach( UPCData T in Tags )
                    {
                        rc = m_UPCData[_EventID].Remove( T );
                    }
                }
                else
                {
                    throw new MissingEventException( _EventID );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::RemoveUPC", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
            return rc;
        }

        public UPCData FindUPCByTag( string _EventID, string _TagID )
        {
            UPCData rc = null;
            try
            {
                m_Mutex.WaitOne( );
                if( m_UPCData.ContainsKey( _EventID ) )
                {
                    rc = m_UPCData[_EventID].Find( U => U.TagID.Equals( _TagID ) );
                }
                else
                {
                    throw new MissingEventException( _EventID );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::FindUPCByTag", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
            return rc;
        }

        public double AdjustTotalAmt( string _EventID, UPCData _UPC, double _PourAmount )
        {
            double rc = _PourAmount;
            try
            {
                m_Mutex.WaitOne( );
                if( m_UPCData.ContainsKey( _EventID ) )
                {
                    UPCData UPC = m_UPCData[_EventID].Find( U => U.TagID.Equals( _UPC.TagID ) );
                    if( null != UPC )
                    {
                        if( UPC.AmountInBottle >= _PourAmount )
                        {
                            UPC.AmountInBottle -= _PourAmount;
                        }
                        else if( 0 != UPC.AmountInBottle ) // BUGFIX: Added to fix defect #208
                        {
                            rc = UPC.AmountInBottle;
                            UPC.AmountInBottle = 0;
                        }
                        //else
                        //{
                        //    rc = UPC.AmountInBottle;
                        //    UPC.AmountInBottle = 0;
                        //}
                    }
                }
                else
                {
                    throw new MissingEventException( _EventID );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::AdjustTotalAmt", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
            return rc;
        }

        public void ClearUPCList( string _EventID )
        {
            try
            {
                m_Mutex.WaitOne( );
                if( m_UPCData.ContainsKey( _EventID ) )
                {
                    m_UPCData[_EventID].Clear( );
                }
                else
                {
                    throw new MissingEventException( _EventID );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ClientData::ClearUPCList", exp );
            }
            finally
            {
                m_Mutex.ReleaseMutex( );
            }
        }

        //public Jaxis.Readers.Identec.UPCData GetUPCFromDB( string _EventID, string _TagID )
        //{
        //    Jaxis.Readers.Identec.UPCData rc = null;
        //    try
        //    {
        //        using( SqlCeConnection connection = new SqlCeConnection( m_ConnectionString ) )
        //        {
        //            SqlCeCommand command = connection.CreateCommand( );
        //            command.CommandText = string.Format( "select RUNNING_VOLUME, START_VOLUME, SPOUT_TYPE, TAG_ID from BM_CART_BOTTLES where TAG_ID = '{0}' and EVENT_ID = '{1}'", _TagID, _EventID );
        //            connection.Open( );
        //            using( SqlCeDataReader Reader = command.ExecuteReader( ) )
        //            {
        //                Dictionary<int, List<Jaxis.Readers.Identec.UPCData>> UPCLists = new Dictionary<int, List<Jaxis.Readers.Identec.UPCData>>( );
        //                while( Reader.Read( ) )
        //                {
        //                    try
        //                    {
        //                        Jaxis.Readers.Identec.UPCData Item = new Jaxis.Readers.Identec.UPCData( );
        //                        Item.AmountInBottle = Convert.ToDouble( Reader[0] );
        //                        Int64 BottleSize = Convert.ToInt64( Reader[1] );
        //                        if( Int16.MaxValue < BottleSize )
        //                        {
        //                            Item.BottleSize = Int16.MaxValue;
        //                        }
        //                        else
        //                        {
        //                            Item.BottleSize = Convert.ToInt16( Reader[1] );
        //                        }
        //                        Item.NozzleDiameter = Convert.ToSingle( Reader[2] );
        //                        Item.TagID = Reader[3].ToString( );
        //                        Item.ViscocityByTemperature = new Dictionary<int, double>( );

        //                        rc = Item;
        //                    }
        //                    catch( Exception err )
        //                    {
        //                        Log.WriteException( "PullInventoryFromDB", err );
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch( Exception err )
        //    {
        //        Log.WriteException( "Get UPC From DB", err );

        //    }
        //    return rc;
        //}

        //public void PullInventoryFromDB( )
        //{
        //    try
        //    {
        //        List<string> Events = PullEventsFromDB( );

        //        foreach( string EventID in Events )
        //        {
        //            List<Jaxis.Readers.Identec.UPCData> UPCList = new List<Jaxis.Readers.Identec.UPCData>( );
        //            using( SqlCeConnection connection = new SqlCeConnection( m_ConnectionString ) )
        //            {
        //                SqlCeCommand command = connection.CreateCommand( );
        //                command.CommandText = string.Format( "select RUNNING_VOLUME, START_VOLUME, SPOUT_TYPE, TAG_ID from BM_CART_BOTTLES where EVENT_ID = '{0}'", EventID );
        //                connection.Open( );
        //                using( SqlCeDataReader Reader = command.ExecuteReader( ) )
        //                {
        //                    Dictionary<int, List<Jaxis.Readers.Identec.UPCData>> UPCLists = new Dictionary<int, List<Jaxis.Readers.Identec.UPCData>>( );
        //                    while( Reader.Read( ) )
        //                    {
        //                        try
        //                        {
        //                            Jaxis.Readers.Identec.UPCData Item = new Jaxis.Readers.Identec.UPCData( );
        //                            Item.AmountInBottle = Convert.ToDouble( Reader[0] );
        //                            Int64 BottleSize = Convert.ToInt64( Reader[1] );
        //                            if( Int16.MaxValue < BottleSize )
        //                            {
        //                                Item.BottleSize = Int16.MaxValue;
        //                            }
        //                            else
        //                            {
        //                                Item.BottleSize = Convert.ToInt16( Reader[1] );
        //                            }
        //                            Item.NozzleDiameter = Convert.ToSingle( Reader[2] );
        //                            Item.TagID = Reader[3].ToString( );
        //                            Item.ViscocityByTemperature = new Dictionary<int, double>( );

        //                            UPCList.Add( Item );
        //                        }
        //                        catch( Exception err )
        //                        {
        //                            Log.WriteException( "PullInventoryFromDB", err );
        //                        }
        //                    }
        //                }
        //            }
        //            ClientData.GetClientData( ).InitUPCList( EventID, UPCList );
        //        }
        //    }
        //    catch( Exception err )
        //    {
        //        Log.WriteException( "PullInventoryFromDB", err );
        //    }
        //}

        //private List<string> PullEventsFromDB( )
        //{
        //    List<string> rc = new List<string>( );
        //    using( SqlCeConnection connection = new SqlCeConnection( m_ConnectionString ) )
        //    {
        //        SqlCeCommand command = connection.CreateCommand( );
        //        command.CommandText = "select distinct( EVENT_ID ) from BM_CART_BOTTLES";
        //        connection.Open( );
        //        using( SqlCeDataReader Reader = command.ExecuteReader( ) )
        //        {
        //            while( Reader.Read( ) )
        //            {
        //                try
        //                {
        //                    string EventID = Reader[0].ToString( );
        //                    Log.Debug( string.Format( "Add Event {0}", EventID ) );
        //                    rc.Add( EventID );
        //                }
        //                catch( Exception err )
        //                {
        //                    Log.WriteException( "PullInventoryFromDB - getting event id's", err );
        //                }
        //            }
        //        }
        //    }
        //    return rc;
        //}

        //private void PullEventsByDeviceFromDB( )
        //{
        //    using( SqlCeConnection connection = new SqlCeConnection( m_ConnectionString ) )
        //    {
        //        SqlCeCommand command = connection.CreateCommand( );
        //        command.CommandText = "select * from BM_FORMULA_VALUES";
        //        connection.Open( );
        //        using( SqlCeDataReader Reader = command.ExecuteReader( ) )
        //        {
        //            while( Reader.Read( ) )
        //            {
        //                try
        //                {
        //                    string DeviceID = Reader[0].ToString( );
        //                    if( DeviceID.StartsWith( "D-" ) )
        //                    {
        //                        DeviceID = DeviceID.Substring( 2 );
        //                        string EventID = Reader[1].ToString( );
        //                        Log.Debug( string.Format( "Add Event {0} for Device{1}", EventID, DeviceID ) );
        //                        m_EventIDs[DeviceID] = EventID;
        //                    }
        //                }
        //                catch( Exception err )
        //                {
        //                    Log.WriteException( "PullInventoryFromDB - getting event id's", err );
        //                }
        //            }
        //        }
        //    }
        //}
    }
}