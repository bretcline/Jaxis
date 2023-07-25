using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BeverageManagement.Forms;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;

namespace BeverageManagement.Controls
{
    public partial class BottleLevel : DevExpress.XtraEditors.XtraUserControl
    {
        private int m_FillLevel = 100;
        protected IStandardNozzle m_BottleNozzle = null;

        public BottleLevel( )
        {
            InitializeComponent( );
            CommonUI.LoadNozzleTypes( cmbNozzleShape );
        }

        private void MonitorChanges( bool _set )
        {
            if (_set)
            {
                spinNozzleLength.EditValueChanged += new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
                spinNozzleWidth.EditValueChanged += new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
                cmbNozzleShape.EditValueChanged += new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
            }
            else
            {
                spinNozzleLength.EditValueChanged -= new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
                spinNozzleWidth.EditValueChanged -= new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
                cmbNozzleShape.EditValueChanged -= new System.EventHandler(this.SpinNozzleDiameterEditValueChanged);
            }
        }

        private void SetBottleFluid_Click( object sender, EventArgs e )
        {
            picBottleEmpty.SuspendLayout();

            picBottleEmpty.Height = 0;
            PictureEdit current = sender as PictureEdit;
            MouseEventArgs args = e as MouseEventArgs;
            FillLevel = 100;
            if( null != args && args.Y > 85 )
            {
                picBottleEmpty.Height = args.Y;
                float top = picBottle.Height - 85;
                int level = args.Y - 85;

                if( 0 < level )
                {
//                    FillLevel = (int)( ( ( 251.0f - ( args.Y - 85 ) ) / 251.0f ) * 100 );
                    FillLevel = (int)( ( ( top - level ) / top ) * 100 );

                }
            }
            lblFillLevel.Text = string.Format( "{0} %", FillLevel );
            picBottleEmpty.Top = picBottle.Top;

            if( null != OnFillLevelChanged )
            {
                OnFillLevelChanged();
            }

            picBottleEmpty.ResumeLayout();
        }

        public int FillLevel 
        { 
            get { return m_FillLevel; }
            set 
            { 
                m_FillLevel = value;

                picBottleEmpty.Height = (int)((picBottle.Height - 85)*( 1 - (FillLevel/100.0f ))) + 85;
                lblFillLevel.Text = string.Format("{0} %", m_FillLevel);

//                picBottleEmpty.Height = (int)( picBottle.Height - picBottle.Height * ( FillLevel / 100.0f ) ) + 85;

                //int height = (int)( ((((m_FillLevel/100.0f)*251.0f) - 251)) + 85 );
                //picBottleEmpty.Height = height;
            }
        }

        public double NozzleLength { get { return Convert.ToDouble( spinNozzleLength.Value ); } set { spinNozzleLength.Value = Convert.ToDecimal( value ); } }
        public double NozzleWidth { get { return Convert.ToDouble( spinNozzleWidth.Value ); } set { spinNozzleWidth.Value = Convert.ToDecimal( value ); } }
        public NozzleShapes NozzleShape { get { return (NozzleShapes)(cmbNozzleShape.SelectedItem ?? NozzleShapes.Round); } set { cmbNozzleShape.SelectedItem = value; } }

        [Browsable( false )]
        public IStandardNozzle BottleNozzle
        {
            get
            {
                if( !DesignMode )
                {
                    m_BottleNozzle.Length = NozzleLength;
                    m_BottleNozzle.Width = NozzleWidth;
                    m_BottleNozzle.Shape = (int)NozzleShape;
                }
                return m_BottleNozzle;
            }
            set
            {
                if( null != value )
                {
                    MonitorChanges(false);
                    NozzleLength = value.Length;
                    NozzleWidth = value.Width;
                    NozzleShape = (NozzleShapes) value.Shape;
                    MonitorChanges(true);
                }
            }
        } 

        [Browsable( false )]
        public Action OnFillLevelChanged { get; set; }
        [Browsable( false )]
        public Action OnNozzleDiameterChanged { get; set; }

        private void BottleLevel_Load( object sender, EventArgs e )
        {
            if( !DesignMode )
            {
                m_BottleNozzle = BLManagerFactory.Get().ManageStandardNozzles().Create();
            }
            picBottleEmpty.Height = 0;
            FillLevel = 100; 
            lblFillLevel.Text = string.Format( "{0} %", FillLevel );
        }

        private void SpinNozzleDiameterEditValueChanged( object sender, EventArgs e )
        {
            if( null != OnNozzleDiameterChanged )
            {
                OnNozzleDiameterChanged();
            }
        }

        internal void SetValues( IStandardNozzle _nozzle )
        {
            BottleNozzle = _nozzle;
        }
    }
}
