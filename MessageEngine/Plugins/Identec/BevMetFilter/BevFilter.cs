using System;
using System.Collections.Generic;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

//<Filters>
//  <FilterConfig>
//    <AssemblyName>BevMetFilter.dll</AssemblyName>
//    <AssemblyType>BevMetFilter.BevFilter</AssemblyType>
//    <AssemblyVersion>1.0</AssemblyVersion>
//    <Name>Bev Filter</Name>
//    <Type>Outbound</Type>
//  </FilterConfig>
//</Filters>

namespace Jaxis.Engine.Filter
{
    public class BevFilter : IFilter
    {
        protected IFilterConfig m_Config = null;
        protected FilterType m_Type;
        protected Dictionary<string, PourMessage> m_Tags = new Dictionary<string, PourMessage>( );

        public BevFilter( IFilterConfig _Config )
        {
            ( _Config as FilterConfig ).AssemblyVersion = Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( );
            m_Config = _Config;
            m_Type = _Config.Type;
        }

        public bool Filter( IMessage _Message )
        {
            // MLF - This will filter pours read across multiple readers and the dup pours sent from tag
            // All pours with pourcount = last pourcount from same tag will be filtered, only pours are filtered
            bool rc = false;

            try
            {
                if( null != _Message as PourMessage )
                {
                    PourMessage Message = _Message as PourMessage;

                    if( !m_Tags.ContainsKey( Message.TagID ) )
                    {
                        m_Tags.Add( Message.TagID, Message );
                    }
                    else if( Message.PourCount == m_Tags[Message.TagID].PourCount )
                    {
                        rc = true;
                    }
                    else
                    {
                        m_Tags[Message.TagID] = Message;
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "BevFilter::Filter", exp );
            }
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