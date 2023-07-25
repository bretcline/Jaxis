using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;

namespace AuditImport
{
    public partial class AuditImportForm : Form
    {
        public event Action<string> DirectorySelected;

        public AuditImportForm( )
        {
            InitializeComponent( );
        }

        public void SetBinding( BindingList<string> _messages )
        {
            this.listBox1.DataSource = _messages;
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
            GetFolder( );
        }

        private void GetFolder( )
        {
            var rc = string.Empty;
            var dialog = new FolderBrowserDialog( );
            dialog.Description = "Select a folder containing audit import files.";
            dialog.ShowNewFolderButton = false;
            dialog.SelectedPath = ConfigurationManager.AppSettings[ "RootFolder" ] ?? null;
            if ( dialog.ShowDialog( ) == DialogResult.OK )
            {
                rc = dialog.SelectedPath;
                var temp = DirectorySelected;
                if ( temp != null )
                {
                    Action<string> action = temp;
                    BeginInvoke( action, dialog.SelectedPath );
                }
            }
        }
    }
}
