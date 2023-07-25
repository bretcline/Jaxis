/* 
 *  
 * Copyright (c) 2006 by Identec Solutions.
 *
 * This Copyright notice may not be removed or modified without prior written consent of Identec Solutions.
 * Identec Solutions, reserves the right to modify this software without notice.
 * 
 *
 * IDENTEC Solutions, Inc.
 *
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
 */



using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using IDENTEC;
using IDENTEC.Readers;
using IDENTEC.Tags;

namespace Basic_ILR_Tutorial
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonScan;
		private System.Windows.Forms.ListBox listBoxTags;
		private System.Windows.Forms.Button buttonPingTag;
		private System.Windows.Forms.TextBox textBoxWriteData;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonWrite;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonRead;
		private System.Windows.Forms.TextBox textBoxRead;

		private iCard3 m_iCard3;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_iCard3 = new iCard3();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonScan = new System.Windows.Forms.Button();
			this.listBoxTags = new System.Windows.Forms.ListBox();
			this.buttonPingTag = new System.Windows.Forms.Button();
			this.textBoxWriteData = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonWrite = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonRead = new System.Windows.Forms.Button();
			this.textBoxRead = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(139, 8);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.TabIndex = 0;
			this.buttonConnect.Text = "Connect!";
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// buttonScan
			// 
			this.buttonScan.Location = new System.Drawing.Point(139, 43);
			this.buttonScan.Name = "buttonScan";
			this.buttonScan.TabIndex = 1;
			this.buttonScan.Text = "Scan!";
			this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
			// 
			// listBoxTags
			// 
			this.listBoxTags.Location = new System.Drawing.Point(5, 4);
			this.listBoxTags.Name = "listBoxTags";
			this.listBoxTags.Size = new System.Drawing.Size(120, 238);
			this.listBoxTags.TabIndex = 2;
			// 
			// buttonPingTag
			// 
			this.buttonPingTag.Location = new System.Drawing.Point(138, 78);
			this.buttonPingTag.Name = "buttonPingTag";
			this.buttonPingTag.TabIndex = 3;
			this.buttonPingTag.Text = "Ping Tag!";
			this.buttonPingTag.Click += new System.EventHandler(this.buttonPingTag_Click);
			// 
			// textBoxWriteData
			// 
			this.textBoxWriteData.Location = new System.Drawing.Point(9, 21);
			this.textBoxWriteData.Multiline = true;
			this.textBoxWriteData.Name = "textBoxWriteData";
			this.textBoxWriteData.Size = new System.Drawing.Size(182, 49);
			this.textBoxWriteData.TabIndex = 4;
			this.textBoxWriteData.Text = "Hello ILR World!";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonWrite);
			this.groupBox1.Controls.Add(this.textBoxWriteData);
			this.groupBox1.Location = new System.Drawing.Point(239, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 109);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Write Data";
			// 
			// buttonWrite
			// 
			this.buttonWrite.Location = new System.Drawing.Point(63, 77);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.TabIndex = 5;
			this.buttonWrite.Text = "Write!";
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonRead);
			this.groupBox2.Controls.Add(this.textBoxRead);
			this.groupBox2.Location = new System.Drawing.Point(244, 121);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 109);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Read Data";
			// 
			// buttonRead
			// 
			this.buttonRead.Location = new System.Drawing.Point(63, 77);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.TabIndex = 5;
			this.buttonRead.Text = "Read!";
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// textBoxRead
			// 
			this.textBoxRead.Location = new System.Drawing.Point(9, 21);
			this.textBoxRead.Multiline = true;
			this.textBoxRead.Name = "textBoxRead";
			this.textBoxRead.ReadOnly = true;
			this.textBoxRead.Size = new System.Drawing.Size(182, 49);
			this.textBoxRead.TabIndex = 4;
			this.textBoxRead.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(473, 247);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonPingTag);
			this.Controls.Add(this.listBoxTags);
			this.Controls.Add(this.buttonScan);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.groupBox2);
			this.Name = "Form1";
			this.Text = "IDENTEC SOLUTIONS Basic ILR Tutorial";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void buttonConnect_Click(object sender, System.EventArgs e)
		{
			try
			{
				m_iCard3.Connect();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonScan_Click(object sender, System.EventArgs e)
		{
			try
			{
				listBoxTags.DataSource = null;
				// Scans the area for up to 64 tags and blinks each tag detected
				// Note that there is exactly enough room for 64 tags to respond in this call
				// So in reality you would want this number to be more than twice the actual number of
				// tags in the read zone.
				TagCollection tags = m_iCard3.ScanForIQTags(64, true);
				//Sort the tags numerically:
				tags.Sort();
				listBoxTags.DataSource = tags;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}			
		}

		private void buttonPingTag_Click(object sender, System.EventArgs e)
		{	
			try	
			{
				// We have to cast the selected item in the list box to the appropriate ILR object, an iQTag:
				iQTag t = listBoxTags.SelectedItem as iQTag;
				if (t == null)
				{
					MessageBox.Show("You must select a tag to ping!");
				}
				else
				{
					// Before making the call to ping, we could change the RF output of the i-CARD 3 if we want to change the size of the read zone			
					if (m_iCard3.PingTag(t))
					{
						MessageBox.Show("Ping Success!"); // TODO: set a breakpoint here and examine the iQTag object t
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonWrite_Click(object sender, System.EventArgs e)
		{
			try	
			{				
				iQTag t = listBoxTags.SelectedItem as iQTag;
				if (t == null)
				{
					MessageBox.Show("You must select a tag to write to!");
				}
				else
				{			
					TagWriteDataResult result = m_iCard3.WriteTagDataString(t, 132, textBoxWriteData.Text);
					if (result.Success)
					{
						MessageBox.Show("Data written to tag!"); 
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}		
		}



		private void buttonRead_Click(object sender, System.EventArgs e)
		{
			try	
			{				
				// Clear out the text box so we are sure it gets new data
				textBoxRead.Text = "";
				iQTag t = listBoxTags.SelectedItem as iQTag;
				if (t == null)
				{
					MessageBox.Show("You must select a tag to read from!");
				}
				else
				{			
					TagReadStringResult result = m_iCard3.ReadTagDataString(t, 132);
					if (result.Success)
					{
						textBoxRead.Text = result.Text;						
					}
					else
					{						
						// A quick and dirty error message from the reader itself will give us a clue to what went wrong:
						MessageBox.Show(m_iCard3.DeviceStatus.ToString(), "Could not read from tag.!"); 
					}
				}
			}
				// TODO: catch the appropriate exceptions such as a data format exception
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}		
		}



	}
}
