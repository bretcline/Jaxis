namespace ReceiverApp
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class StartWizard_Intro : Form
    {
        private Button btnCancel;
        private Button btnNext;
        public CheckBox checkAutoGetStatus;
        private IContainer components;
        private GroupBox groupBox1;
        public RadioButton radioAutodiscover;
        public RadioButton radioDoNothing;
        public RadioButton radioLoadSettings;

        public StartWizard_Intro()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.checkAutoGetStatus = new CheckBox();
            this.radioDoNothing = new RadioButton();
            this.radioLoadSettings = new RadioButton();
            this.radioAutodiscover = new RadioButton();
            this.btnNext = new Button();
            this.btnCancel = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.checkAutoGetStatus);
            this.groupBox1.Controls.Add(this.radioDoNothing);
            this.groupBox1.Controls.Add(this.radioLoadSettings);
            this.groupBox1.Controls.Add(this.radioAutodiscover);
            this.groupBox1.Location = new Point(10, 11);
            this.groupBox1.Margin = new Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(2, 2, 2, 2);
            this.groupBox1.Size = new Size(0x159, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "How would you like to start?";
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.checkAutoGetStatus.AutoSize = true;
            this.checkAutoGetStatus.Checked = true;
            this.checkAutoGetStatus.CheckState = CheckState.Checked;
            this.checkAutoGetStatus.Location = new Point(5, 0x56);
            this.checkAutoGetStatus.Margin = new Padding(2, 2, 2, 2);
            this.checkAutoGetStatus.Name = "checkAutoGetStatus";
            this.checkAutoGetStatus.Size = new Size(0x125, 0x11);
            this.checkAutoGetStatus.TabIndex = 3;
            this.checkAutoGetStatus.Text = "Automatically get status of receivers added by the wizard";
            this.checkAutoGetStatus.UseVisualStyleBackColor = true;
            this.radioDoNothing.AutoSize = true;
            this.radioDoNothing.Location = new Point(5, 0x3f);
            this.radioDoNothing.Margin = new Padding(2, 2, 2, 2);
            this.radioDoNothing.Name = "radioDoNothing";
            this.radioDoNothing.Size = new Size(0x6b, 0x11);
            this.radioDoNothing.TabIndex = 2;
            this.radioDoNothing.TabStop = true;
            this.radioDoNothing.Text = "Proceed regularly";
            this.radioDoNothing.UseVisualStyleBackColor = true;
            this.radioLoadSettings.AutoSize = true;
            this.radioLoadSettings.Location = new Point(5, 0x29);
            this.radioLoadSettings.Margin = new Padding(2, 2, 2, 2);
            this.radioLoadSettings.Name = "radioLoadSettings";
            this.radioLoadSettings.Size = new Size(0xaf, 0x11);
            this.radioLoadSettings.TabIndex = 1;
            this.radioLoadSettings.TabStop = true;
            this.radioLoadSettings.Text = "Load settings from a settings file";
            this.radioLoadSettings.UseVisualStyleBackColor = true;
            this.radioAutodiscover.AutoSize = true;
            this.radioAutodiscover.Checked = true;
            this.radioAutodiscover.Location = new Point(5, 0x12);
            this.radioAutodiscover.Margin = new Padding(2, 2, 2, 2);
            this.radioAutodiscover.Name = "radioAutodiscover";
            this.radioAutodiscover.Size = new Size(0x88, 0x11);
            this.radioAutodiscover.TabIndex = 0;
            this.radioAutodiscover.TabStop = true;
            this.radioAutodiscover.Text = "Auto-discover receivers";
            this.radioAutodiscover.UseVisualStyleBackColor = true;
            this.btnNext.DialogResult = DialogResult.OK;
            this.btnNext.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnNext.Location = new Point(0x100, 0xce);
            this.btnNext.Margin = new Padding(2, 2, 2, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x63, 0x23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next...";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xb1);
            this.btnCancel.Location = new Point(9, 0xce);
            this.btnCancel.Margin = new Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = RightToLeft.Yes;
            this.btnCancel.Size = new Size(100, 0x23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x16c, 0xf6);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Margin = new Padding(2, 2, 2, 2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "StartWizard_Intro";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PureRF Receiver";
            base.Load += new EventHandler(this.StartWizard_Intro_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void StartWizard_Intro_Load(object sender, EventArgs e)
        {
            base.ActiveControl = this.btnNext;
        }
    }
}

