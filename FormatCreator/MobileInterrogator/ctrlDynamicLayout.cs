using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using LFI.RFID.Format;

namespace MobileInterrogator
{
    public partial class ctrlDynamicLayout : UserControl
    {

        protected const int VERT_POSITION = 27;
        #region Fields

        protected TagData m_Element = null;

        Dictionary<string, Control> m_Values = new Dictionary<string, Control>( );

        #endregion Fields

        #region Constructors

        public ctrlDynamicLayout( )
        {
            InitializeComponent( );
        }

        #endregion Constructors

        #region Properties

        public TagData Element
        {
            get
            {
                return m_Element;
            }
            set
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    pnlTagInfo.Controls.Clear( );
                    m_Values.Clear( );

                    m_Element = value;

                    TagDataItems( );
                 }
                catch( Exception err )
                {

                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        #endregion Properties

        #region Methods


        protected void TagDataItems( )
        {
            TagData Tag = m_Element as TagData;
            int Position = 4;

            //foreach( LFI.RFID.Format.DataRow data in Tag.DataRows )
            //{
            //    TextBox A = new TextBox( );
            //    A.Name = String.Format( "{0}Label", data.Values.Name );
            //    A.Location = new Point( 4, Position );
            //    A.Text = data.Name;
            //    A.BorderStyle = BorderStyle.None;
            //    A.Width = 121;
            //    A.ReadOnly = true;
            //    pnlTagInfo.Controls.Add( A );
            //    Control B = null;
            //    switch( data.DataType )
            //    {
            //        case ItemDataType.Text:
            //        case ItemDataType.Number:
            //        {
            //            TextBox C = new TextBox( );
            //            C.TextChanged += B_TextChanged;
            //            C.Tag = data;
            //            C.Name = data.Name;
            //            C.Location = new Point( 125, Position );
            //            if( !string.IsNullOrEmpty( data.Value ) )
            //                C.Text = data.Value;
            //            B = C;
            //            break;
            //        }
            //        case ItemDataType.Date:
            //        {
            //            DateTimePicker C = new DateTimePicker( );
            //            C.TextChanged += B_TextChanged;
            //            C.Tag = data;
            //            C.Name = data.Name;
            //            C.Location = new Point( 125, Position );
            //            if( !string.IsNullOrEmpty( data.Value ) )
            //                C.Text = data.Value;
            //            B = C;
            //            break;
            //        }
            //        case ItemDataType.PickList:
            //        {
            //            ComboBox C = new ComboBox( );
            //            C.SelectedIndexChanged += B_TextChanged;
            //            C.Tag = data;
            //            C.Name = data.Name;
            //            C.Location = new Point( 125, Position );
            //            if( !string.IsNullOrEmpty( data.Constraints ) )
            //            {
            //                string[] Items = data.Constraints.Split( '|' );
            //                foreach( string Item in Items )
            //                {
            //                    C.Items.Add( Item );
            //                }
            //            }
            //            if( !string.IsNullOrEmpty( data.Value ) )
            //                C.Text = data.Value;
            //            B = C;
            //            break;
            //        }
            //    }
            //    pnlTagInfo.Controls.Add( B );
            //    m_Values[data.Name] = B;
            //    Position += VERT_POSITION;
            //}
        }

        public void Populate( TagData _Element )
        {
            if( 0 < m_Values.Count )
            {
                m_Element = _Element;

                //for( int i = 0; i < m_Element.Data.Count; ++i )
                //{
                //    Control B = m_Values[m_Element.Data[i].Name];
                //    if( !string.IsNullOrEmpty( m_Element.Data[i].Value ) )
                //    {
                //        B.Text = m_Element.Data[i].Value;
                //    }
                //}
            }
        }


        void B_TextChanged( object sender, EventArgs e )
        {
            try
            {
                Control Txt = sender as Control;
                if( null != Txt )
                {
                    //DataRow Info = Txt.Tag as DataRow;
                    //if( null != Info )
                    //{
                    //    Info.Value = Txt.Text;
                    //}
                }
            }
            catch
            {

            }
        }

        public void PageDown( )
        {
            int Position = Math.Abs( pnlTagInfo.AutoScrollPosition.Y ) + VERT_POSITION;
            pnlTagInfo.AutoScrollPosition = new Point( 0, Position );
        }

        public void PageUp( )
        {
            int Position = Math.Abs( pnlTagInfo.AutoScrollPosition.Y ) - VERT_POSITION;
            pnlTagInfo.AutoScrollPosition = new Point( 0, Position );
        }
        #endregion Methods
    }
}