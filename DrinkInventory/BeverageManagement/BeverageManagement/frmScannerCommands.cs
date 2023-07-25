using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BeverageManagement
{
    public partial class frmScannerCommands : XtraForm
    {
        protected ScannerCommand m_ScannerCommands = new ScannerCommand();

        public frmScannerCommands( )
        {
            this.KeyPreview = true;
            this.KeyPress += frm_KeyPress;
        }

        private void frm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ProcessKey(e);
        }

        protected bool ProcessKey( KeyPressEventArgs e )
        {
            return m_ScannerCommands.ProcessKeys(e);
        }
    }
}