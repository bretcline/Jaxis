using System;
using System.Collections.Generic;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.Util.Log4Net;

namespace Jaxis.Engine.Filter
{
    public class ReadTimeFilter : IFilter
    {
        public static FilterConfig GetDefaultFilterConfig()
        {
            FilterConfig rc = new FilterConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.Name = "ReadTimeFilter";
            rc.Type = FilterType.Outbound;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "Timeout";
            Option1.Value = "1000";
            rc.Options.Add(Option1);
            DeviceConfigOption Option2 = new DeviceConfigOption();
            Option2.Name = "Backlog";
            Option2.Value = "1000";
            rc.Options.Add(Option2);
            return rc;
        }

        protected IFilterConfig m_Config = null;
        protected FilterType m_Type;
        protected int m_Timeout = 0;
        protected int m_Backlog = 0;
        protected TimeSpan m_ReadWindow;
        protected Dictionary<string, ITagRead> m_Tags = new Dictionary<string, ITagRead>( );
        protected Queue<string> m_KeysToRemove = new Queue<string>( );

        public ReadTimeFilter( IFilterConfig _Config )
        {
            try
            {
                ( _Config as FilterConfig ).AssemblyVersion = Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( );
                m_Config = _Config;
                m_Type = _Config.Type;
                if( 2 < _Config.Options.Count )
                {
                    DeviceConfigOption Option1 = new DeviceConfigOption();
                    Option1.Name = "Timeout";
                    Option1.Value = "0";
                    _Config.Options.Add( Option1 );
                    DeviceConfigOption Option2 = new DeviceConfigOption();
                    Option2.Name = "Backlog";
                    Option2.Value = "0";
                    _Config.Options.Add( Option2 );
                }
                m_Timeout = Convert.ToInt32( _Config.Options[0].Value );
                m_Backlog = Convert.ToInt32( _Config.Options[1].Value );
                m_ReadWindow = new TimeSpan( 0, 0, m_Timeout );
            }
            catch
            {
            
            }
        }

        public bool Filter( IMessage _Message )
        {
            bool rc = false;
            rc = Log.Time<bool>( "Processor::Filter", LogType.Debug, true, ( ) =>
            {
                ITagRead Message = _Message as ITagRead;
                try
                {
                    if( null != Message )
                    {
                        string TagID = Message.TagID;

                        if( !m_Tags.ContainsKey( TagID ) )
                        {
                            m_Tags.Add( Message.TagID, Message );
                            m_KeysToRemove.Enqueue( TagID );
                            rc = true;
                            if( m_Backlog < m_KeysToRemove.Count )
                            {
                                string Remove = m_KeysToRemove.Dequeue( );
                                m_Tags.Remove( Remove );
                            }
                        }
                        else if( ( DateTime.Now - m_Tags[TagID].ReadTime ) > m_ReadWindow )
                        {
                            m_Tags[Message.TagID] = Message;
                            rc = true;
                        }
                    }
                    else
                    {
                        // Not a Tag Read...dont filter.
                        rc = true;
                    }
                }
                catch( Exception err )
                {
                    Log.WriteException( string.Format( "On Read: {0}", Message.TagID ), err );
                }
                return rc;
            } );
            Log.Debug( string.Format( "Processor::Filter - {0}", rc ) );
            return rc;
        }

        #region IFilter Members

        public IFilterConfig Config
        {
            get { return m_Config; }
        }

        #endregion IFilter Members
    }
}