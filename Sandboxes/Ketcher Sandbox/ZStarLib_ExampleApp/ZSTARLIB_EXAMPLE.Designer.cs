namespace ZStarLib_ExampleApp
{
    partial class ZSTARLIB_EXAMPLE
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZSTARLIB_EXAMPLE));
            this.gb_ComPort = new System.Windows.Forms.GroupBox();
            this.bt_ClosePort = new System.Windows.Forms.Button();
            this.bt_OpenPort = new System.Windows.Forms.Button();
            this.bt_RefreshComPort = new System.Windows.Forms.Button();
            this.cb_ComPortList = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.gb_Sensor = new System.Windows.Forms.GroupBox();
            this.lb_Status = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_AxisY = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTimeDisplay = new System.Windows.Forms.Label();
            this.lblCurrentAngle = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.gb_ComPort.SuspendLayout();
            this.gb_Sensor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_ComPort
            // 
            this.gb_ComPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gb_ComPort.Controls.Add(this.bt_ClosePort);
            this.gb_ComPort.Controls.Add(this.bt_OpenPort);
            this.gb_ComPort.Controls.Add(this.bt_RefreshComPort);
            this.gb_ComPort.Controls.Add(this.cb_ComPortList);
            this.gb_ComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gb_ComPort.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gb_ComPort.Location = new System.Drawing.Point(0, 214);
            this.gb_ComPort.Name = "gb_ComPort";
            this.gb_ComPort.Size = new System.Drawing.Size(314, 70);
            this.gb_ComPort.TabIndex = 1;
            this.gb_ComPort.TabStop = false;
            this.gb_ComPort.Text = "ComPort";
            // 
            // bt_ClosePort
            // 
            this.bt_ClosePort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_ClosePort.BackColor = System.Drawing.Color.Gray;
            this.bt_ClosePort.Enabled = false;
            this.bt_ClosePort.Location = new System.Drawing.Point(138, 14);
            this.bt_ClosePort.Name = "bt_ClosePort";
            this.bt_ClosePort.Size = new System.Drawing.Size(60, 23);
            this.bt_ClosePort.TabIndex = 3;
            this.bt_ClosePort.Text = "Close";
            this.bt_ClosePort.UseVisualStyleBackColor = false;
            this.bt_ClosePort.Click += new System.EventHandler(this.bt_ClosePort_Click);
            // 
            // bt_OpenPort
            // 
            this.bt_OpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_OpenPort.BackColor = System.Drawing.Color.Gray;
            this.bt_OpenPort.Location = new System.Drawing.Point(72, 14);
            this.bt_OpenPort.Name = "bt_OpenPort";
            this.bt_OpenPort.Size = new System.Drawing.Size(60, 23);
            this.bt_OpenPort.TabIndex = 2;
            this.bt_OpenPort.Text = "Open";
            this.bt_OpenPort.UseVisualStyleBackColor = false;
            this.bt_OpenPort.Click += new System.EventHandler(this.bt_OpenPort_Click);
            // 
            // bt_RefreshComPort
            // 
            this.bt_RefreshComPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_RefreshComPort.BackColor = System.Drawing.Color.Gray;
            this.bt_RefreshComPort.Location = new System.Drawing.Point(6, 14);
            this.bt_RefreshComPort.Name = "bt_RefreshComPort";
            this.bt_RefreshComPort.Size = new System.Drawing.Size(60, 23);
            this.bt_RefreshComPort.TabIndex = 1;
            this.bt_RefreshComPort.Text = "Refresh List";
            this.bt_RefreshComPort.UseVisualStyleBackColor = false;
            this.bt_RefreshComPort.Click += new System.EventHandler(this.bt_RefreshComPort_Click);
            // 
            // cb_ComPortList
            // 
            this.cb_ComPortList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_ComPortList.BackColor = System.Drawing.Color.Gray;
            this.cb_ComPortList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cb_ComPortList.FormattingEnabled = true;
            this.cb_ComPortList.Location = new System.Drawing.Point(6, 43);
            this.cb_ComPortList.Name = "cb_ComPortList";
            this.cb_ComPortList.Size = new System.Drawing.Size(256, 21);
            this.cb_ComPortList.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Location = new System.Drawing.Point(239, 179);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // gb_Sensor
            // 
            this.gb_Sensor.Controls.Add(this.lb_Status);
            this.gb_Sensor.Controls.Add(this.label6);
            this.gb_Sensor.Controls.Add(this.label5);
            this.gb_Sensor.Controls.Add(this.lb_AxisY);
            this.gb_Sensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gb_Sensor.Location = new System.Drawing.Point(0, 12);
            this.gb_Sensor.Name = "gb_Sensor";
            this.gb_Sensor.Size = new System.Drawing.Size(314, 130);
            this.gb_Sensor.TabIndex = 2;
            this.gb_Sensor.TabStop = false;
            this.gb_Sensor.Text = "Sensor Index 0";
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lb_Status.Location = new System.Drawing.Point(98, 16);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(156, 26);
            this.lb_Status.TabIndex = 7;
            this.lb_Status.Text = "Disconnected";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(12, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 26);
            this.label6.TabIndex = 6;
            this.label6.Text = "Status:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(64, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 26);
            this.label5.TabIndex = 5;
            this.label5.Text = "Axis Y:";
            // 
            // lb_AxisY
            // 
            this.lb_AxisY.AutoSize = true;
            this.lb_AxisY.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lb_AxisY.Location = new System.Drawing.Point(158, 68);
            this.lb_AxisY.Name = "lb_AxisY";
            this.lb_AxisY.Size = new System.Drawing.Size(25, 26);
            this.lb_AxisY.TabIndex = 1;
            this.lb_AxisY.Text = "0";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(5, 184);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(33, 13);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "Time:";
            // 
            // lblTimeDisplay
            // 
            this.lblTimeDisplay.AutoSize = true;
            this.lblTimeDisplay.Location = new System.Drawing.Point(44, 184);
            this.lblTimeDisplay.Name = "lblTimeDisplay";
            this.lblTimeDisplay.Size = new System.Drawing.Size(31, 13);
            this.lblTimeDisplay.TabIndex = 4;
            this.lblTimeDisplay.Text = "1234";
            // 
            // lblCurrentAngle
            // 
            this.lblCurrentAngle.AutoSize = true;
            this.lblCurrentAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentAngle.Location = new System.Drawing.Point(3, 145);
            this.lblCurrentAngle.Name = "lblCurrentAngle";
            this.lblCurrentAngle.Size = new System.Drawing.Size(139, 25);
            this.lblCurrentAngle.TabIndex = 5;
            this.lblCurrentAngle.Text = "Current Angle:";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Location = new System.Drawing.Point(163, 179);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // ZSTARLIB_EXAMPLE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 286);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblCurrentAngle);
            this.Controls.Add(this.lblTimeDisplay);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.gb_Sensor);
            this.Controls.Add(this.gb_ComPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ZSTARLIB_EXAMPLE";
            this.Text = "ZStarLib Example Application";
            this.Load += new System.EventHandler(this.ZSTARLIB_EXAMPLE_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ZSTARLIB_EXAMPLE_FormClosed);
            this.gb_ComPort.ResumeLayout(false);
            this.gb_Sensor.ResumeLayout(false);
            this.gb_Sensor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_ComPort;
        private System.Windows.Forms.Button bt_ClosePort;
        private System.Windows.Forms.Button bt_OpenPort;
        private System.Windows.Forms.Button bt_RefreshComPort;
        private System.Windows.Forms.ComboBox cb_ComPortList;
        private System.Windows.Forms.GroupBox gb_Sensor;
        private System.Windows.Forms.Label lb_AxisY;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTimeDisplay;
        private System.Windows.Forms.Label lblCurrentAngle;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
    }
}

