/* COPYRIGHT AND COMPANY INFORMATION ****************************************************************************
 *  
 * Copyright (c) 2006 by IDENTEC SOLUTIONS.
 *
 * This Copyright notice may not be removed or modified without prior written consent of Identec Solutions.
 * Identec Solutions, reserves the right to modify this software without notice.
 * 
 *
 * IDENTEC Solutions, Inc.
 * www.identecsolutions.com                  
 *
 * IDENTEC SOLUTIONS Inc. grants you a nonexclusive copyright license to use all programming code examples 
 * from which you can generate similar function tailored to your own specific needs.
 *
 * All sample code is provided by IDENTEC SOLUTIONS Inc. for illustrative purposes only. 
 * These examples have not been thoroughly tested under all conditions. IDENTEC SOLUTIONS Inc., therefore, 
 * cannot guarantee or imply reliability, serviceability, or function of these programs.

 * All programs contained herein are provided to you "AS IS" without any warranties of any kind. 
 * The implied warranties of non-infringement, 
 * merchantability and fitness for a particular purpose are expressly disclaimed.

 ****************************************************************************************************************/


using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

#region >>>>> IDENTEC Specific Using Statements <<<<<
using IDENTEC;
using IDENTEC.Readers;
using IDENTEC.Readers.BeaconReaders;
using IDENTEC.Tags;
using IDENTEC.Tags.BeaconTags;
#endregion


namespace Smart_ILR_Beacon_Device_Application
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.ColumnHeader columnHeaderTags;
		private System.Windows.Forms.ColumnHeader columnHeaderTime;
		private System.Windows.Forms.StatusBar statusBarMain;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownQueryInterval;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Timer timerGetTags;
		private System.Windows.Forms.MenuItem menuItemStart;
		private System.Windows.Forms.MenuItem menuItemStop;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.ListView listViewTags;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnTagID;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumnTime;

		#region >>>>> Member Variables <<<<<
		private iCardCFB m_reader;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Label labelTagCount;
		private System.Windows.Forms.MenuItem menuItemClearTags;

		/// <summary>
		/// The lookup key is the tag serial number and the item is the list view item
		/// </summary>
		Hashtable m_htTagsToListViewItems = new Hashtable();
		#endregion
	
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//The one and only reader object
			m_reader = new iCardCFB();

			//TODO: Enable sorting the tags when the header for tag or time is clicked		
			//TODO: persist the com port value used			

		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.labelTagCount = new System.Windows.Forms.Label();
			this.listViewTags = new System.Windows.Forms.ListView();
			this.columnHeaderTags = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderTime = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownQueryInterval = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemClearTags = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItemStart = new System.Windows.Forms.MenuItem();
			this.menuItemStop = new System.Windows.Forms.MenuItem();
			this.statusBarMain = new System.Windows.Forms.StatusBar();
			this.timerGetTags = new System.Windows.Forms.Timer();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumnTagID = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumnTime = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(7, 32);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(227, 244);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.labelTagCount);
			this.tabPage1.Controls.Add(this.listViewTags);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Size = new System.Drawing.Size(219, 218);
			this.tabPage1.Text = "Tags";
			// 
			// labelTagCount
			// 
			this.labelTagCount.Location = new System.Drawing.Point(59, 4);
			this.labelTagCount.Size = new System.Drawing.Size(100, 17);
			this.labelTagCount.Text = "0 Tags";
			this.labelTagCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// listViewTags
			// 
			this.listViewTags.Columns.Add(this.columnHeaderTags);
			this.listViewTags.Columns.Add(this.columnHeaderTime);
			this.listViewTags.FullRowSelect = true;
			this.listViewTags.Location = new System.Drawing.Point(3, 32);
			this.listViewTags.Size = new System.Drawing.Size(210, 182);
			this.listViewTags.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderTags
			// 
			this.columnHeaderTags.Text = "Tag";
			this.columnHeaderTags.Width = 100;
			// 
			// columnHeaderTime
			// 
			this.columnHeaderTime.Text = "Time";
			this.columnHeaderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeaderTime.Width = 100;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.numericUpDownQueryInterval);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Size = new System.Drawing.Size(219, 218);
			this.tabPage2.Text = "Options";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(85, 29);
			this.label3.Size = new System.Drawing.Size(50, 20);
			this.label3.Text = "seconds";
			// 
			// numericUpDownQueryInterval
			// 
			this.numericUpDownQueryInterval.Increment = new System.Decimal(new int[] {
																						 10,
																						 0,
																						 0,
																						 0});
			this.numericUpDownQueryInterval.Location = new System.Drawing.Point(6, 29);
			this.numericUpDownQueryInterval.Maximum = new System.Decimal(new int[] {
																					   600,
																					   0,
																					   0,
																					   0});
			this.numericUpDownQueryInterval.Minimum = new System.Decimal(new int[] {
																					   2,
																					   0,
																					   0,
																					   0});
			this.numericUpDownQueryInterval.Size = new System.Drawing.Size(68, 20);
			this.numericUpDownQueryInterval.Value = new System.Decimal(new int[] {
																					 10,
																					 0,
																					 0,
																					 0});
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 10);
			this.label2.Size = new System.Drawing.Size(127, 20);
			this.label2.Text = "Query Reader Every:";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			this.mainMenu1.MenuItems.Add(this.menuItem3);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItemExit);
			this.menuItem1.Text = "&File";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItemClearTags);
			this.menuItem2.Text = "&Edit";
			// 
			// menuItemClearTags
			// 
			this.menuItemClearTags.Text = "&Clear!";
			this.menuItemClearTags.Click += new System.EventHandler(this.menuItemClearTags_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.MenuItems.Add(this.menuItemStart);
			this.menuItem3.MenuItems.Add(this.menuItemStop);
			this.menuItem3.Text = "Action";
			// 
			// menuItemStart
			// 
			this.menuItemStart.Text = "Start!";
			this.menuItemStart.Click += new System.EventHandler(this.menuItemStart_Click);
			// 
			// menuItemStop
			// 
			this.menuItemStop.Text = "Stop!";
			this.menuItemStop.Click += new System.EventHandler(this.menuItemStop_Click);
			// 
			// statusBarMain
			// 
			this.statusBarMain.Location = new System.Drawing.Point(0, 386);
			this.statusBarMain.Size = new System.Drawing.Size(239, 22);
			this.statusBarMain.Text = "Ready";
			// 
			// timerGetTags
			// 
			this.timerGetTags.Tick += new System.EventHandler(this.timerGetTags_Tick);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnTagID);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumnTime);
			// 
			// dataGridTextBoxColumnTagID
			// 
			this.dataGridTextBoxColumnTagID.HeaderText = "Tag";
			this.dataGridTextBoxColumnTagID.NullText = "(null)";
			// 
			// dataGridTextBoxColumnTime
			// 
			this.dataGridTextBoxColumnTime.HeaderText = "Time";
			this.dataGridTextBoxColumnTime.NullText = "(null)";
			this.dataGridTextBoxColumnTime.Width = 100;
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(239, 408);
			this.Controls.Add(this.statusBarMain);
			this.Controls.Add(this.tabControl1);
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "Smart ILR Beacon";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItemStart_Click(object sender, System.EventArgs e)
		{
			if (QueryReaderForTags())
			{
				timerGetTags.Interval = (int)(numericUpDownQueryInterval.Value * 1000);
				timerGetTags.Enabled = true;
			}
		}

		private void menuItemStop_Click(object sender, System.EventArgs e)
		{
			timerGetTags.Enabled = false;
			statusBarMain.Text = "Stopped.";
		}

		private void timerGetTags_Tick(object sender, System.EventArgs e)
		{
			timerGetTags.Enabled = false;
			if (QueryReaderForTags())
			{
				timerGetTags.Enabled = true;
			}
		}

		private bool QueryReaderForTags()
		{
			statusBarMain.Text = "Listening for tags...";
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				if (!m_reader.Connected)
				{									
					if (!ConnectToReader())
						return false;
				}
				
				TagCollection tags = m_reader.GetTags(true);
				UpdateTagView(tags);
				//MessageBox.Show(tags.Count + " tags found!");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				m_reader.Disconnect();				
				statusBarMain.Text = "Stopped.";
				return false;
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
			return true;
			
		}

		private void UpdateTagView(TagCollection tags)
		{
			//TODO: create a filter or option so that tags that are old are discarded from the list
			foreach (iB2Tag t in tags)
			{
				ListViewItem lvi = null;
				lvi = m_htTagsToListViewItems[t.Number] as ListViewItem;

				if (lvi == null)
				{
					string [] strItems = new string [] {t.Label, t.ContactTime.ToLongTimeString()};
					lvi = new ListViewItem(strItems);
					listViewTags.Items.Add(lvi);
					m_htTagsToListViewItems[t.Number] = lvi;
				}
				else
				{
					//update the time
					lvi.SubItems[1].Text = t.ContactTime.ToLongTimeString();					
				}
			}

			labelTagCount.Text = listViewTags.Items.Count + " Tags";
		}
		private bool ConnectToReader()
		{
			if (m_reader.Connected)
				return true;

			try
			{
				int port = CFReaderSearch.FindReaderComPort();
				m_reader.Connect(port);
				//TODO: if you want the reader to be in short range, the next call should be set to false
				m_reader.EnableHighRfSensitivity(true);
				m_reader.SetTagListBehavior(TagListBehavior.RemoveTagsWhenReported);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

			return false;
		}

		private void menuItemClearTags_Click(object sender, System.EventArgs e)
		{
			listViewTags.Clear();
			m_htTagsToListViewItems.Clear();
			labelTagCount.Text = "0 Tags";
		}
	}
}
