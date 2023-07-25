using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.Util.Log4Net;
using Jaxis.MessageLibrary;

namespace Jaxis.Engine.Filter
{
/*
                    <FilterConfig>
                    <AssemblyName>Jaxis.Engine.Filter.dll</AssemblyName>
                    <AssemblyType>Jaxis.Engine.Filter.DuplicateTagMessageFilter</AssemblyType>
                    <AssemblyVersion>1.0</AssemblyVersion>
                    <Name>Read Time Filter</Name>
                    <Type>Outbound</Type>
                    <Options>
                        <!-- Timeout -->
                        <string>1.5</string>
                        <string>PhaseMessage</string>
                    </Options>
                </FilterConfig>
*/
    public class DuplicateTagMessageFilter : IFilter
    {
        public static FilterConfig GetDefaultFilterConfig()
        {
            var rc = new FilterConfig();
            var asm = Assembly.GetExecutingAssembly();
            rc.AssemblyName = asm.ManifestModule.Name;
            rc.AssemblyType = MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.Name = "DuplicateTagMessageFilter";
            rc.Type = FilterType.Outbound;
            var option1 = new DeviceConfigOption {Name = "Timeout", Value = "1.5"};
            rc.Options.Add(option1);
            var option2 = new DeviceConfigOption {Name = "Type", Value = "PhaseMessage"};
            rc.Options.Add(option2);
            return rc;
        }

        protected IFilterConfig m_Config = null;
        protected FilterType m_Type;
        protected double m_Timeout = 0;
        protected Dictionary<string, Dictionary<string, ITag>> m_MessageTypes = new Dictionary<string, Dictionary<string, ITag>>( );

        protected TimeSpan m_ReadWindow;
        protected Queue<string> m_KeysToRemove = new Queue<string>( );

        public DuplicateTagMessageFilter( IFilterConfig _config )
        {
            try
            {
                ( (FilterConfig) _config ).AssemblyVersion = Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( );
                m_Config = _config;
                m_Type = _config.Type;
                if( 2 < _config.Options.Count )
                {
                    var option1 = new DeviceConfigOption {Name = "Timeout", Value = "0"};
                    _config.Options.Add(option1);
                    var option2 = new DeviceConfigOption {Name = "Types", Value = "0"};
                    _config.Options.Add(option2);

                }
                m_Timeout = Convert.ToDouble(_config.Options[0].Value, CultureInfo.InvariantCulture);

                var types = _config.Options[1].Value.Split( '|' );

                foreach( string messageType in types )
                {
                    m_MessageTypes[messageType] = new Dictionary<string, ITag>( );
                }
                var seconds = (int)m_Timeout;
                var thousands = (int)(( m_Timeout % 1 ) * 1000);
                m_ReadWindow = new TimeSpan( 0, 0, 0, seconds, thousands );
            }
            catch (Exception)
            {
            
            }
        }

        public bool Filter( IMessage _message )
        {
            bool rc = false;
            if( _message is ITag )
            {
                var tag = _message as ITag;
                string messageType = _message.GetType( ).Name;
                if( m_MessageTypes.ContainsKey( messageType ) )
                {
                    try
                    {
                        var deviceId = string.Empty;
                        var tagKey = tag.TagID;
                        if( tag is PhaseMessage )
                        {
                            var phase = tag as PhaseMessage;
                            tagKey += string.Format( " {0}",phase.EventType );
                            deviceId = phase.DeviceID;
                        }
                        if( !m_MessageTypes[messageType].ContainsKey( tagKey ) )
                        {
                            m_MessageTypes[messageType][tagKey] = tag;
                        }
                        else
                        {
                            if( ( tag.ReadTime - m_MessageTypes[messageType][tagKey].ReadTime ) < m_ReadWindow )
                            {
                                Log.Debug( string.Format( "{6} Filter out TagID {0} {5} - {1} {2} {3} < {4}"
                                    , tagKey, tag.ReadTime, m_MessageTypes[messageType][tagKey].ReadTime
                                    , tag.ReadTime - m_MessageTypes[messageType][tagKey].ReadTime
                                    , m_ReadWindow, messageType, deviceId ) );
                                rc = true;
                            }
                            m_MessageTypes[messageType][tagKey] = tag;
                        }
                    }
                    catch( Exception err )
                    {
                        Log.WriteException( string.Format( "On Read: {0}", tag.TagID ), err );
                    }
                }
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