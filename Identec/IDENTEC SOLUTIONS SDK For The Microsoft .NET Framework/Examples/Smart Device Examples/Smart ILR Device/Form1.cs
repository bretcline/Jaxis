/* COPYRIGHT AND COMPANY INFORMATION ****************************************************************************
 *  
 * Copyright (c) 2006 by IDENTEC SOLUTIONS Inc.
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
using IDENTEC;
using IDENTEC.Readers;
using IDENTEC.Tags;


namespace Smart_ILR_Device_Test
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button buttonScan;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.ListBox listBoxTags;
		private System.Windows.Forms.MainMenu mainMenu1;		
		private System.Windows.Forms.Label labelTags;
		private System.Windows.Forms.RadioButton radioButtonCF;
		private System.Windows.Forms.RadioButton radioButtonPCMCIA;
		private System.Windows.Forms.MenuItem menuItem2;
		private iCard m_reader;		

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();		

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.buttonScan = new System.Windows.Forms.Button();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.listBoxTags = new System.Windows.Forms.ListBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.labelTags = new System.Windows.Forms.Label();
			this.radioButtonCF = new System.Windows.Forms.RadioButton();
			this.radioButtonPCMCIA = new System.Windows.Forms.RadioButton();
			// 
			// buttonScan
			// 
			this.buttonScan.Location = new System.Drawing.Point(19, 28);
			this.buttonScan.Text = "Scan";
			this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
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
			// listBoxTags
			// 
			this.listBoxTags.Location = new System.Drawing.Point(5, 51);
			this.listBoxTags.Size = new System.Drawing.Size(100, 145);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "";
			// 
			// labelTags
			// 
			this.labelTags.Location = new System.Drawing.Point(6, 210);
			this.labelTags.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// radioButtonCF
			// 
			this.radioButtonCF.Checked = true;
			this.radioButtonCF.Location = new System.Drawing.Point(111, 53);
			this.radioButtonCF.Size = new System.Drawing.Size(103, 20);
			this.radioButtonCF.Text = "Compact Flash";
			this.radioButtonCF.CheckedChanged += new System.EventHandler(this.radioButtonCF_CheckedChanged);
			// 
			// radioButtonPCMCIA
			// 
			this.radioButtonPCMCIA.Location = new System.Drawing.Point(111, 75);
			this.radioButtonPCMCIA.Text = "PCMCIA";
			this.radioButtonPCMCIA.CheckedChanged += new System.EventHandler(this.radioButtonPCMCIA_CheckedChanged);
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(227, 321);
			this.Controls.Add(this.radioButtonPCMCIA);
			this.Controls.Add(this.radioButtonCF);
			this.Controls.Add(this.labelTags);
			this.Controls.Add(this.listBoxTags);
			this.Controls.Add(this.buttonScan);
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "Smart ILR Device Example";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();	
		}
		

		private void buttonScan_Click(object sender, System.EventArgs e)
		{
			Scan();
		}

		private void Scan()
		{
			Cursor.Current = Cursors.WaitCursor;
			labelTags.Text = "";
			try
			{
				listBoxTags.DataSource = null;

				if (null == m_reader)
					Connect();

				if (null != m_reader)
				{
					ITagReaderIQ iqReader = m_reader as ITagReaderIQ;
					TagCollection tags = iqReader.ScanForIQTags(64, true);				
					tags.Sort();
					listBoxTags.DataSource = tags;
					labelTags.Text = tags.Count + " tags";
				}
			}
				//Note that you may a have longer lasting battery if you close the connection to the card when not in use
			catch (iCardCommunicationsException)
			{
				try
				{
					Connect();
					Scan();
				}
				catch (Exception)
				{
					MessageBox.Show("Could not connect to the selected card");
					return;
				}			
			}
			catch (CommPortException)
			{				
				try
				{
					Connect();
					Scan();
				}
				catch (Exception connectEx)
				{
					MessageBox.Show("Could not connect to the selected card");
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}			

		}

		private void Connect()
		{
			if (m_reader != null)
				((IDisposable)(m_reader)).Dispose();

			m_reader = null;
				
			if (radioButtonCF.Checked)
				ConnectCF();
			else
				ConnectPCMCIA();
		}

		private void ConnectCF()
		{			
			try
			{
				if (m_reader == null)				
				{				
					m_reader = new iCardCF();
				}	

				iCardCF cf = m_reader as iCardCF;
				if (cf.Connected)
					cf.Disconnect();			
				
				int i = CFReaderSearch.FindReaderComPort();
				if (i > 0)
					cf.Connect(i);
				else
					MessageBox.Show("NO CF Card could be found!");						
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
		}

		private void ConnectPCMCIA()
		{			
			iCard3 pc = new iCard3();
			pc.Connect();
			m_reader = pc;			
		}

		private void radioButtonCF_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		private void radioButtonPCMCIA_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}
	}

	}

