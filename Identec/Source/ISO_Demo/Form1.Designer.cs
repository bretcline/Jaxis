namespace ISO_Demo
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
            this.components = new System.ComponentModel.Container( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle( );
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Form1 ) );
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer( );
            this.statusStrip1 = new System.Windows.Forms.StatusStrip( );
            this.toolStripStatusLabelConnection = new System.Windows.Forms.ToolStripStatusLabel( );
            this.toolStripStatusLabelCommand = new System.Windows.Forms.ToolStripStatusLabel( );
            this.toolStripStatusLabelNbTags = new System.Windows.Forms.ToolStripStatusLabel( );
            this.splitContainer1 = new System.Windows.Forms.SplitContainer( );
            this.splitContainer2 = new System.Windows.Forms.SplitContainer( );
            this.dataGridView1 = new System.Windows.Forms.DataGridView( );
            this.manufacturerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.sNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.uDBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.routingCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.rSSIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataTable1BindingSource1 = new System.Windows.Forms.BindingSource( this.components );
            this.dsTags1BindingSource = new System.Windows.Forms.BindingSource( this.components );
            this.dsTags1 = new ISO_Demo.dsTags( );
            this.tabControl1 = new System.Windows.Forms.TabControl( );
            this.tabPage1 = new System.Windows.Forms.TabPage( );
            this.buttonWriteMemory = new System.Windows.Forms.Button( );
            this.buttonReadMemory = new System.Windows.Forms.Button( );
            this.checkBoxUseASCII = new System.Windows.Forms.CheckBox( );
            this.richTextBox1 = new ISO_Demo.HexEditBox( );
            this.groupBox1 = new System.Windows.Forms.GroupBox( );
            this.label2 = new System.Windows.Forms.Label( );
            this.label1 = new System.Windows.Forms.Label( );
            this.label4 = new System.Windows.Forms.Label( );
            this.numericUpDownAddress = new System.Windows.Forms.NumericUpDown( );
            this.textBoxMemoryWritten = new System.Windows.Forms.TextBox( );
            this.textBoxMemoryRead = new System.Windows.Forms.TextBox( );
            this.label3 = new System.Windows.Forms.Label( );
            this.numericUpDownNbByte = new System.Windows.Forms.NumericUpDown( );
            this.tabPage2 = new System.Windows.Forms.TabPage( );
            this.tabPage3 = new System.Windows.Forms.TabPage( );
            this.tabPage4 = new System.Windows.Forms.TabPage( );
            this.splitContainer3 = new System.Windows.Forms.SplitContainer( );
            this.comboBoxUDBType = new System.Windows.Forms.ComboBox( );
            this.buttonReadUDB = new System.Windows.Forms.Button( );
            this.label6 = new System.Windows.Forms.Label( );
            this.textBoxMaxPackageLength = new System.Windows.Forms.TextBox( );
            this.label5 = new System.Windows.Forms.Label( );
            this.dataGridView2 = new System.Windows.Forms.DataGridView( );
            this.Parameter = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.Length = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.lengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.parameterDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataSetUDBElementsBindingSource = new System.Windows.Forms.BindingSource( this.components );
            this.dataSetUDBElements = new ISO_Demo.DataSetUDBElements( );
            this.tabPage5 = new System.Windows.Forms.TabPage( );
            this.groupBox5 = new System.Windows.Forms.GroupBox( );
            this.buttonReadTableData = new System.Windows.Forms.Button( );
            this.textBoxReadTableDataTableID = new System.Windows.Forms.TextBox( );
            this.label20 = new System.Windows.Forms.Label( );
            this.groupBox4 = new System.Windows.Forms.GroupBox( );
            this.textBoxAddRecordSize = new System.Windows.Forms.TextBox( );
            this.label16 = new System.Windows.Forms.Label( );
            this.buttonRandom = new System.Windows.Forms.Button( );
            this.textBoxRowData = new System.Windows.Forms.TextBox( );
            this.label13 = new System.Windows.Forms.Label( );
            this.textBoxAddRecordCount = new System.Windows.Forms.TextBox( );
            this.label14 = new System.Windows.Forms.Label( );
            this.buttonAddData = new System.Windows.Forms.Button( );
            this.textBoxAddTableID = new System.Windows.Forms.TextBox( );
            this.label15 = new System.Windows.Forms.Label( );
            this.groupBox3 = new System.Windows.Forms.GroupBox( );
            this.textBoxPropertiesMAxRecords = new System.Windows.Forms.TextBox( );
            this.textBoxPropertiesNumRecords = new System.Windows.Forms.TextBox( );
            this.label12 = new System.Windows.Forms.Label( );
            this.label11 = new System.Windows.Forms.Label( );
            this.button2 = new System.Windows.Forms.Button( );
            this.textBoxReadPropertiesTableID = new System.Windows.Forms.TextBox( );
            this.label10 = new System.Windows.Forms.Label( );
            this.groupBox2 = new System.Windows.Forms.GroupBox( );
            this.button1 = new System.Windows.Forms.Button( );
            this.textBoxRecordSize = new System.Windows.Forms.TextBox( );
            this.label9 = new System.Windows.Forms.Label( );
            this.textBoxMaxRecords = new System.Windows.Forms.TextBox( );
            this.label8 = new System.Windows.Forms.Label( );
            this.textBoxTableID = new System.Windows.Forms.TextBox( );
            this.label7 = new System.Windows.Forms.Label( );
            this.richTextBox2 = new System.Windows.Forms.RichTextBox( );
            this.toolStrip2 = new System.Windows.Forms.ToolStrip( );
            this.toolStripButtonSleep = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonSleepAllbut = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonReadRoutingCode = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonWriteRoutingCode = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonBeeperON = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonBeeperOFF = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonClearMemory = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator( );
            this.menuStrip2 = new System.Windows.Forms.MenuStrip( );
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.routingCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.toolStripTextBoxRoutingCode = new System.Windows.Forms.ToolStripTextBox( );
            this.collectionWindowSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.toolStripTextBoxWindowSize = new System.Windows.Forms.ToolStripTextBox( );
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.manufacturerIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.uDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.routingCodeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem( );
            this.rSSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem( );
            this.toolStripTextBoxMaxPacketLength = new System.Windows.Forms.ToolStripTextBox( );
            this.toolStripComboBoxWakeUp = new System.Windows.Forms.ToolStripComboBox( );
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem( );
            this.toolStrip1 = new System.Windows.Forms.ToolStrip( );
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonDisconnect = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonClearTagList = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonCollection2 = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel( );
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripComboBoxSerialPort = new System.Windows.Forms.ToolStripComboBox( );
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator( );
            this.toolStripButtonAbout = new System.Windows.Forms.ToolStripButton( );
            this.iSO18000TagBindingSource = new System.Windows.Forms.BindingSource( this.components );
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout( );
            this.toolStripContainer1.ContentPanel.SuspendLayout( );
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout( );
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout( );
            this.toolStripContainer1.SuspendLayout( );
            this.statusStrip1.SuspendLayout( );
            this.splitContainer1.Panel1.SuspendLayout( );
            this.splitContainer1.Panel2.SuspendLayout( );
            this.splitContainer1.SuspendLayout( );
            this.splitContainer2.Panel1.SuspendLayout( );
            this.splitContainer2.Panel2.SuspendLayout( );
            this.splitContainer2.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataTable1BindingSource1 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dsTags1BindingSource ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dsTags1 ) ).BeginInit( );
            this.tabControl1.SuspendLayout( );
            this.tabPage1.SuspendLayout( );
            this.groupBox1.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDownAddress ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDownNbByte ) ).BeginInit( );
            this.tabPage4.SuspendLayout( );
            this.splitContainer3.Panel1.SuspendLayout( );
            this.splitContainer3.Panel2.SuspendLayout( );
            this.splitContainer3.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView2 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataSetUDBElementsBindingSource ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataSetUDBElements ) ).BeginInit( );
            this.tabPage5.SuspendLayout( );
            this.groupBox5.SuspendLayout( );
            this.groupBox4.SuspendLayout( );
            this.groupBox3.SuspendLayout( );
            this.groupBox2.SuspendLayout( );
            this.toolStrip2.SuspendLayout( );
            this.menuStrip2.SuspendLayout( );
            this.toolStrip1.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.iSO18000TagBindingSource ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add( this.statusStrip1 );
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add( this.splitContainer1 );
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size( 875, 743 );
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add( this.toolStrip2 );
            this.toolStripContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            this.toolStripContainer1.RightToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripContainer1.Size = new System.Drawing.Size( 989, 816 );
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add( this.menuStrip2 );
            this.toolStripContainer1.TopToolStripPanel.Controls.Add( this.toolStrip1 );
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConnection,
            this.toolStripStatusLabelCommand,
            this.toolStripStatusLabelNbTags} );
            this.statusStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size( 989, 24 );
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabelConnection
            // 
            this.toolStripStatusLabelConnection.Name = "toolStripStatusLabelConnection";
            this.toolStripStatusLabelConnection.Padding = new System.Windows.Forms.Padding( 0, 0, 20, 0 );
            this.toolStripStatusLabelConnection.Size = new System.Drawing.Size( 138, 19 );
            this.toolStripStatusLabelConnection.Text = "No reader connected";
            // 
            // toolStripStatusLabelCommand
            // 
            this.toolStripStatusLabelCommand.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelCommand.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelCommand.Name = "toolStripStatusLabelCommand";
            this.toolStripStatusLabelCommand.Padding = new System.Windows.Forms.Padding( 0, 0, 10, 0 );
            this.toolStripStatusLabelCommand.Size = new System.Drawing.Size( 53, 19 );
            this.toolStripStatusLabelCommand.Text = "Ready";
            // 
            // toolStripStatusLabelNbTags
            // 
            this.toolStripStatusLabelNbTags.BorderSides = ( (System.Windows.Forms.ToolStripStatusLabelBorderSides)( ( System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right ) ) );
            this.toolStripStatusLabelNbTags.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabelNbTags.Name = "toolStripStatusLabelNbTags";
            this.toolStripStatusLabelNbTags.Size = new System.Drawing.Size( 37, 19 );
            this.toolStripStatusLabelNbTags.Text = "0 tag";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add( this.splitContainer2 );
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add( this.richTextBox2 );
            this.splitContainer1.Size = new System.Drawing.Size( 875, 743 );
            this.splitContainer1.SplitterDistance = 486;
            this.splitContainer1.TabIndex = 13;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add( this.dataGridView1 );
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add( this.tabControl1 );
            this.splitContainer2.Size = new System.Drawing.Size( 875, 486 );
            this.splitContainer2.SplitterDistance = 289;
            this.splitContainer2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.manufacturerIDDataGridViewTextBoxColumn,
            this.sNDataGridViewTextBoxColumn,
            this.uDBDataGridViewTextBoxColumn,
            this.routingCodeDataGridViewTextBoxColumn,
            this.rSSIDataGridViewTextBoxColumn} );
            this.dataGridView1.DataSource = this.dataTable1BindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point( 0, 0 );
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size( 289, 486 );
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler( this.dataGridView1_RowsAdded );
            // 
            // manufacturerIDDataGridViewTextBoxColumn
            // 
            this.manufacturerIDDataGridViewTextBoxColumn.DataPropertyName = "ManufacturerID";
            this.manufacturerIDDataGridViewTextBoxColumn.HeaderText = "ManufacturerID";
            this.manufacturerIDDataGridViewTextBoxColumn.Name = "manufacturerIDDataGridViewTextBoxColumn";
            this.manufacturerIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.manufacturerIDDataGridViewTextBoxColumn.Width = 106;
            // 
            // sNDataGridViewTextBoxColumn
            // 
            this.sNDataGridViewTextBoxColumn.DataPropertyName = "SN";
            this.sNDataGridViewTextBoxColumn.HeaderText = "SN";
            this.sNDataGridViewTextBoxColumn.Name = "sNDataGridViewTextBoxColumn";
            this.sNDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uDBDataGridViewTextBoxColumn
            // 
            this.uDBDataGridViewTextBoxColumn.DataPropertyName = "UDB";
            this.uDBDataGridViewTextBoxColumn.HeaderText = "User ID";
            this.uDBDataGridViewTextBoxColumn.Name = "uDBDataGridViewTextBoxColumn";
            this.uDBDataGridViewTextBoxColumn.ReadOnly = true;
            this.uDBDataGridViewTextBoxColumn.Width = 150;
            // 
            // routingCodeDataGridViewTextBoxColumn
            // 
            this.routingCodeDataGridViewTextBoxColumn.DataPropertyName = "RoutingCode";
            this.routingCodeDataGridViewTextBoxColumn.HeaderText = "RoutingCode";
            this.routingCodeDataGridViewTextBoxColumn.Name = "routingCodeDataGridViewTextBoxColumn";
            this.routingCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.routingCodeDataGridViewTextBoxColumn.Width = 94;
            // 
            // rSSIDataGridViewTextBoxColumn
            // 
            this.rSSIDataGridViewTextBoxColumn.DataPropertyName = "RSSI";
            this.rSSIDataGridViewTextBoxColumn.HeaderText = "RSSI";
            this.rSSIDataGridViewTextBoxColumn.Name = "rSSIDataGridViewTextBoxColumn";
            this.rSSIDataGridViewTextBoxColumn.ReadOnly = true;
            this.rSSIDataGridViewTextBoxColumn.Width = 57;
            // 
            // dataTable1BindingSource1
            // 
            this.dataTable1BindingSource1.DataMember = "DataTable1";
            this.dataTable1BindingSource1.DataSource = this.dsTags1BindingSource;
            // 
            // dsTags1BindingSource
            // 
            this.dsTags1BindingSource.DataSource = this.dsTags1;
            this.dsTags1BindingSource.Position = 0;
            // 
            // dsTags1
            // 
            this.dsTags1.DataSetName = "dsTags";
            this.dsTags1.Locale = new System.Globalization.CultureInfo( "en" );
            this.dsTags1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add( this.tabPage1 );
            this.tabControl1.Controls.Add( this.tabPage2 );
            this.tabControl1.Controls.Add( this.tabPage3 );
            this.tabControl1.Controls.Add( this.tabPage4 );
            this.tabControl1.Controls.Add( this.tabPage5 );
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point( 0, 0 );
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size( 582, 486 );
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add( this.buttonWriteMemory );
            this.tabPage1.Controls.Add( this.buttonReadMemory );
            this.tabPage1.Controls.Add( this.checkBoxUseASCII );
            this.tabPage1.Controls.Add( this.richTextBox1 );
            this.tabPage1.Controls.Add( this.groupBox1 );
            this.tabPage1.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage1.Size = new System.Drawing.Size( 574, 460 );
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MEMORY RW";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonWriteMemory
            // 
            this.buttonWriteMemory.Location = new System.Drawing.Point( 171, 136 );
            this.buttonWriteMemory.Name = "buttonWriteMemory";
            this.buttonWriteMemory.Size = new System.Drawing.Size( 75, 23 );
            this.buttonWriteMemory.TabIndex = 17;
            this.buttonWriteMemory.Text = "Write";
            this.buttonWriteMemory.UseVisualStyleBackColor = true;
            this.buttonWriteMemory.Click += new System.EventHandler( this.buttonWriteMemory_Click );
            // 
            // buttonReadMemory
            // 
            this.buttonReadMemory.Location = new System.Drawing.Point( 51, 137 );
            this.buttonReadMemory.Name = "buttonReadMemory";
            this.buttonReadMemory.Size = new System.Drawing.Size( 75, 23 );
            this.buttonReadMemory.TabIndex = 16;
            this.buttonReadMemory.Text = "Read";
            this.buttonReadMemory.UseVisualStyleBackColor = true;
            this.buttonReadMemory.Click += new System.EventHandler( this.buttonReadMemory_Click );
            // 
            // checkBoxUseASCII
            // 
            this.checkBoxUseASCII.AutoSize = true;
            this.checkBoxUseASCII.Location = new System.Drawing.Point( 35, 204 );
            this.checkBoxUseASCII.Name = "checkBoxUseASCII";
            this.checkBoxUseASCII.Size = new System.Drawing.Size( 124, 17 );
            this.checkBoxUseASCII.TabIndex = 15;
            this.checkBoxUseASCII.Text = "Use HEXADECIMAL";
            this.checkBoxUseASCII.UseVisualStyleBackColor = true;
            this.checkBoxUseASCII.CheckedChanged += new System.EventHandler( this.checkBoxUseASCII_CheckedChanged );
            // 
            // richTextBox1
            // 
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point( 33, 166 );
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size( 414, 22 );
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler( this.richTextBox1_TextChanged );
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add( this.label2 );
            this.groupBox1.Controls.Add( this.label1 );
            this.groupBox1.Controls.Add( this.label4 );
            this.groupBox1.Controls.Add( this.numericUpDownAddress );
            this.groupBox1.Controls.Add( this.textBoxMemoryWritten );
            this.groupBox1.Controls.Add( this.textBoxMemoryRead );
            this.groupBox1.Controls.Add( this.label3 );
            this.groupBox1.Controls.Add( this.numericUpDownNbByte );
            this.groupBox1.Location = new System.Drawing.Point( 33, 45 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 427, 77 );
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MEMORY Read/Write parameters";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 6, 51 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 85, 13 );
            this.label2.TabIndex = 8;
            this.label2.Text = "Number of Bytes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 6, 19 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 85, 13 );
            this.label1.TabIndex = 7;
            this.label1.Text = "Memory Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point( 177, 54 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size( 54, 13 );
            this.label4.TabIndex = 14;
            this.label4.Text = "Data read";
            // 
            // numericUpDownAddress
            // 
            this.numericUpDownAddress.Hexadecimal = true;
            this.numericUpDownAddress.Location = new System.Drawing.Point( 109, 17 );
            this.numericUpDownAddress.Maximum = new decimal( new int[] {
            16777215,
            0,
            0,
            0} );
            this.numericUpDownAddress.Name = "numericUpDownAddress";
            this.numericUpDownAddress.Size = new System.Drawing.Size( 61, 20 );
            this.numericUpDownAddress.TabIndex = 5;
            // 
            // textBoxMemoryWritten
            // 
            this.textBoxMemoryWritten.Location = new System.Drawing.Point( 248, 16 );
            this.textBoxMemoryWritten.MaxLength = 32;
            this.textBoxMemoryWritten.Name = "textBoxMemoryWritten";
            this.textBoxMemoryWritten.Size = new System.Drawing.Size( 163, 20 );
            this.textBoxMemoryWritten.TabIndex = 9;
            this.textBoxMemoryWritten.Text = "Data written";
            // 
            // textBoxMemoryRead
            // 
            this.textBoxMemoryRead.Location = new System.Drawing.Point( 248, 51 );
            this.textBoxMemoryRead.MaxLength = 32;
            this.textBoxMemoryRead.Name = "textBoxMemoryRead";
            this.textBoxMemoryRead.ReadOnly = true;
            this.textBoxMemoryRead.Size = new System.Drawing.Size( 163, 20 );
            this.textBoxMemoryRead.TabIndex = 12;
            this.textBoxMemoryRead.Text = "Data read";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 177, 18 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 67, 13 );
            this.label3.TabIndex = 13;
            this.label3.Text = "Data to write";
            // 
            // numericUpDownNbByte
            // 
            this.numericUpDownNbByte.Location = new System.Drawing.Point( 109, 49 );
            this.numericUpDownNbByte.Maximum = new decimal( new int[] {
            32,
            0,
            0,
            0} );
            this.numericUpDownNbByte.Name = "numericUpDownNbByte";
            this.numericUpDownNbByte.Size = new System.Drawing.Size( 61, 20 );
            this.numericUpDownNbByte.TabIndex = 6;
            this.numericUpDownNbByte.Value = new decimal( new int[] {
            4,
            0,
            0,
            0} );
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage2.Size = new System.Drawing.Size( 580, 462 );
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User ID";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage3.Size = new System.Drawing.Size( 580, 462 );
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Routing Code";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add( this.splitContainer3 );
            this.tabPage4.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage4.Size = new System.Drawing.Size( 580, 462 );
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "UDB";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point( 3, 3 );
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add( this.comboBoxUDBType );
            this.splitContainer3.Panel1.Controls.Add( this.buttonReadUDB );
            this.splitContainer3.Panel1.Controls.Add( this.label6 );
            this.splitContainer3.Panel1.Controls.Add( this.textBoxMaxPackageLength );
            this.splitContainer3.Panel1.Controls.Add( this.label5 );
            this.splitContainer3.Panel1.Paint += new System.Windows.Forms.PaintEventHandler( this.splitContainer3_Panel1_Paint );
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add( this.dataGridView2 );
            this.splitContainer3.Size = new System.Drawing.Size( 574, 456 );
            this.splitContainer3.SplitterDistance = 105;
            this.splitContainer3.TabIndex = 1;
            // 
            // comboBoxUDBType
            // 
            this.comboBoxUDBType.FormattingEnabled = true;
            this.comboBoxUDBType.Items.AddRange( new object[] {
            "0",
            "3"} );
            this.comboBoxUDBType.Location = new System.Drawing.Point( 160, 24 );
            this.comboBoxUDBType.Name = "comboBoxUDBType";
            this.comboBoxUDBType.Size = new System.Drawing.Size( 166, 21 );
            this.comboBoxUDBType.TabIndex = 5;
            this.comboBoxUDBType.Text = "0";
            this.comboBoxUDBType.SelectedIndexChanged += new System.EventHandler( this.comboBox1_SelectedIndexChanged );
            // 
            // buttonReadUDB
            // 
            this.buttonReadUDB.Location = new System.Drawing.Point( 440, 48 );
            this.buttonReadUDB.Name = "buttonReadUDB";
            this.buttonReadUDB.Size = new System.Drawing.Size( 75, 23 );
            this.buttonReadUDB.TabIndex = 4;
            this.buttonReadUDB.Text = "Read";
            this.buttonReadUDB.UseVisualStyleBackColor = true;
            this.buttonReadUDB.Click += new System.EventHandler( this.buttonReadUDB_Click );
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point( 49, 54 );
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size( 109, 13 );
            this.label6.TabIndex = 3;
            this.label6.Text = "Max Package Length";
            this.label6.Click += new System.EventHandler( this.label6_Click );
            // 
            // textBoxMaxPackageLength
            // 
            this.textBoxMaxPackageLength.Location = new System.Drawing.Point( 160, 51 );
            this.textBoxMaxPackageLength.Name = "textBoxMaxPackageLength";
            this.textBoxMaxPackageLength.Size = new System.Drawing.Size( 166, 20 );
            this.textBoxMaxPackageLength.TabIndex = 2;
            this.textBoxMaxPackageLength.Text = "32";
            this.textBoxMaxPackageLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxMaxPackageLength.TextChanged += new System.EventHandler( this.textBox2_TextChanged );
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point( 101, 28 );
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size( 57, 13 );
            this.label5.TabIndex = 1;
            this.label5.Text = "UDB Type";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.Parameter,
            this.Length,
            this.Data,
            this.lengthDataGridViewTextBoxColumn,
            this.parameterDataGridViewTextBoxColumn,
            this.dataDataGridViewTextBoxColumn} );
            this.dataGridView2.DataSource = this.dataSetUDBElementsBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point( 0, 0 );
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size( 574, 347 );
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler( this.dataGridView2_CellContentClick );
            // 
            // Parameter
            // 
            this.Parameter.DataPropertyName = "Parameter";
            this.Parameter.HeaderText = "Parameter";
            this.Parameter.Name = "Parameter";
            this.Parameter.ReadOnly = true;
            // 
            // Length
            // 
            this.Length.DataPropertyName = "Length";
            this.Length.HeaderText = "Length";
            this.Length.Name = "Length";
            this.Length.ReadOnly = true;
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // lengthDataGridViewTextBoxColumn
            // 
            this.lengthDataGridViewTextBoxColumn.DataPropertyName = "Length";
            this.lengthDataGridViewTextBoxColumn.HeaderText = "Length";
            this.lengthDataGridViewTextBoxColumn.Name = "lengthDataGridViewTextBoxColumn";
            this.lengthDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // parameterDataGridViewTextBoxColumn
            // 
            this.parameterDataGridViewTextBoxColumn.DataPropertyName = "Parameter";
            this.parameterDataGridViewTextBoxColumn.HeaderText = "Parameter";
            this.parameterDataGridViewTextBoxColumn.Name = "parameterDataGridViewTextBoxColumn";
            this.parameterDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataDataGridViewTextBoxColumn
            // 
            this.dataDataGridViewTextBoxColumn.DataPropertyName = "Data";
            this.dataDataGridViewTextBoxColumn.HeaderText = "Data";
            this.dataDataGridViewTextBoxColumn.Name = "dataDataGridViewTextBoxColumn";
            this.dataDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataSetUDBElementsBindingSource
            // 
            this.dataSetUDBElementsBindingSource.DataMember = "DataTable1";
            this.dataSetUDBElementsBindingSource.DataSource = this.dataSetUDBElements;
            // 
            // dataSetUDBElements
            // 
            this.dataSetUDBElements.DataSetName = "DataSetUDBElements";
            this.dataSetUDBElements.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add( this.groupBox5 );
            this.tabPage5.Controls.Add( this.groupBox4 );
            this.tabPage5.Controls.Add( this.groupBox3 );
            this.tabPage5.Controls.Add( this.groupBox2 );
            this.tabPage5.Location = new System.Drawing.Point( 4, 22 );
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage5.Size = new System.Drawing.Size( 580, 462 );
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Table";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox5.Controls.Add( this.buttonReadTableData );
            this.groupBox5.Controls.Add( this.textBoxReadTableDataTableID );
            this.groupBox5.Controls.Add( this.label20 );
            this.groupBox5.Location = new System.Drawing.Point( 31, 361 );
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size( 530, 65 );
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Read Data";
            this.groupBox5.Enter += new System.EventHandler( this.groupBox5_Enter );
            // 
            // buttonReadTableData
            // 
            this.buttonReadTableData.Location = new System.Drawing.Point( 421, 24 );
            this.buttonReadTableData.Name = "buttonReadTableData";
            this.buttonReadTableData.Size = new System.Drawing.Size( 98, 23 );
            this.buttonReadTableData.TabIndex = 4;
            this.buttonReadTableData.Text = "Read Table Data";
            this.buttonReadTableData.UseVisualStyleBackColor = true;
            this.buttonReadTableData.Click += new System.EventHandler( this.buttonReadTableData_Click );
            // 
            // textBoxReadTableDataTableID
            // 
            this.textBoxReadTableDataTableID.Location = new System.Drawing.Point( 103, 26 );
            this.textBoxReadTableDataTableID.Name = "textBoxReadTableDataTableID";
            this.textBoxReadTableDataTableID.Size = new System.Drawing.Size( 99, 20 );
            this.textBoxReadTableDataTableID.TabIndex = 3;
            this.textBoxReadTableDataTableID.Text = "12";
            this.textBoxReadTableDataTableID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point( 48, 29 );
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size( 48, 13 );
            this.label20.TabIndex = 2;
            this.label20.Text = "Table ID";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add( this.textBoxAddRecordSize );
            this.groupBox4.Controls.Add( this.label16 );
            this.groupBox4.Controls.Add( this.buttonRandom );
            this.groupBox4.Controls.Add( this.textBoxRowData );
            this.groupBox4.Controls.Add( this.label13 );
            this.groupBox4.Controls.Add( this.textBoxAddRecordCount );
            this.groupBox4.Controls.Add( this.label14 );
            this.groupBox4.Controls.Add( this.buttonAddData );
            this.groupBox4.Controls.Add( this.textBoxAddTableID );
            this.groupBox4.Controls.Add( this.label15 );
            this.groupBox4.Location = new System.Drawing.Point( 31, 213 );
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size( 530, 142 );
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Data";
            // 
            // textBoxAddRecordSize
            // 
            this.textBoxAddRecordSize.Location = new System.Drawing.Point( 320, 52 );
            this.textBoxAddRecordSize.Name = "textBoxAddRecordSize";
            this.textBoxAddRecordSize.Size = new System.Drawing.Size( 99, 20 );
            this.textBoxAddRecordSize.TabIndex = 11;
            this.textBoxAddRecordSize.Text = "12";
            this.textBoxAddRecordSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point( 249, 55 );
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size( 65, 13 );
            this.label16.TabIndex = 10;
            this.label16.Text = "Record Size";
            // 
            // buttonRandom
            // 
            this.buttonRandom.Location = new System.Drawing.Point( 421, 76 );
            this.buttonRandom.Name = "buttonRandom";
            this.buttonRandom.Size = new System.Drawing.Size( 59, 23 );
            this.buttonRandom.TabIndex = 9;
            this.buttonRandom.Text = "Random";
            this.buttonRandom.UseVisualStyleBackColor = true;
            this.buttonRandom.Click += new System.EventHandler( this.buttonRandom_Click );
            // 
            // textBoxRowData
            // 
            this.textBoxRowData.Location = new System.Drawing.Point( 103, 78 );
            this.textBoxRowData.Name = "textBoxRowData";
            this.textBoxRowData.Size = new System.Drawing.Size( 316, 20 );
            this.textBoxRowData.TabIndex = 8;
            this.textBoxRowData.Text = "12";
            this.textBoxRowData.TextChanged += new System.EventHandler( this.textBoxRowData_TextChanged );
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point( 36, 81 );
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size( 55, 13 );
            this.label13.TabIndex = 7;
            this.label13.Text = "Row Data";
            // 
            // textBoxAddRecordCount
            // 
            this.textBoxAddRecordCount.Location = new System.Drawing.Point( 103, 52 );
            this.textBoxAddRecordCount.Name = "textBoxAddRecordCount";
            this.textBoxAddRecordCount.Size = new System.Drawing.Size( 99, 20 );
            this.textBoxAddRecordCount.TabIndex = 6;
            this.textBoxAddRecordCount.Text = "12";
            this.textBoxAddRecordCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point( 20, 55 );
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size( 73, 13 );
            this.label14.TabIndex = 5;
            this.label14.Text = "Record Count";
            this.label14.Click += new System.EventHandler( this.label14_Click );
            // 
            // buttonAddData
            // 
            this.buttonAddData.Location = new System.Drawing.Point( 421, 113 );
            this.buttonAddData.Name = "buttonAddData";
            this.buttonAddData.Size = new System.Drawing.Size( 98, 23 );
            this.buttonAddData.TabIndex = 4;
            this.buttonAddData.Text = "Add Data";
            this.buttonAddData.UseVisualStyleBackColor = true;
            this.buttonAddData.Click += new System.EventHandler( this.buttonAddData_Click );
            // 
            // textBoxAddTableID
            // 
            this.textBoxAddTableID.Location = new System.Drawing.Point( 103, 26 );
            this.textBoxAddTableID.Name = "textBoxAddTableID";
            this.textBoxAddTableID.Size = new System.Drawing.Size( 99, 20 );
            this.textBoxAddTableID.TabIndex = 3;
            this.textBoxAddTableID.Text = "12";
            this.textBoxAddTableID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point( 48, 29 );
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size( 48, 13 );
            this.label15.TabIndex = 2;
            this.label15.Text = "Table ID";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add( this.textBoxPropertiesMAxRecords );
            this.groupBox3.Controls.Add( this.textBoxPropertiesNumRecords );
            this.groupBox3.Controls.Add( this.label12 );
            this.groupBox3.Controls.Add( this.label11 );
            this.groupBox3.Controls.Add( this.button2 );
            this.groupBox3.Controls.Add( this.textBoxReadPropertiesTableID );
            this.groupBox3.Controls.Add( this.label10 );
            this.groupBox3.Location = new System.Drawing.Point( 31, 116 );
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size( 530, 91 );
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Table Properties";
            // 
            // textBoxPropertiesMAxRecords
            // 
            this.textBoxPropertiesMAxRecords.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPropertiesMAxRecords.Enabled = false;
            this.textBoxPropertiesMAxRecords.Location = new System.Drawing.Point( 173, 61 );
            this.textBoxPropertiesMAxRecords.Name = "textBoxPropertiesMAxRecords";
            this.textBoxPropertiesMAxRecords.Size = new System.Drawing.Size( 96, 20 );
            this.textBoxPropertiesMAxRecords.TabIndex = 8;
            // 
            // textBoxPropertiesNumRecords
            // 
            this.textBoxPropertiesNumRecords.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPropertiesNumRecords.Enabled = false;
            this.textBoxPropertiesNumRecords.Location = new System.Drawing.Point( 50, 61 );
            this.textBoxPropertiesNumRecords.Name = "textBoxPropertiesNumRecords";
            this.textBoxPropertiesNumRecords.Size = new System.Drawing.Size( 96, 20 );
            this.textBoxPropertiesNumRecords.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point( 48, 44 );
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size( 99, 13 );
            this.label12.TabIndex = 6;
            this.label12.Text = "Number of Records";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point( 170, 44 );
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size( 94, 13 );
            this.label11.TabIndex = 5;
            this.label11.Text = "Maximum Records";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point( 421, 21 );
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size( 98, 23 );
            this.button2.TabIndex = 4;
            this.button2.Text = "Read Properties";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler( this.button2_Click );
            // 
            // textBoxReadPropertiesTableID
            // 
            this.textBoxReadPropertiesTableID.Location = new System.Drawing.Point( 103, 21 );
            this.textBoxReadPropertiesTableID.Name = "textBoxReadPropertiesTableID";
            this.textBoxReadPropertiesTableID.Size = new System.Drawing.Size( 99, 20 );
            this.textBoxReadPropertiesTableID.TabIndex = 3;
            this.textBoxReadPropertiesTableID.Text = "12";
            this.textBoxReadPropertiesTableID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point( 48, 24 );
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size( 48, 13 );
            this.label10.TabIndex = 2;
            this.label10.Text = "Table ID";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add( this.button1 );
            this.groupBox2.Controls.Add( this.textBoxRecordSize );
            this.groupBox2.Controls.Add( this.label9 );
            this.groupBox2.Controls.Add( this.textBoxMaxRecords );
            this.groupBox2.Controls.Add( this.label8 );
            this.groupBox2.Controls.Add( this.textBoxTableID );
            this.groupBox2.Controls.Add( this.label7 );
            this.groupBox2.Location = new System.Drawing.Point( 31, 6 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size( 530, 104 );
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Table";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 421, 71 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 98, 23 );
            this.button1.TabIndex = 6;
            this.button1.Text = "Create Table";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler( this.button1_Click );
            // 
            // textBoxRecordSize
            // 
            this.textBoxRecordSize.Location = new System.Drawing.Point( 104, 71 );
            this.textBoxRecordSize.Name = "textBoxRecordSize";
            this.textBoxRecordSize.Size = new System.Drawing.Size( 289, 20 );
            this.textBoxRecordSize.TabIndex = 5;
            this.textBoxRecordSize.Text = "255,255,255,255";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point( 28, 74 );
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size( 65, 13 );
            this.label9.TabIndex = 4;
            this.label9.Text = "Record Size";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxMaxRecords
            // 
            this.textBoxMaxRecords.Location = new System.Drawing.Point( 104, 45 );
            this.textBoxMaxRecords.Name = "textBoxMaxRecords";
            this.textBoxMaxRecords.Size = new System.Drawing.Size( 99, 20 );
            this.textBoxMaxRecords.TabIndex = 3;
            this.textBoxMaxRecords.Text = "255";
            this.textBoxMaxRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxMaxRecords.TextChanged += new System.EventHandler( this.textBox1_TextChanged );
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point( 28, 48 );
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size( 70, 13 );
            this.label8.TabIndex = 2;
            this.label8.Text = "Max Records";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Click += new System.EventHandler( this.label8_Click );
            // 
            // textBoxTableID
            // 
            this.textBoxTableID.Location = new System.Drawing.Point( 104, 19 );
            this.textBoxTableID.Name = "textBoxTableID";
            this.textBoxTableID.Size = new System.Drawing.Size( 99, 20 );
            this.textBoxTableID.TabIndex = 1;
            this.textBoxTableID.Text = "12";
            this.textBoxTableID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point( 49, 22 );
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size( 48, 13 );
            this.label7.TabIndex = 0;
            this.label7.Text = "Table ID";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Click += new System.EventHandler( this.label7_Click );
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point( 0, 0 );
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size( 875, 253 );
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Enabled = false;
            this.toolStrip2.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSleep,
            this.toolStripSeparator17,
            this.toolStripSeparator4,
            this.toolStripButtonSleepAllbut,
            this.toolStripSeparator12,
            this.toolStripSeparator5,
            this.toolStripButtonReadRoutingCode,
            this.toolStripSeparator8,
            this.toolStripButtonWriteRoutingCode,
            this.toolStripSeparator9,
            this.toolStripButtonBeeperON,
            this.toolStripSeparator10,
            this.toolStripButtonBeeperOFF,
            this.toolStripSeparator16,
            this.toolStripButtonClearMemory,
            this.toolStripSeparator3} );
            this.toolStrip2.Location = new System.Drawing.Point( 0, 3 );
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size( 114, 219 );
            this.toolStrip2.TabIndex = 0;
            // 
            // toolStripButtonSleep
            // 
            this.toolStripButtonSleep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSleep.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonSleep.Image" ) ) );
            this.toolStripButtonSleep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSleep.Name = "toolStripButtonSleep";
            this.toolStripButtonSleep.Size = new System.Drawing.Size( 112, 19 );
            this.toolStripButtonSleep.Text = "Sleep";
            this.toolStripButtonSleep.Click += new System.EventHandler( this.toolStripButtonSleep_Click );
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripButtonSleepAllbut
            // 
            this.toolStripButtonSleepAllbut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSleepAllbut.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonSleepAllbut.Image" ) ) );
            this.toolStripButtonSleepAllbut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSleepAllbut.Name = "toolStripButtonSleepAllbut";
            this.toolStripButtonSleepAllbut.Size = new System.Drawing.Size( 112, 19 );
            this.toolStripButtonSleepAllbut.Text = "Sleep All but";
            this.toolStripButtonSleepAllbut.Click += new System.EventHandler( this.toolStripButtonSleepAllbut_Click );
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripButtonReadRoutingCode
            // 
            this.toolStripButtonReadRoutingCode.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonReadRoutingCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonReadRoutingCode.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonReadRoutingCode.Image" ) ) );
            this.toolStripButtonReadRoutingCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReadRoutingCode.Name = "toolStripButtonReadRoutingCode";
            this.toolStripButtonReadRoutingCode.Size = new System.Drawing.Size( 112, 19 );
            this.toolStripButtonReadRoutingCode.Text = "Read Routing code";
            this.toolStripButtonReadRoutingCode.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButtonReadRoutingCode.Click += new System.EventHandler( this.toolStripButtonReadRoutingCode_Click );
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripButtonWriteRoutingCode
            // 
            this.toolStripButtonWriteRoutingCode.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonWriteRoutingCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonWriteRoutingCode.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonWriteRoutingCode.Image" ) ) );
            this.toolStripButtonWriteRoutingCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonWriteRoutingCode.Name = "toolStripButtonWriteRoutingCode";
            this.toolStripButtonWriteRoutingCode.Size = new System.Drawing.Size( 112, 19 );
            this.toolStripButtonWriteRoutingCode.Text = "Write Routing code";
            this.toolStripButtonWriteRoutingCode.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButtonWriteRoutingCode.Click += new System.EventHandler( this.toolStripButtonWriteRoutingCode_Click );
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripButtonBeeperON
            // 
            this.toolStripButtonBeeperON.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonBeeperON.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonBeeperON.Image" ) ) );
            this.toolStripButtonBeeperON.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBeeperON.Name = "toolStripButtonBeeperON";
            this.toolStripButtonBeeperON.Size = new System.Drawing.Size( 112, 19 );
            this.toolStripButtonBeeperON.Text = "Beeper ON";
            this.toolStripButtonBeeperON.Click += new System.EventHandler( this.toolStripButtonBeeperON_Click );
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripButtonBeeperOFF
            // 
            this.toolStripButtonBeeperOFF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonBeeperOFF.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonBeeperOFF.Image" ) ) );
            this.toolStripButtonBeeperOFF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBeeperOFF.Name = "toolStripButtonBeeperOFF";
            this.toolStripButtonBeeperOFF.Size = new System.Drawing.Size( 112, 19 );
            this.toolStripButtonBeeperOFF.Text = "Beeper OFF";
            this.toolStripButtonBeeperOFF.Click += new System.EventHandler( this.toolStripButtonBeeperOFF_Click );
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size( 112, 6 );
            // 
            // toolStripButtonClearMemory
            // 
            this.toolStripButtonClearMemory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonClearMemory.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonClearMemory.Image" ) ) );
            this.toolStripButtonClearMemory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearMemory.Name = "toolStripButtonClearMemory";
            this.toolStripButtonClearMemory.Size = new System.Drawing.Size( 112, 19 );
            this.toolStripButtonClearMemory.Text = "Clear Memory";
            this.toolStripButtonClearMemory.Click += new System.EventHandler( this.toolStripButton1_Click );
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size( 112, 6 );
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.helpToolStripMenuItem} );
            this.menuStrip2.Location = new System.Drawing.Point( 0, 0 );
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.ShowItemToolTips = true;
            this.menuStrip2.Size = new System.Drawing.Size( 989, 24 );
            this.menuStrip2.TabIndex = 10;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem} );
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size( 37, 20 );
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size( 92, 22 );
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler( this.exitToolStripMenuItem_Click );
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.routingCodeToolStripMenuItem,
            this.collectionWindowSizeToolStripMenuItem,
            this.displayToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripComboBoxWakeUp} );
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size( 56, 20 );
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // routingCodeToolStripMenuItem
            // 
            this.routingCodeToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxRoutingCode} );
            this.routingCodeToolStripMenuItem.Name = "routingCodeToolStripMenuItem";
            this.routingCodeToolStripMenuItem.Size = new System.Drawing.Size( 197, 22 );
            this.routingCodeToolStripMenuItem.Text = "Routing Code";
            // 
            // toolStripTextBoxRoutingCode
            // 
            this.toolStripTextBoxRoutingCode.Name = "toolStripTextBoxRoutingCode";
            this.toolStripTextBoxRoutingCode.Size = new System.Drawing.Size( 100, 23 );
            this.toolStripTextBoxRoutingCode.Text = "5A5";
            // 
            // collectionWindowSizeToolStripMenuItem
            // 
            this.collectionWindowSizeToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxWindowSize} );
            this.collectionWindowSizeToolStripMenuItem.Name = "collectionWindowSizeToolStripMenuItem";
            this.collectionWindowSizeToolStripMenuItem.Size = new System.Drawing.Size( 197, 22 );
            this.collectionWindowSizeToolStripMenuItem.Text = "Collection Window size";
            // 
            // toolStripTextBoxWindowSize
            // 
            this.toolStripTextBoxWindowSize.Name = "toolStripTextBoxWindowSize";
            this.toolStripTextBoxWindowSize.Size = new System.Drawing.Size( 100, 23 );
            this.toolStripTextBoxWindowSize.Text = "12";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.manufacturerIDToolStripMenuItem,
            this.uDBToolStripMenuItem,
            this.routingCodeToolStripMenuItem1,
            this.rSSIToolStripMenuItem} );
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size( 197, 22 );
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // manufacturerIDToolStripMenuItem
            // 
            this.manufacturerIDToolStripMenuItem.Checked = true;
            this.manufacturerIDToolStripMenuItem.CheckOnClick = true;
            this.manufacturerIDToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.manufacturerIDToolStripMenuItem.Name = "manufacturerIDToolStripMenuItem";
            this.manufacturerIDToolStripMenuItem.Size = new System.Drawing.Size( 160, 22 );
            this.manufacturerIDToolStripMenuItem.Text = "Manufacturer ID";
            this.manufacturerIDToolStripMenuItem.CheckStateChanged += new System.EventHandler( this.manufacturerIDToolStripMenuItem_CheckStateChanged );
            // 
            // uDBToolStripMenuItem
            // 
            this.uDBToolStripMenuItem.Checked = true;
            this.uDBToolStripMenuItem.CheckOnClick = true;
            this.uDBToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uDBToolStripMenuItem.Name = "uDBToolStripMenuItem";
            this.uDBToolStripMenuItem.Size = new System.Drawing.Size( 160, 22 );
            this.uDBToolStripMenuItem.Text = "User ID";
            this.uDBToolStripMenuItem.CheckStateChanged += new System.EventHandler( this.uDBToolStripMenuItem_CheckStateChanged );
            // 
            // routingCodeToolStripMenuItem1
            // 
            this.routingCodeToolStripMenuItem1.Checked = true;
            this.routingCodeToolStripMenuItem1.CheckOnClick = true;
            this.routingCodeToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.routingCodeToolStripMenuItem1.Name = "routingCodeToolStripMenuItem1";
            this.routingCodeToolStripMenuItem1.Size = new System.Drawing.Size( 160, 22 );
            this.routingCodeToolStripMenuItem1.Text = "Routing code";
            this.routingCodeToolStripMenuItem1.CheckStateChanged += new System.EventHandler( this.routingCodeToolStripMenuItem1_CheckStateChanged );
            // 
            // rSSIToolStripMenuItem
            // 
            this.rSSIToolStripMenuItem.Checked = true;
            this.rSSIToolStripMenuItem.CheckOnClick = true;
            this.rSSIToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rSSIToolStripMenuItem.Name = "rSSIToolStripMenuItem";
            this.rSSIToolStripMenuItem.Size = new System.Drawing.Size( 160, 22 );
            this.rSSIToolStripMenuItem.Text = "RSSI";
            this.rSSIToolStripMenuItem.CheckStateChanged += new System.EventHandler( this.rSSIToolStripMenuItem_CheckStateChanged );
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxMaxPacketLength} );
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size( 197, 22 );
            this.toolStripMenuItem1.Text = "Max packet Lenght";
            // 
            // toolStripTextBoxMaxPacketLength
            // 
            this.toolStripTextBoxMaxPacketLength.Name = "toolStripTextBoxMaxPacketLength";
            this.toolStripTextBoxMaxPacketLength.Size = new System.Drawing.Size( 100, 23 );
            this.toolStripTextBoxMaxPacketLength.Text = "30";
            this.toolStripTextBoxMaxPacketLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.toolStripTextBoxMaxPacketLength_KeyPress );
            // 
            // toolStripComboBoxWakeUp
            // 
            this.toolStripComboBoxWakeUp.Items.AddRange( new object[] {
            "Auto wake_up",
            "Always Wake_up",
            "Never Wake_up"} );
            this.toolStripComboBoxWakeUp.Name = "toolStripComboBoxWakeUp";
            this.toolStripComboBoxWakeUp.Size = new System.Drawing.Size( 121, 23 );
            this.toolStripComboBoxWakeUp.SelectedIndexChanged += new System.EventHandler( this.toolStripComboBoxWakeUp_SelectedIndexChanged );
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem} );
            this.helpToolStripMenuItem.Image = global::ISO_Demo.Properties.Resources.Symbols_Help_icon;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size( 60, 20 );
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size( 107, 22 );
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler( this.aboutToolStripMenuItem_Click );
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonConnect,
            this.toolStripSeparator1,
            this.toolStripButtonDisconnect,
            this.toolStripSeparator20,
            this.toolStripButtonClearTagList,
            this.toolStripSeparator2,
            this.toolStripButtonCollection2,
            this.toolStripSeparator18,
            this.toolStripSeparator19,
            this.toolStripLabel1,
            this.toolStripSeparator21,
            this.toolStripComboBoxSerialPort,
            this.toolStripSeparator22,
            this.toolStripButtonAbout} );
            this.toolStrip1.Location = new System.Drawing.Point( 3, 24 );
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size( 635, 25 );
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripButtonConnect
            // 
            this.toolStripButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonConnect.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonConnect.Image" ) ) );
            this.toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size( 56, 22 );
            this.toolStripButtonConnect.Text = "Connect";
            this.toolStripButtonConnect.Click += new System.EventHandler( this.toolStripButtonConnect_Click );
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripButtonDisconnect
            // 
            this.toolStripButtonDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDisconnect.Enabled = false;
            this.toolStripButtonDisconnect.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonDisconnect.Image" ) ) );
            this.toolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            this.toolStripButtonDisconnect.Size = new System.Drawing.Size( 70, 22 );
            this.toolStripButtonDisconnect.Text = "Disconnect";
            this.toolStripButtonDisconnect.Click += new System.EventHandler( this.toolStripButtonDisconnect_Click );
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripButtonClearTagList
            // 
            this.toolStripButtonClearTagList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonClearTagList.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonClearTagList.Image" ) ) );
            this.toolStripButtonClearTagList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearTagList.Name = "toolStripButtonClearTagList";
            this.toolStripButtonClearTagList.Size = new System.Drawing.Size( 82, 22 );
            this.toolStripButtonClearTagList.Text = "Clear Tag List";
            this.toolStripButtonClearTagList.Click += new System.EventHandler( this.toolStripButtonClearTagList_Click );
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripButtonCollection2
            // 
            this.toolStripButtonCollection2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCollection2.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonCollection2.Image" ) ) );
            this.toolStripButtonCollection2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCollection2.Name = "toolStripButtonCollection2";
            this.toolStripButtonCollection2.Size = new System.Drawing.Size( 65, 22 );
            this.toolStripButtonCollection2.Text = "Collection";
            this.toolStripButtonCollection2.Click += new System.EventHandler( this.toolStripButtonCollection2_Click );
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Margin = new System.Windows.Forms.Padding( 50, 0, 0, 0 );
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size( 75, 22 );
            this.toolStripLabel1.Text = "Comm Port :";
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripComboBoxSerialPort
            // 
            this.toolStripComboBoxSerialPort.Name = "toolStripComboBoxSerialPort";
            this.toolStripComboBoxSerialPort.Size = new System.Drawing.Size( 121, 25 );
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size( 6, 25 );
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.Image = ( (System.Drawing.Image)( resources.GetObject( "toolStripButtonAbout.Image" ) ) );
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size( 60, 22 );
            this.toolStripButtonAbout.Text = "About";
            this.toolStripButtonAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButtonAbout.Click += new System.EventHandler( this.aboutToolStripMenuItem_Click );
            // 
            // iSO18000TagBindingSource
            // 
            this.iSO18000TagBindingSource.DataSource = typeof( IDENTEC.Tags.ISO18000Tag );
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size( 989, 816 );
            this.Controls.Add( this.toolStripContainer1 );
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject( "$this.Icon" ) ) );
            this.Name = "Form1";
            this.Text = "ISODemo -- Demonstration application for RFID ISO 18000-7 standard";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout( false );
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout( );
            this.toolStripContainer1.ContentPanel.ResumeLayout( false );
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout( false );
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout( );
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout( false );
            this.toolStripContainer1.TopToolStripPanel.PerformLayout( );
            this.toolStripContainer1.ResumeLayout( false );
            this.toolStripContainer1.PerformLayout( );
            this.statusStrip1.ResumeLayout( false );
            this.statusStrip1.PerformLayout( );
            this.splitContainer1.Panel1.ResumeLayout( false );
            this.splitContainer1.Panel2.ResumeLayout( false );
            this.splitContainer1.ResumeLayout( false );
            this.splitContainer2.Panel1.ResumeLayout( false );
            this.splitContainer2.Panel2.ResumeLayout( false );
            this.splitContainer2.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataTable1BindingSource1 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dsTags1BindingSource ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dsTags1 ) ).EndInit( );
            this.tabControl1.ResumeLayout( false );
            this.tabPage1.ResumeLayout( false );
            this.tabPage1.PerformLayout( );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDownAddress ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.numericUpDownNbByte ) ).EndInit( );
            this.tabPage4.ResumeLayout( false );
            this.splitContainer3.Panel1.ResumeLayout( false );
            this.splitContainer3.Panel1.PerformLayout( );
            this.splitContainer3.Panel2.ResumeLayout( false );
            this.splitContainer3.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView2 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataSetUDBElementsBindingSource ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataSetUDBElements ) ).EndInit( );
            this.tabPage5.ResumeLayout( false );
            this.groupBox5.ResumeLayout( false );
            this.groupBox5.PerformLayout( );
            this.groupBox4.ResumeLayout( false );
            this.groupBox4.PerformLayout( );
            this.groupBox3.ResumeLayout( false );
            this.groupBox3.PerformLayout( );
            this.groupBox2.ResumeLayout( false );
            this.groupBox2.PerformLayout( );
            this.toolStrip2.ResumeLayout( false );
            this.toolStrip2.PerformLayout( );
            this.menuStrip2.ResumeLayout( false );
            this.menuStrip2.PerformLayout( );
            this.toolStrip1.ResumeLayout( false );
            this.toolStrip1.PerformLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.iSO18000TagBindingSource ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnection;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSerialPort;
        private System.Windows.Forms.BindingSource iSO18000TagBindingSource;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCommand;
        private dsTags dsTags1;
        private System.Windows.Forms.BindingSource dataTable1BindingSource1;
        private System.Windows.Forms.BindingSource dsTags1BindingSource;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonSleepAllbut;
        private System.Windows.Forms.ToolStripButton toolStripButtonSleep;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.NumericUpDown numericUpDownAddress;
        private System.Windows.Forms.TextBox textBoxMemoryWritten;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownNbByte;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelNbTags;
        private System.Windows.Forms.ToolStripButton toolStripButtonReadRoutingCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButtonWriteRoutingCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripButtonBeeperON;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton toolStripButtonBeeperOFF;
        private System.Windows.Forms.TextBox textBoxMemoryRead;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routingCodeToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxRoutingCode;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manufacturerIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routingCodeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rSSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearTagList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripButton toolStripButtonAbout;
        private System.Windows.Forms.ToolStripMenuItem collectionWindowSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxWindowSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMaxPacketLength;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxWakeUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn manufacturerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sNDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uDBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn routingCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rSSIDataGridViewTextBoxColumn;
        private HexEditBox richTextBox1;
        private System.Windows.Forms.CheckBox checkBoxUseASCII;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button buttonWriteMemory;
        private System.Windows.Forms.Button buttonReadMemory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonCollection2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMaxPackageLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonReadUDB;
        private System.Windows.Forms.ComboBox comboBoxUDBType;
        private System.Windows.Forms.BindingSource dataSetUDBElementsBindingSource;
        private DataSetUDBElements dataSetUDBElements;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Length;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn parameterDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxRecordSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxMaxRecords;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxTableID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxReadPropertiesTableID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxPropertiesNumRecords;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxPropertiesMAxRecords;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonAddData;
        private System.Windows.Forms.TextBox textBoxAddTableID;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonRandom;
        private System.Windows.Forms.TextBox textBoxRowData;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxAddRecordCount;
        private System.Windows.Forms.TextBox textBoxAddRecordSize;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearMemory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonReadTableData;
        private System.Windows.Forms.TextBox textBoxReadTableDataTableID;
        private System.Windows.Forms.Label label20;
    }
}

