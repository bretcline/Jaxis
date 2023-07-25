namespace InSyncSimulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer( );
            this.btnPushTicket = new System.Windows.Forms.Button( );
            this.btnPushPour = new System.Windows.Forms.Button( );
            ( (System.ComponentModel.ISupportInitialize)( this.splitContainer1 ) ).BeginInit( );
            this.splitContainer1.Panel1.SuspendLayout( );
            this.splitContainer1.Panel2.SuspendLayout( );
            this.splitContainer1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.btnPushTicket );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.btnPushPour );
            this.splitContainer1.Size = new System.Drawing.Size( 559, 539 );
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnPushTicket
            // 
            this.btnPushTicket.Location = new System.Drawing.Point( 198, 512 );
            this.btnPushTicket.Name = "btnPushTicket";
            this.btnPushTicket.Size = new System.Drawing.Size( 75, 23 );
            this.btnPushTicket.TabIndex = 0;
            this.btnPushTicket.Text = "Push Ticket";
            this.btnPushTicket.UseVisualStyleBackColor = true;
            // 
            // btnPushPour
            // 
            this.btnPushPour.Location = new System.Drawing.Point( 198, 512 );
            this.btnPushPour.Name = "btnPushPour";
            this.btnPushPour.Size = new System.Drawing.Size( 75, 23 );
            this.btnPushPour.TabIndex = 1;
            this.btnPushPour.Text = "Push Pour";
            this.btnPushPour.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 559, 539 );
            this.Controls.Add( this.splitContainer1 );
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.splitContainer1 ) ).EndInit( );
            this.splitContainer1.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnPushTicket;
        private System.Windows.Forms.Button btnPushPour;
    }
}

