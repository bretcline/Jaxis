using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BeverageManagement.Forms.Activity.Widgets;
using DevExpress.Charts.Native;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;
using HostWCFService;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.Activity
{
    public partial class frmActivity : DevExpress.XtraEditors.XtraForm
    {
        private IEnumerable<IBLCategory> m_RootCategories = null;
        readonly Dictionary<string, CheckedListBoxItem> m_WidgetList = new Dictionary<string, CheckedListBoxItem>();
        Dictionary<string, int> m_MessageCount = new Dictionary<string, int>( );
        readonly Dictionary<string, IActivityControl> m_Widgets = new Dictionary<string, IActivityControl>();
        private readonly string m_LayoutPath = string.Format( "{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData ), "docActivities.xml" );
        readonly Dictionary<Type, IList<IActivityControl>> m_ActiveWidgets = new Dictionary<Type, IList<IActivityControl>>();


        public frmActivity( )
        {
            InitializeComponent( );
            LoadWidgets();
        }

        private void PreLoadData()
        {
            try
            {
                Log.Time( "PreLoadData", LogType.Debug, true, ( ) =>
                {
                    LiveDataStore.Get( ).AddData += AddNewData;
                } );
            }
            catch( Exception e)
            {
                Log.WriteException( "PreLoadData", e );
            }
        }

        private void FrmActivityLoad(object _sender, EventArgs _e)
        {
            Log.MsgWrap(false, () =>
            {
                PreLoadData();
                if (File.Exists(m_LayoutPath))
                {
                    docActivities.RestoreLayoutFromXml(m_LayoutPath);
                    if (File.Exists(m_LayoutPath + ".txt"))
                    {
                        using (var reader = new StreamReader(m_LayoutPath + ".txt"))
                        {
                            string data = reader.ReadLine();
                            while (!string.IsNullOrWhiteSpace(data))
                            {
                                if (data != null)
                                {
                                    string[] layout = data.Split(',');
                                    if (m_Widgets.ContainsKey(layout[0]))
                                    {
                                        m_Widgets[layout[0]].Width = Convert.ToInt32(layout[1]);
                                        m_Widgets[layout[0]].Height = Convert.ToInt32(layout[2]);
                                    }
                                }
                                data = reader.ReadLine();
                            }
                        }
                    }
                } 
                foreach (DockPanel panel in from DockPanel panel in docActivities.Panels
                                            where null != panel.ControlContainer
                                            where m_WidgetList.ContainsKey(panel.Text)
                                            select panel)
                {
                    m_WidgetList[panel.Text].CheckState = CheckState.Checked;
                    m_WidgetList[panel.Text].Value = panel;
                    IActivityControl widget = m_Widgets[panel.Text];
                    if (null != widget)
                    {
                        Log.Debug("Widget", string.Format("Activity::FrmActivityLoad Pannel {0}", panel.Name));
                        if( null != widget.MessageType )
                        {
                            foreach (var type in widget.MessageType)
                            {
                                if (!m_ActiveWidgets.ContainsKey(type))
                                {
                                    m_ActiveWidgets.Add( type, new List<IActivityControl>());
                                }
                                m_ActiveWidgets[type].Add( widget );
                            }
                        }

                        var c = widget as Control;
                        c.Dock = DockStyle.Fill;

                        panel.Tag = m_WidgetList[panel.Text];
                        panel.ClosedPanel += new DockPanelEventHandler(panel_ClosedPanel);
                        panel.VisibilityChanged += new VisibilityChangedEventHandler(panel_VisibilityChanged);

                        panel.Controls.Add(c);
                        panel.ID = Guid.NewGuid();
                        panel.Location = new System.Drawing.Point(0, 164);
                        panel.Name = string.Format("dpnl{0}", widget.DisplayName);
                        panel.Text = widget.DisplayName;
                    }
                }
                return true;
            });
        }

        private void LoadWidgets()
        {
            try
            {
                clstWidgets.Items.Clear();

                Assembly asm = Assembly.GetExecutingAssembly();
                Type[] Types = asm.GetTypes();
                if (null != Types)
                {
                    foreach (Type T in
                        Types.Where(T => T.IsClass && null != T.GetInterface("IActivityControl") && !T.IsSubclassOf(typeof (Form))))
                    {
                        try
                        {
                            var widget = Activator.CreateInstance(T) as IActivityControl;
                            m_Widgets[widget.DisplayName] = widget;

                            var item = new CheckedListBoxItem {Description = widget.DisplayName};
                            widget.ControlTag = item;

                            m_WidgetList[widget.DisplayName] = item;
                            clstWidgets.Items.Add(item);
                        }
                        catch( Exception err )
                        {
                            Log.WriteException( "LoadWidgets", err );
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Log.WriteException("LoadWidgets", err);
            }
        }

        private bool AddNewData( object _data )
        {
            bool rc = false;
            Log.Time(string.Format("AddNewData - frmActivity - {0}", _data.GetType() ), LogType.Debug, false, () =>
            {
                //Log.Debug( "Widget", "Activity::AddNewData");
                try
                {
                    if (this.Created)
                    {
                        if (InvokeRequired)
                        {
                            Invoke((MethodInvoker)(() => AddNewData(_data)));
                        }
                        else
                        {
                            var type = _data.GetType();
                            foreach (var widget in m_ActiveWidgets[type])
                            {
                                Log.Debug( string.Format( "Widget - {0}, Message - {1}", widget.DisplayName, _data.GetType() ) );
                                widget.AddActivityItem(_data);
                            }
                            //foreach (var ctrl in from DockPanel panel in docActivities.Panels
                            //                     where null != panel.ControlContainer && true == panel.Visible
                            //                     from Control c in panel.ControlContainer.Controls
                            //                     where c is IActivityControl && true == c.Visible
                            //                     select (IActivityControl)c
                            //                         into ctrl
                            //                         where null != ctrl.MessageType && ctrl.MessageType.Contains(_data.GetType())
                            //                         select ctrl)
                            //{
                            //    rc = ctrl.AddActivityItem(_data as IActivityItem);
                            //}

                            //foreach (DockPanel panel in docActivities.Panels)
                            //{
                            //    //Log.Debug( "Widget", string.Format( "Activity::AddNewData Pannel {0}", panel.Name ) );
                            //    if (null != panel.ControlContainer && true == panel.Visible)
                            //    {
                            //        foreach (Control c in panel.ControlContainer.Controls)
                            //        {
                            //            if (c is IActivityControl && true == c.Visible)
                            //            {
                            //                var ctrl = (IActivityControl) c;
                            //                if (null != ctrl.MessageType && ctrl.MessageType.Contains(_data.GetType()))
                            //                {
                            //                    //Log.Debug( "Widget", string.Format( "Activity::AddNewData Control {0}", c.Name ) );
                            //                    rc = ctrl.AddActivityItem(_data as IActivityItem);
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                catch (Exception err)
                {
                    Log.WriteException("AddNewData", err);
                }
                
            } )
            ;
           
            return rc;
        }

        private void frmActivity_FormClosing( object _sender, FormClosingEventArgs _e )
        {
            LiveDataStore.Stop();
            docActivities.SaveLayoutToXml( m_LayoutPath );

            var builder = new StringBuilder();
            foreach (DockPanel panel in docActivities.Panels)
            {
                builder.Append( string.Format( "{0},{1},{2}{3}", panel.Text, panel.Width, panel.Height, System.Environment.NewLine ) );
            }

            using (var writer = new StreamWriter(m_LayoutPath + ".txt", false))
            {
                writer.Write(builder.ToString());
            }
        }

        private void clstWidgets_Click(object sender, EventArgs e)
        {
            Log.MsgWrap(false, () =>
            {
                var o = clstWidgets.SelectedItem as CheckedListBoxItem;
                if ( o.CheckState == CheckState.Unchecked)
                {
                    var panel = o.Value as DockPanel;
                    if( null != panel )
                    {
                        panel.Dock = DockingStyle.Float;
                        panel.Location = new Point(100, 164);
                        panel.Show();
                        o.InvertCheckState();
                    }
                    else
                    {
                        var widget = m_Widgets[o.Description];
                        if (null != widget)
                        {
                            var c = widget as Control;
                            if (c != null)
                            {
                                c.Dock = DockStyle.Fill;
                                panel = docActivities.AddPanel(DockingStyle.Float);
                                panel.Tag = o;
                                panel.ClosedPanel += new DockPanelEventHandler(panel_ClosedPanel);
                                panel.VisibilityChanged += new VisibilityChangedEventHandler(panel_VisibilityChanged);
                                panel.Controls.Add(c);
                                panel.ID = Guid.NewGuid();
                                panel.Location = new Point(100, 164);
                                panel.Name = string.Format("dpnl{0}", widget.DisplayName);
                                panel.OriginalSize = new Size(200, 165);
                                panel.Size = new Size(372, 165);
                                panel.Text = widget.DisplayName;
                                panel.Show();

                                o.Value = panel;
                            }
                        }
                    }
                }
                else
                {
                    var panel = o.Value as DockPanel;
                    if (null != panel)
                    {
                        panel.HideImmediately();

                        var widget = m_Widgets[o.Description];
                        if (null != widget && null != widget.MessageType )
                        {
                            foreach (var type in widget.MessageType )
                            {
                                m_ActiveWidgets[type].Remove(widget);
                            }
                        }
                    }
                }
                return true;
            });
        }

        void panel_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            //var check = e.Panel.Tag as CheckedListBoxItem;
            //if (null != check)
            //{
            //    if (e.Visibility == DockVisibility.Visible)
            //    {
            //        check.CheckState = CheckState.Checked;
            //    }
            //    else
            //    {
            //        check.CheckState = CheckState.Unchecked;
            //    }
            //}
        }

        void panel_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            var check = e.Panel.Tag as CheckedListBoxItem;
            if (null != check)
            {
                check.CheckState = CheckState.Unchecked;
            }
        }
    }
}
