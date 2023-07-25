using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using DevExpress.XtraEditors;

namespace Snapper.Controls
{
    [Designer( typeof( CollapsiblePannelDesigner ) )]
    public partial class CollapsiblePannel : DevExpress.XtraEditors.XtraUserControl
    {
        #region Constructors

        //public delegate string ChangeTitle( );
        //public event ChangeTitle OnTitleChanged;
        public CollapsiblePannel( )
        {
            InitializeComponent( );
            Collapsed = false;
        }

        #endregion Constructors

        #region Properties

        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DevExpress.XtraEditors.PanelControl Body
        {
            get { return pnlBody; }
        }

        public bool Collapsed
        {
            get;
            set;
        }

        public int CollapsedHeight
        {
            get;
            set;
        }

        public int ExpandedHeight
        {
            get;
            set;
        }

        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        #endregion Properties

        #region Methods

        public void AddControl( Control _Ctrl )
        {
            pnlBody.Controls.Add( _Ctrl );
        }

        public void RemoveControl( Control _Ctrl )
        {
            pnlBody.Controls.Remove( _Ctrl );
        }

        private void CollapsiblePannel_Load( object sender, EventArgs e )
        {
            ExpandedHeight = pnlBody.Height + pnlHeader.Height;
            CollapsedHeight = pnlHeader.Height;
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            if (false == Collapsed)
            {
                this.Height = CollapsedHeight;
                //grpPanel.Height = CollapsedHeight;
            }
            else
            {
                this.Height = ExpandedHeight;
                //grpPanel.Height = ExpandedHeight;
            }

            Collapsed = !Collapsed;
        }

        #endregion Methods

        #region Nested Types

        class CollapsiblePannelDesigner : ControlDesigner
        {
            #region Methods

            public override void Initialize(IComponent comp)
            {
                base.Initialize(comp);
                CollapsiblePannel uc = (CollapsiblePannel)comp;
                EnableDesignMode(uc.pnlBody, "Body");
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}