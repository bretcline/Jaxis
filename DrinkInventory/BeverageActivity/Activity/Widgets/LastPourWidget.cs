using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.MessageLibrary;
using Jaxis.Util.Log4Net;
using Qios.DevSuite.Components;

namespace BeverageActivity.Forms.Activity.Widgets
{
    public partial class LastPourWidget : QueueingWidget<CalcPour>, IActivityControl, ILoadable
    {
        StringBuilder m_Builder = new StringBuilder();
        StringBuilder m_Text = new StringBuilder();
        private QMarkupLabel m_Markup;

        private bool m_Loaded = false;

        

        private const string m_TitleStart = "<FONT face=\"Verdana\" color=\"Red\" size=\"15\">";
        private const string m_TitleEnd = "</FONT><br />";
        private const string m_OtherStart = "<FONT face=\"Verdana\" color=\"Black\" size=\"10\">";
        private const string m_OtherEnd = "</FONT><br />";

        public LastPourWidget()
        {
            InitializeComponent();
            MessageType = new List<Type> { typeof(CalcPour) };

        }

        #region IActivityControl Members

        protected override void ProcessItem(CalcPour _item )
        {
            Log.Debug("Widget", string.Format("LastPourWidget::AddActivityItem {0}", _item));

            var data = _item as CalcPour;

            m_Builder.Clear();
            m_Builder.Append(m_Text);
            m_Builder.Replace(m_TitleStart, m_OtherStart);
            m_Builder.Replace(m_TitleEnd, m_OtherEnd);

            m_Text.Append(string.Format(m_Builder.ToString(), "<h1>", "</h1><br/>", data.PourAmount));

            m_Builder.Insert(0, "{0}{2} {3}{1}");
            m_Text.Clear();
            m_Text.Append(string.Format(m_Builder.ToString(), m_TitleStart, m_TitleEnd, ConvertedVolume( _item.PourAmount ), _item.UPCName));

            var text = m_Text.ToString();
            if (m_Markup.InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { m_Markup.MarkupText = text; }));
            }
            else
            {
                m_Markup.MarkupText = text;
            }
        }

        public string DisplayName { get { return "Last Pour"; } }
        public Guid DisplayID { get { return new Guid("2231B441-F8C8-40EF-9C6F-4D339F64B6C0"); } }
        public object ControlTag { get; set; }

        #endregion

        private void LastPourWidget_Load(object sender, EventArgs e)
        {
            m_Markup = new QMarkupLabel();
            this.Controls.Add(m_Markup);
            m_Markup.Dock = DockStyle.Fill;
            m_Markup.Font = new Font( m_Markup.Font.FontFamily, 15 );
            m_Loaded = true;
        }
    }
}
