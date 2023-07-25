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
using DevExpress.XtraGrid.Views.Grid;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;

namespace BeverageActivity.Forms.Activity.Widgets
{
    public partial class TagAlertWidget : QueueingWidget<object>, IActivityControl, ILoadable
    {
        private const int ActivityLogMax = 10;
        BindingList<IUITagActivity> m_TagActivity = new BindingList<IUITagActivity>( );

        public TagAlertWidget( )
        {
            InitializeComponent( );
            MessageType = new List<Type> { typeof(DataTagActivity), typeof(DataTagAlert) };
        }

        #region IActivityControl Members

        protected override void ProcessItem(object _item)
        {
            if (_item is DataTagActivity)
            {
                Application.DoEvents();
                m_TagActivity.Insert(0, new UIActivityLog(_item as DataTagActivity));
                if (ActivityLogMax < m_TagActivity.Count)
                {
                    m_TagActivity.RemoveAt(ActivityLogMax);
                }
                Application.DoEvents();
            }
            else if (_item is DataTagAlert)
            {
                Application.DoEvents();
                m_TagActivity.Insert(0, new UIActivityLog(_item as DataTagAlert));
                if (ActivityLogMax < m_TagActivity.Count)
                {
                    m_TagActivity.RemoveAt(ActivityLogMax);
                }
                Application.DoEvents();
            }
        }

        //public bool AddActivityItem( object _item )
        //{
        //    bool rc = true;
        //    //Log.Debug( "Widget", string.Format( "TagAlertWidget::AddActivityItem {0}", _item.ToString() ) );
        //    if( _item is DataTagActivity )
        //    {
        //        Application.DoEvents( );
        //        m_TagActivity.Insert( 0, new UIActivityLog( _item as DataTagActivity ) );
        //        if( ActivityLogMax < m_TagActivity.Count )
        //        {
        //            m_TagActivity.RemoveAt( ActivityLogMax );
        //        }
        //        Application.DoEvents( );
        //    }
        //    else if ( _item is DataTagAlert )
        //    {
        //        Application.DoEvents( );
        //        m_TagActivity.Insert( 0, new UIActivityLog( _item as DataTagAlert ) );
        //        if( ActivityLogMax < m_TagActivity.Count )
        //        {
        //            m_TagActivity.RemoveAt( ActivityLogMax );
        //        }
        //        Application.DoEvents( );
        //    }
        //    return rc;

        //}

        public string DisplayName { get { return "Tag Alerts"; } }
        public Guid DisplayID { get { return new Guid("1EF9D264-30AC-4C4B-8DF1-2DAD8CAD2D88"); } }
        public object ControlTag { get; set; }

        #endregion

        private void LoadTagElements( )
        {
            IList<IBLTagActivity> tagElements = null;
            Log.Time( "Get Tag Activity", LogType.Debug, true, ( ) =>
            {
                tagElements = BLManagerFactory.Get( ).ManageTagActivity( ).Top( ActivityLogMax ).ToList( );
            } );
            for( int i = 0; i < ( ( tagElements.Count < ActivityLogMax ) ? tagElements.Count : ActivityLogMax ); ++i )
            {
                m_TagActivity.Add( new UIActivityLog( tagElements[i] ) );
            }

            if( grdActivityLog.InvokeRequired )
            {
                Invoke( (MethodInvoker)( ( ) =>
                {
                    grdActivityLog.DataSource = m_TagActivity;
                } ) );
            }
            else
            {
                grdActivityLog.DataSource = m_TagActivity;
            }

            if( null != gvActivityLog.Columns.ColumnByFieldName( "ActivityTime" ) )
            {
                gvActivityLog.Columns["ActivityTime"].DisplayFormat.FormatType = FormatType.DateTime;
                gvActivityLog.Columns["ActivityTime"].DisplayFormat.FormatString = "MM/dd hh:mm:ss";
                gvActivityLog.SortInfo.Add( gvActivityLog.Columns["ActivityTime"], ColumnSortOrder.Descending );
            }
        }



        //private void LoadActivtyLog( )
        //{
        //    if( InvokeRequired )
        //    {
        //        BeginInvoke( (MethodInvoker)( LoadActivtyLog ) );
        //    }
        //    else
        //    {
        //        IList<IBLActivityLog> elements = null;
        //        Log.Time( "Get Activity Log", LogType.Debug, true, ( ) =>
        //        {
        //            elements = BLManagerFactory.Get( ).ManageActivityLog( ).Top( ActivityLogMax ).ToList( );
        //        } );
        //        for( int i = 0; i < ( ( elements.Count < ActivityLogMax ) ? elements.Count : ActivityLogMax ); ++i )
        //        {
        //            m_TagActivity.Add( new UIActivityLog( elements[i] ) );
        //        }

        //        if( this.grdActivityLog.InvokeRequired )
        //        {
        //            this.Invoke( (MethodInvoker)( ( ) =>
        //            {
        //                grdActivityLog.DataSource = m_TagActivity;
        //            } ) );
        //        }
        //        else
        //        {
        //            grdActivityLog.DataSource = m_TagActivity;
        //        }

        //        if( null != gvActivityLog.Columns.ColumnByFieldName( "ActivityTime" ) )
        //        {
        //            gvActivityLog.Columns["ActivityTime"].DisplayFormat.FormatType = FormatType.DateTime;
        //            gvActivityLog.Columns["ActivityTime"].DisplayFormat.FormatString = "MM/dd hh:mm:ss";
        //            gvActivityLog.SortInfo.Add( gvActivityLog.Columns["ActivityTime"], ColumnSortOrder.Descending );
        //        }
        //    }
        //}

        private void grdActivityLog_Load( object sender, EventArgs e )
        {
            if( !DesignMode )
            {
                LoadTagElements();
            }
        }


        private void gvActivityLog_RowStyle( object _sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs _e )
        {
            GridView view = _sender as GridView;
            if( _e.RowHandle >= 0 && view != null )
            {
                string category = view.GetRowCellDisplayText( _e.RowHandle, view.Columns["Type"] );
                if( category == "BadBottleAttach" || category == "NonEmptyBottle" )
                {
                    _e.Appearance.BackColor = Color.White;
                    _e.Appearance.BackColor2 = Color.Red;
                }
            }
        }
    }
}
