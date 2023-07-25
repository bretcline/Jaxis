namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MainWin : Form
    {
        private Button btnAbort;
        private Button btnAdd;
        private Button btnAutoDiscover;
        private Button btnAutoGetTags;
        private Button btnClearLog;
        private Button btnDBSetup;
        private Button btnExportList;
        private Button btnGainSet;
        private Button btnGetTags;
        private Button btnImportList;
        private Button btnInvert;
        private Button btnManageLoops;
        private Button btnMiscCmd;
        private Button btnReceiverModulationType;
        private Button btnRelay;
        private Button btnRemoveSelected;
        private Button btnSelectAll;
        private Button btnSelectNone;
        private Button btnSetName;
        private Button btnSetRFBaud;
        private Button btnSetRSSIFilter;
        private Button btnSetSiteCode;
        private CheckBox checkUseDB;
        private ComboBox cmbRFBaud;
        private ColumnHeader columnLoop;
        private ColumnHeader columnName;
        private ColumnHeader columnUnitID;
        private IContainer components;
        public const string DLGBOX_TITLE = "PureRF Receiver";
        private TextBox edtUnitName;
        private SaveFileDialog exportListDlg;
        private GroupBox groupBox1;
        private GroupBox groupBox10;
        private GroupBox groupBox11;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private OpenFileDialog importListDlg;
        private ToolStripStatusLabel lblStatus;
        private ListView listReceivers;
        private bool mAutoGetStatus;
        public ReceiversManager mReceiversManager;
        private TagsOleDB mTagsDB;
        private NumericUpDown numRSSI;
        private NumericUpDown numSiteCode;
        private RadioButton radioASK;
        private RadioButton radioAutoGetTagsMultipleLines;
        private RadioButton radioAutoGetTagsSingleLine;
        private RadioButton radioCompleteStatus;
        private RadioButton radioFlushTagsBuf;
        private RadioButton radioFSK;
        private RadioButton radioGain;
        private RadioButton radioGain14;
        private RadioButton radioGain20;
        private RadioButton radioGain6;
        private RadioButton radioGainMax;
        private RadioButton radioGetBootloaderVersion;
        private RadioButton radioGetFirmwareChecksum;
        private RadioButton radioGetNoiseLevel;
        private RadioButton radioGetPowerControl;
        private RadioButton radioGetReceiverInfo;
        private RadioButton radioGetReceiverName;
        private RadioButton radioGetReceiverStatus;
        private RadioButton radioGetSiteCode;
        private RadioButton radioModulation;
        private RadioButton radioRFBaud;
        private RadioButton radioRSSI;
        private RadioButton radioSerial;
        private RadioButton radioTagsWithoutTS;
        private RadioButton radioTagsWithTS;
        private RadioButton radioUploadFirmware;
        private RadioButton Relay5;
        private ComboBox RelayNumber;
        private RadioButton RelayOff;
        private RadioButton RelayOn;
        private ToolStripProgressBar statusProgBar;
        private StatusStrip statusStrip1;
        private TextBox txtLog;

        public MainWin()
        {
            SplashScreen screen = new SplashScreen();
            this.InitializeComponent();
            this.mTagsDB = new TagsOleDB();
            this.mReceiversManager = new ReceiversManager();
            this.mReceiversManager.SetEventCallback(new ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
            screen.ShowDialog();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.Log("Abort requested: " + this.mReceiversManager.AbortRequest().ToString(), new object[0]);
            this.btnAbort.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddReceiverWin win = new AddReceiverWin(this.mReceiversManager);
            if (win.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem(new string[] { win.txtName.Text, win.numUnitID.Value.ToString(), win.comboLoop.SelectedItem.ToString() });
                this.listReceivers.Items.Add(item);
            }
        }

        private void btnAutoDiscover_Click(object sender, EventArgs e)
        {
            new AutoDiscoverWin(this.mReceiversManager).ShowDialog();
            this.listReceivers.Items.Clear();
            foreach (ReceiversManager.ManagedReceiver receiver in this.mReceiversManager.ReceiversList)
            {
                ListViewItem item = new ListViewItem(new string[] { receiver.Name, receiver.UnitID.ToString(), receiver.Loop.Name });
                this.listReceivers.Items.Add(item);
            }
        }

        private void btnAutoGetTags_Click(object sender, EventArgs e)
        {
            AutoGetTagsWin.DisplayType displayType = AutoGetTagsWin.DisplayType.MULTIPLE_LINES_PER_TAG;
            if (this.radioAutoGetTagsSingleLine.Checked)
            {
                displayType = AutoGetTagsWin.DisplayType.SINGLE_LINE_PER_TAG;
            }
            if (!this.mReceiversManager.IsReady)
            {
                MessageBox.Show("A request is still executing. Please wait for it to terminate", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string[] receiversWorkSet = this.GetReceiversWorkSet();
                if (receiversWorkSet.Length == 0)
                {
                    MessageBox.Show("Please select at least one receiver.", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    new AutoGetTagsWin(displayType, this.mReceiversManager, receiversWorkSet, this.mTagsDB, this.checkUseDB.Checked).ShowDialog();
                    this.mReceiversManager.SetEventCallback(new ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
                }
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            this.txtLog.Clear();
        }

        private void btnDBSetup_Click(object sender, EventArgs e)
        {
            new DBSetupWin(this.mTagsDB).ShowDialog();
            if (this.mTagsDB.IsOpened)
            {
                this.checkUseDB.Enabled = true;
            }
            else
            {
                this.checkUseDB.Enabled = false;
            }
        }

        private void btnExportList_Click(object sender, EventArgs e)
        {
            if (this.exportListDlg.ShowDialog() == DialogResult.OK)
            {
                if (!this.mReceiversManager.SaveXML(this.exportListDlg.FileName))
                {
                    MessageBox.Show("Unable to write receivers list to file!", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    this.Log("Exported receivers/loops list to " + this.exportListDlg.FileName, new object[0]);
                }
            }
        }

        private void btnGainSet_Click(object sender, EventArgs e)
        {
            Receiver.AntennaGain iNVALID = Receiver.AntennaGain.INVALID;
            if (this.radioGainMax.Checked)
            {
                iNVALID = Receiver.AntennaGain.MAX;
            }
            if (this.radioGain6.Checked)
            {
                iNVALID = Receiver.AntennaGain.MINUS_6DB;
            }
            if (this.radioGain14.Checked)
            {
                iNVALID = Receiver.AntennaGain.MINUS_14DB;
            }
            if (this.radioGain20.Checked)
            {
                iNVALID = Receiver.AntennaGain.MINUS_20DB;
            }
            this.StartRequest(ReceiversManager.RequestType.SET_ANTENNA_GAIN, new object[] { iNVALID });
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            Receiver.UnitNameParameter parameter = new Receiver.UnitNameParameter();
            parameter.UnitName = this.edtUnitName.Text;
            this.StartRequest(ReceiversManager.RequestType.GET_UNIT_NAME, new object[] { parameter });
        }

        private void btnGetRSSIFilter_Click(object sender, EventArgs e)
        {
            Receiver.RSSIFilterParameter parameter = new Receiver.RSSIFilterParameter();
            parameter.RSSIFilter = (int) this.numRSSI.Value;
            this.StartRequest(ReceiversManager.RequestType.GET_RSSI_THRESHOLD, new object[] { parameter });
        }

        private void btnGetSiteCode_Click(object sender, EventArgs e)
        {
            Receiver.SiteCodeParameter parameter = new Receiver.SiteCodeParameter();
            parameter.SiteCode = (int) this.numSiteCode.Value;
            this.StartRequest(ReceiversManager.RequestType.GET_SITE_CODE, new object[] { parameter });
        }

        private void btnGetTags_Click(object sender, EventArgs e)
        {
            if (this.radioTagsWithoutTS.Checked)
            {
                this.StartRequest(ReceiversManager.RequestType.GET_ALL_TAGS, new object[] { false });
            }
            else if (this.radioTagsWithTS.Checked)
            {
                this.StartRequest(ReceiversManager.RequestType.GET_ALL_TAGS, new object[] { true });
            }
        }

        private void btnImportList_Click(object sender, EventArgs e)
        {
            this.ImportSettings();
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listReceivers.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        private void btnManageLoops_Click(object sender, EventArgs e)
        {
            new ManageLoopsWin(this.mReceiversManager).ShowDialog();
            this.listReceivers.Items.Clear();
            foreach (ReceiversManager.ManagedReceiver receiver in this.mReceiversManager.ReceiversList)
            {
                ListViewItem item = new ListViewItem(new string[] { receiver.Name, receiver.UnitID.ToString(), receiver.Loop.Name });
                this.listReceivers.Items.Add(item);
            }
        }

        private void btnMiscCmd_Click(object sender, EventArgs e)
        {
            ReceiversManager.RequestType requestType = ReceiversManager.RequestType.INVALID_REQUEST;
            if (this.radioGetReceiverInfo.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_UNIT_INFO;
            }
            else if (this.radioGetReceiverStatus.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_UNIT_STATUS;
            }
            else if (this.radioGetReceiverName.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_UNIT_NAME;
            }
            else if (this.radioGetNoiseLevel.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_NOISE_LEVEL;
            }
            else if (this.radioGetSiteCode.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_SITE_CODE;
            }
            else if (this.radioRSSI.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_RSSI_THRESHOLD;
            }
            else if (this.radioRFBaud.Checked)
            {
                requestType = ReceiversManager.RequestType.GetRFBaudRate;
            }
            else if (this.radioModulation.Checked)
            {
                requestType = ReceiversManager.RequestType.GetModulation;
            }
            else if (this.radioSerial.Checked)
            {
                requestType = ReceiversManager.RequestType.GetSerialNum;
            }
            else if (this.radioGain.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_ANTENNA_GAIN;
            }
            else if (this.radioGetPowerControl.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_POWER_CONTROL;
            }
            else if (this.radioFlushTagsBuf.Checked)
            {
                requestType = ReceiversManager.RequestType.FLUSH_TAG_BUFFER;
            }
            else if (this.radioCompleteStatus.Checked)
            {
                requestType = ReceiversManager.RequestType.GET_ALL_RECEIVER_INFO;
            }
            else if ((this.radioGetFirmwareChecksum.Checked || this.radioGetBootloaderVersion.Checked) || this.radioUploadFirmware.Checked)
            {
                if (MessageBox.Show("This command will put the receiver temporarily into bootloader mode." + Environment.NewLine + "Do you wish to proceed?", "PureRF Receiver", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                if (this.radioGetFirmwareChecksum.Checked)
                {
                    requestType = ReceiversManager.RequestType.BOOTLOADER_FIRMWARE_STATE;
                }
                else if (this.radioGetBootloaderVersion.Checked)
                {
                    requestType = ReceiversManager.RequestType.BOOTLOADER_GET_UNIT_INFO;
                }
                else if (this.radioUploadFirmware.Checked)
                {
                    FirmwareWin win = new FirmwareWin();
                    requestType = ReceiversManager.RequestType.INVALID_REQUEST;
                    if (win.ShowDialog() == DialogResult.OK)
                    {
                        new FirmwareWorkerWin(this.mReceiversManager, this.GetReceiversWorkSet(), (int) win.numResendCnt.Value, (int) win.numResendDelay.Value, (int) win.numPageDelay.Value, win.mFirmware).ShowDialog();
                        this.mReceiversManager.SetEventCallback(new ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
                    }
                    return;
                }
            }
            this.StartRequest(requestType, new object[0]);
        }

        private void btnReceiverModulationType_Click(object sender, EventArgs e)
        {
            Receiver.ModulationParameter modASK;
            if (this.radioASK.Checked)
            {
                modASK = Receiver.ModulationParameter.ModASK;
            }
            else
            {
                modASK = Receiver.ModulationParameter.ModFSK;
            }
            this.StartRequest(ReceiversManager.RequestType.SetModulation, new object[] { modASK });
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            while (this.listReceivers.CheckedItems.Count > 0)
            {
                this.mReceiversManager.RemoveReceiver(this.listReceivers.CheckedItems[0].SubItems[0].Text);
                this.listReceivers.Items.RemoveAt(this.listReceivers.CheckedIndices[0]);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listReceivers.Items)
            {
                item.Checked = true;
            }
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listReceivers.Items)
            {
                item.Checked = false;
            }
        }

        private void btnSetReceiverSensitivity_Click(object sender, EventArgs e)
        {
        }

        private void btnSetRFBaud_Click(object sender, EventArgs e)
        {
            if (this.cmbRFBaud.SelectedIndex != -1)
            {
                Receiver.RFBaudRates iNVALID = Receiver.RFBaudRates.INVALID;
                switch (this.cmbRFBaud.SelectedIndex)
                {
                    case 0:
                        iNVALID = Receiver.RFBaudRates.b9600;
                        break;

                    case 1:
                        iNVALID = Receiver.RFBaudRates.b19200;
                        break;

                    case 2:
                        iNVALID = Receiver.RFBaudRates.b28800;
                        break;

                    case 3:
                        iNVALID = Receiver.RFBaudRates.b38400;
                        break;

                    case 4:
                        iNVALID = Receiver.RFBaudRates.b57600;
                        break;

                    case 5:
                        iNVALID = Receiver.RFBaudRates.b115200;
                        break;

                    default:
                        iNVALID = Receiver.RFBaudRates.b38400;
                        break;
                }
                this.StartRequest(ReceiversManager.RequestType.SetRFBaudRate, new object[] { iNVALID });
            }
        }

        private void btnSetRSSIFilter_Click(object sender, EventArgs e)
        {
            Receiver.RSSIFilterParameter parameter = new Receiver.RSSIFilterParameter();
            parameter.RSSIFilter = (int) this.numRSSI.Value;
            this.StartRequest(ReceiversManager.RequestType.SET_RSSI_THRESHOLD, new object[] { parameter });
        }

        private void btnSetSiteCode_Click(object sender, EventArgs e)
        {
            Receiver.SiteCodeParameter parameter = new Receiver.SiteCodeParameter();
            parameter.SiteCode = (int) this.numSiteCode.Value;
            this.StartRequest(ReceiversManager.RequestType.SET_SITE_CODE, new object[] { parameter });
        }

        private void btnSetTimeCmd_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.RelayNumber.SelectedIndex != -1)
            {
                Receiver.ActivateRelayParameter parameter = new Receiver.ActivateRelayParameter();
                parameter.Relay = this.RelayNumber.SelectedIndex;
                if (this.RelayOff.Checked)
                {
                    parameter.Interval = 0;
                }
                else if (this.RelayOn.Checked)
                {
                    parameter.Interval = 0xff;
                }
                else if (this.Relay5.Checked)
                {
                    parameter.Interval = 50;
                }
                this.StartRequest(ReceiversManager.RequestType.ACTIVATE_RELAY, new object[] { parameter });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Receiver.UnitNameParameter parameter = new Receiver.UnitNameParameter();
            parameter.UnitName = this.edtUnitName.Text;
            this.StartRequest(ReceiversManager.RequestType.SET_UNIT_NAME, new object[] { parameter });
        }

        private void DisableAllButtons()
        {
            this.btnAutoGetTags.Enabled = false;
            this.btnGetTags.Enabled = false;
            this.btnMiscCmd.Enabled = false;
            this.btnAdd.Enabled = false;
            this.btnRemoveSelected.Enabled = false;
            this.btnSelectAll.Enabled = false;
            this.btnSelectNone.Enabled = false;
            this.btnManageLoops.Enabled = false;
            this.btnInvert.Enabled = false;
            this.btnAutoDiscover.Enabled = false;
            this.btnImportList.Enabled = false;
            this.btnExportList.Enabled = false;
            this.btnRelay.Enabled = false;
            this.btnSetRSSIFilter.Enabled = false;
            this.btnSetSiteCode.Enabled = false;
            this.btnSetRFBaud.Enabled = false;
            this.btnSetName.Enabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EnableAllButtons()
        {
            this.btnAutoGetTags.Enabled = true;
            this.btnGetTags.Enabled = true;
            this.btnMiscCmd.Enabled = true;
            this.btnAdd.Enabled = true;
            this.btnRemoveSelected.Enabled = true;
            this.btnSelectAll.Enabled = true;
            this.btnSelectNone.Enabled = true;
            this.btnManageLoops.Enabled = true;
            this.btnInvert.Enabled = true;
            this.btnAutoDiscover.Enabled = true;
            this.btnImportList.Enabled = true;
            this.btnExportList.Enabled = true;
            this.btnRelay.Enabled = true;
            this.btnSetRSSIFilter.Enabled = true;
            this.btnSetSiteCode.Enabled = true;
            this.btnSetRFBaud.Enabled = true;
            this.btnSetName.Enabled = true;
        }

        public string[] GetReceiversWorkSet()
        {
            string[] strArray = new string[this.listReceivers.CheckedItems.Count];
            for (int i = 0; i < this.listReceivers.CheckedItems.Count; i++)
            {
                strArray[i] = this.listReceivers.CheckedItems[i].SubItems[0].Text;
            }
            return strArray;
        }

        private bool ImportSettings()
        {
            string str;
            if (this.importListDlg.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            this.listReceivers.Items.Clear();
            if (!this.mReceiversManager.LoadXML(this.importListDlg.FileName, out str))
            {
                MessageBox.Show(str, "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            foreach (ReceiversManager.ManagedReceiver receiver in this.mReceiversManager.ReceiversList)
            {
                ListViewItem item = new ListViewItem(new string[] { receiver.Name, receiver.UnitID.ToString(), receiver.Loop.Name });
                this.listReceivers.Items.Add(item);
                this.listReceivers.Items[this.listReceivers.Items.Count - 1].Checked = true;
            }
            this.Log("Loaded receivers/loops list from " + this.importListDlg.FileName, new object[0]);
            return true;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MainWin));
            this.statusStrip1 = new StatusStrip();
            this.statusProgBar = new ToolStripProgressBar();
            this.lblStatus = new ToolStripStatusLabel();
            this.groupBox1 = new GroupBox();
            this.btnAutoDiscover = new Button();
            this.btnManageLoops = new Button();
            this.listReceivers = new ListView();
            this.columnName = new ColumnHeader();
            this.columnUnitID = new ColumnHeader();
            this.columnLoop = new ColumnHeader();
            this.btnInvert = new Button();
            this.btnAdd = new Button();
            this.btnRemoveSelected = new Button();
            this.btnExportList = new Button();
            this.btnImportList = new Button();
            this.btnSelectNone = new Button();
            this.btnSelectAll = new Button();
            this.txtLog = new TextBox();
            this.groupBox2 = new GroupBox();
            this.btnGetTags = new Button();
            this.radioTagsWithTS = new RadioButton();
            this.radioTagsWithoutTS = new RadioButton();
            this.groupBox5 = new GroupBox();
            this.radioGain = new RadioButton();
            this.radioSerial = new RadioButton();
            this.radioModulation = new RadioButton();
            this.radioRFBaud = new RadioButton();
            this.radioRSSI = new RadioButton();
            this.radioGetSiteCode = new RadioButton();
            this.radioGetReceiverName = new RadioButton();
            this.radioGetPowerControl = new RadioButton();
            this.radioGetReceiverStatus = new RadioButton();
            this.radioCompleteStatus = new RadioButton();
            this.radioUploadFirmware = new RadioButton();
            this.radioGetBootloaderVersion = new RadioButton();
            this.radioGetFirmwareChecksum = new RadioButton();
            this.btnMiscCmd = new Button();
            this.radioFlushTagsBuf = new RadioButton();
            this.radioGetNoiseLevel = new RadioButton();
            this.radioGetReceiverInfo = new RadioButton();
            this.btnAbort = new Button();
            this.exportListDlg = new SaveFileDialog();
            this.importListDlg = new OpenFileDialog();
            this.btnAutoGetTags = new Button();
            this.btnClearLog = new Button();
            this.groupBox7 = new GroupBox();
            this.btnDBSetup = new Button();
            this.checkUseDB = new CheckBox();
            this.radioAutoGetTagsSingleLine = new RadioButton();
            this.radioAutoGetTagsMultipleLines = new RadioButton();
            this.groupBox6 = new GroupBox();
            this.RelayNumber = new ComboBox();
            this.btnRelay = new Button();
            this.Relay5 = new RadioButton();
            this.RelayOff = new RadioButton();
            this.RelayOn = new RadioButton();
            this.groupBox8 = new GroupBox();
            this.edtUnitName = new TextBox();
            this.btnSetName = new Button();
            this.groupBox9 = new GroupBox();
            this.numRSSI = new NumericUpDown();
            this.btnSetRSSIFilter = new Button();
            this.groupBox3 = new GroupBox();
            this.numSiteCode = new NumericUpDown();
            this.btnSetSiteCode = new Button();
            this.groupBox4 = new GroupBox();
            this.cmbRFBaud = new ComboBox();
            this.btnSetRFBaud = new Button();
            this.groupBox10 = new GroupBox();
            this.radioGain20 = new RadioButton();
            this.radioGain14 = new RadioButton();
            this.btnGainSet = new Button();
            this.radioGain6 = new RadioButton();
            this.radioGainMax = new RadioButton();
            this.groupBox11 = new GroupBox();
            this.btnReceiverModulationType = new Button();
            this.radioFSK = new RadioButton();
            this.radioASK = new RadioButton();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.numRSSI.BeginInit();
            this.groupBox3.SuspendLayout();
            this.numSiteCode.BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            base.SuspendLayout();
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.statusProgBar, this.lblStatus });
            this.statusStrip1.Location = new Point(0, 0x27d);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new Size(0x312, 0x16);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            this.statusProgBar.Margin = new Padding(3, 3, 1, 3);
            this.statusProgBar.Name = "statusProgBar";
            this.statusProgBar.Size = new Size(0x4b, 0x10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x2a, 0x11);
            this.lblStatus.Text = "Ready.";
            this.groupBox1.Controls.Add(this.btnAutoDiscover);
            this.groupBox1.Controls.Add(this.btnManageLoops);
            this.groupBox1.Controls.Add(this.listReceivers);
            this.groupBox1.Controls.Add(this.btnInvert);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnRemoveSelected);
            this.groupBox1.Controls.Add(this.btnExportList);
            this.groupBox1.Controls.Add(this.btnImportList);
            this.groupBox1.Controls.Add(this.btnSelectNone);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Location = new Point(10, 11);
            this.groupBox1.Margin = new Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2);
            this.groupBox1.Size = new Size(260, 0x1b7);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Receivers";
            this.btnAutoDiscover.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnAutoDiscover.Location = new Point(4, 0x17d);
            this.btnAutoDiscover.Margin = new Padding(2);
            this.btnAutoDiscover.Name = "btnAutoDiscover";
            this.btnAutoDiscover.Size = new Size(0x66, 0x34);
            this.btnAutoDiscover.TabIndex = 11;
            this.btnAutoDiscover.Text = "Auto-discover";
            this.btnAutoDiscover.UseVisualStyleBackColor = true;
            this.btnAutoDiscover.Click += new EventHandler(this.btnAutoDiscover_Click);
            this.btnManageLoops.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnManageLoops.Location = new Point(4, 0x129);
            this.btnManageLoops.Margin = new Padding(2);
            this.btnManageLoops.Name = "btnManageLoops";
            this.btnManageLoops.Size = new Size(0x66, 0x18);
            this.btnManageLoops.TabIndex = 10;
            this.btnManageLoops.Text = "Manage loops";
            this.btnManageLoops.UseVisualStyleBackColor = true;
            this.btnManageLoops.Click += new EventHandler(this.btnManageLoops_Click);
            this.listReceivers.CheckBoxes = true;
            this.listReceivers.Columns.AddRange(new ColumnHeader[] { this.columnName, this.columnUnitID, this.columnLoop });
            this.listReceivers.LabelEdit = true;
            this.listReceivers.Location = new Point(4, 13);
            this.listReceivers.Margin = new Padding(2);
            this.listReceivers.Name = "listReceivers";
            this.listReceivers.Size = new Size(0xfb, 0xe0);
            this.listReceivers.TabIndex = 9;
            this.listReceivers.UseCompatibleStateImageBehavior = false;
            this.listReceivers.View = View.Details;
            this.listReceivers.AfterLabelEdit += new LabelEditEventHandler(this.listReceivers_AfterLabelEdit);
            this.columnName.Text = "Conn. Name";
            this.columnName.Width = 0x4e;
            this.columnUnitID.Text = "Unit ID";
            this.columnUnitID.Width = 0x30;
            this.columnLoop.Text = "Communication Loop";
            this.columnLoop.Width = 0x70;
            this.btnInvert.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnInvert.Location = new Point(0x99, 0x129);
            this.btnInvert.Margin = new Padding(2);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new Size(0x66, 0x18);
            this.btnInvert.TabIndex = 8;
            this.btnInvert.Text = "Invert selection";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.Click += new EventHandler(this.btnInvert_Click);
            this.btnAdd.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnAdd.Location = new Point(4, 0xf1);
            this.btnAdd.Margin = new Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x66, 0x18);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnRemoveSelected.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnRemoveSelected.Location = new Point(4, 0x10d);
            this.btnRemoveSelected.Margin = new Padding(2);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new Size(0x66, 0x18);
            this.btnRemoveSelected.TabIndex = 5;
            this.btnRemoveSelected.Text = "Remove selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new EventHandler(this.btnRemoveSelected_Click);
            this.btnExportList.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnExportList.Location = new Point(0x99, 0x199);
            this.btnExportList.Margin = new Padding(2);
            this.btnExportList.Name = "btnExportList";
            this.btnExportList.Size = new Size(0x66, 0x18);
            this.btnExportList.TabIndex = 4;
            this.btnExportList.Text = "Export";
            this.btnExportList.UseVisualStyleBackColor = true;
            this.btnExportList.Click += new EventHandler(this.btnExportList_Click);
            this.btnImportList.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnImportList.Location = new Point(0x99, 0x17d);
            this.btnImportList.Margin = new Padding(2);
            this.btnImportList.Name = "btnImportList";
            this.btnImportList.Size = new Size(0x66, 0x18);
            this.btnImportList.TabIndex = 3;
            this.btnImportList.Text = "Import";
            this.btnImportList.UseVisualStyleBackColor = true;
            this.btnImportList.Click += new EventHandler(this.btnImportList_Click);
            this.btnSelectNone.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSelectNone.Location = new Point(0x99, 0x10d);
            this.btnSelectNone.Margin = new Padding(2);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new Size(0x66, 0x18);
            this.btnSelectNone.TabIndex = 2;
            this.btnSelectNone.Text = "Select none";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new EventHandler(this.btnSelectNone_Click);
            this.btnSelectAll.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSelectAll.Location = new Point(0x99, 0xf1);
            this.btnSelectAll.Margin = new Padding(2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(0x66, 0x18);
            this.btnSelectAll.TabIndex = 1;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.txtLog.BackColor = Color.Black;
            this.txtLog.ForeColor = Color.Lime;
            this.txtLog.Location = new Point(9, 0x1c7);
            this.txtLog.Margin = new Padding(2);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Both;
            this.txtLog.Size = new Size(690, 0xb2);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "Application started.\r\n";
            this.txtLog.WordWrap = false;
            this.groupBox2.Controls.Add(this.btnGetTags);
            this.groupBox2.Controls.Add(this.radioTagsWithTS);
            this.groupBox2.Controls.Add(this.radioTagsWithoutTS);
            this.groupBox2.Location = new Point(0x1bc, 0x9c);
            this.groupBox2.Margin = new Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(2);
            this.groupBox2.Size = new Size(0xa6, 0x5d);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Get tags";
            this.btnGetTags.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnGetTags.Location = new Point(60, 0x3e);
            this.btnGetTags.Margin = new Padding(2);
            this.btnGetTags.Name = "btnGetTags";
            this.btnGetTags.Size = new Size(0x66, 0x18);
            this.btnGetTags.TabIndex = 2;
            this.btnGetTags.Text = "Get tags";
            this.btnGetTags.UseVisualStyleBackColor = true;
            this.btnGetTags.Click += new EventHandler(this.btnGetTags_Click);
            this.radioTagsWithTS.AutoSize = true;
            this.radioTagsWithTS.Enabled = false;
            this.radioTagsWithTS.Location = new Point(5, 0x29);
            this.radioTagsWithTS.Margin = new Padding(2);
            this.radioTagsWithTS.Name = "radioTagsWithTS";
            this.radioTagsWithTS.Size = new Size(0x61, 0x11);
            this.radioTagsWithTS.TabIndex = 1;
            this.radioTagsWithTS.Text = "With timestamp";
            this.radioTagsWithTS.UseVisualStyleBackColor = true;
            this.radioTagsWithoutTS.AutoSize = true;
            this.radioTagsWithoutTS.Checked = true;
            this.radioTagsWithoutTS.Enabled = false;
            this.radioTagsWithoutTS.Location = new Point(5, 0x12);
            this.radioTagsWithoutTS.Margin = new Padding(2);
            this.radioTagsWithoutTS.Name = "radioTagsWithoutTS";
            this.radioTagsWithoutTS.Size = new Size(0x70, 0x11);
            this.radioTagsWithoutTS.TabIndex = 0;
            this.radioTagsWithoutTS.TabStop = true;
            this.radioTagsWithoutTS.Text = "Without timestamp";
            this.radioTagsWithoutTS.UseVisualStyleBackColor = true;
            this.groupBox5.Controls.Add(this.radioGain);
            this.groupBox5.Controls.Add(this.radioSerial);
            this.groupBox5.Controls.Add(this.radioModulation);
            this.groupBox5.Controls.Add(this.radioRFBaud);
            this.groupBox5.Controls.Add(this.radioRSSI);
            this.groupBox5.Controls.Add(this.radioGetSiteCode);
            this.groupBox5.Controls.Add(this.radioGetReceiverName);
            this.groupBox5.Controls.Add(this.radioGetPowerControl);
            this.groupBox5.Controls.Add(this.radioGetReceiverStatus);
            this.groupBox5.Controls.Add(this.radioCompleteStatus);
            this.groupBox5.Controls.Add(this.radioUploadFirmware);
            this.groupBox5.Controls.Add(this.radioGetBootloaderVersion);
            this.groupBox5.Controls.Add(this.radioGetFirmwareChecksum);
            this.groupBox5.Controls.Add(this.btnMiscCmd);
            this.groupBox5.Controls.Add(this.radioFlushTagsBuf);
            this.groupBox5.Controls.Add(this.radioGetNoiseLevel);
            this.groupBox5.Controls.Add(this.radioGetReceiverInfo);
            this.groupBox5.Location = new Point(0x112, 11);
            this.groupBox5.Margin = new Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new Padding(2);
            this.groupBox5.Size = new Size(0xa6, 0x1a3);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Misc. commands";
            this.radioGain.AutoSize = true;
            this.radioGain.Location = new Point(4, 0x13e);
            this.radioGain.Margin = new Padding(2);
            this.radioGain.Name = "radioGain";
            this.radioGain.Size = new Size(0x67, 0x11);
            this.radioGain.TabIndex = 0x13;
            this.radioGain.Text = "Get Gain Setting";
            this.radioGain.UseVisualStyleBackColor = true;
            this.radioSerial.AutoSize = true;
            this.radioSerial.Location = new Point(4, 0x12a);
            this.radioSerial.Margin = new Padding(2);
            this.radioSerial.Name = "radioSerial";
            this.radioSerial.Size = new Size(0x6f, 0x11);
            this.radioSerial.TabIndex = 0x12;
            this.radioSerial.Text = "Get Serial Number";
            this.radioSerial.UseVisualStyleBackColor = true;
            this.radioModulation.AutoSize = true;
            this.radioModulation.Location = new Point(4, 0x116);
            this.radioModulation.Margin = new Padding(2);
            this.radioModulation.Name = "radioModulation";
            this.radioModulation.Size = new Size(0x7c, 0x11);
            this.radioModulation.TabIndex = 0x11;
            this.radioModulation.Text = "Get Modulation Type";
            this.radioModulation.UseVisualStyleBackColor = true;
            this.radioRFBaud.AutoSize = true;
            this.radioRFBaud.Location = new Point(4, 0x8a);
            this.radioRFBaud.Margin = new Padding(2);
            this.radioRFBaud.Name = "radioRFBaud";
            this.radioRFBaud.Size = new Size(0x71, 0x11);
            this.radioRFBaud.TabIndex = 0x10;
            this.radioRFBaud.Text = "Get RF Baud Rate";
            this.radioRFBaud.UseVisualStyleBackColor = true;
            this.radioRSSI.AutoSize = true;
            this.radioRSSI.Location = new Point(4, 0x76);
            this.radioRSSI.Margin = new Padding(2);
            this.radioRSSI.Name = "radioRSSI";
            this.radioRSSI.Size = new Size(70, 0x11);
            this.radioRSSI.TabIndex = 15;
            this.radioRSSI.Text = "Get RSSI";
            this.radioRSSI.UseVisualStyleBackColor = true;
            this.radioGetSiteCode.AutoSize = true;
            this.radioGetSiteCode.Location = new Point(4, 0x62);
            this.radioGetSiteCode.Margin = new Padding(2);
            this.radioGetSiteCode.Name = "radioGetSiteCode";
            this.radioGetSiteCode.Size = new Size(0x5b, 0x11);
            this.radioGetSiteCode.TabIndex = 14;
            this.radioGetSiteCode.Text = "Get Site Code";
            this.radioGetSiteCode.UseVisualStyleBackColor = true;
            this.radioGetReceiverName.AutoSize = true;
            this.radioGetReceiverName.Location = new Point(4, 0x4e);
            this.radioGetReceiverName.Margin = new Padding(2);
            this.radioGetReceiverName.Name = "radioGetReceiverName";
            this.radioGetReceiverName.Size = new Size(0x77, 0x11);
            this.radioGetReceiverName.TabIndex = 13;
            this.radioGetReceiverName.Text = "Get Receiver Name";
            this.radioGetReceiverName.UseVisualStyleBackColor = true;
            this.radioGetPowerControl.AutoSize = true;
            this.radioGetPowerControl.Location = new Point(4, 0x9e);
            this.radioGetPowerControl.Margin = new Padding(2);
            this.radioGetPowerControl.Name = "radioGetPowerControl";
            this.radioGetPowerControl.Size = new Size(0x5b, 0x11);
            this.radioGetPowerControl.TabIndex = 12;
            this.radioGetPowerControl.Text = "Power Control";
            this.radioGetPowerControl.UseVisualStyleBackColor = true;
            this.radioGetReceiverStatus.AutoSize = true;
            this.radioGetReceiverStatus.Location = new Point(4, 0x3a);
            this.radioGetReceiverStatus.Margin = new Padding(2);
            this.radioGetReceiverStatus.Name = "radioGetReceiverStatus";
            this.radioGetReceiverStatus.Size = new Size(0x4b, 0x11);
            this.radioGetReceiverStatus.TabIndex = 11;
            this.radioGetReceiverStatus.Text = "Get Status";
            this.radioGetReceiverStatus.UseVisualStyleBackColor = true;
            this.radioCompleteStatus.AutoSize = true;
            this.radioCompleteStatus.Checked = true;
            this.radioCompleteStatus.Location = new Point(4, 0x12);
            this.radioCompleteStatus.Margin = new Padding(2);
            this.radioCompleteStatus.Name = "radioCompleteStatus";
            this.radioCompleteStatus.Size = new Size(0x66, 0x11);
            this.radioCompleteStatus.TabIndex = 10;
            this.radioCompleteStatus.TabStop = true;
            this.radioCompleteStatus.Text = "Complete Status";
            this.radioCompleteStatus.UseVisualStyleBackColor = true;
            this.radioUploadFirmware.AutoSize = true;
            this.radioUploadFirmware.Location = new Point(4, 0x102);
            this.radioUploadFirmware.Margin = new Padding(2);
            this.radioUploadFirmware.Name = "radioUploadFirmware";
            this.radioUploadFirmware.Size = new Size(0x68, 0x11);
            this.radioUploadFirmware.TabIndex = 9;
            this.radioUploadFirmware.Text = "Upload Firmware";
            this.radioUploadFirmware.UseVisualStyleBackColor = true;
            this.radioGetBootloaderVersion.AutoSize = true;
            this.radioGetBootloaderVersion.Location = new Point(4, 0xb2);
            this.radioGetBootloaderVersion.Margin = new Padding(2);
            this.radioGetBootloaderVersion.Name = "radioGetBootloaderVersion";
            this.radioGetBootloaderVersion.Size = new Size(0x86, 0x11);
            this.radioGetBootloaderVersion.TabIndex = 8;
            this.radioGetBootloaderVersion.Text = "Get Bootloader Version";
            this.radioGetBootloaderVersion.UseVisualStyleBackColor = true;
            this.radioGetFirmwareChecksum.AutoSize = true;
            this.radioGetFirmwareChecksum.Location = new Point(4, 0xc6);
            this.radioGetFirmwareChecksum.Margin = new Padding(2);
            this.radioGetFirmwareChecksum.Name = "radioGetFirmwareChecksum";
            this.radioGetFirmwareChecksum.Size = new Size(140, 0x11);
            this.radioGetFirmwareChecksum.TabIndex = 7;
            this.radioGetFirmwareChecksum.Text = "Get Firmware Checksum";
            this.radioGetFirmwareChecksum.UseVisualStyleBackColor = true;
            this.btnMiscCmd.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnMiscCmd.Location = new Point(60, 0x187);
            this.btnMiscCmd.Margin = new Padding(2);
            this.btnMiscCmd.Name = "btnMiscCmd";
            this.btnMiscCmd.Size = new Size(0x66, 0x18);
            this.btnMiscCmd.TabIndex = 6;
            this.btnMiscCmd.Text = "Execute";
            this.btnMiscCmd.UseVisualStyleBackColor = true;
            this.btnMiscCmd.Click += new EventHandler(this.btnMiscCmd_Click);
            this.radioFlushTagsBuf.AutoSize = true;
            this.radioFlushTagsBuf.Location = new Point(4, 0xee);
            this.radioFlushTagsBuf.Margin = new Padding(2);
            this.radioFlushTagsBuf.Name = "radioFlushTagsBuf";
            this.radioFlushTagsBuf.Size = new Size(0x6c, 0x11);
            this.radioFlushTagsBuf.TabIndex = 5;
            this.radioFlushTagsBuf.Text = "Flush Tags Buffer";
            this.radioFlushTagsBuf.UseVisualStyleBackColor = true;
            this.radioGetNoiseLevel.AutoSize = true;
            this.radioGetNoiseLevel.Location = new Point(4, 0xda);
            this.radioGetNoiseLevel.Margin = new Padding(2);
            this.radioGetNoiseLevel.Name = "radioGetNoiseLevel";
            this.radioGetNoiseLevel.Size = new Size(0x65, 0x11);
            this.radioGetNoiseLevel.TabIndex = 1;
            this.radioGetNoiseLevel.Text = "Get Noise Level";
            this.radioGetNoiseLevel.UseVisualStyleBackColor = true;
            this.radioGetReceiverInfo.AutoSize = true;
            this.radioGetReceiverInfo.Location = new Point(4, 0x26);
            this.radioGetReceiverInfo.Margin = new Padding(2);
            this.radioGetReceiverInfo.Name = "radioGetReceiverInfo";
            this.radioGetReceiverInfo.Size = new Size(0x3f, 0x11);
            this.radioGetReceiverInfo.TabIndex = 0;
            this.radioGetReceiverInfo.Text = "Get Info";
            this.radioGetReceiverInfo.UseVisualStyleBackColor = true;
            this.btnAbort.Enabled = false;
            this.btnAbort.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnAbort.Location = new Point(0x2c0, 0x246);
            this.btnAbort.Margin = new Padding(2);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new Size(0x4f, 0x19);
            this.btnAbort.TabIndex = 14;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new EventHandler(this.btnAbort_Click);
            this.exportListDlg.DefaultExt = "xml";
            this.exportListDlg.Filter = "Receivers list|*.xml";
            this.exportListDlg.Title = "Export receivers list";
            this.importListDlg.DefaultExt = "xml";
            this.importListDlg.Filter = "Receivers list|*.xml";
            this.importListDlg.Title = "Import receivers list";
            this.btnAutoGetTags.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnAutoGetTags.Location = new Point(60, 0x70);
            this.btnAutoGetTags.Margin = new Padding(2);
            this.btnAutoGetTags.Name = "btnAutoGetTags";
            this.btnAutoGetTags.Size = new Size(0x66, 0x18);
            this.btnAutoGetTags.TabIndex = 15;
            this.btnAutoGetTags.Text = "Start";
            this.btnAutoGetTags.UseVisualStyleBackColor = true;
            this.btnAutoGetTags.Click += new EventHandler(this.btnAutoGetTags_Click);
            this.btnClearLog.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnClearLog.Location = new Point(0x2bf, 0x263);
            this.btnClearLog.Margin = new Padding(2);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new Size(0x4f, 0x16);
            this.btnClearLog.TabIndex = 0x10;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new EventHandler(this.btnClearLog_Click);
            this.groupBox7.Controls.Add(this.btnDBSetup);
            this.groupBox7.Controls.Add(this.checkUseDB);
            this.groupBox7.Controls.Add(this.radioAutoGetTagsSingleLine);
            this.groupBox7.Controls.Add(this.radioAutoGetTagsMultipleLines);
            this.groupBox7.Controls.Add(this.btnAutoGetTags);
            this.groupBox7.Location = new Point(0x1bc, 11);
            this.groupBox7.Margin = new Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new Padding(2);
            this.groupBox7.Size = new Size(0xa6, 0x8d);
            this.groupBox7.TabIndex = 0x11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Auto-Get Tags";
            this.btnDBSetup.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnDBSetup.Location = new Point(60, 0x54);
            this.btnDBSetup.Margin = new Padding(2);
            this.btnDBSetup.Name = "btnDBSetup";
            this.btnDBSetup.Size = new Size(0x66, 0x18);
            this.btnDBSetup.TabIndex = 0x11;
            this.btnDBSetup.Text = "DB Setup...";
            this.btnDBSetup.UseVisualStyleBackColor = true;
            this.btnDBSetup.Click += new EventHandler(this.btnDBSetup_Click);
            this.checkUseDB.AutoSize = true;
            this.checkUseDB.Checked = true;
            this.checkUseDB.CheckState = CheckState.Checked;
            this.checkUseDB.Enabled = false;
            this.checkUseDB.Location = new Point(5, 0x3f);
            this.checkUseDB.Margin = new Padding(2);
            this.checkUseDB.Name = "checkUseDB";
            this.checkUseDB.Size = new Size(0x3f, 0x11);
            this.checkUseDB.TabIndex = 0x10;
            this.checkUseDB.Text = "Use DB";
            this.checkUseDB.UseVisualStyleBackColor = true;
            this.radioAutoGetTagsSingleLine.AutoSize = true;
            this.radioAutoGetTagsSingleLine.Location = new Point(5, 40);
            this.radioAutoGetTagsSingleLine.Margin = new Padding(2);
            this.radioAutoGetTagsSingleLine.Name = "radioAutoGetTagsSingleLine";
            this.radioAutoGetTagsSingleLine.Size = new Size(0x6d, 0x11);
            this.radioAutoGetTagsSingleLine.TabIndex = 1;
            this.radioAutoGetTagsSingleLine.TabStop = true;
            this.radioAutoGetTagsSingleLine.Text = "Single line per tag";
            this.radioAutoGetTagsSingleLine.UseVisualStyleBackColor = true;
            this.radioAutoGetTagsMultipleLines.AutoSize = true;
            this.radioAutoGetTagsMultipleLines.Checked = true;
            this.radioAutoGetTagsMultipleLines.Location = new Point(5, 0x12);
            this.radioAutoGetTagsMultipleLines.Margin = new Padding(2);
            this.radioAutoGetTagsMultipleLines.Name = "radioAutoGetTagsMultipleLines";
            this.radioAutoGetTagsMultipleLines.Size = new Size(0x79, 0x11);
            this.radioAutoGetTagsMultipleLines.TabIndex = 0;
            this.radioAutoGetTagsMultipleLines.TabStop = true;
            this.radioAutoGetTagsMultipleLines.Text = "Multiple lines per tag";
            this.radioAutoGetTagsMultipleLines.UseVisualStyleBackColor = true;
            this.groupBox6.Controls.Add(this.RelayNumber);
            this.groupBox6.Controls.Add(this.btnRelay);
            this.groupBox6.Controls.Add(this.Relay5);
            this.groupBox6.Controls.Add(this.RelayOff);
            this.groupBox6.Controls.Add(this.RelayOn);
            this.groupBox6.Location = new Point(0x266, 0x13f);
            this.groupBox6.Margin = new Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new Padding(2);
            this.groupBox6.Size = new Size(0xa8, 0x6f);
            this.groupBox6.TabIndex = 0x12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Set Relay State";
            this.RelayNumber.FormattingEnabled = true;
            this.RelayNumber.Items.AddRange(new object[] { "Relay 1", "Relay 2", "Relay 3" });
            this.RelayNumber.Location = new Point(5, 0x12);
            this.RelayNumber.Name = "RelayNumber";
            this.RelayNumber.Size = new Size(0x9e, 0x15);
            this.RelayNumber.TabIndex = 8;
            this.btnRelay.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnRelay.Location = new Point(0x3e, 0x53);
            this.btnRelay.Margin = new Padding(2);
            this.btnRelay.Name = "btnRelay";
            this.btnRelay.Size = new Size(0x66, 0x18);
            this.btnRelay.TabIndex = 4;
            this.btnRelay.Text = "Activate Relay";
            this.btnRelay.UseVisualStyleBackColor = true;
            this.btnRelay.Click += new EventHandler(this.button1_Click);
            this.Relay5.AutoSize = true;
            this.Relay5.Location = new Point(4, 0x59);
            this.Relay5.Margin = new Padding(2);
            this.Relay5.Name = "Relay5";
            this.Relay5.Size = new Size(0x36, 0x11);
            this.Relay5.TabIndex = 2;
            this.Relay5.TabStop = true;
            this.Relay5.Text = "5 sec.";
            this.Relay5.UseVisualStyleBackColor = true;
            this.RelayOff.AutoSize = true;
            this.RelayOff.Location = new Point(4, 0x43);
            this.RelayOff.Margin = new Padding(2);
            this.RelayOff.Name = "RelayOff";
            this.RelayOff.Size = new Size(0x27, 0x11);
            this.RelayOff.TabIndex = 1;
            this.RelayOff.TabStop = true;
            this.RelayOff.Text = "Off";
            this.RelayOff.UseVisualStyleBackColor = true;
            this.RelayOn.AutoSize = true;
            this.RelayOn.Checked = true;
            this.RelayOn.Location = new Point(5, 0x2d);
            this.RelayOn.Margin = new Padding(2);
            this.RelayOn.Name = "RelayOn";
            this.RelayOn.Size = new Size(0x27, 0x11);
            this.RelayOn.TabIndex = 0;
            this.RelayOn.TabStop = true;
            this.RelayOn.Text = "On";
            this.RelayOn.UseVisualStyleBackColor = true;
            this.groupBox8.Controls.Add(this.edtUnitName);
            this.groupBox8.Controls.Add(this.btnSetName);
            this.groupBox8.Location = new Point(0x266, 11);
            this.groupBox8.Margin = new Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new Padding(2);
            this.groupBox8.Size = new Size(0xa8, 0x4b);
            this.groupBox8.TabIndex = 0x13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Receiver Name";
            this.edtUnitName.Location = new Point(5, 0x15);
            this.edtUnitName.Name = "edtUnitName";
            this.edtUnitName.Size = new Size(0x9e, 20);
            this.edtUnitName.TabIndex = 5;
            this.btnSetName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSetName.Location = new Point(60, 0x2e);
            this.btnSetName.Margin = new Padding(2);
            this.btnSetName.Name = "btnSetName";
            this.btnSetName.Size = new Size(0x67, 0x18);
            this.btnSetName.TabIndex = 4;
            this.btnSetName.Text = "Set Name";
            this.btnSetName.UseVisualStyleBackColor = true;
            this.btnSetName.Click += new EventHandler(this.button2_Click);
            this.groupBox9.Controls.Add(this.numRSSI);
            this.groupBox9.Controls.Add(this.btnSetRSSIFilter);
            this.groupBox9.Location = new Point(0x266, 0xa7);
            this.groupBox9.Margin = new Padding(2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new Padding(2);
            this.groupBox9.Size = new Size(0xa8, 0x48);
            this.groupBox9.TabIndex = 20;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "RSSI Filter";
            this.numRSSI.Location = new Point(5, 0x12);
            int[] bits = new int[4];
            bits[0] = 0x3ff;
            this.numRSSI.Maximum = new decimal(bits);
            this.numRSSI.Name = "numRSSI";
            this.numRSSI.Size = new Size(0x9e, 20);
            this.numRSSI.TabIndex = 7;
            this.numRSSI.TextAlign = HorizontalAlignment.Right;
            this.btnSetRSSIFilter.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSetRSSIFilter.Location = new Point(60, 0x2b);
            this.btnSetRSSIFilter.Margin = new Padding(2);
            this.btnSetRSSIFilter.Name = "btnSetRSSIFilter";
            this.btnSetRSSIFilter.Size = new Size(0x68, 0x18);
            this.btnSetRSSIFilter.TabIndex = 4;
            this.btnSetRSSIFilter.Text = "Set RSSI Filter";
            this.btnSetRSSIFilter.UseVisualStyleBackColor = true;
            this.btnSetRSSIFilter.Click += new EventHandler(this.btnSetRSSIFilter_Click);
            this.groupBox3.Controls.Add(this.numSiteCode);
            this.groupBox3.Controls.Add(this.btnSetSiteCode);
            this.groupBox3.Location = new Point(0x266, 0x5b);
            this.groupBox3.Margin = new Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new Padding(2);
            this.groupBox3.Size = new Size(0xa8, 0x48);
            this.groupBox3.TabIndex = 0x15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Site Code";
            this.numSiteCode.Location = new Point(5, 0x12);
            int[] numArray2 = new int[4];
            numArray2[0] = 0xff;
            this.numSiteCode.Maximum = new decimal(numArray2);
            this.numSiteCode.Name = "numSiteCode";
            this.numSiteCode.Size = new Size(0x9e, 20);
            this.numSiteCode.TabIndex = 7;
            this.numSiteCode.TextAlign = HorizontalAlignment.Right;
            this.btnSetSiteCode.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSetSiteCode.Location = new Point(60, 0x2b);
            this.btnSetSiteCode.Margin = new Padding(2);
            this.btnSetSiteCode.Name = "btnSetSiteCode";
            this.btnSetSiteCode.Size = new Size(0x67, 0x18);
            this.btnSetSiteCode.TabIndex = 4;
            this.btnSetSiteCode.Text = "Set Site Code";
            this.btnSetSiteCode.UseVisualStyleBackColor = true;
            this.btnSetSiteCode.Click += new EventHandler(this.btnSetSiteCode_Click);
            this.groupBox4.Controls.Add(this.cmbRFBaud);
            this.groupBox4.Controls.Add(this.btnSetRFBaud);
            this.groupBox4.Location = new Point(0x266, 0xf3);
            this.groupBox4.Margin = new Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new Padding(2);
            this.groupBox4.Size = new Size(0xa8, 0x48);
            this.groupBox4.TabIndex = 0x16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "RF Baud Rate";
            this.cmbRFBaud.FormattingEnabled = true;
            this.cmbRFBaud.Items.AddRange(new object[] { "9600", "19200", "28800", "38400", "57600", "115200" });
            this.cmbRFBaud.Location = new Point(5, 0x12);
            this.cmbRFBaud.Name = "cmbRFBaud";
            this.cmbRFBaud.Size = new Size(0x9e, 0x15);
            this.cmbRFBaud.TabIndex = 9;
            this.btnSetRFBaud.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnSetRFBaud.Location = new Point(60, 0x2b);
            this.btnSetRFBaud.Margin = new Padding(2);
            this.btnSetRFBaud.Name = "btnSetRFBaud";
            this.btnSetRFBaud.Size = new Size(0x67, 0x18);
            this.btnSetRFBaud.TabIndex = 4;
            this.btnSetRFBaud.Text = "Set RF Baud";
            this.btnSetRFBaud.UseVisualStyleBackColor = true;
            this.btnSetRFBaud.Click += new EventHandler(this.btnSetRFBaud_Click);
            this.groupBox10.Controls.Add(this.radioGain20);
            this.groupBox10.Controls.Add(this.radioGain14);
            this.groupBox10.Controls.Add(this.btnGainSet);
            this.groupBox10.Controls.Add(this.radioGain6);
            this.groupBox10.Controls.Add(this.radioGainMax);
            this.groupBox10.Location = new Point(0x1bc, 0xfd);
            this.groupBox10.Margin = new Padding(2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new Padding(2);
            this.groupBox10.Size = new Size(0xa6, 0x5d);
            this.groupBox10.TabIndex = 0x17;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Gain Control ";
            this.radioGain20.AutoSize = true;
            this.radioGain20.Location = new Point(5, 0x45);
            this.radioGain20.Margin = new Padding(2);
            this.radioGain20.Name = "radioGain20";
            this.radioGain20.Size = new Size(0x40, 0x11);
            this.radioGain20.TabIndex = 4;
            this.radioGain20.Text = "-20 dBm";
            this.radioGain20.UseVisualStyleBackColor = true;
            this.radioGain14.AutoSize = true;
            this.radioGain14.Location = new Point(5, 0x33);
            this.radioGain14.Margin = new Padding(2);
            this.radioGain14.Name = "radioGain14";
            this.radioGain14.Size = new Size(0x40, 0x11);
            this.radioGain14.TabIndex = 3;
            this.radioGain14.Text = "-14 dBm";
            this.radioGain14.UseVisualStyleBackColor = true;
            this.btnGainSet.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnGainSet.Location = new Point(0x4d, 0x41);
            this.btnGainSet.Margin = new Padding(2);
            this.btnGainSet.Name = "btnGainSet";
            this.btnGainSet.Size = new Size(0x55, 0x18);
            this.btnGainSet.TabIndex = 2;
            this.btnGainSet.Text = "Set Gain";
            this.btnGainSet.UseVisualStyleBackColor = true;
            this.btnGainSet.Click += new EventHandler(this.btnGainSet_Click);
            this.radioGain6.AutoSize = true;
            this.radioGain6.Location = new Point(5, 0x22);
            this.radioGain6.Margin = new Padding(2);
            this.radioGain6.Name = "radioGain6";
            this.radioGain6.Size = new Size(0x72, 0x11);
            this.radioGain6.TabIndex = 1;
            this.radioGain6.Text = "-6 dBm / Low Gain";
            this.radioGain6.UseVisualStyleBackColor = true;
            this.radioGainMax.AutoSize = true;
            this.radioGainMax.Checked = true;
            this.radioGainMax.Location = new Point(5, 0x11);
            this.radioGainMax.Margin = new Padding(2);
            this.radioGainMax.Name = "radioGainMax";
            this.radioGainMax.Size = new Size(0x67, 0x11);
            this.radioGainMax.TabIndex = 0;
            this.radioGainMax.TabStop = true;
            this.radioGainMax.Text = "Max / High Gain";
            this.radioGainMax.UseVisualStyleBackColor = true;
            this.groupBox11.Controls.Add(this.btnReceiverModulationType);
            this.groupBox11.Controls.Add(this.radioFSK);
            this.groupBox11.Controls.Add(this.radioASK);
            this.groupBox11.Location = new Point(0x1bc, 350);
            this.groupBox11.Margin = new Padding(2);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new Padding(2);
            this.groupBox11.Size = new Size(0xa6, 80);
            this.groupBox11.TabIndex = 0x18;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Receiver Modulation Type";
            this.btnReceiverModulationType.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnReceiverModulationType.Location = new Point(60, 0x34);
            this.btnReceiverModulationType.Margin = new Padding(2);
            this.btnReceiverModulationType.Name = "btnReceiverModulationType";
            this.btnReceiverModulationType.Size = new Size(0x66, 0x18);
            this.btnReceiverModulationType.TabIndex = 2;
            this.btnReceiverModulationType.Text = "Set Modulation";
            this.btnReceiverModulationType.UseVisualStyleBackColor = true;
            this.btnReceiverModulationType.Click += new EventHandler(this.btnReceiverModulationType_Click);
            this.radioFSK.AutoSize = true;
            this.radioFSK.Location = new Point(5, 0x29);
            this.radioFSK.Margin = new Padding(2);
            this.radioFSK.Name = "radioFSK";
            this.radioFSK.Size = new Size(0x2d, 0x11);
            this.radioFSK.TabIndex = 1;
            this.radioFSK.Text = "FSK";
            this.radioFSK.UseVisualStyleBackColor = true;
            this.radioASK.AutoSize = true;
            this.radioASK.Checked = true;
            this.radioASK.Location = new Point(5, 0x12);
            this.radioASK.Margin = new Padding(2);
            this.radioASK.Name = "radioASK";
            this.radioASK.Size = new Size(0x2e, 0x11);
            this.radioASK.TabIndex = 0;
            this.radioASK.TabStop = true;
            this.radioASK.Text = "ASK";
            this.radioASK.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x312, 0x293);
            base.Controls.Add(this.groupBox11);
            base.Controls.Add(this.groupBox10);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox9);
            base.Controls.Add(this.groupBox8);
            base.Controls.Add(this.groupBox6);
            base.Controls.Add(this.groupBox7);
            base.Controls.Add(this.btnClearLog);
            base.Controls.Add(this.btnAbort);
            base.Controls.Add(this.groupBox5);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.txtLog);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.statusStrip1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Margin = new Padding(2);
            base.MaximizeBox = false;
            base.Name = "MainWin";
            this.Text = "VUANCE PureRF API Ver. 2.1.2.0";
            base.Load += new EventHandler(this.MainWin_Load);
            base.Shown += new EventHandler(this.MainWin_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.numRSSI.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.numSiteCode.EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listReceivers_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string text = this.listReceivers.Items[e.Item].SubItems[0].Text;
            string label = e.Label;
            if (label != null)
            {
                ReceiversManager.RetVal val = this.mReceiversManager.RenameReceiver(text, label);
                switch (val)
                {
                    case ReceiversManager.RetVal.SUCCESS:
                        this.Log("Renamed receiver {0} to {1}.", new object[] { text, label });
                        return;

                    case ReceiversManager.RetVal.NAME_IN_USE:
                        MessageBox.Show("New name already in use", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.CancelEdit = true;
                        return;
                }
                MessageBox.Show("Error renaming receiver: " + val.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.CancelEdit = true;
            }
        }

        public void Log(string logMsg, params object[] args)
        {
            this.txtLog.AppendText(string.Format(logMsg + Environment.NewLine, args));
        }

        private void MainWin_Load(object sender, EventArgs e)
        {
            this.exportListDlg.InitialDirectory = Application.ExecutablePath;
            this.importListDlg.InitialDirectory = Application.ExecutablePath;
            this.RelayNumber.SelectedIndex = 0;
        }

        private void MainWin_Shown(object sender, EventArgs e)
        {
            this.StartWizard();
            if (this.listReceivers.Items.Count > 0)
            {
                foreach (ListViewItem item in this.listReceivers.Items)
                {
                    item.Checked = true;
                }
                if (this.mAutoGetStatus)
                {
                    this.StartRequest(ReceiversManager.RequestType.GET_ALL_RECEIVER_INFO, new object[0]);
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ReceiversManagerEvent(ReceiversManager m, ReceiversManager.ProgressEvent e)
        {
            string logMsg = string.Format("ReceiversManager event {0}: {1}", e.EventID.ToString(), e.EventStr);
            switch (e.EventID)
            {
                case ReceiversManager.ProgressEvent.ID.STARTED:
                    this.lblStatus.Text = "Request started.";
                    this.btnAbort.Enabled = true;
                    this.DisableAllButtons();
                    break;

                case ReceiversManager.ProgressEvent.ID.COMPLETED:
                    this.statusProgBar.Value = 0;
                    this.lblStatus.Text = "Ready.";
                    this.btnAbort.Enabled = false;
                    this.EnableAllButtons();
                    this.ShowResultsWin(e.RequestType);
                    break;

                case ReceiversManager.ProgressEvent.ID.PROGRESS_UPDATE:
                    this.lblStatus.Text = e.EventStr;
                    this.statusProgBar.Value = (int) e.EventArgs["PERCENT_COMPLETE"];
                    break;
            }
            this.Log(logMsg, new object[0]);
        }

        public void ShowResultsWin(ReceiversManager.RequestType Request)
        {
            ReceiversManager.ResultSet set;
            ReceiversManager.RetVal allResults = this.mReceiversManager.GetAllResults(out set);
            this.Log("Getting all results: " + allResults.ToString(), new object[0]);
            if (allResults != ReceiversManager.RetVal.SUCCESS)
            {
                MessageBox.Show("Unable to get results: " + allResults.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                new ResultsWin(Request, set).ShowDialog();
            }
        }

        public void StartRequest(ReceiversManager.RequestType RequestType, params object[] args)
        {
            if (!this.mReceiversManager.IsReady)
            {
                MessageBox.Show("A request is still executing. Please wait for it to terminate", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string[] receiversWorkSet = this.GetReceiversWorkSet();
                if (receiversWorkSet.Length == 0)
                {
                    MessageBox.Show("Please select at least one receiver.", "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    ReceiversManager.RetVal val = this.mReceiversManager.StartRequest(receiversWorkSet, RequestType, args);
                    this.Log("StartRequest: " + val.ToString(), new object[0]);
                    if (val != ReceiversManager.RetVal.SUCCESS)
                    {
                        MessageBox.Show("Unable to start request: " + val.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        this.statusProgBar.Value = 0;
                        this.lblStatus.Text = "Starting...";
                    }
                }
            }
        }

        public void StartWizard()
        {
            StartWizard_Intro intro = new StartWizard_Intro();
            if (intro.ShowDialog() != DialogResult.Cancel)
            {
                this.mAutoGetStatus = intro.checkAutoGetStatus.Checked;
                if (intro.radioAutodiscover.Checked)
                {
                    this.StartWizard_AutoDiscover(null);
                }
                else if (intro.radioLoadSettings.Checked && !this.ImportSettings())
                {
                    this.StartWizard();
                }
            }
        }

        public void StartWizard_AutoDiscover(StartWizard_DiscoverLoops.WinState state)
        {
            StartWizard_DiscoverLoops loops = new StartWizard_DiscoverLoops(state);
            DialogResult result = loops.ShowDialog();
            switch (result)
            {
                case DialogResult.No:
                    this.StartWizard();
                    return;

                case DialogResult.OK:
                {
                    StartWizard_DiscoverReceivers receivers = new StartWizard_DiscoverReceivers(loops.m_ReceiversManager);
                    result = receivers.ShowDialog();
                    if (result == DialogResult.No)
                    {
                        this.StartWizard_AutoDiscover(loops.State);
                        return;
                    }
                    if (result == DialogResult.OK)
                    {
                        this.mReceiversManager = receivers.ReceiversManager;
                        this.mReceiversManager.SetEventCallback(new ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
                        foreach (ReceiversManager.ManagedReceiver receiver in this.mReceiversManager.ReceiversList)
                        {
                            ListViewItem item = new ListViewItem(new string[] { receiver.Name, receiver.UnitID.ToString(), receiver.Loop.Name });
                            this.listReceivers.Items.Add(item);
                        }
                        this.Log(string.Format("{0} receivers on {1} loops were added by the wizard.", this.listReceivers.Items.Count, this.mReceiversManager.LoopsList.Count), new object[0]);
                    }
                    break;
                }
            }
        }
    }
}

