using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Jaxis.Inventory.Data;
using Jaxis.Util.Log4Net;

namespace BeverageActivity.Forms.Activity.Widgets
{
    public partial class PourVolumePieWidget : DevExpress.XtraEditors.XtraUserControl, IActivityControl
    {

        List<KeyValuePair<string, int>> m_Data = new List<KeyValuePair<string, int>>();

        public PourVolumePieWidget()
        {
            InitializeComponent();
            MessageType = new List<Type> {typeof (DataPour)};
        }

        

        #region IActivityControl Members

        public List<Type> MessageType { get; protected set; }


        public bool AddActivityItem(object _item)
        {
            var rc = true;

            if (_item is DataPour)
            {
                Log.Debug("Widget", string.Format("LastPourWidget::AddActivityItem {0}", _item.ToString()));
                var data = _item as DataPour;
                PourVolumePieWidget_Load(null, null);
            }

            return rc;
        }


        public string DisplayName { get { return "Pour Volume"; } }
        public Guid DisplayID { get { return new Guid("{CD390899-4423-4142-80DB-32BD46A5B9DE}"); } }
        public object ControlTag { get; set; }

        #endregion


        protected void BuildChart( object _data )
        {
        }

        private void PourVolumePieWidget_Load(object sender, EventArgs e)
        {
            var db = new BeverageMonitorDB();
            var now = DateTime.Now;
            now = new DateTime( now.Year, now.Month, now.Day );
            var data = db.widgetHourlyConsumption(now);
            if (null != data)
            {
                var values = new Dictionary<string, List<SeriesPoint>>();
                using (var reader = data.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = reader.GetString(reader.GetOrdinal("Category"));
                        var pourTime = reader.GetDateTime(reader.GetOrdinal("PourTime"));
                        var volume = reader.GetDouble(reader.GetOrdinal("Volume"));
                        if (!values.ContainsKey(category))
                        {
                            values[category] = new List<SeriesPoint>();
                        }
                        values[category].Add(new SeriesPoint(pourTime, volume));
                    }
                }
                foreach (Series series in
                    chartCurrentPours.Series)
                {
                    series.Points.Clear();
                    if( values.ContainsKey( series.Name ))
                    {
                        var array = values[series.Name].OrderBy(i => i.DateTimeArgument).ToArray();
                        series.Points.AddRange(array);
                    }
                }
            }
        }

        private void PourVolumePieWidget_MouseHover(object sender, EventArgs e)
        {
        }

        private void chartCurrentPours_Click(object sender, EventArgs e)
        {
        }
    }
}
