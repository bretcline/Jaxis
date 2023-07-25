using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jaxis.Interfaces;
using System.Reflection;
using Jaxis.Engine.Base.Device;
using Jaxis.Util.Log4Net;
using Jaxis.Interfaces.Tags;

namespace Jaxis.Engine.Filter
{
    class DatabaseStorageFilter : IFilter
    {
        public static FilterConfig GetDefaultFilterConfig()
        {
            FilterConfig rc = new FilterConfig();
            System.Reflection.Assembly Asm = System.Reflection.Assembly.GetExecutingAssembly();
            rc.AssemblyName = Asm.ManifestModule.Name;
            rc.AssemblyType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
            rc.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + "\\" + rc.AssemblyName).FileVersion;
            rc.Name = "DatabaseStorageFilter";
            rc.Type = FilterType.Outbound;
            DeviceConfigOption Option1 = new DeviceConfigOption();
            Option1.Name = "ConnectionString";
            Option1.Value = "";
            rc.Options.Add(Option1);
            return rc;
        }

        protected IFilterConfig m_Config = null;
        protected FilterType m_Type;
        
        protected string m_ConnectionString = string.Empty;

        public DatabaseStorageFilter( IFilterConfig _Config )
        {
            try
            {
                ( _Config as FilterConfig ).AssemblyVersion = Assembly.GetExecutingAssembly( ).GetName( ).Version.ToString( );
                m_Config = _Config;
                m_Type = _Config.Type;
                m_ConnectionString = _Config.Options[0].Value;
            }
            catch
            {

            }
        }

        public bool Filter( IMessage _Message )
        {
            return Log.Wrap<bool>( "Processor::Filter", LogType.Debug, true, ( ) =>
            {
                bool rc = false;

                ITagRead Message = _Message as ITagRead;

                try
                {
                    if( null != Message )
                    {
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
        }

        #region IFilter Members

        public IFilterConfig Config
        {
            get { return m_Config; }
        }
        #endregion IFilter Members
    }
}
