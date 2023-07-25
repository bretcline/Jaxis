/* COPYRIGHT AND COMPANY INFORMATION ****************************************************************************
 *  
 * Copyright (c) 2005 by Identec Solutions.
 *
 * This Copyright notice may not be removed or modified without prior written consent of Identec Solutions.
 * Identec Solutions, reserves the right to modify this software without notice.
 * 
 *
 * IDENTEC Solutions, Inc.
 *
 * (250) 860-6567; Fax: (250) 860-6541
 * 1860 Dayton Street  Suite 102
 * Kelowna, British Columbia, Canada V1Y 7W6
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

/**
 * 
 * Notes: You must download and install the Smart Device Framework from the OpenNetCF Community:
 * 
 * http://www.opennetcf.org
 *  
 * 
 * For help on intercepting Windows Messages in the Compact Framework see this article:
 * http://tinyurl.com/4aq6e  (this will help you intercept the iCard driver messages for driver notification events)
 * 
 * !!! Your Smart Device Application *must* handle the card initialize and unaviable events in order to work properly !!!
 * 
 * 
 * To test this sample, pop the card in and out of the PCMCIA slot. Alternatively, power the device on/off.
 * 
 * DO NOT USE THE EMULATOR. USE A REAL DEVICE! * 
 * */

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using Microsoft.WindowsCE.Forms; // You must add the reference for this assembly 
using System.Diagnostics;
using System.Threading;
using IDENTEC.Readers;
using OpenNETCF.Windows.Forms; // You must add the reference for this assembly  see http://www.opennetcf.org

namespace IDENTECSmartDeviceApplication
{


	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxMessages;
		private System.Windows.Forms.Button buttonExit;
		private iCard3 m_iCard;
	
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//!! Need to make sure the message filter is hooked up !!
			ApplicationEx.AddMessageFilter(new CCardDetector(this));


			m_iCard = new iCard3();

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
			this.textBoxMessages = new System.Windows.Forms.TextBox();
			this.buttonExit = new System.Windows.Forms.Button();
			// 
			// textBoxMessages
			// 
			this.textBoxMessages.Location = new System.Drawing.Point(10, 13);
			this.textBoxMessages.Multiline = true;
			this.textBoxMessages.ReadOnly = true;
			this.textBoxMessages.Size = new System.Drawing.Size(213, 232);
			this.textBoxMessages.Text = "";
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(80, 261);
			this.buttonExit.Text = "E&xit";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(245, 322);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.textBoxMessages);
			this.Text = "IDENTEC Smart Device Application";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			//!! Note the ApplicationEx.Run rather than the standard Application.Run !!
			// see http://www.opennetcf.org for details
			ApplicationEx.Run(new MainForm());
		}

		public void CardAvailable()
		{
			textBoxMessages.Text += "Card available\r\n";
			//Do a sanity check to see if the card is connected
			if (!m_iCard.Connected)
			{
				// Pause for a moment as some devices don't load the driver immediately
				Thread.Sleep(1000);
				try
				{
					if(m_iCard.Connect())
					{					
						textBoxMessages.Text += "Card info: " + m_iCard.Information + "\r\n";
					}
					else
					{
						textBoxMessages.Text += "Could not detect card\r\n";
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		public void CardRemoved()
		{
			textBoxMessages.Text += "Card not available\r\n";
			m_iCard.Disconnect();
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	}

	class CCardDetector : IMessageFilter
	{
		public CCardDetector(MainForm form)
		{
			m_Form = form;
		}

		private MainForm m_Form;

		// These messages are sent several times when the event occurs:
		private const int ICARD_INIT = 0x499; 
		private const int ICARD_GONE = 0x498;

		public bool PreFilterMessage(ref Microsoft.WindowsCE.Forms.Message msg)
		{

			switch(msg.Msg)
			{
				case ICARD_INIT:					
					m_Form.CardAvailable(); 
					break;

				case ICARD_GONE:					
					m_Form.CardRemoved(); 
					break;
			}
			return false;
		}
	} 

}
