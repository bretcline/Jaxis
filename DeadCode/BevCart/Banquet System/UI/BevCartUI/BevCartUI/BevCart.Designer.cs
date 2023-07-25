namespace BevCartUI
{
    partial class BevCart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BevCart));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nextPage = new System.Windows.Forms.PictureBox();
            this.prevPage = new System.Windows.Forms.PictureBox();
            this.screenIndicator1 = new System.Windows.Forms.PictureBox();
            this.screenIndicator2 = new System.Windows.Forms.PictureBox();
            this.screenIndicator3 = new System.Windows.Forms.PictureBox();
            this.statusMessage = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.SectionHeader = new System.Windows.Forms.Label();
            this.panelDesktopContainer = new System.Windows.Forms.Panel();
            this.panelHeaderExtension = new System.Windows.Forms.Panel();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.desktop1 = new BevCartUI.Desktop();
            ((System.ComponentModel.ISupportInitialize)(this.nextPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prevPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenIndicator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenIndicator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenIndicator3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.panelDesktopContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // nextPage
            // 
            this.nextPage.BackgroundImage = global::BevCartUI.Properties.Resources.black_arrow_glossy_next;
            this.nextPage.Location = new System.Drawing.Point(961, 244);
            this.nextPage.Name = "nextPage";
            this.nextPage.Size = new System.Drawing.Size(46, 322);
            this.nextPage.TabIndex = 1;
            this.nextPage.TabStop = false;
            this.nextPage.Click += new System.EventHandler(this.nextPage_Click);
            // 
            // prevPage
            // 
            this.prevPage.BackgroundImage = global::BevCartUI.Properties.Resources.black_arrow_glossy_prev;
            this.prevPage.Location = new System.Drawing.Point(4, 244);
            this.prevPage.Name = "prevPage";
            this.prevPage.Size = new System.Drawing.Size(46, 322);
            this.prevPage.TabIndex = 1;
            this.prevPage.TabStop = false;
            this.prevPage.Click += new System.EventHandler(this.prevPage_Click);
            // 
            // screenIndicator1
            // 
            this.screenIndicator1.BackgroundImage = global::BevCartUI.Properties.Resources.square_white;
            this.screenIndicator1.Location = new System.Drawing.Point(482, 712);
            this.screenIndicator1.Name = "screenIndicator1";
            this.screenIndicator1.Size = new System.Drawing.Size(8, 6);
            this.screenIndicator1.TabIndex = 2;
            this.screenIndicator1.TabStop = false;
            // 
            // screenIndicator2
            // 
            this.screenIndicator2.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
            this.screenIndicator2.Location = new System.Drawing.Point(498, 712);
            this.screenIndicator2.Name = "screenIndicator2";
            this.screenIndicator2.Size = new System.Drawing.Size(8, 6);
            this.screenIndicator2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.screenIndicator2.TabIndex = 3;
            this.screenIndicator2.TabStop = false;
            // 
            // screenIndicator3
            // 
            this.screenIndicator3.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
            this.screenIndicator3.Location = new System.Drawing.Point(515, 712);
            this.screenIndicator3.Name = "screenIndicator3";
            this.screenIndicator3.Size = new System.Drawing.Size(8, 6);
            this.screenIndicator3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.screenIndicator3.TabIndex = 3;
            this.screenIndicator3.TabStop = false;
            // 
            // statusMessage
            // 
            this.statusMessage.BackColor = System.Drawing.Color.White;
            this.statusMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.statusMessage.Location = new System.Drawing.Point(1, 55);
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(1024, 20);
            this.statusMessage.TabIndex = 4;
            this.statusMessage.Text = "Status messages will go here.";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::BevCartUI.Properties.Resources.BevMetLogo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 54);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // picHeader
            // 
            this.picHeader.BackgroundImage = global::BevCartUI.Properties.Resources.bevcart_header;
            this.picHeader.Location = new System.Drawing.Point(137, 0);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(834, 54);
            this.picHeader.TabIndex = 6;
            this.picHeader.TabStop = false;
            this.picHeader.DoubleClick += new System.EventHandler(this.picHeader_DoubleClick);
            // 
            // SectionHeader
            // 
            this.SectionHeader.AutoSize = true;
            this.SectionHeader.BackColor = System.Drawing.Color.DarkRed;
            this.SectionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionHeader.ForeColor = System.Drawing.Color.White;
            this.SectionHeader.Location = new System.Drawing.Point(676, 20);
            this.SectionHeader.Name = "SectionHeader";
            this.SectionHeader.Size = new System.Drawing.Size(86, 31);
            this.SectionHeader.TabIndex = 7;
            this.SectionHeader.Text = "label1";
            // 
            // panelDesktopContainer
            // 
            this.panelDesktopContainer.Controls.Add(this.desktop1);
            this.panelDesktopContainer.Location = new System.Drawing.Point(0, -1);
            this.panelDesktopContainer.Name = "panelDesktopContainer";
            this.panelDesktopContainer.Size = new System.Drawing.Size(1024, 768);
            this.panelDesktopContainer.TabIndex = 8;
            // 
            // panelHeaderExtension
            // 
            this.panelHeaderExtension.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeaderExtension.AutoSize = true;
            this.panelHeaderExtension.BackgroundImage = global::BevCartUI.Properties.Resources.bevmet_header_tile;
            this.panelHeaderExtension.Location = new System.Drawing.Point(961, 0);
            this.panelHeaderExtension.Name = "panelHeaderExtension";
            this.panelHeaderExtension.Size = new System.Drawing.Size(74, 54);
            this.panelHeaderExtension.TabIndex = 2;
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // desktop1
            // 
            this.desktop1.BackgroundImage = global::BevCartUI.Properties.Resources.bevmet_background_tile;
            this.desktop1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.desktop1.Location = new System.Drawing.Point(0, 0);
            this.desktop1.Name = "desktop1";
            this.desktop1.Size = new System.Drawing.Size(3840, 2400);
            this.desktop1.TabIndex = 0;
            this.desktop1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseMove);
            this.desktop1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseDown);
            this.desktop1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseUp);
            // 
            // BevCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.BackgroundImage = global::BevCartUI.Properties.Resources.bevmet_background_tile;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.prevPage);
            this.Controls.Add(this.nextPage);
            this.Controls.Add(this.SectionHeader);
            this.Controls.Add(this.panelHeaderExtension);
            this.Controls.Add(this.picHeader);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusMessage);
            this.Controls.Add(this.screenIndicator3);
            this.Controls.Add(this.screenIndicator2);
            this.Controls.Add(this.screenIndicator1);
            this.Controls.Add(this.panelDesktopContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "BevCart";
            this.Text = "Beverage Metrics";
            this.Load += new System.EventHandler(this.BevCart_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BevCart_KeyPress);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BevCart_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.nextPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prevPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenIndicator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenIndicator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenIndicator3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.panelDesktopContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Desktop desktop1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox prevPage;
        private System.Windows.Forms.PictureBox nextPage;
        private System.Windows.Forms.PictureBox screenIndicator1;
        private System.Windows.Forms.PictureBox screenIndicator2;
        private System.Windows.Forms.PictureBox screenIndicator3;
        private System.Windows.Forms.Label statusMessage;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Label SectionHeader;
        private System.Windows.Forms.Panel panelDesktopContainer;
        private System.Windows.Forms.Panel panelHeaderExtension;
        private System.Windows.Forms.Timer timer3;

    }
    }

