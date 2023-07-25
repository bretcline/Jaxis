namespace TestClient
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
            this.btnRead = new System.Windows.Forms.Button( );
            this.btnWrite = new System.Windows.Forms.Button( );
            this.btnAdd = new System.Windows.Forms.Button( );
            this.btnDelete = new System.Windows.Forms.Button( );
            this.btnInitialize = new System.Windows.Forms.Button( );
            this.btnGetAll = new System.Windows.Forms.Button( );
            this.txtDelete = new System.Windows.Forms.TextBox( );
            this.btnDelete2 = new System.Windows.Forms.Button( );
            this.SuspendLayout( );
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point( 159, 78 );
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size( 75, 23 );
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler( this.btnRead_Click );
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point( 159, 49 );
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size( 75, 23 );
            this.btnWrite.TabIndex = 1;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler( this.btnWrite_Click );
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point( 56, 78 );
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size( 75, 23 );
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler( this.btnAdd_Click );
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point( 56, 107 );
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size( 75, 23 );
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler( this.btnDelete_Click );
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point( 56, 49 );
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size( 75, 23 );
            this.btnInitialize.TabIndex = 4;
            this.btnInitialize.Text = "Initialize";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler( this.btnInitialize_Click );
            // 
            // btnGetAll
            // 
            this.btnGetAll.Location = new System.Drawing.Point( 159, 107 );
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size( 75, 23 );
            this.btnGetAll.TabIndex = 5;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = true;
            this.btnGetAll.Click += new System.EventHandler( this.btnGetAll_Click );
            // 
            // txtDelete
            // 
            this.txtDelete.Location = new System.Drawing.Point( 56, 167 );
            this.txtDelete.Name = "txtDelete";
            this.txtDelete.Size = new System.Drawing.Size( 75, 20 );
            this.txtDelete.TabIndex = 6;
            this.txtDelete.Text = "0";
            // 
            // btnDelete2
            // 
            this.btnDelete2.Location = new System.Drawing.Point( 56, 136 );
            this.btnDelete2.Name = "btnDelete2";
            this.btnDelete2.Size = new System.Drawing.Size( 75, 23 );
            this.btnDelete2.TabIndex = 7;
            this.btnDelete2.Text = "Delete 2";
            this.btnDelete2.UseVisualStyleBackColor = true;
            this.btnDelete2.Click += new System.EventHandler( this.btnDelete2_Click );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 292, 266 );
            this.Controls.Add( this.btnDelete2 );
            this.Controls.Add( this.txtDelete );
            this.Controls.Add( this.btnGetAll );
            this.Controls.Add( this.btnInitialize );
            this.Controls.Add( this.btnDelete );
            this.Controls.Add( this.btnAdd );
            this.Controls.Add( this.btnWrite );
            this.Controls.Add( this.btnRead );
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.TextBox txtDelete;
        private System.Windows.Forms.Button btnDelete2;
    }
}

