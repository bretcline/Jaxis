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
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using IDENTEC;
using IDENTEC.Readers;
using IDENTEC.Tags;


namespace Advanced_ILR_Tutorial
{
    public partial class MainForm : Form
    {
        private iCard3 m_iCard3;
        int m_ExpectedTags;
        bool m_blinkOnScan;
        int m_ScanRepeats;

        public MainForm()
        {
            InitializeComponent();
            m_iCard3 = new iCard3();         
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                m_iCard3.Connect();
                // Hint: the current transmit power is set to maximum when the card connects 
                // so there is no point in trying to set the output power before connecting
                DisplayCardInformation();
                InitializeRFPowerGroupBox();
                dataGridViewTags.Sort(serialNumberDataGridViewTextBoxColumn, ListSortDirection.Ascending);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            
        }

        private void InitializeRFPowerGroupBox()
        {
            // This run-time check ensures that we don't go beyond the bounds of the power settings for the RF output
            trackBarOutputPower.Maximum = m_iCard3.MaxOutputdBmIQ;
            trackBarOutputPower.Minimum = m_iCard3.MinOutputdBm;
            trackBarOutputPower.Value = m_iCard3.TxPowerIQ;
            checkBoxRxBoost.Checked = m_iCard3.EnableReceiveBoost;
        }

        private void DisplayCardInformation()
        {
            // It's important to track the serial number and firmware information in your app:
            toolStripStatusLabelCardInfo.Text = "Firmware Info: " + m_iCard3.Information;
            toolStripStatusLabelCardSerialNumber.Text = "Serial #: " + m_iCard3.SerialNumber;
        }

        private void clearTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myTagsDataset.Clear();
            toolStripStatusLabelTagCount.Text = "0 Tags";
        }

        private void toolStripButtonStartScan_Click(object sender, EventArgs e)
        {
            // To be thread safe, we'll store the settings in member variables 
            // instead of using the controls directly
            m_blinkOnScan = checkBoxScanBlink.Checked;
            m_ExpectedTags = (int) numericUpDownExpectedTags.Value;
            m_ScanRepeats = (int)numericUpDownScanRepeats.Value;
           
            Thread t = new Thread(new ThreadStart(ScanThreadProc));
            t.IsBackground = true;
            t.Start();

            EnableControls(false);
            toolStripProgressBarWorking.Style = ProgressBarStyle.Marquee;
        }

        private void ScanThreadProc()
        {
            // We'll use a stopwatch to help us time the code to get an idea of how long a scan takes.
            Stopwatch s = new Stopwatch();
            for (int i = 0; i < m_ScanRepeats; i++)
            {
                s.Reset();
                s.Start();
                // Note that there are two overloaded ScanForIQTags methods, one does not include blinking the tags
                TagCollection tags = m_iCard3.ScanForIQTags(m_ExpectedTags, m_blinkOnScan);
                s.Stop();
                AddMessage("Scan took " + s.Elapsed.ToString() + " and detected " + tags.Count.ToString()
                    + " tags with options: Expected Tags: " + m_ExpectedTags
                    + ", Blink: " + m_blinkOnScan.ToString() + " and "
                    + m_iCard3.TxPowerIQ.ToString() + "dBm output, Rx Boost: "
                    + m_iCard3.EnableReceiveBoost.ToString());
                DisplayTags(tags);
            }
            ScanFinished();
        }



        private delegate void ScanFinishedDelegate();
        private void ScanFinished()
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new ScanFinishedDelegate(ScanFinished));
                return;
            }

            EnableControls(true);
            toolStripProgressBarWorking.Style = ProgressBarStyle.Blocks;
        }

        private void EnableControls(bool enable)
        {
            toolStripButtonStartScan.Enabled = enable;
            trackBarOutputPower.Enabled = enable;
            checkBoxRxBoost.Enabled = enable;
        }

        private delegate void AddMessageDelegate(string message);
        private void AddMessage(string message)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new AddMessageDelegate(AddMessage), new object[] { message });
                return;
            }
            message = DateTime.Now.ToLongTimeString() + ": " + message;
            listBoxMessages.Items.Add(message);
            
        }

        #region >>>>> Update Tag Display <<<<<
        private delegate void DisplayTagsDelegate(IDENTEC.Tags.TagCollection tags);
        private void DisplayTags(IDENTEC.Tags.TagCollection tags)
        {
            // Check if we need to call BeginInvoke.
            if (this.InvokeRequired)
            {
                // Pass the same function to BeginInvoke,
                // but the call would come on the correct
                // thread and InvokeRequired will be false.
                this.BeginInvoke(new DisplayTagsDelegate(DisplayTags),
                    new object[] { tags });

                return;
            }

            //this iteration occurs in the main thread:			
            foreach (iQTag tag in tags)
            {                
                DataSetTags.DataTableTagsRow row = myTagsDataset.DataTableTags.FindBySerialNumber(tag.Label);
                if (row == null)
                {
                    //We have to add a new row:                                        

                    row = myTagsDataset.DataTableTags.AddDataTableTagsRow(tag.Label, tag.Signal, tag.ModelType.ToString(), false,
                        0);

                    row.SetBatteryConsumedNull();

                    // Tidy up the display according to the tag's properties:
                    // We know that the tag properties are valid when the model type is known:
                    if (tag.Range == iQTag.RangeState.Indeterminate)
                        row.SetLongRangeEnabledNull();
                    else
                    {
                        row.LongRangeEnabled = tag.Range == iQTag.RangeState.ExtendedRange;
                        
                        if (tag.ReportsBatteryPercentConsumed)
                            row.BatteryConsumed = tag.BatteryPercentConsumed;                    
                    }
                        


                    toolStripStatusLabelTagCount.Text = myTagsDataset.DataTableTags.Count.ToString() + " Tags";
                }
                else
                {
                    if (tag.Range != iQTag.RangeState.Indeterminate)
                    {
                        row.LongRangeEnabled = tag.Range == iQTag.RangeState.ExtendedRange;
                        if (tag.ReportsBatteryPercentConsumed)
                            row.BatteryConsumed = tag.BatteryPercentConsumed;
                        row.SignalStrength = tag.Signal;
                        row.Model = tag.ModelType.ToString();

                    }
                }
            }
            dataGridViewTags.CurrentCell = null;
        }
        #endregion

        private void clearMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxMessages.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBarOutputPower_ValueChanged(object sender, EventArgs e)
        {
            // We shouldn't have to do any try/catch here as we've already queried the card and set the control within the allowed bounds
            m_iCard3.TxPowerIQ = (int)trackBarOutputPower.Value;
            textBoxCurrentOutput.Text = m_iCard3.TxPowerIQ.ToString() + "dBm";
        }

        private void checkBoxRxBoost_CheckedChanged(object sender, EventArgs e)
        {
            m_iCard3.EnableReceiveBoost = checkBoxRxBoost.Checked;
        }

        private void dataGridViewTags_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // To be safe we don't want to allow two threads accessing the card at once:
            if (!toolStripButtonStartScan.Enabled)
                return;
            // We're not interested in the header row:		
            if (e.RowIndex < 0)
                return;

            // Here we are demonstrating how to use a new tag object that did not come from a scan:
            string tagLabel = dataGridViewTags.Rows[e.RowIndex].Cells[0].Value.ToString();
            iQTag tag = new iQTag();
            tag.Label = tagLabel;


            if (e.ColumnIndex == longRangeEnabledDataGridViewCheckBoxColumn.Index)
            {
                object obj = dataGridViewTags.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                // The checkbox is tri-state, and the third state equates to a null object:
                if (obj is bool)
                {
                    bool enable = (bool)obj;
                    if (m_iCard3.SetTagRangeState(tag, enable))
                    {
                        // Here we update the display and our helper method takes a tag collection
                        // We want to force an update of possibly all fields including the signal strength
                        TagCollection tags = new TagCollection();
                        tags.Add(tag);
                        DisplayTags(tags);
                        AddMessage(tagLabel + " enabled long range: " + enable.ToString());
                    }
                    else
                    {
                        // If the operation failed to switch the range, we need to ensure the GUI matches:
                        dataGridViewTags.CancelEdit();
                        // The i-CARD 3 reports a device status code with each call. It can help us determine why the operation failed:
                        AddMessage(tagLabel + " failed to switch range mode. Device code: " + m_iCard3.DeviceStatus.ToString());
                    }
                }
            }
        }

        private void toolStripButtonMultiBlink_Click(object sender, EventArgs e)
        {
            if (!toolStripButtonStartScan.Enabled)
                return;

            TagCollection tags = new TagCollection();
            foreach (DataGridViewRow viewRow in dataGridViewTags.SelectedRows)
            {
                if (viewRow.Index > 0)
                {
                    string tagLabel = viewRow.Cells[0].Value.ToString();
                    iQTag tag = new iQTag();
                    tag.Label = tagLabel;

                    if (m_iCard3.BlinkTag(tag, int.Parse(toolStripTextBoxBlinkCount.Text)))
                    {
                        AddMessage(tagLabel + " now blinking.");
                        tags.Add(tag);
                    }
                    else
                    {
                        AddMessage(tagLabel + " failed to blink. Device code: " + m_iCard3.DeviceStatus.ToString());
                    }
                }
            }
            DisplayTags(tags);            
        }    
        

    }
}