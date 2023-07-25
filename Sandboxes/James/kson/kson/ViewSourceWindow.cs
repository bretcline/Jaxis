using System.Windows.Forms;

namespace kson
{
    public partial class ViewSourceWindow : Form
    {
        public ViewSourceWindow()
        {
            InitializeComponent();
        }

        public ViewSourceWindow(string _source)
        {
            InitializeComponent();
            textBox.Text = _source;
            textBox.SelectionStart = 0;
            textBox.SelectionLength = 0;
        }
    }
}
