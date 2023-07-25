using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Interfaces.Tags;
using Jaxis.Util.Log4Net;
using Jaxis.MessageLibrary;
using System.Collections;
using System.Linq;

namespace Jaxis.Engine.Filter
{
/*
                <FilterConfig>
                    <AssemblyName>Jaxis.Engine.Filter.dll</AssemblyName>
                    <AssemblyType>Jaxis.Engine.Filter.TagDetachFilter</AssemblyType>
                    <AssemblyVersion>1.0</AssemblyVersion>
                    <Name>Tag Detach Filter</Name>
                    <Type>Outbound</Type>
                    <Options>
                        <!-- Timeout -->
                        <string>1.5</string>
                    </Options>
                </FilterConfig>
*/

    public class TagDetachFilter : IFilter
    {

        public static FilterConfig GetDefaultFilterConfig()
        {
            FilterConfig rc = new FilterConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.Name = "TagDetachFilter";
            rc.Type = FilterType.Outbound;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "Timeout";
            Option1.Value = "1.5";
            rc.Options.Add(Option1);
            return rc;
        }

        protected IFilterConfig m_Config = null;
        private bool m_Running = false;
        protected FilterType m_Type;
        protected double m_Timeout = 0;
        protected Dictionary<string, PhaseMessage> m_TagList = new Dictionary<string, PhaseMessage>( );

        protected TimeSpan m_ReadWindow;
        protected Queue<string> m_KeysToRemove = new Queue<string>( );

        public TagDetachFilter( IFilterConfig _Config )
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
                    _Config.Options.Add(Option1);
                    DeviceConfigOption Option2 = new DeviceConfigOption();
                    Option2.Name = "";
                    Option2.Value = "0";
                    _Config.Options.Add(Option2);

                }
                m_Timeout = Convert.ToDouble(_Config.Options[0].Value, CultureInfo.InvariantCulture);

                int Seconds = (int)m_Timeout;
                int Thousands = (int)(( m_Timeout % 1 ) * 1000);
                m_ReadWindow = new TimeSpan( 0, 0, 0, Seconds, Thousands );

                m_Running = true;

                Task.Factory.StartNew(ProcessTagList);
            }
            catch
            {
            
            }
        }

        protected void ProcessTagList( )
        {
            try
            {
                while( true == m_Running )
                {
                    lock( m_TagList )
                    {
                        List<PhaseMessage> tagList = m_TagList.Values.ToList();
                        foreach( var phaseMessage in tagList )
                        {
                            if( phaseMessage.ReadTime < DateTime.Now.Subtract( m_ReadWindow ) )
                            {
                                m_TagList.Remove(phaseMessage.TagID);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public bool Filter( IMessage _Message )
        {
            bool rc = false;
            if( _Message is ITag )
            {
                PhaseMessage Tag = _Message as PhaseMessage;
                if( null != Tag )
                {
                    if( TagPhaseType.Disconnect == Tag.EventType )
                    {
                        m_TagList[Tag.TagID] = Tag;
                    }
                    else if( TagPhaseType.Connect == Tag.EventType )
                    {
                        if( m_TagList.ContainsKey( Tag.TagID ) )
                        {
                            m_TagList.Remove( Tag.TagID );
                        }
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