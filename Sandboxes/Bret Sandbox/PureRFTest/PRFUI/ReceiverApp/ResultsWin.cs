namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ResultsWin : Form
    {
        private Button btnClose;
        private Button btnExportCSV;
        private Button btnExportXML;
        private IContainer components;
        private SaveFileDialog dlgSaveCSV;
        private SaveFileDialog dlgSaveXML;
        private ListView listResults;
        private TableLayoutPanel tableLayoutPanel1;

        public ResultsWin(ReceiversManager.RequestType Request, ReceiversManager.ResultSet Results)
        {
            int num3 = 0;
            this.InitializeComponent();
            int num = Convert.ToInt32(this.listResults.Font.SizeInPoints);
            this.listResults.Columns.Add("#", (int) (3 * num));
            this.listResults.Columns.Add("Receiver Name", (int) (15 * num));
            this.listResults.Columns.Add("Unit ID", (int) (8 * num));
            this.listResults.Columns.Add("Loop", (int) (8 * num));
            this.listResults.Columns.Add("RetVal", (int) (20 * num));
            switch (Request)
            {
                case ReceiversManager.RequestType.GET_UNIT_INFO:
                case ReceiversManager.RequestType.BOOTLOADER_GET_UNIT_INFO:
                    this.listResults.Columns.Add("Protocol Version", (int) (15 * num));
                    this.listResults.Columns.Add("Device Class", (int) (15 * num));
                    this.listResults.Columns.Add("Device SubClass", (int) (15 * num));
                    this.listResults.Columns.Add("Firmware Version", (int) (15 * num));
                    this.listResults.Columns.Add("Unit Name", (int) (15 * num));
                    this.listResults.Columns.Add("Unit Serial Number", (int) (15 * num));
                    break;

                case ReceiversManager.RequestType.GET_UNIT_STATUS:
                    this.listResults.Columns.Add("Sub Mode", (int) (15 * num));
                    this.listResults.Columns.Add("Up Time", (int) (15 * num));
                    this.listResults.Columns.Add("Average Processor Workload", (int) (15 * num));
                    this.listResults.Columns.Add("Number of Resets", (int) (15 * num));
                    this.listResults.Columns.Add("Tag Messages in buffer", (int) (15 * num));
                    break;

                case ReceiversManager.RequestType.GET_ALL_TAGS:
                    this.listResults.Columns.Add("Tag ID", (int) (20 * num));
                    this.listResults.Columns.Add("Transmission Index", (int) (20 * num));
                    this.listResults.Columns.Add("Tag Message", (int) (15 * num));
                    this.listResults.Columns.Add("Activator Number", (int) (20 * num));
                    this.listResults.Columns.Add("RSSI", (int) (6 * num));
                    this.listResults.Columns.Add("Noise Level", (int) (15 * num));
                    this.listResults.Columns.Add("Timestamp", (int) (0x19 * num));
                    break;

                case ReceiversManager.RequestType.GET_NOISE_LEVEL:
                    this.listResults.Columns.Add("Noise Level", (int) (15 * num));
                    break;

                case ReceiversManager.RequestType.GET_ANTENNA_GAIN:
                    this.listResults.Columns.Add("Receiver Sensitivity", (int) (20 * num));
                    break;

                case ReceiversManager.RequestType.SET_ANTENNA_GAIN:
                case ReceiversManager.RequestType.SET_TIME:
                case ReceiversManager.RequestType.FLUSH_TAG_BUFFER:
                case ReceiversManager.RequestType.ACTIVATE_RELAY:
                    break;

                case ReceiversManager.RequestType.GET_TIME:
                    this.listResults.Columns.Add("Time", (int) (15 * num));
                    break;

                case ReceiversManager.RequestType.GET_ALL_RECEIVER_INFO:
                    this.listResults.Columns.Add("Protocol Version", (int) (15 * num));
                    this.listResults.Columns.Add("Device Class", (int) (15 * num));
                    this.listResults.Columns.Add("Device SubClass", (int) (15 * num));
                    this.listResults.Columns.Add("Firmware Version", (int) (15 * num));
                    this.listResults.Columns.Add("Unit Name", (int) (15 * num));
                    this.listResults.Columns.Add("Unit Serial Number", (int) (15 * num));
                    this.listResults.Columns.Add("Sub Mode", (int) (15 * num));
                    this.listResults.Columns.Add("Up Time", (int) (15 * num));
                    this.listResults.Columns.Add("Average Processor Workload", (int) (15 * num));
                    this.listResults.Columns.Add("Number of Resets", (int) (15 * num));
                    this.listResults.Columns.Add("Tag Messages in buffer", (int) (15 * num));
                    this.listResults.Columns.Add("Noise Level", (int) (15 * num));
                    this.listResults.Columns.Add("Current power mode", (int) (0x23 * num));
                    this.listResults.Columns.Add("Measured input voltage", (int) (0x23 * num));
                    break;

                case ReceiversManager.RequestType.BOOTLOADER_FIRMWARE_STATE:
                    this.listResults.Columns.Add("Firmware Checksum", (int) (0x19 * num));
                    break;

                case ReceiversManager.RequestType.SET_UNIT_NAME:
                case ReceiversManager.RequestType.GET_UNIT_NAME:
                    this.listResults.Columns.Add("Unit Name", (int) (0x19 * num));
                    break;

                case ReceiversManager.RequestType.SET_SITE_CODE:
                case ReceiversManager.RequestType.GET_SITE_CODE:
                    this.listResults.Columns.Add("Site Code", (int) (0x19 * num));
                    break;

                case ReceiversManager.RequestType.GetRFBaudRate:
                case ReceiversManager.RequestType.SetRFBaudRate:
                    this.listResults.Columns.Add("RF Baud Rate", (int) (0x37 * num));
                    break;

                case ReceiversManager.RequestType.GetModulation:
                case ReceiversManager.RequestType.SetModulation:
                    this.listResults.Columns.Add("RF Modulation Type", (int) (0x37 * num));
                    break;

                case ReceiversManager.RequestType.GetSerialNum:
                case ReceiversManager.RequestType.SetSerialNum:
                    this.listResults.Columns.Add("Serial Number", (int) (0x37 * num));
                    break;

                case ReceiversManager.RequestType.SET_RSSI_THRESHOLD:
                case ReceiversManager.RequestType.GET_RSSI_THRESHOLD:
                    this.listResults.Columns.Add("RSSI Filter", (int) (0x19 * num));
                    break;

                case ReceiversManager.RequestType.GET_POWER_CONTROL:
                    this.listResults.Columns.Add("Current power mode", (int) (0x23 * num));
                    this.listResults.Columns.Add("Measured input voltage", (int) (0x23 * num));
                    break;

                default:
                    MessageBox.Show("Bug :(");
                    return;
            }
            foreach (ReceiversManager.ManagedReceiver receiver in Results.Keys)
            {
                Receiver.Tag[] tagArray;
                string unitName;
                ReceiversManager.ReceiverResult result = Results[receiver];
                object obj2 = result.Result;
                string[] strArray = new string[] { "?", receiver.Name, receiver.UnitID.ToString(), receiver.Loop.Name, result.RetVal.ToString() };
                string[][] strArray2 = new string[1][];
                Color[] colorArray = new Color[] { Program.RetVal2Color(result.RetVal) };
                strArray2[0] = new string[0];
                switch (Request)
                {
                    case ReceiversManager.RequestType.GET_UNIT_INFO:
                    case ReceiversManager.RequestType.BOOTLOADER_GET_UNIT_INFO:
                    {
                        if (result.RetVal != ReceiverRetVal.SUCCESS)
                        {
                            break;
                        }
                        Receiver.UnitInfo info = obj2 as Receiver.UnitInfo;
                        strArray2[0] = new string[] { info.Protocol_Version.ToString(), info.Device_Class.ToString(), info.Device_SubClass.ToString(), info.Firmware_Version.ToString(), info.Unit_Name.ToString(), info.Unit_Serial_Number.ToString() };
                        goto Label_0DC1;
                    }
                    case ReceiversManager.RequestType.GET_UNIT_STATUS:
                    {
                        if (result.RetVal != ReceiverRetVal.SUCCESS)
                        {
                            goto Label_0862;
                        }
                        Receiver.UnitStatus status = obj2 as Receiver.UnitStatus;
                        strArray2[0] = new string[] { status.Sub_mode.ToString(), status.Uptime.ToString(), status.Average_Processor_Workload.ToString(), status.Number_of_Resets.ToString(), status.Tag_messages_in_buffer.ToString() };
                        goto Label_0DC1;
                    }
                    case ReceiversManager.RequestType.GET_ALL_TAGS:
                        if (result.RetVal == ReceiverRetVal.SUCCESS)
                        {
                            goto Label_09AA;
                        }
                        strArray2[0] = new string[] { "", "", "", "", "", "" };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.GET_NOISE_LEVEL:
                        if (result.RetVal != ReceiverRetVal.SUCCESS)
                        {
                            goto Label_08AF;
                        }
                        strArray2[0] = new string[] { obj2.ToString() };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.GET_ANTENNA_GAIN:
                    {
                        strArray2[0] = new string[1];
                        if (result.RetVal != ReceiverRetVal.SUCCESS)
                        {
                            goto Label_08FA;
                        }
                        Receiver.AntennaGain antennaGain = (Receiver.AntennaGain) obj2;
                        strArray2[0][0] = Receiver.AntennaGainToString(antennaGain);
                        goto Label_0DC1;
                    }
                    case ReceiversManager.RequestType.GET_TIME:
                    {
                        if (result.RetVal != ReceiverRetVal.SUCCESS)
                        {
                            goto Label_093B;
                        }
                        Receiver.Time time = obj2 as Receiver.Time;
                        strArray2[0] = new string[] { time.ToString() };
                        goto Label_0DC1;
                    }
                    case ReceiversManager.RequestType.GET_ALL_RECEIVER_INFO:
                        strArray2[0] = new string[6];
                        if (obj2 != null)
                        {
                            Receiver.AllReceiverInfo info2 = obj2 as Receiver.AllReceiverInfo;
                            strArray2[0] = new string[] { info2.UnitInfo.Protocol_Version.ToString(), info2.UnitInfo.Device_Class.ToString(), info2.UnitInfo.Device_SubClass.ToString(), info2.UnitInfo.Firmware_Version.ToString(), info2.UnitInfo.Unit_Name.ToString(), info2.UnitInfo.Unit_Serial_Number.ToString(), info2.UnitStatus.Sub_mode.ToString(), info2.UnitStatus.Uptime.ToString(), info2.UnitStatus.Average_Processor_Workload.ToString(), info2.UnitStatus.Number_of_Resets.ToString(), info2.UnitStatus.Tag_messages_in_buffer.ToString(), info2.NoiseLevel.ToString(), info2.PowerControl.Power_Mode.ToString(), (((double) info2.PowerControl.Input_Voltage) / 10.0).ToString() };
                        }
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.BOOTLOADER_FIRMWARE_STATE:
                    {
                        ushort num4 = 0;
                        if (obj2 != null)
                        {
                            num4 = (ushort) obj2;
                        }
                        strArray2[0] = new string[] { "0x" + num4.ToString("X") };
                        goto Label_0DC1;
                    }
                    case ReceiversManager.RequestType.SET_UNIT_NAME:
                    case ReceiversManager.RequestType.GET_UNIT_NAME:
                        unitName = "";
                        if (obj2 != null)
                        {
                            unitName = ((Receiver.UnitNameParameter) obj2).UnitName;
                        }
                        strArray2[0] = new string[] { unitName };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.SET_SITE_CODE:
                    case ReceiversManager.RequestType.GET_SITE_CODE:
                        unitName = "";
                        if (obj2 != null)
                        {
                            unitName = ((Receiver.SiteCodeParameter) obj2).SiteCode.ToString();
                        }
                        strArray2[0] = new string[] { unitName };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.GetRFBaudRate:
                    case ReceiversManager.RequestType.SetRFBaudRate:
                        unitName = "";
                        if (obj2 != null)
                        {
                            unitName = ((Receiver.RFBaudRates) obj2).ToString();
                        }
                        strArray2[0] = new string[] { unitName };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.GetModulation:
                    case ReceiversManager.RequestType.SetModulation:
                        unitName = "";
                        if (obj2 != null)
                        {
                            unitName = ((Receiver.ModulationParameter) obj2).ToString();
                        }
                        strArray2[0] = new string[] { unitName };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.GetSerialNum:
                    case ReceiversManager.RequestType.SetSerialNum:
                        unitName = "";
                        if (obj2 != null)
                        {
                            unitName = ((Receiver.SerialNumParameter) obj2).Serial.ToString();
                        }
                        strArray2[0] = new string[] { unitName };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.SET_RSSI_THRESHOLD:
                    case ReceiversManager.RequestType.GET_RSSI_THRESHOLD:
                        unitName = "";
                        if (obj2 != null)
                        {
                            unitName = ((Receiver.RSSIFilterParameter) obj2).RSSIFilter.ToString();
                        }
                        strArray2[0] = new string[] { unitName };
                        goto Label_0DC1;

                    case ReceiversManager.RequestType.GET_POWER_CONTROL:
                        if (obj2 != null)
                        {
                            Receiver.Power_Control control = (Receiver.Power_Control) obj2;
                            strArray2[0] = new string[] { control.Power_Mode.ToString(), (((double) control.Input_Voltage) / 10.0).ToString() };
                        }
                        goto Label_0DC1;

                    default:
                        goto Label_0DC1;
                }
                strArray2[0] = new string[] { "", "" };
                goto Label_0DC1;
            Label_0862:;
                strArray2[0] = new string[] { "", "" };
                goto Label_0DC1;
            Label_08AF:;
                strArray2[0] = new string[] { "" };
                goto Label_0DC1;
            Label_08FA:
                strArray2[0][0] = "???";
                goto Label_0DC1;
            Label_093B:;
                strArray2[0] = new string[] { "" };
                goto Label_0DC1;
            Label_09AA:
                tagArray = obj2 as Receiver.Tag[];
                strArray2 = new string[tagArray.Length][];
                colorArray = new Color[tagArray.Length];
                int index = 0;
                while (index < tagArray.Length)
                {
                    strArray2[index] = new string[] { tagArray[index].tagID.ToString(), tagArray[index].transmissionIndex.ToString(), tagArray[index].tagMsg.ToString(), tagArray[index].activatorNum.ToString(), tagArray[index].RSSI.ToString(), tagArray[index].NoiseLevel.ToString(), tagArray[index].ts.ToString() };
                    colorArray[index] = Program.TagMsg2Color(tagArray[index].tagMsg);
                    index++;
                }
            Label_0DC1:
                index = 0;
                while (index < strArray2.Length)
                {
                    string[] array = new string[strArray.Length + strArray2[index].Length];
                    num3++;
                    strArray[0] = num3.ToString();
                    strArray.CopyTo(array, 0);
                    strArray2[index].CopyTo(array, strArray.Length);
                    ListViewItem item = new ListViewItem(array);
                    item.ForeColor = colorArray[index];
                    this.listResults.Items.Add(item);
                    index++;
                }
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            Program.ExportListViewToCSV(this.listResults, this.dlgSaveCSV);
        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            Program.ExportListViewToXML(this.listResults, this.dlgSaveXML);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listResults = new ListView();
            this.btnClose = new Button();
            this.btnExportCSV = new Button();
            this.dlgSaveCSV = new SaveFileDialog();
            this.dlgSaveXML = new SaveFileDialog();
            this.btnExportXML = new Button();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            base.SuspendLayout();
            this.tableLayoutPanel1.SetColumnSpan(this.listResults, 4);
            this.listResults.Dock = DockStyle.Fill;
            this.listResults.Location = new Point(2, 2);
            this.listResults.Margin = new Padding(2, 2, 2, 2);
            this.listResults.Name = "listResults";
            this.listResults.Size = new Size(0x2b6, 0x1be);
            this.listResults.TabIndex = 0;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.View = View.Details;
            this.btnClose.DialogResult = DialogResult.OK;
            this.btnClose.Dock = DockStyle.Fill;
            this.btnClose.Location = new Point(600, 0x1c4);
            this.btnClose.Margin = new Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x60, 20);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnExportCSV.Dock = DockStyle.Fill;
            this.btnExportCSV.Location = new Point(500, 0x1c4);
            this.btnExportCSV.Margin = new Padding(2, 2, 2, 2);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new Size(0x60, 20);
            this.btnExportCSV.TabIndex = 2;
            this.btnExportCSV.Text = "Export to CSV";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new EventHandler(this.btnExportCSV_Click);
            this.dlgSaveCSV.DefaultExt = "csv";
            this.dlgSaveCSV.Filter = "CSV|*.csv";
            this.dlgSaveCSV.Title = "Export results to CSV";
            this.dlgSaveXML.DefaultExt = "xml";
            this.dlgSaveXML.Filter = "XML|*.xml";
            this.dlgSaveXML.Title = "Export results to XML";
            this.btnExportXML.Dock = DockStyle.Fill;
            this.btnExportXML.Location = new Point(400, 0x1c4);
            this.btnExportXML.Margin = new Padding(2, 2, 2, 2);
            this.btnExportXML.Name = "btnExportXML";
            this.btnExportXML.Size = new Size(0x60, 20);
            this.btnExportXML.TabIndex = 3;
            this.btnExportXML.Text = "Export to XML";
            this.btnExportXML.UseVisualStyleBackColor = true;
            this.btnExportXML.Click += new EventHandler(this.btnExportXML_Click);
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            this.tableLayoutPanel1.Controls.Add(this.listResults, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExportXML, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExportCSV, 2, 1);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Margin = new Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
            this.tableLayoutPanel1.Size = new Size(0x2ba, 0x1da);
            this.tableLayoutPanel1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2ba, 0x1da);
            base.Controls.Add(this.tableLayoutPanel1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.Name = "ResultsWin";
            this.Text = "PureRF - Results";
            base.Load += new EventHandler(this.ResultsWin_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void ResultsWin_Load(object sender, EventArgs e)
        {
            this.dlgSaveCSV.InitialDirectory = Application.ExecutablePath;
            this.dlgSaveXML.InitialDirectory = Application.ExecutablePath;
        }
    }
}

