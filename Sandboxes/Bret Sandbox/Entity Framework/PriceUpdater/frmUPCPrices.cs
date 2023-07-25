using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using BeverageMonitor.Entities;
using DevExpress.XtraEditors.Controls;

namespace PriceUpdater
{
    public partial class frnUPCPrices : Form
    {
        public class UPCQuality
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private BeverageMonitorEntities m_Session = null;
        List<UPC> upcs = null;
        private List<UPCQuality> quality = new List<UPCQuality>();


        public frnUPCPrices()
        {
            InitializeComponent();

            quality.Add(new UPCQuality { Name = "Unknown", Value = 0 });
            quality.Add(new UPCQuality { Name = "Well", Value = 1 });
            quality.Add(new UPCQuality { Name = "Call", Value = 2 });
            quality.Add(new UPCQuality { Name = "Premium", Value = 3 });
            quality.Add(new UPCQuality { Name = "Super Premium", Value = 4 });

            txtFileName.Text = Application.StartupPath + "\\DataFile.xml";
        }

        private void frnUPCPrices_Load(object sender, EventArgs e)
        {
            //cmbDatabase.Properties


            //if (File.Exists(txtFileName.Text))
            //{
            //    using (var reader = new StreamReader("DataFile.xml"))
            //    {
            //        XmlSerializer serializer = new XmlSerializer(typeof( List<UPC>) );
            //        upcs = serializer.Deserialize(reader) as List<UPC>;
            //    }
            //}
            //else
            //{
            //    m_Session = new BeverageMonitorEntities();
            //    upcs = m_Session.UPCs.ToList();

            //    var manufacturers = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            //    manufacturers.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            //    manufacturers.Columns.Clear();
            //    manufacturers.ValueMember = "ManufacturerID";
            //    manufacturers.ShowHeader = false;
            //    manufacturers.DisplayMember = "Name";
            //    manufacturers.Columns.Add(new LookUpColumnInfo("Name"));
            //    manufacturers.DataSource = m_Session.Manufacturers;

            //    gvUPCs.Columns["ManufacturerID"].ColumnEdit = manufacturers;

            //}
            ////UpdatePrices(upcs);

            ////var xSunday = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ////xSunday.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            ////xSunday.Columns.Clear();
            ////xSunday.ValueMember = "Value";
            ////xSunday.ShowHeader = false;
            ////xSunday.DisplayMember = "Name";
            ////xSunday.Columns.Add(new LookUpColumnInfo("Name"));
            ////xSunday.DataSource = quality;

            ////gvUPCs.Columns["Quality"].ColumnEdit = xSunday;
            //grdUPCs.DataSource = null;
            //grdUPCs.DataSource = new BindingList<IUPC>(upcs.Cast<IUPC>().ToList());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(upcs.GetType());

            using (var writer = new StreamWriter(txtFileName.Text))
            {
                serializer.Serialize(writer, upcs);
            }
            if (null != m_Session)
            {
                m_Session.SaveChanges();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }



        private void UpdatePrices(List<UPC> upcs)
        {
            using (var reader = new StreamReader("HyattHouston.csv"))
            {
                using (var writer = new StreamWriter("MissingItems.text"))
                {
                    using (var data = new BeverageMonitorEntities())
                    {
                        var line = reader.ReadLine();
                        while (null != line)
                        {
                            var items = line.Split(',');
                            if (items.Count() == 3)
                            {
                                items[0] += items[1];
                                items[1] = items[2];
                            }
                            if (1 < items.Count())
                            {
                                var name = items[0].Replace("Liqueur", "").Replace("1L", "");
                                var milliter = data.SizeTypes.Where(s => s.Abbreviation == "ML").FirstOrDefault();
                                var ounce = data.SizeTypes.Where(s => s.Abbreviation == "OZ").FirstOrDefault();
                                var sizeTypeID = milliter.SizeTypeID;
                                var size = 0.0;

                                var matches = Regex.Matches(name, " ([0-9]+)(ml|oz|l)", RegexOptions.IgnoreCase);
                                if (0 < matches.Count)
                                {
                                    size = Double.Parse(matches[0].Groups[1].Value);
                                    switch (matches[0].Groups[2].Value)
                                    {
                                        case "ml":
                                            {
                                                sizeTypeID = milliter.SizeTypeID;
                                                break;
                                            }
                                        case "l":
                                            {
                                                sizeTypeID = milliter.SizeTypeID;
                                                size = size * 1000;
                                                break;
                                            }
                                        case "oz":
                                            {
                                                sizeTypeID = ounce.SizeTypeID;
                                                break;
                                            }
                                    }
                                }

                                var upc = GetUPC(upcs, 0, name.ToLower(), sizeTypeID, size);
                                if (null != upc && 1 == upc.Count)
                                {
                                    foreach (var upc1 in upc)
                                    {
                                        upc1.UnitPrice = decimal.Parse(items[1].Replace("$", ""));

                                    }
                                }
                                else
                                {
                                    upc = GetUPC(upcs, 4, name.ToLower(), sizeTypeID, size);
                                    if (null != upc && 1 == upc.Count)
                                    {
                                        upc[0].UnitPrice = decimal.Parse(items[1].Replace("$", ""));
                                    }
                                }
                            }
                            line = reader.ReadLine();
                        }
                        data.SaveChanges();
                    }
                }
            }
        }

        private static List<UPC> GetUPC(List<UPC> upcs, int matchIndex, string name, Guid sizeTypeID, double size)
        {
            List<UPC> rc = null;

            if (0.0 == size)
                rc = upcs.Where(u => matchIndex >= Compute(u.Name.Replace("1000 mL", "").Replace("750 mL", ""), name) && u.SizeTypeID == sizeTypeID).ToList();
            else
                rc = upcs.Where(u => matchIndex >= Compute(u.Name.Replace("1000 mL", "").Replace("750 mL", ""), name) && u.SizeTypeID == sizeTypeID && u.Size == size).ToList();

            return rc;
        }

        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)
        {
            if (null == m_Session)
            {
                m_Session = new BeverageMonitorEntities();
            }
            var dbupcs = m_Session.UPCs.ToList();

            foreach (var upc in upcs)
            {
                var dbupc = dbupcs.Find(u => u.UPCID == upc.UPCID);
                dbupc.Name = upc.Name;
                dbupc.UnitPrice = upc.UnitPrice;
                dbupc.Size = upc.Size;
                dbupc.ItemNumber = upc.ItemNumber;
            }

            m_Session.SaveChanges();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == ofdDataFile.ShowDialog())
            {
                txtFileName.Text = ofdDataFile.FileName;
                frnUPCPrices_Load(null, null);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtFileName.Text))
            {
                using (var reader = new StreamReader(txtFileName.Text))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<UPC>));
                    upcs = serializer.Deserialize(reader) as List<UPC>;
                }
                grdUPCs.DataSource = null;
                grdUPCs.DataSource = new BindingList<IUPC>(upcs.Cast<IUPC>().ToList());
            }
            else
            {
                MessageBox.Show("File doesn't exist.");
            }
        }

        private void btnLoadFromDB_Click(object sender, EventArgs e)
        {


            m_Session = new BeverageMonitorEntities( );
            upcs = m_Session.UPCs.ToList();

            var manufacturers = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            manufacturers.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            manufacturers.Columns.Clear();
            manufacturers.ValueMember = "ManufacturerID";
            manufacturers.ShowHeader = false;
            manufacturers.DisplayMember = "Name";
            manufacturers.Columns.Add(new LookUpColumnInfo("Name"));
            manufacturers.DataSource = m_Session.Manufacturers;

            grdUPCs.DataSource = null;
            grdUPCs.DataSource = new BindingList<IUPC>(upcs.Cast<IUPC>().ToList());
            gvUPCs.Columns["ManufacturerID"].ColumnEdit = manufacturers;
        }
    }
}
