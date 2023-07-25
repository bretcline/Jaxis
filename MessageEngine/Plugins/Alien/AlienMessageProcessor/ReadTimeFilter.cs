using System;
using System.Collections.Generic;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.Util.Log4Net;
using Jaxis.Engine.Base.Device;

namespace Jaxis.MessageProcessor.Alien
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
        protected Dictionary<string, ITagRead> m_Tags = new Dictionary<string, ITagRead>( );
        protected Queue<string> m_KeysToRemove = new Queue<string>( );

        public ReadTimeFilter( IFilterConfig _Config )
        {
            m_Config = _Config;
            m_Type = _Config.Type;
            m_Timeout = Convert.ToInt32( _Config.Options[0].Value );
            m_Backlog = Convert.ToInt32( _Config.Options[1].Value );
        }

        public bool Filter( IMessage _Message )
        {
            return Log.Wrap<bool>( "Processor::Filter", LogType.Debug, true, ( ) =>
            {
                TimeSpan ReadWindow = new TimeSpan( 0, 0, m_Timeout );
                bool rc = false;

                ITagRead Message = _Message as ITagRead;

                try
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
                    else
                    {
                        if( ( Message.ReadTime - m_Tags[TagID].ReadTime ) > ReadWindow )
                        {
                            m_Tags[TagID] = Message;
                            rc = true;
                        }
                        //else if( m_Tags[TagID].ReadTime < Message.ReadTime )
                        //{
                        //    m_Tags[TagID] = Message;
                        //}
                    }
                }
                catch( Exception err )
                {
                    Log.WriteException( string.Format( "On Read: {0}", Message.TagID ), err );
                }
                return rc;
            } );
        }

        #region IFilter Members

        public IFilterConfig Config
        {
            get { return m_Config; }
        }

        #endregion IFilter Members
    }
}