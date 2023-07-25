using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using Jaxis.Engine.Base;
using Jaxis.Engine.Base.Device;
using Jaxis.Interfaces;
using Jaxis.Util.Log4Net;
using Jaxis.Utility.Encryption;
using System.Reflection;

namespace EngineConfigUtil
{
    public partial class ConfigUtilForm : Form
    {
        List<DeviceConfig> m_Devices = new List<DeviceConfig>( );
        List<string> m_DependentNames = new List<string>( );
        List<string> m_FilterDependentNames = new List<string>();

        public ConfigUtilForm( )
        {
            try
            {
                InitializeComponent( );
                //using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                //{
                //    List<string> UIList = new List<string>();
                //    m_Devices = Client.GetDeviceList();
                //    foreach (DeviceConfig C in m_Devices)
                //    {
                //        UIList.Add(C.Name);
                //    }
                //    lbDevices.DataSource = UIList;
                //    lbFilterDevices.DataSource = UIList;
                //    tbError.Text = "No WCF Error";
                //}
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbGetDevices_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbGetDevices_Click( object sender, EventArgs e )
        {
            try
            {
                using( svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient( ) )
                {
                    //List<string> UIList = new List<string>( );
                    m_Devices = Client.GetDeviceList( );
                    UpdateGuiDeviceList();
                    //foreach( DeviceConfig C in m_Devices )
                    //{
                    //    UIList.Add( C.Name );
                    //}
                    //lbDevices.DataSource = UIList;
                    //lbFilterDevices.DataSource = UIList;
                    //lbFilterPlugInDevices.DataSource = UIList;
                    tbError.Text = "No WCF Error";
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbGetDevices_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbGetLog_Click( object sender, EventArgs e )
        {
            try
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    string EventLog = Client.GetEventLog( );
                    if( null != EventLog )
                    {
                        mLog.Text = EventLog;
                    }
                    tbError.Text = "No WCF Error";
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbGetLog_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void lbDevices_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                if( -1 != lbDevices.SelectedIndex )
                {
                    cmbState.Text = m_Devices[lbDevices.SelectedIndex].State.ToString( );
                    txtAssemblyName.Text = m_Devices[lbDevices.SelectedIndex].AssemblyName;
                    txtAssemblyType.Text = m_Devices[lbDevices.SelectedIndex].AssemblyType;
                    txtAssemblyVersion.Text = m_Devices[lbDevices.SelectedIndex].AssemblyVersion;
                    txtConsumerMessageType.Text = m_Devices[lbDevices.SelectedIndex].ConsumerMessageType.ToString( );
                    txtProducerMessageType.Text = m_Devices[lbDevices.SelectedIndex].ProducerMessageType.ToString( );
                    txtID.Text = m_Devices[lbDevices.SelectedIndex].ID;
                    txtName.Text = m_Devices[lbDevices.SelectedIndex].Name;
                    txtType.Text = m_Devices[lbDevices.SelectedIndex].Type.ToString( );
                    gridControlOptions.DataSource = null;
                    gridControlOptions.DataSource = m_Devices[lbDevices.SelectedIndex].Options;
                    //foreach (DeviceConfigOption S in m_Devices[lbDevices.SelectedIndex].Options)
                    //{

                    //    mOptions.Text += S.Name + Environment.NewLine;
                    //}
                    lbFilters.Items.Clear( );
                    gridControlDeviceFilterOptions.DataSource = null;
                    if( 0 != m_Devices[lbDevices.SelectedIndex].Filters.Count )
                    {
                        foreach( FilterConfig F in m_Devices[lbDevices.SelectedIndex].Filters )
                            lbFilters.Items.Add( F.Name );
                        gridControlDeviceFilterOptions.DataSource = m_Devices[lbDevices.SelectedIndex].Filters[0].Options;
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.lbDevices_SelectedIndexChanged()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbStopDevice_Click( object sender, EventArgs e )
        {
            try
            {
                if( -1 != lbDevices.SelectedIndex )
                {
                    using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                    {
                        Client.StopDevice( m_Devices[lbDevices.SelectedIndex] );
                        tbError.Text = "No WCF Error";
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbStopDevice_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbStartDevice_Click( object sender, EventArgs e )
        {
            try
            {
                if( -1 != lbDevices.SelectedIndex )
                {
                    using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                    {
                        Client.StartDevice( m_Devices[lbDevices.SelectedIndex] );
                        tbError.Text = "No WCF Error";
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbStartDevice_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbPushPlugIn_Click( object sender, EventArgs e )
        {
            List<Byte[]> Dependents = new List<byte[]>( );
            List<string> DependentNames = new List<string>( );
            try
            {
                if( null != tPlugIn.Text && System.IO.File.Exists( tPlugIn.Text ) )
                {
                    using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                    {
//                        Assembly Asm = Assembly.ReflectionOnlyLoadFrom(tPlugIn.Text);
//                        if (null != Asm)
                        {
                            //Asm.GetExportedTypes

                            //AssemblyName[] Refs = Asm.GetReferencedAssemblies();
                            //if (null != Refs)
                            //{
                            //    foreach (AssemblyName Name in Refs)
                            //    {
                            //        Assembly.ReflectionOnlyLoadFrom(Name.FullName);
                            //    }
                            //}
                            //Type[] Tps = Asm.GetExportedTypes();
                            DeviceConfig Config = new DeviceConfig();
                            Config.AssemblyName = tPlugIn.Text.Substring(tPlugIn.Text.LastIndexOf('\\') + 1);
                            Config.AssemblyVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(BaseDevice.GetPath() + tPlugIn.Text).FileVersion.ToString();
                            Config.ID = Guid.NewGuid().ToString();
                            Config.Name = Config.AssemblyName.Substring(0, Config.AssemblyName.LastIndexOf('.'));
                            //Type T = Asm.GetType(Config.Name);
                            //Config.AssemblyType = T.ToString();
                            Config.State = DeviceState.Stopped;
                            Config.Type = DeviceType.DataProducerConsumer;
                            //for ( int i = 0; i < gridViewPluginOptions.DataRowCount; i++)
                            //{
                            //    Config.Options.Add(gridViewPluginOptions.GetRow(i) as DeviceConfigOption);
                            //}
                            byte[] iv;
                            byte[] encrypt = BlowFishEncryption.Encrypt(System.IO.File.ReadAllBytes(tPlugIn.Text), out iv);
                            foreach (string Dependent in m_DependentNames)
                            {
                                int index = 0;
                                if (Dependent.Contains("\\"))
                                {
                                    index = Dependent.LastIndexOf('\\') + 1;
                                } 
                                DependentNames.Add(Dependent.Substring(index));
                                index = openFileDialog1.FileName.Length;
                                if (openFileDialog1.FileName.Contains("\\"))
                                {
                                    index = openFileDialog1.FileName.LastIndexOf('\\') + 1;
                                }
                                Dependents.Add(BlowFishEncryption.Encrypt(System.IO.File.ReadAllBytes(openFileDialog1.FileName.Substring(0, index) + Dependent), out iv));
                            }
                            Client.DownloadDevicePlugin(Config, encrypt, DependentNames, Dependents, iv);
                            m_DependentNames.Clear();
                            tbError.Text = "No WCF Error";
                        }
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbStartDevice_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbSelectFile_Click( object sender, EventArgs e )
        {
            try
            {
                if( DialogResult.OK == openFileDialog1.ShowDialog( ) )
                {
                    tPlugIn.Text = openFileDialog1.FileName;
                    m_DependentNames.Clear();
                    AddToDepList(m_DependentNames, tPlugIn.Text);
                    lbDependents.DataSource = m_DependentNames;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbSelectFile_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void AddToDepList(List<string> _DepList, string _AsmFile)
        {
            Assembly Asm = Assembly.ReflectionOnlyLoadFrom(_AsmFile);
            if (null != Asm)
            {
                AssemblyName[] Refs = Asm.GetReferencedAssemblies();
                if (null != Refs)
                {
                    foreach (AssemblyName Name in Refs)
                    {
                        if (!Name.Name.StartsWith("System") && !Name.Name.StartsWith("Jaxis") && !Name.Name.StartsWith("mscor") && !Name.Name.StartsWith("MessageLib"))
                        {
                            _DepList.Add(Name.Name + ".dll");
                            AddToDepList(_DepList, _AsmFile.Substring(0, _AsmFile.LastIndexOf('\\') + 1) + Name.Name + ".dll");
                        }
                    }
                }
            }
        }


        //private void sbSelectConfigFile_Click( object sender, EventArgs e )
        //{
        //    try
        //    {
        //        if( DialogResult.OK == openFileDialog1.ShowDialog( ) )
        //        {
        //            tConfigFile.Text = openFileDialog1.FileName;
        //            if( null != openFileDialog1.FileName )
        //            {
        //                using( StreamReader Reader = new StreamReader( openFileDialog1.FileName ) )
        //                {
        //                    string Data = Reader.ReadToEnd( );
        //                    DeviceConfigCollection Configs = null;
        //                    Configs = DeviceConfig.DeserializeObject<DeviceConfigCollection>( Data );
        //                    if( 0 != Configs.Configs.Count )
        //                    {
        //                        cbPlugInState.Text = Configs.Configs[0].State.ToString( );
        //                        tPlugInAssemblyName.Text = Configs.Configs[0].AssemblyName;
        //                        tPlugInAssemblyType.Text = Configs.Configs[0].AssemblyType;
        //                        tPlugInAssemblyVersion.Text = Configs.Configs[0].AssemblyVersion;
        //                        cbPlugInConMsgType.Text = Configs.Configs[0].ConsumerMessageType.ToString( );
        //                        tPlugInID.Text = Configs.Configs[0].ID;
        //                        tPlugInName.Text = Configs.Configs[0].Name;
        //                        cbPlugInType.Text = Configs.Configs[0].Type.ToString( );
        //                        gridControlPluginOptions.DataSource = null;
        //                        gridControlPluginOptions.DataSource = Configs.Configs[0].Options;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch( Exception exp )
        //    {
        //        Log.WriteException( "ConfigUtilForm.sbSelectConfigFile_Click()", exp );
        //        tbError.Text = exp.ToString( );
        //    }
        //}

        private void sbGetEngineVersion_Click( object sender, EventArgs e )
        {
            try
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    tEngineVersion.Text = Client.GetEngineRev( );
                    tbError.Text = "No WCF Error";
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbGetEngineVersion_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbGetEngineState_Click( object sender, EventArgs e )
        {
            try
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    tEngineState.Text = Client.GetEngineState( ).ToString( );
                    tbError.Text = "No WCF Error";
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbGetEngineState_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbStopEngine_Click( object sender, EventArgs e )
        {
            try
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    Client.StopEngine( );
                    tbError.Text = "No WCF Error";
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbStopEngine_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbStartEngine_Click( object sender, EventArgs e )
        {
            try
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    Client.StartEngine( );
                    tbError.Text = "No WCF Error";
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbStartEngine_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void lbFilters_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                gridControlDeviceFilterOptions.DataSource = null;
                if (lbFilters.SelectedIndex != -1 && lbFilters.SelectedIndex < m_Devices[lbDevices.SelectedIndex].Filters.Count)
                {
                    gridControlDeviceFilterOptions.DataSource = m_Devices[lbDevices.SelectedIndex].Filters[lbFilters.SelectedIndex].Options;
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.lbFilters_SelectedIndexChanged()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void lbFiltersFilters_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                tFilterAssemblyName.Text = "";
                tFilterAssemblyType.Text = "";
                tFilterName.Text = "";
                tFilterType.Text = "";
                tFilterVersion.Text = "";
                gridControlFilterOptions.DataSource = null;
                if (-1 != lbFilterDevices.SelectedIndex)
                {
                    if (lbFiltersFilters.SelectedIndex != -1 && lbFiltersFilters.SelectedIndex < m_Devices[lbFilterDevices.SelectedIndex].Filters.Count)
                    {
                        tFilterAssemblyName.Text = m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex].AssemblyName;
                        tFilterAssemblyType.Text = m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex].AssemblyType;
                        tFilterName.Text = m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex].Name;
                        tFilterType.Text = m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex].Type.ToString( );
                        tFilterVersion.Text = m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex].AssemblyVersion;
                        gridControlFilterOptions.DataSource = m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex].Options;
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.lbFilterDevices_SelectedIndexChanged()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void lbFilterDevices_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                lbFiltersFilters.Items.Clear( );
                if( 0 != m_Devices[lbFilterDevices.SelectedIndex].Filters.Count )
                {
                    foreach( FilterConfig F in m_Devices[lbFilterDevices.SelectedIndex].Filters )
                        lbFiltersFilters.Items.Add( F.Name );
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.lbDevices_SelectedIndexChanged()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        //private void sbFilterSelectPlugInFile_Click( object sender, EventArgs e )
        //{
        //    try
        //    {
        //        if( DialogResult.OK == openFileDialog1.ShowDialog( ) )
        //        {
        //            m_FilterDependentNames.Clear();
        //            AddToDepList(m_FilterDependentNames, tFilterPlugIn.Text);
        //            lbFilterDependents.DataSource = m_FilterDependentNames;
        //        }
        //    }
        //    catch( Exception exp )
        //    {
        //        Log.WriteException( "ConfigUtilForm.sbSelectFile_Click()", exp );
        //        tbError.Text = exp.ToString( );
        //    }
        //}

        //private void sbFilterSelectConfigFile_Click( object sender, EventArgs e )
        //{
        //    try
        //    {
        //        if( DialogResult.OK == openFileDialog1.ShowDialog( ) )
        //        {
        //            tFilterConfigFile.Text = openFileDialog1.FileName;
        //            if( null != openFileDialog1.FileName )
        //            {
        //                using( StreamReader Reader = new StreamReader( openFileDialog1.FileName ) )
        //                {
        //                    string Data = Reader.ReadToEnd( );
        //                    DeviceConfigCollection Configs = null;
        //                    Configs = DeviceConfig.DeserializeObject<DeviceConfigCollection>( Data );
        //                    if( 0 != Configs.Filters.Count )
        //                    {
        //                        tFilterPlugInAssemblyName.Text = Configs.Filters[0].AssemblyName;
        //                        tFilterPlugInAssemblyType.Text = Configs.Filters[0].AssemblyType;
        //                        tFilterPlugInAssemblyVersion.Text = Configs.Filters[0].AssemblyVersion;
        //                        tFilterPlugInName.Text = Configs.Filters[0].Name;
        //                        cbFilterPlugInType.Text = Configs.Filters[0].Type.ToString( );
        //                        gridControlFilterPlugInOptions.DataSource = null;
        //                        //gridControlPluginOptions.DataSource = null;
        //                        //gridControlPluginOptions.DataSource = Configs.Filters[0].Options;
        //                    }
        //                    else if( 0 != Configs.Configs[0].Filters.Count )
        //                    {
        //                        tFilterPlugInAssemblyName.Text = Configs.Configs[0].Filters[0].AssemblyName;
        //                        tFilterPlugInAssemblyType.Text = Configs.Configs[0].Filters[0].AssemblyType;
        //                        tFilterPlugInAssemblyVersion.Text = Configs.Configs[0].Filters[0].AssemblyVersion;
        //                        tFilterPlugInName.Text = Configs.Configs[0].Filters[0].Name;
        //                        cbFilterPlugInType.Text = Configs.Configs[0].Filters[0].Type.ToString( );
        //                        gridControlFilterPlugInOptions.DataSource = Configs.Configs[0].Filters[0].Options;
        //                        //gridControlPluginOptions.DataSource = null;
        //                        //gridControlPluginOptions.DataSource = Configs.Configs[0].Filters[0];
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch( Exception exp )
        //    {
        //        Log.WriteException( "ConfigUtilForm.sbSelectConfigFile_Click()", exp );
        //        tbError.Text = exp.ToString( );
        //    }
        //}

//        private void sbFilterPushPlugIn_Click( object sender, EventArgs e )
//        {
//            List<Byte[]> Dependents = new List<byte[]>();
//            List<string> DependentNames = new List<string>();
//            try
//            {
//                if( null != tFilterPlugIn.Text && System.IO.File.Exists( tFilterPlugIn.Text ) &&
//                    -1 != lbFilterDevices.SelectedIndex )
//                {
//                    using( svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient( "EngineConfig", txtURL.Text) )
//                    {
//                        FilterConfig Config = new FilterConfig( );
//                        //Config.AssemblyName = tFilterPlugInAssemblyName.Text;
//                        //Config.AssemblyType = tFilterPlugInAssemblyType.Text;
//                        //Config.AssemblyVersion = tFilterPlugInAssemblyVersion.Text;
//                        //Config.Name = tFilterPlugInName.Text;
//                        //if( FilterType.Inbound.ToString( ) == cbFilterPlugInType.Text )
//                        //    Config.Type = FilterType.Inbound;
//                        //else if( FilterType.InOut.ToString( ) == cbFilterPlugInType.Text )
//                        //    Config.Type = FilterType.InOut;
//                        //else if( FilterType.Outbound.ToString( ) == cbFilterPlugInType.Text )
//                        //    Config.Type = FilterType.Outbound;

//                        //for (int i = 0; i < gridViewFilterPlugInOptions.DataRowCount; i++)
//                        //{
//                        //    Config.Options.Add(gridViewFilterPlugInOptions.GetRow(i) as DeviceConfigOption);
//                        //}
//                        byte[] iv;
//                        byte[] encrypt = BlowFishEncryption.Encrypt( System.IO.File.ReadAllBytes( tFilterPlugIn.Text ), out iv );
//#warning need to add denp to wcf push
//                        foreach (string Dependent in m_FilterDependentNames)
//                        {
//                            DependentNames.Add(Dependent.Substring(openFileDialog1.FileName.LastIndexOf('\\') + 1));
//                            Dependents.Add( BlowFishEncryption.Encrypt( System.IO.File.ReadAllBytes( Dependent ), out iv ) );
//                        }
//                        Client.DownloadFilterPlugin(m_Devices[lbFilterDevices.SelectedIndex], Config, encrypt, iv);
//                        m_FilterDependentNames.Clear();
//                        tbError.Text = "No WCF Error";
//                    }
//                }
//            }
//            catch( Exception exp )
//            {
//                Log.WriteException( "ConfigUtilForm.sbStartDevice_Click()", exp );
//                tbError.Text = exp.ToString( );
//            }
//        }

        private void sbUpdateFilterOptions_Click( object sender, EventArgs e )
        {
            try
            {
                if( -1 != lbFilterDevices.SelectedIndex && -1 != lbFiltersFilters.SelectedIndex )
                {
                    using( svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient( ) )
                    {
                        //m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex].Options.Clear( );
                        //for (int i = 0; i < gridViewFilterOptions.DataRowCount; i++)
                        //{
                        //    m_Devices[lbDevices.SelectedIndex].Options.Add(gridViewFilterOptions.GetRow(i) as DeviceConfigOption);
                        //}
                        Client.UpdateFilterOptions( m_Devices[lbDevices.SelectedIndex], m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex] );
                        tbError.Text = "No WCF Error";
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbUpdateFilterOptions_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbUpdateOptions_Click( object sender, EventArgs e )
        {
            try
            {
                if( -1 != lbDevices.SelectedIndex )
                {
                    using( svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient( ) )
                    {
                        //m_Devices[lbDevices.SelectedIndex].ID = txtID.Text;
                        m_Devices[lbDevices.SelectedIndex].ConsumerMessageType = Convert.ToUInt32(txtConsumerMessageType.Text);
                        m_Devices[lbDevices.SelectedIndex].Name = txtName.Text;
                        m_Devices[lbDevices.SelectedIndex].ProducerMessageType = Convert.ToUInt32(txtProducerMessageType.Text);

                        if (DeviceState.Started.ToString() == cmbState.Text)
                            m_Devices[lbDevices.SelectedIndex].State = DeviceState.Started;
                        else 
                            m_Devices[lbDevices.SelectedIndex].State = DeviceState.Stopped;

                        Client.UpdateOptions(m_Devices[lbDevices.SelectedIndex]);
                        tbError.Text = "No WCF Error";
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbUpdateOptions_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbUnloadFilter_Click( object sender, EventArgs e )
        {
            try
            {
                if( -1 != lbFilterDevices.SelectedIndex && -1 != lbFiltersFilters.SelectedIndex )
                {
                    using( svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient( ) )
                    {
                        Client.UnLoadFilter( m_Devices[lbDevices.SelectedIndex], m_Devices[lbFilterDevices.SelectedIndex].Filters[lbFiltersFilters.SelectedIndex] );
                        tbError.Text = "No WCF Error";
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbUnloadFilter_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbUnloadDevice_Click( object sender, EventArgs e )
        {
            try
            {
                if( -1 != lbDevices.SelectedIndex )
                {
                    using( svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient( ) )
                    {
                        Client.UnLoadDevice( m_Devices[lbDevices.SelectedIndex] );
                        tbError.Text = "No WCF Error";
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbUnloadDevice_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbDependSelectFile_Click( object sender, EventArgs e )
        {
            try
            {
                if( DialogResult.OK == openFileDialog1.ShowDialog( ) )
                {
                    if( null != openFileDialog1.FileName )
                    {
                        m_DependentNames.Add( openFileDialog1.FileName );
                        lbDependents.DataSource = m_DependentNames;
                    }
                }
            }
            catch( Exception exp )
            {
                Log.WriteException( "ConfigUtilForm.sbDependSelectFile_Click()", exp );
                tbError.Text = exp.ToString( );
            }
        }

        private void sbRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbDependents.SelectedIndex > 0)
                {
                    m_DependentNames.RemoveAt(lbDependents.SelectedIndex);
                    lbDependents.DataSource = m_DependentNames;
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("ConfigUtilForm.sbRemove_Click()", exp);
                tbError.Text = exp.ToString();
            }
        }

        private void sbFilterDependAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                {
                    if (null != openFileDialog1.FileName)
                    {
                        m_DependentNames.Add(openFileDialog1.FileName);
                        lbDependents.DataSource = m_DependentNames;
                    }
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("ConfigUtilForm.sbFilterDependAdd_Click()", exp);
                tbError.Text = exp.ToString();
            }
        }

        //private void sbFilterRemove_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (lbFilterDependents.SelectedIndex > 0)
        //        {
        //            m_FilterDependentNames.RemoveAt(lbFilterDependents.SelectedIndex);
        //            lbFilterDependents.DataSource = m_FilterDependentNames;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        Log.WriteException("ConfigUtilForm.sbFilterRemove_Click()", exp);
        //        tbError.Text = exp.ToString();
        //    }
        //}

        private void sbRemoveOption_Click(object sender, EventArgs e)
        {
            gridViewOptions.DeleteSelectedRows();
            gridViewOptions.RefreshData();
            //for (int i = 0; i < gridViewOptions.DataRowCount; i++)
            //{
            //    if ( gridViewOptions.IsRowSelected(i) )
            //    {
            //        m_Devices[lbDevices.SelectedIndex].Options.Remove(gridViewOptions.GetRow(i) as DeviceConfigOption);
            //    }
            //}
            //gridControlPluginOptions.DataSource = null;
            //gridControlPluginOptions.DataSource = m_Devices[lbDevices.SelectedIndex].Options;
        }

        private void sbAddOption_Click(object sender, EventArgs e)
        {
            //gridViewOptions.AddNewRow();
            DeviceConfigOption Option = new DeviceConfigOption();
            Option.Name = "Name";
            Option.Value = "Value";
            m_Devices[lbDevices.SelectedIndex].Options.Add(Option);
            gridControlOptions.DataSource = null;
            gridControlOptions.DataSource = m_Devices[lbDevices.SelectedIndex].Options;
            gridViewOptions.RefreshData();
        }

        private void sbRemoveButton_Click(object sender, EventArgs e)
        {
            gridViewFilterOptions.DeleteSelectedRows();
            gridViewFilterOptions.RefreshData();
        }

        private void sbFilterAddOption_Click(object sender, EventArgs e)
        {
            DeviceConfigOption Option = new DeviceConfigOption();
            Option.Name = "Name";
            Option.Value = "Value";
            m_Devices[lbFilterDevices.SelectedIndex].Options.Add(Option);
            gridControlFilterOptions.DataSource = null;
            gridControlFilterOptions.DataSource = m_Devices[lbFilterDevices.SelectedIndex].Options;
            gridViewFilterOptions.RefreshData();
        }

        private void abAddDevice_Click(object sender, EventArgs e)
        {
            using (AddDeviceForm Form = new AddDeviceForm())
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    Cursor = Cursors.WaitCursor;
                    List<DeviceConfig> Configs = Client.GetLocalDevices();
                    Cursor = Cursors.Default;
                    Form.gridControlLocalDevices.DataSource = Configs;
                    if (DialogResult.OK == Form.ShowDialog())
                    {
                        for (int i = 0; i < Form.gridViewLocalDevices.DataRowCount; i++)
                        {
                            if (Form.gridViewLocalDevices.IsRowSelected(i))
                            {
                                Client.AddDevice(Form.gridViewLocalDevices.GetRow(i) as DeviceConfig);
                                m_Devices.Add(Form.gridViewLocalDevices.GetRow(i) as DeviceConfig);
                            }
                        }
                        UpdateGuiDeviceList();
                    }
                }
            }
        }

        private void sbAddFilter_Click(object sender, EventArgs e)
        {
            using (AddDeviceForm Form = new AddDeviceForm())
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    Cursor = Cursors.WaitCursor;
                    List<FilterConfig> Configs = Client.GetLocalFilters();
                    Cursor = Cursors.Default;
                    Form.gridControlLocalDevices.DataSource = Configs;
                    if (DialogResult.OK == Form.ShowDialog())
                    {
                        for (int i = 0; i < Form.gridViewLocalDevices.DataRowCount; i++)
                        {
                            if (Form.gridViewLocalDevices.IsRowSelected(i) )
                            {
                                Client.AddFilter(m_Devices[lbFilterDevices.SelectedIndex], Form.gridViewLocalDevices.GetRow(i) as FilterConfig);
                                m_Devices[lbFilterDevices.SelectedIndex].Filters.Add(Form.gridViewLocalDevices.GetRow(i) as FilterConfig);
                            }
                        }
                        UpdateGuiDeviceList();
                    }
                }
            }
        }

        private void sbDeviceAddFilter_Click(object sender, EventArgs e)
        {
            using (AddDeviceForm Form = new AddDeviceForm())
            {
                using (svcEngineConfig.EngineConfigServiceClient Client = new svcEngineConfig.EngineConfigServiceClient())
                {
                    Cursor = Cursors.WaitCursor;
                    List<FilterConfig> Configs = Client.GetLocalFilters();
                    Cursor = Cursors.Default;
                    Form.gridControlLocalDevices.DataSource = Configs;
                    if (DialogResult.OK == Form.ShowDialog())
                    {
                        for (int i = 0; i < Form.gridViewLocalDevices.DataRowCount; i++)
                        {
                            if (Form.gridViewLocalDevices.IsRowSelected(i))
                            {
                                Client.AddFilter(m_Devices[lbDevices.SelectedIndex], Form.gridViewLocalDevices.GetRow(i) as FilterConfig);
                                m_Devices[lbFilterDevices.SelectedIndex].Filters.Add(Form.gridViewLocalDevices.GetRow(i) as FilterConfig);
                            }
                        }
                        UpdateGuiDeviceList();
                    }
                }
            }
        }

        private void UpdateGuiDeviceList()
        {
            List<string> UIList = new List<string>();
            foreach (DeviceConfig C in m_Devices)
            {
                UIList.Add(C.Name);
            }
            lbDevices.DataSource = UIList;
            lbFilterDevices.DataSource = UIList;
        }

        //private void sbPluginRemoveOption_Click(object sender, EventArgs e)
        //{
        //    gridViewPluginOptions.DeleteSelectedRows();
        //    gridViewPluginOptions.RefreshData();
        //}

        //private void sbPluginAddOption_Click(object sender, EventArgs e)
        //{
        //    List<DeviceConfigOption> Options = new List<DeviceConfigOption>();
        //    for ( int i = 0; i < gridViewPluginOptions.DataRowCount; i++)
        //    {
        //        Options.Add(gridViewPluginOptions.GetRow(i) as DeviceConfigOption);
        //    }
        //    DeviceConfigOption Option = new DeviceConfigOption();
        //    Option.Name = "Name";
        //    Option.Value = "Value";
        //    Options.Add(Option);
        //    gridControlPluginOptions.DataSource = null;
        //    gridControlPluginOptions.DataSource = Options;
        //    gridViewPluginOptions.RefreshData();
        //}

        //private void sbPluginFilterRemoveOption_Click(object sender, EventArgs e)
        //{
        //    gridViewFilterPlugInOptions.DeleteSelectedRows();
        //    gridViewFilterPlugInOptions.RefreshData();
        //}

        //private void sbPluginFilterAddOption_Click(object sender, EventArgs e)
        //{
        //    List<DeviceConfigOption> Options = new List<DeviceConfigOption>();
        //    for (int i = 0; i < gridViewFilterPlugInOptions.DataRowCount; i++)
        //    {
        //        Options.Add(gridViewFilterPlugInOptions.GetRow(i) as DeviceConfigOption);
        //    }
        //    DeviceConfigOption Option = new DeviceConfigOption();
        //    Option.Name = "Name";
        //    Option.Value = "Value";
        //    Options.Add(Option);
        //    gridControlFilterPlugInOptions.DataSource = null;
        //    gridControlFilterPlugInOptions.DataSource = Options;
        //    gridViewFilterPlugInOptions.RefreshData();
        //}
    }
}