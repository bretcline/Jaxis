namespace ReceiverApp
{
    using PureRF;
    using ReceiverApp.Properties;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class FirmwareWorkerWin : Form
    {
        private Button btnClose;
        private ColumnHeader columnReceiver;
        private ColumnHeader columnStatus;
        private IContainer components;
        private ToolStripStatusLabel lblStatus;
        private ListView listReceivers;
        private ArrayList mBootloaderReceivers;
        private ReceiverApp.Properties.Settings mDefSettings;
        private byte[] mFirmware;
        private int mNumLoops;
        private int mPageDelay;
        private double mPercentComplete;
        private string[] mReceivers;
        private ReceiversManager mReceiversManager;
        private int mResendCount;
        private int mResendDelay;
        private State mState;
        private ToolStripProgressBar statusProgBar;
        private StatusStrip statusStrip1;
        private TextBox txtLog;

        public FirmwareWorkerWin(ReceiversManager _ReceiversManager, string[] receivers, int resendCount, int resendDelay, int pageDelay, byte[] firmware)
        {
            this.InitializeComponent();
            this.mDefSettings = ReceiverApp.Properties.Settings.Default;
            this.mReceiversManager = _ReceiversManager;
            this.mReceivers = receivers;
            this.mReceivers = receivers;
            this.mResendCount = resendCount;
            this.mResendDelay = resendDelay;
            this.mPageDelay = pageDelay;
            this.mFirmware = firmware;
            this.mState = State.INVALID_STATE;
            this.mBootloaderReceivers = new ArrayList();
            this.mReceiversManager.SetEventCallback(new ReceiversManager.EventCallback(this.ReceiversManagerEvent), this);
            for (int i = 0; i < this.mReceivers.Length; i++)
            {
                ListViewItem item = new ListViewItem(new string[] { this.mReceivers[i], "Firmware upgrade is starting..." });
                this.listReceivers.Items.Add(item);
            }
            this.StartFirmwareUpgrade();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void EnterFirmwareMode()
        {
            this.mReceivers = new string[this.mBootloaderReceivers.Count];
            for (int i = 0; i < this.mReceivers.Length; i++)
            {
                this.mReceivers[i] = this.mBootloaderReceivers[i] as string;
            }
            this.mState = State.ENTERING_FIRMWARE;
            ReceiversManager.RetVal val = this.mReceiversManager.SetModeFirmware(this.mReceivers);
            if (val != ReceiversManager.RetVal.SUCCESS)
            {
                MessageBox.Show("Unable to enter firmware mode: " + val.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                base.Dispose();
            }
            else
            {
                this.Log("Entering firmware mode...", new object[0]);
            }
        }

        private void FirmwareWorkerWin_Load(object sender, EventArgs e)
        {
        }

        private void HandleResultUpdate(ReceiversManager.ProgressEvent e)
        {
            ReceiverRetVal val = (ReceiverRetVal) e.EventArgs["RETVAL"];
            ReceiversManager.ManagedReceiver receiver = e.EventArgs["RECEIVER"] as ReceiversManager.ManagedReceiver;
            object obj2 = e.EventArgs["RESULT"];
            switch (this.mState)
            {
                case State.INVALID_STATE:
                    if (val != ReceiverRetVal.SUCCESS)
                    {
                        this.SetReceiverStatusMsg(this.mDefSettings.Color_Error, receiver.Name, "Unable to enter bootloader mode: {0}", new object[] { val.ToString() });
                        return;
                    }
                    this.SetReceiverStatusMsg(this.mDefSettings.Color_Success, receiver.Name, "Entered bootloader mode!", new object[0]);
                    this.mBootloaderReceivers.Add(receiver.Name);
                    return;

                case State.UPLOADING_FIRMWARE:
                    break;

                case State.VERIFY_FIRMWARE:
                {
                    if (val != ReceiverRetVal.FLASH_OK)
                    {
                        this.SetReceiverStatusMsg(this.mDefSettings.Color_Error, receiver.Name, "Firmware upgrade failed: {0}", new object[] { val.ToString() });
                        this.Log("Receiver {0}: Firmware upgrade failed: {1}", new object[] { receiver.Name, val.ToString() });
                        break;
                    }
                    ushort num = (ushort) obj2;
                    this.SetReceiverStatusMsg(this.mDefSettings.Color_Success, receiver.Name, "Firmware upgraded. New checksum: 0x{0}", new object[] { num.ToString("X") });
                    this.Log("Receiver {0}: Firmware upgraded. New checksum: 0x{1}", new object[] { receiver.Name, num.ToString("X") });
                    return;
                }
                case State.ENTERING_FIRMWARE:
                    if (val != ReceiverRetVal.SUCCESS)
                    {
                        this.SetReceiverStatusMsg(this.mDefSettings.Color_Error, receiver.Name, "Unable to enter firmware mode: {0}", new object[] { val.ToString() });
                        return;
                    }
                    this.SetReceiverStatusMsg(this.mDefSettings.Color_Success, receiver.Name, "Entered firmware mode!", new object[0]);
                    return;

                default:
                    return;
            }
        }

        private void InitializeComponent()
        {
            this.txtLog = new TextBox();
            this.btnClose = new Button();
            this.statusStrip1 = new StatusStrip();
            this.statusProgBar = new ToolStripProgressBar();
            this.lblStatus = new ToolStripStatusLabel();
            this.listReceivers = new ListView();
            this.columnReceiver = new ColumnHeader();
            this.columnStatus = new ColumnHeader();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.txtLog.BackColor = Color.Black;
            this.txtLog.ForeColor = Color.Lime;
            this.txtLog.Location = new Point(12, 0xd5);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Both;
            this.txtLog.Size = new Size(0x1c9, 0x72);
            this.txtLog.TabIndex = 3;
            this.txtLog.WordWrap = false;
            this.btnClose.DialogResult = DialogResult.OK;
            this.btnClose.Location = new Point(0x18a, 0xb8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.statusProgBar, this.lblStatus });
            this.statusStrip1.Location = new Point(0, 330);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x1e1, 0x17);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            this.statusProgBar.Margin = new Padding(3, 3, 1, 3);
            this.statusProgBar.Name = "statusProgBar";
            this.statusProgBar.Size = new Size(100, 0x11);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(0x36, 0x12);
            this.lblStatus.Text = "Ready.";
            this.listReceivers.Columns.AddRange(new ColumnHeader[] { this.columnReceiver, this.columnStatus });
            this.listReceivers.Location = new Point(12, 13);
            this.listReceivers.Name = "listReceivers";
            this.listReceivers.Size = new Size(0x1c9, 0xa5);
            this.listReceivers.TabIndex = 6;
            this.listReceivers.UseCompatibleStateImageBehavior = false;
            this.listReceivers.View = View.Details;
            this.columnReceiver.Text = "Receiver";
            this.columnReceiver.Width = 150;
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 290;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1e1, 0x161);
            base.Controls.Add(this.listReceivers);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.txtLog);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FirmwareWorkerWin";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver - Firmware Upgrade";
            base.Load += new EventHandler(this.FirmwareWorkerWin_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void Log(string logMsg, params object[] args)
        {
            this.txtLog.AppendText(string.Format(logMsg, args) + Environment.NewLine);
        }

        private void ReceiversManagerEvent(ReceiversManager m, ReceiversManager.ProgressEvent e)
        {
            switch (e.EventID)
            {
                case ReceiversManager.ProgressEvent.ID.STARTED:
                    this.statusProgBar.Value = 0;
                    switch (this.mState)
                    {
                        case State.INVALID_STATE:
                            this.btnClose.Enabled = false;
                            this.SetAllReceiversStatusMsg(this.mDefSettings.Color_Default, "Entering boot-loader mode...", new object[0]);
                            return;

                        case State.UPLOADING_FIRMWARE:
                            this.SetAllReceiversStatusMsg(this.mDefSettings.Color_Default, "Uploading firmware...", new object[0]);
                            this.mNumLoops = (int) e.EventArgs["NUM_LOOPS"];
                            this.mPercentComplete = 0.0;
                            this.Log("Number of loops: {0}", new object[] { this.mNumLoops });
                            this.lblStatus.Text = "Uploading firmware: 0%";
                            return;

                        case State.VERIFY_FIRMWARE:
                            this.SetAllReceiversStatusMsg(this.mDefSettings.Color_Default, "Verifying firmware...", new object[0]);
                            this.lblStatus.Text = "Verifying firmware...";
                            return;

                        case State.ENTERING_FIRMWARE:
                            this.SetAllReceiversStatusMsg(this.mDefSettings.Color_Default, "Entering firmware mode...", new object[0]);
                            return;
                    }
                    return;

                case ReceiversManager.ProgressEvent.ID.COMPLETED:
                    this.RequestCompleted(e);
                    return;

                case ReceiversManager.ProgressEvent.ID.PROGRESS_UPDATE:
                    switch (this.mState)
                    {
                        case State.INVALID_STATE:
                            this.lblStatus.Text = "Entering bootloader mode";
                            this.lblStatus.Text = this.lblStatus.Text + ": " + e.EventStr;
                            this.statusProgBar.Value = (int) e.EventArgs["PERCENT_COMPLETE"];
                            return;

                        case State.ENTERING_FIRMWARE:
                            this.lblStatus.Text = "Entering firmware mode";
                            this.lblStatus.Text = this.lblStatus.Text + ": " + e.EventStr;
                            this.statusProgBar.Value = (int) e.EventArgs["PERCENT_COMPLETE"];
                            return;
                    }
                    return;

                case ReceiversManager.ProgressEvent.ID.BEFORE_REQUEST_NOTIFICATION:
                case ReceiversManager.ProgressEvent.ID.AFTER_REQUEST_NOTIFICATION:
                    break;

                case ReceiversManager.ProgressEvent.ID.LOG_MSG:
                    this.Log(e.EventStr, new object[0]);
                    return;

                case ReceiversManager.ProgressEvent.ID.RESULT_UPDATE:
                    this.HandleResultUpdate(e);
                    return;

                case ReceiversManager.ProgressEvent.ID.DOWNLOAD_PROGRESS:
                {
                    double num = (1.0 / ((double) ((int) e.EventArgs["TOTAL_PACKETS"]))) * 100.0;
                    this.mPercentComplete += num / ((double) this.mNumLoops);
                    this.statusProgBar.Value = (int) this.mPercentComplete;
                    this.lblStatus.Text = string.Format("Uploading firmware: {0}%", this.statusProgBar.Value);
                    this.Log("Loop {0}: {1}/{2} ({3}%)", new object[] { e.EventArgs["LOOP"], e.EventArgs["SENT_PACKETS"], e.EventArgs["TOTAL_PACKETS"], e.EventArgs["PERCENT_COMPLETE"] });
                    break;
                }
                default:
                    return;
            }
        }

        public void RequestCompleted(ReceiversManager.ProgressEvent e)
        {
            ReceiversManager.ResultSet set;
            bool flag = true;
            this.mReceiversManager.WaitForRequestCompletion();
            ReceiversManager.RetVal allResults = this.mReceiversManager.GetAllResults(out set);
            if (allResults != ReceiversManager.RetVal.SUCCESS)
            {
                MessageBox.Show("Unable to get results: " + allResults.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                switch (this.mState)
                {
                    case State.INVALID_STATE:
                        foreach (ReceiversManager.ReceiverResult result2 in set.Values)
                        {
                            if (result2.RetVal != ReceiverRetVal.SUCCESS)
                            {
                                this.Log("Receiver {0} didn't enter boot-loader mode: {1}", new object[] { result2.Receiver.Name, result2.RetVal.ToString() });
                                flag = false;
                            }
                            else
                            {
                                this.Log("Receiver {0} successfully entered boot-loader mode", new object[] { result2.Receiver.Name });
                                result2.Receiver.Loop.SetPortBaudrate(0xe100);
                            }
                        }
                        this.mReceivers = new string[this.mBootloaderReceivers.Count];
                        for (int i = 0; i < this.mReceivers.Length; i++)
                        {
                            this.mReceivers[i] = this.mBootloaderReceivers[i] as string;
                        }
                        if (this.mReceivers.Length == 0)
                        {
                            this.Log("No receivers entered bootloader mode!", new object[0]);
                            this.lblStatus.Text = "No receivers entered bootloader mode";
                            this.statusProgBar.Value = 0;
                            this.btnClose.Enabled = true;
                            return;
                        }
                        Thread.Sleep(0x3e8);
                        this.mState = State.UPLOADING_FIRMWARE;
                        this.mReceiversManager.Download(this.mReceivers, this.mFirmware, this.mResendCount, this.mResendDelay, this.mPageDelay);
                        return;

                    case State.UPLOADING_FIRMWARE:
                        this.Log("Finished uploading new firmware. Verifying...", new object[0]);
                        this.mState = State.VERIFY_FIRMWARE;
                        this.mReceiversManager.FirmwareState(this.mReceivers);
                        return;

                    case State.VERIFY_FIRMWARE:
                        this.Log("Finished verifying firmware.", new object[0]);
                        foreach (ReceiversManager.ReceiverResult result4 in set.Values)
                        {
                            if (result4.RetVal != ReceiverRetVal.FLASH_OK)
                            {
                                if (MessageBox.Show("At least one receiver did not upgrade correctly." + Environment.NewLine + "Would you like to re-upload firmware?", "PureRF Receiver", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                                {
                                    flag = false;
                                    break;
                                }
                                this.ReuploadFirmware(set);
                                return;
                            }
                        }
                        break;

                    case State.ENTERING_FIRMWARE:
                        foreach (ReceiversManager.ReceiverResult result3 in set.Values)
                        {
                            if (result3.RetVal != ReceiverRetVal.SUCCESS)
                            {
                                this.Log("Receiver {0} didn't enter firmware mode: {1}", new object[] { result3.Receiver.Name, result3.RetVal.ToString() });
                            }
                            else
                            {
                                this.Log("Receiver {0} succesfully entered firmware mode.", new object[] { result3.Receiver.Name });
                                result3.Receiver.Loop.SetPortBaudrate(0xe100);
                            }
                        }
                        Thread.Sleep(0x3e8);
                        this.btnClose.Enabled = true;
                        this.statusProgBar.Value = 0;
                        this.lblStatus.Text = "Firmware upgrade completed.";
                        return;

                    default:
                        return;
                }
                if (!flag && (MessageBox.Show("At least one receiver did not upgrade correctly." + Environment.NewLine + "Enter firmware mode on failed receivers as well?", "PureRF Receiver", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No))
                {
                    foreach (ReceiversManager.ReceiverResult result5 in set.Values)
                    {
                        if (result5.RetVal != ReceiverRetVal.FLASH_OK)
                        {
                            this.mBootloaderReceivers.Remove(result5.Receiver.Name);
                        }
                    }
                }
                this.mState = State.ENTERING_FIRMWARE;
                this.EnterFirmwareMode();
            }
        }

        private void ReuploadFirmware(ReceiversManager.ResultSet Results)
        {
            ArrayList list = new ArrayList();
            foreach (ReceiversManager.ReceiverResult result in Results.Values)
            {
                if (result.RetVal != ReceiverRetVal.FLASH_OK)
                {
                    list.Add(result.Receiver.Name);
                }
            }
            this.mReceivers = new string[list.Count];
            for (int i = 0; i < this.mReceivers.Length; i++)
            {
                this.mReceivers[i] = list[i] as string;
            }
            this.Log("Reuploading firmware to {0} receivers...", new object[] { this.mReceivers.Length });
            this.mState = State.UPLOADING_FIRMWARE;
            this.mReceiversManager.Download(this.mReceivers, this.mFirmware, this.mResendCount, this.mResendDelay, this.mPageDelay);
        }

        private void SetAllReceiversStatusMsg(Color c, string statusMsg, params object[] args)
        {
            for (int i = 0; i < this.mReceivers.Length; i++)
            {
                this.SetReceiverStatusMsg(c, this.mReceivers[i], statusMsg, args);
            }
        }

        private void SetReceiverStatusMsg(Color c, string receiverName, string statusMsg, params object[] args)
        {
            for (int i = 0; i < this.listReceivers.Items.Count; i++)
            {
                if (!(this.listReceivers.Items[i].SubItems[0].Text != receiverName))
                {
                    this.listReceivers.Items[i].ForeColor = c;
                    this.listReceivers.Items[i].SubItems[1].Text = string.Format(statusMsg, args);
                    return;
                }
            }
        }

        private void StartFirmwareUpgrade()
        {
            this.mState = State.INVALID_STATE;
            ReceiversManager.RetVal val = this.mReceiversManager.SetModeBootloader(this.mReceivers);
            if (val != ReceiversManager.RetVal.SUCCESS)
            {
                MessageBox.Show("Unable to enter bootloader mode: " + val.ToString(), "PureRF Receiver", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                base.Dispose();
            }
        }

        private enum State
        {
            ENTERING_BOOTLOADER = 0,
            ENTERING_FIRMWARE = 3,
            INVALID_STATE = 0,
            UPLOADING_FIRMWARE = 1,
            VERIFY_FIRMWARE = 2
        }
    }
}

