using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace BeverageManagement.Forms.Activity.Widgets
{
    public partial class ActivityLogWidget : QueueingWidget<DataActivityLog>, IActivityControl
    {
        private const int ActivityLogMax = 10;
        readonly BindingList<IUITagActivity> m_ActivityLog = new BindingList<IUITagActivity>( );
        public object ControlTag { get; set; }

        public ActivityLogWidget( )
        {
            InitializeComponent( );
            MessageType = new List<Type> {typeof (DataActivityLog)};
        }

        #region IActivityControl Members

        protected override void ProcessItem(DataActivityLog _item)
        {
            UIActivityLog item = null;

            if (ActivityLogMax < m_ActivityLog.Count)
            {
                item = m_ActivityLog[ActivityLogMax] as UIActivityLog;
                m_ActivityLog.RemoveAt(ActivityLogMax);
                item.ReloadData( _item );
            }
            else
            {
                item = new UIActivityLog(_item as DataActivityLog);
            }
            m_ActivityLog.Insert(0, item);
        }

        //public bool AddActivityItem( object _item )
        //{
        //    bool rc = true;
        //    //Log.Debug( "Widget", string.Format( "ActivityLogWidget::AddActivityItem {0}", _item.ToString( ) ) );
        //    if( _item is DataActivityLog )
        //    {
        //        Application.DoEvents( );

        //        var item = new UIActivityLog( _item as DataActivityLog );

        //        m_ActivityLog.Insert( 0, item );
        //        if( ActivityLogMax < m_ActivityLog.Count )
        //        {
        //            m_ActivityLog.RemoveAt( ActivityLogMax );
        //        }
        //        Application.DoEvents( );
        //    }
        //    return rc;
        //}

        public string DisplayName { get { return "Activity Log"; } }
        public Guid DisplayID { get { return new Guid("DAD611D9-F0B0-4E1C-ADCA-406C1106D304"); } }


        #endregion



        private void LoadActivtyLog( )
        {
            if( InvokeRequired )
            {
                if( this.Created )
                {
                    BeginInvoke( (MethodInvoker)( LoadActivtyLog ) );
                }
            }
            else
            {
                IList<IBLActivityLog> elements = null;
                Log.Time( "Get Activity Log", LogType.Debug, true, ( ) =>
                {
                    elements = BLManagerFactory.Get( ).ManageActivityLog( ).Top( ActivityLogMax ).ToList( );
                } );
                for( int i = 0; i < ( ( elements.Count < ActivityLogMax ) ? elements.Count : ActivityLogMax ); ++i )
                {
                    m_ActivityLog.Add( new UIActivityLog( elements[i] ) );
                }

                if( this.grdActivityLog.InvokeRequired )
                {
                    this.Invoke( (MethodInvoker)( ( ) =>
                    {
                        grdActivityLog.DataSource = m_ActivityLog;
                    } ) );
                }
                else
                {
                    grdActivityLog.DataSource = m_ActivityLog;
                }

                if( null != gvActivityLog.Columns.ColumnByFieldName( "ActivityTime" ) )
                {
                    gvActivityLog.Columns["ActivityTime"].DisplayFormat.FormatType = FormatType.DateTime;
                    gvActivityLog.Columns["ActivityTime"].DisplayFormat.FormatString = "MM/dd hh:mm:ss";
                    gvActivityLog.SortInfo.Add( gvActivityLog.Columns["ActivityTime"], ColumnSortOrder.Descending );
                }
            }
        }

        private void grdActivityLog_Load( object sender, EventArgs e )
        {
            if( !DesignMode )
            {
                LoadActivtyLog();
            }
        }
    }
}
