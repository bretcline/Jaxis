using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisEngine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.AlienRFID.MessageLibrary;
using Jaxis.Util.Log4Net;


namespace Jaxis.MessageProcessor.Alien
{
    public class ReadTimeFilter : IFilter
    {
        protected IFilterConfig m_Config = null;
        protected FilterType m_Type;
        protected int m_Timeout = 0;
        protected int m_Backlog = 0;
        protected Dictionary<string, IDeviceMessage> m_Tags = new Dictionary<string, IDeviceMessage>( );
        protected Queue<string> m_KeysToRemove = new Queue<string>( );

        public ReadTimeFilter( IFilterConfig _Config )
        {
            m_Config = _Config;
            m_Type = _Config.Type;
            m_Timeout = Convert.ToInt32( _Config.Options[0] );
            m_Backlog = Convert.ToInt32( _Config.Options[1] );
        }

        public bool Filter( IMessage _Message )
        {
            return Log.Wrap<bool>( "Processor::Filter", LogType.Debug, true, ( ) =>
            {
                TimeSpan ReadWindow = new TimeSpan( 0, 0, m_Timeout );
                bool rc = false;

                IDeviceMessage Message = _Message as IDeviceMessage;

                try
                {
                    string TagID = Message.Tag;

                    if( !m_Tags.ContainsKey( TagID ) )
                    {
                        m_Tags.Add( Message.Tag, Message );
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
                    Log.WriteException( string.Format( "On Read: {0}", Message.Tag ), err );
                }
                return rc;
            } );

        }

        #region IFilter Members


        public IFilterConfig Config
        {
            get { return m_Config; }
        }

        #endregion
    }

}
