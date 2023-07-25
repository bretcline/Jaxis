using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IDENTEC;
using Win32API;
using IDENTEC.UdbElements;
using IDENTEC.Util;

namespace ISO_Demo
{
    public partial class Form1 : Form
    {
        IDENTEC.Readers.iPortMCI m_Reader;
        System.Collections.ArrayList m_TagArray;
        System.Text.RegularExpressions.Regex m_regexp;
        byte[] HexaData = new byte[16];
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            richTextBox1.InitializeComponent();
            richTextBox1.LoadData(HexaData);

            this.toolStripComboBoxSerialPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (toolStripComboBoxSerialPort.Items.Count > 0)
                toolStripComboBoxSerialPort.SelectedIndex = 0;
            m_Reader = new IDENTEC.Readers.iPortMCI();
            m_TagArray = new System.Collections.ArrayList();
            m_Reader.ErrorCommandEvent += new IDENTEC.Readers.iPortMCI.OnCommandError(m_Reader_ErrorEvent);
            toolStripComboBoxWakeUp.SelectedIndex = 0;
            m_regexp = new System.Text.RegularExpressions.Regex(@"[0-9A-Fa-f]");
            //m_regexps.Add('H', @"[0-9A-F]"); 
//            this.maskedTextBox1.Mask = 
        }

        void m_Reader_ErrorEvent(string msg)
        {
            this.toolStripStatusLabelCommand.Text = msg;
            this.richTextBox2.AppendText(DateTime.Now.ToLongTimeString() + ": " + msg + "\n");
        }

        private void startCommand(string msg)
        {
            this.toolStripStatusLabelCommand.Text = msg;
            Update();

        }

        private void checkPacketLength()
        {
            int len = Convert.ToInt16(this.toolStripTextBoxMaxPacketLength.Text);
            if (len > 76)
            {
                this.toolStripTextBoxMaxPacketLength.Text = "46";
                MessageBox.Show("Max Packet Length set to 46");
            }
/*            if (len < 16)
            {
                this.toolStripTextBoxMaxPacketLength.Text = "16";
                MessageBox.Show("Max Packet Length set to 16");
            }
 */
            m_Reader.MaxPacketLength = (char)len;
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            startCommand("Connecting to reader");
            if (toolStripComboBoxSerialPort.SelectedIndex >= 0)
            {
                try
                {
                    if (m_Reader.Connect(toolStripComboBoxSerialPort.Text))
                    {
                        toolStripStatusLabelConnection.Text = "Connected to Reader " + m_Reader.ToString() + " "+ m_Reader.SerialNumber;
                        this.toolStripButtonConnect.Enabled = false;
                        this.toolStripButtonDisconnect.Enabled = true;
                        this.toolStrip2.Enabled = true;
                        // todo check if we need to increase
                        m_Reader.Retries = 1;
                    }
                }
                catch(Exception ex)
                {
                    toolStripStatusLabelConnection.Text = "Failed to connect to Reader";
                }
            }
            // clear the list
            this.dsTags1.Tables[0].Clear();
            if (m_TagArray != null)
                m_TagArray.Clear();
            toolStripStatusLabelNbTags.Text = "0 tag";
        }

        private void toolStripButtonDisconnect_Click(object sender, EventArgs e)
        {
            startCommand("Disconnecting reader");
            if (m_Reader.IsConnected())
                m_Reader.Disconnect();
            this.toolStripButtonConnect.Enabled = true;
            this.toolStripButtonDisconnect.Enabled = false;
            toolStripStatusLabelConnection.Text = "No Reader connected";
            this.toolStrip2.Enabled = false;
        }

        private void toolStripButtonCollection_Click(object sender, EventArgs e)
        {
            short windowSize;
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Running Collection command");
            checkPacketLength();
            IDENTEC.Tags.ISO18000Tag newTag = new IDENTEC.Tags.ISO18000Tag();
            Random r = new Random();

            if (m_TagArray.Count == 0)
                newTag.ID = 900000001;
            else
                newTag.ID = (uint)r.Next(1, 10);
            newTag.ManufacturerID = 0x4911;
            windowSize = Convert.ToSByte(toolStripTextBoxWindowSize.Text);

            System.Collections.ArrayList TagArray = new System.Collections.ArrayList();
            m_Reader.CollectWithUDB(ref TagArray, windowSize);
//            if (TagArray.Count == 0)
//                TagArray.Add(newTag);
            foreach (IDENTEC.Tags.ISO18000Tag Tag in TagArray)
            {
                int test = m_TagArray.BinarySearch(Tag);
                int pos = m_TagArray.BinarySearch(Tag);
                if (pos < 0)
                {
                    addTagToGrid(Tag);
                    m_TagArray.Add(Tag);
                    m_TagArray.Sort();
                }
                else
                {
                    IDENTEC.Tags.ISO18000Tag updatedTag = (IDENTEC.Tags.ISO18000Tag)m_TagArray[pos];
                    updatedTag.LastTimeAwake = Tag.LastTimeAwake;
                    UpdateTagDataset(Tag);
                }
            }
            this.toolStripStatusLabelCommand.Text = "Collection found " + TagArray.Count.ToString() + " tag";

        }

        private void addTagToGrid(IDENTEC.Tags.ISO18000Tag tag)
        {
            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
            if (tag == null)
                return;
            dsTags.DataTable1Row dgRow;
            dgRow = (dsTags.DataTable1Row)this.dsTags1.Tables[0].NewRow();
            dgRow.SN = tag.ID.ToString();
            dgRow.ManufacturerID = tag.ManufacturerID.ToString();
            dgRow.RSSI = tag.GetSignalStrength(1).ToString();
            if (tag.m_RoutingCode != null)
                dgRow.RoutingCode = encoding.GetString(tag.m_RoutingCode);
            else
                dgRow.RoutingCode = "";
            if (tag.m_UserID != null)
                dgRow.UDB = encoding.GetString(tag.m_UserID);
            else
                dgRow.UDB = "";
            try
            {
                this.dsTags1.Tables[0].Rows.Add(dgRow);
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateTagDataset(IDENTEC.Tags.ISO18000Tag tag)
        {
            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
            dsTags.DataTable1Row dgRow;
            dgRow =  (dsTags.DataTable1Row)(dsTags1.Tables[0].Rows.Find(tag.ID.ToString()));
            dgRow.RSSI = tag.GetSignalStrength(1).ToString();
            if (tag.m_RoutingCode != null)
                dgRow.RoutingCode = encoding.GetString(tag.m_RoutingCode);
            else
                dgRow.RoutingCode = "";
            if (tag.m_UserID != null)
                dgRow.UDB = encoding.GetString(tag.m_UserID);
            else
                dgRow.UDB = "";
        }

        private IDENTEC.Tags.ISO18000Tag findSelectedTag()
        {
            IDENTEC.Tags.ISO18000Tag selectedTag = null;
            System.Data.DataRowView SelectedRow;
            if (dataGridView1.SelectedRows.Count != 1)
            {
                this.toolStripStatusLabelCommand.Text = "No Tag selected"; 
                return null;
            }
            SelectedRow = (System.Data.DataRowView)dataGridView1.SelectedRows[0].DataBoundItem;
            int id;
            if (int.TryParse(SelectedRow.Row.ItemArray[1].ToString(), out id))
            {
                selectedTag = new IDENTEC.Tags.ISO18000Tag();
                selectedTag.ID = (uint)id;
                int test = m_TagArray.BinarySearch(selectedTag);
                if (test < 0)
                {
                    this.toolStripStatusLabelCommand.Text = "No Tag selected"; 
                    selectedTag = null;
                }
                else
                    selectedTag = (IDENTEC.Tags.ISO18000Tag)m_TagArray[test];
            }
            return selectedTag;
        }


        private void toolStripButtonSleep_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Sending all tags to sleep");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                if (tag.Sleep(m_Reader))
                    this.toolStripStatusLabelCommand.Text = "Sleep Tag " + tag.ID.ToString() + " OK";
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }

        private void toolStripButtonSleepAllbut_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Sleep All But 1 Tag to sleep");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                if (m_Reader.SleepAllBut(tag))
                {
                    // change all tag awake status
                    foreach (IDENTEC.Tags.ISO18000Tag myTag in m_TagArray)
                    {
                        myTag.LastTimeAwake = DateTime.Now;
                        myTag.LastTimeAwake.Subtract(new TimeSpan(1, 0, 0));
                    }
                    this.toolStripStatusLabelCommand.Text = "Sleep All Tag but " + tag.ID.ToString() + " OK";
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }

        private void toolStripButtonReadRoutingCode_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            byte[] routingCode;
            startCommand("Read Routing Code");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                if (tag.ReadRoutingCode(m_Reader, out routingCode))
                {
                    this.toolStripStatusLabelCommand.Text = "Read Tag " + tag.ID.ToString() + " routing code OK";
                    UpdateTagDataset(tag);
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }

        private void toolStripButtonWriteRoutingCode_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Write Routing Code");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] routingCode;
                routingCode = encoding.GetBytes(toolStripTextBoxRoutingCode.Text);
                if (tag.WriteRoutingCode(m_Reader, (byte)routingCode.Length,
                                            ref routingCode))
                {
                    this.toolStripStatusLabelCommand.Text = "Write Tag " + tag.ID.ToString() + " routing code OK";
//                    tag.m_RoutingCode = routingCode;
                    UpdateTagDataset(tag);
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }


        private void toolStripButtonReadUDB_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Reading UDB");
            checkPacketLength();
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            byte[] UDB;
            try
            {
                if (tag.ReadUDB(m_Reader, out UDB))
                {
                    this.toolStripStatusLabelCommand.Text = "read Tag " + tag.ID.ToString() + " UDB OK";
                    UpdateTagDataset(tag);
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            } 
        }

        private void toolStripButtonBeeperON_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Turning Beeper ON");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                if (tag.BeeperControl(m_Reader, true))
                {
                    this.toolStripStatusLabelCommand.Text = "Tag " + tag.ID.ToString() + " beeper ON";
                    UpdateTagDataset(tag);
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }

        }

        private void toolStripButtonBeeperOFF_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Turning Beeper OFF");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                if (tag.BeeperControl(m_Reader, false))
                {
                    this.toolStripStatusLabelCommand.Text = "Tag " + tag.ID.ToString() + " beeper OFF";
                    UpdateTagDataset(tag);
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        #region >>>>> Tag Table displayed field <<<<<
        private void manufacturerIDToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Columns[0].Visible = ((System.Windows.Forms.ToolStripMenuItem)sender).Checked;
        }

        private void uDBToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Columns[2].Visible = ((System.Windows.Forms.ToolStripMenuItem)sender).Checked;
        }

        private void routingCodeToolStripMenuItem1_CheckStateChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Columns[3].Visible = ((System.Windows.Forms.ToolStripMenuItem)sender).Checked;
        }

        private void rSSIToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Columns[4].Visible = ((System.Windows.Forms.ToolStripMenuItem)sender).Checked;
        }
        #endregion

        private void toolStripButtonClearTagList_Click(object sender, EventArgs e)
        {
            this.dsTags1.Tables[0].Clear();
            if (m_TagArray != null)
                m_TagArray.Clear();
            toolStripStatusLabelNbTags.Text = "0 tag";
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            toolStripStatusLabelNbTags.Text = this.dsTags1.Tables[0].Rows.Count.ToString() + " tag";
            if (this.dsTags1.Tables[0].Rows.Count > 1)
                toolStripStatusLabelNbTags.Text += "s";
        }

        private void toolStripComboBoxWakeUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxWakeUp.SelectedIndex)
            {
                case 0:
                    m_Reader.WakeUp = IDENTEC.Readers.iPortMCI.Wake_Up.SelfManaged;
                    break;
                case 1:
                    m_Reader.WakeUp = IDENTEC.Readers.iPortMCI.Wake_Up.Force;
                    break;
                case 2:
                    m_Reader.WakeUp = IDENTEC.Readers.iPortMCI.Wake_Up.NoWakeUp;
                    break;
                default:
                    break;
            }
        }

        private void toolStripTextBoxMaxPacketLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("pressed");
           if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true; //Remove/Prevent values from being entered if non-digit
        }

        private void checkBoxUseASCII_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMemoryWritten.Enabled = !checkBoxUseASCII.Checked;
            numericUpDownNbByte.Enabled = !checkBoxUseASCII.Checked;
            richTextBox1.Enabled = checkBoxUseASCII.Checked;
        }

        private void buttonReadMemory_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Read memory");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            byte[] data;
            byte nbByte;
            if (checkBoxUseASCII.Checked)
                nbByte = 16;
            else
                nbByte = (byte)this.numericUpDownNbByte.Value;
            try
            {
                if (tag.ReadMemory(m_Reader, (int)this.numericUpDownAddress.Value, nbByte, out data))
                {
                    this.toolStripStatusLabelCommand.Text = "read Tag " + tag.ID.ToString() + " OK";
                    this.textBoxMemoryRead.Clear();
                    System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
                    this.textBoxMemoryRead.Text = encoding.GetString(data);
                    if (checkBoxUseASCII.Checked)
                    {
                        richTextBox1.LoadData(data);
                    }

                    UpdateTagDataset(tag);
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }

        private void buttonWriteMemory_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Update();
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data;
                if (checkBoxUseASCII.Checked)
                {
                    data = richTextBox1.ArrayData;
                }
                else
                {
                    data = encoding.GetBytes(this.textBoxMemoryWritten.Text);
                }
                startCommand("Writing " + data.Length.ToString() + " byte to Memory");
                if (tag.WriteMemory(m_Reader, (int)this.numericUpDownAddress.Value, (byte)data.Length,
                                            ref data))
                {
                    this.toolStripStatusLabelCommand.Text = "Write Tag " + tag.ID.ToString() + " OK";
                    UpdateTagDataset(tag);
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }

        private void toolStripButtonCollection2_Click(object sender, EventArgs e)
        {
            short windowSize;
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Running Collection command");
            checkPacketLength();
            IDENTEC.Tags.ISO18000Tag newTag = new IDENTEC.Tags.ISO18000Tag();
            Random r = new Random();

            if (m_TagArray.Count == 0)
                newTag.ID = 900000001;
            else
                newTag.ID = (uint)r.Next(1, 10);
            newTag.ManufacturerID = 0x4911;
            windowSize = Convert.ToSByte(toolStripTextBoxWindowSize.Text);

            System.Collections.ArrayList TagArray = new System.Collections.ArrayList();
            m_Reader.CollectWithUDB(ref TagArray, windowSize);
            //            if (TagArray.Count == 0)
            //                TagArray.Add(newTag);
            foreach (IDENTEC.Tags.ISO18000Tag Tag in TagArray)
            {
                int test = m_TagArray.BinarySearch(Tag);
                int pos = m_TagArray.BinarySearch(Tag);
                if (pos < 0)
                {
                    addTagToGrid(Tag);
                    m_TagArray.Add(Tag);
                    m_TagArray.Sort();
                }
                else
                {
                    IDENTEC.Tags.ISO18000Tag updatedTag = (IDENTEC.Tags.ISO18000Tag)m_TagArray[pos];
                    updatedTag.LastTimeAwake = Tag.LastTimeAwake;
                    UpdateTagDataset(Tag);
                }
            }
            this.toolStripStatusLabelCommand.Text = "Collection found " + TagArray.Count.ToString() + " tag";

        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonReadUDB_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            startCommand("Reading UDB");
            checkPacketLength();
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;

            byte maxPackPacketLength = Convert.ToByte(textBoxMaxPackageLength.Text);
            byte udbType = Convert.ToByte(comboBoxUDBType.Text);
            

            System.Collections.ArrayList UDB;
            try
            {
                if (tag.ReadUDB(m_Reader,udbType,  maxPackPacketLength, out UDB))
                {
                    this.toolStripStatusLabelCommand.Text = "read Tag " + tag.ID.ToString() + " UDB OK";
                    UpdateTagDataset(tag);
                    dataSetUDBElements.Tables[0].Clear();
                    if(UDB != null)
                    {

                        foreach (Object udbElement in UDB)
                        {
                            DataSetUDBElements.DataTable1Row row = (DataSetUDBElements.DataTable1Row) dataSetUDBElements.Tables[0].NewRow(); ;
                        
                            row.Parameter = udbElement.GetType().Name;
                            row.Length = ((UDBElement)udbElement).getLength().ToString();
                            row.Data = ((UDBElement)udbElement).getData().ToString();

                            dataSetUDBElements.Tables[0].Rows.Add(row);
                         }
                        
                    }

                    
                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            startCommand("Creating Table");
            checkPacketLength();
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();

            if (tag == null) return;

            int tableID = Convert.ToInt16(textBoxTableID.Text);
            int maxRecors = Convert.ToInt16(textBoxMaxRecords.Text);
            String recordSize = textBoxRecordSize.Text;

            if (recordSize != null){
                string[] elements = recordSize.Split(";,/".ToCharArray());
                byte[] recordSizeBytes = new byte[elements.Length];
                int index = 0;
                foreach(string element in elements){
                    byte record = Convert.ToByte(element);
                    recordSizeBytes[index++] = record;
                }
                tag.TableCreate(m_Reader, tableID, maxRecors, recordSizeBytes.Length, recordSizeBytes);

            }

 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            startCommand("Reading Table Properties");
            checkPacketLength();
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();

            if (tag == null) return;

            int tableID = Convert.ToInt16(textBoxReadPropertiesTableID.Text);
            int nbRecords = 0;
            int maxRecords = 0;
            Boolean ret = tag.TableGetProperties(m_Reader, tableID, out nbRecords, out maxRecords);
            if (ret)
            {
                textBoxPropertiesMAxRecords.Text = maxRecords.ToString();
                textBoxPropertiesNumRecords.Text = nbRecords.ToString();
            }
            else
            {

            }



        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBoxRowData_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddData_Click(object sender, EventArgs e)
        {

            startCommand("Adding table data");
            checkPacketLength();
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();

            if (tag == null) return;
            byte[] data = null;

            // create the table payload
            int recordCount = Convert.ToInt16(textBoxAddRecordCount.Text);
            byte[] result = new byte[recordCount * textBoxRowData.Text.Length];
            for (int i = 0; i < recordCount; i++)
            {
                data = System.Text.Encoding.GetEncoding(1252).GetBytes(textBoxRowData.Text);

                Array.Copy(data, 0, result, i * textBoxRowData.Text.Length, textBoxRowData.Text.Length);
            }

            int nbRecords = Convert.ToInt16(textBoxAddRecordCount.Text);
            int tableID = Convert.ToInt16(textBoxAddTableID.Text);

            Boolean status = tag.TableAddRecords(this.m_Reader, tableID, nbRecords, result);
            if (!status)
            {
                startCommand("Could not add table data");
              
            }
            else startCommand("data successfully added");

        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int recordCount = Convert.ToInt16(textBoxAddRecordSize.Text);

            char[] result = new char[recordCount];
            for (int i = 0; i < recordCount; i++)
            {
                int randInt = rand.Next(65,90);
                result[i] = Convert.ToChar(randInt);               
            }

            textBoxRowData.Text = new String(result);
            

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            byte[] routingCode;
            startCommand("Clearing Tag Memory");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            try
            {
                if (tag.ReadRoutingCode(m_Reader, out routingCode))
                {
                    this.toolStripStatusLabelCommand.Text = "Read Tag " + tag.ID.ToString() + " routing code OK";
                    if (tag.DeleteWriteableMemory(m_Reader))
                    {
                        this.toolStripStatusLabelCommand.Text = "Tag Data Wiped";

                        if (tag.WriteRoutingCode(m_Reader, (byte)routingCode.Length,
                                            ref routingCode))
                        {
                            this.toolStripStatusLabelCommand.Text = "Wrote Tag " + tag.ID.ToString() + " routing code OK";
                            
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                this.toolStripStatusLabelCommand.Text = ex.Message;
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void buttonReadTableData_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            byte[] routingCode;
            startCommand("Reading Table Data");
            IDENTEC.Tags.ISO18000Tag tag = findSelectedTag();
            if (tag == null)
                return;
            
            // getting table properties
            int tableID = Convert.ToInt16(textBoxReadTableDataTableID.Text);
            int nbRecords = 0;
            int maxRecords = 0;
            byte[] tableData = null;
            //Boolean ret = tag.TableGetProperties(m_Reader, tableID, out nbRecords, out maxRecords);
            Boolean ret = tag.TableReadAll(m_Reader, tableID, out tableData);

            if (!ret)
            {
                startCommand("Error reading Table Properties");
                return;
            }
            else
            {
                if(tableData != null)
                richTextBox2.AppendText(BitConverter.ToString(tableData));
                else
                richTextBox2.AppendText("No data received \r\n");
            }




        }

    }

    public class HexEditBase : RichTextBox
    {
        /*
        ** Local Variables
        */
        ///<summary>Flag to prevent linked update</summary>
        protected bool m_bNoUpdate = false;  // Control the Updates between windows

        ///<summary>The Controls Contect Menu</summary>
        protected ContextMenu m_menuContext = null;

        /*
        ** Constants
        */
        ///<summary>Constants for Line Index - SDK Call</summary>
        protected const int EM_LINEINDEX = 0xbb;

        ///<summary>Constants for Line From Char - SDK Call</summary>
        protected const int EM_LINEFROMCHAR = 0xc9;

        ///<summary>Constants for Get Selection - SDK Call</summary>
        protected const int EM_GETSEL = 0xb0;

        /*
        ** Definitions from the Outer World
        */

        /*
        ***************************************************************************
        ** 
        ** Class: CharPosition
        ** 
        */
        ///<summary>
        /// A class the contains the line and character position in the bxo
        ///</summary>
        ///<remarks>
        ///</remarks>
        ///
        public class CharPosition
        {
            /*
            ** Local Variables
            */
            ///<summary>Current Line Position</summary>
            protected int m_iLine = 0;

            ///<summary>Current Character Position</summary>
            protected int m_iChar = 0;

            /*
            ***************************************************************************
            ** 
            ** Function: CharPosition
            */
            ///<summary>
            /// Default empty constructor
            ///</summary>
            ///<returns>void</returns>
            ///
            public CharPosition()
            {
            }

            /*
            ***************************************************************************
            ** 
            ** Function: CharPosition
            */
            ///<summary>
            /// Constructor that sets the Line and Char position
            ///</summary>
            ///<param name="iLine" type="int">Line position to create with</param>
            ///<param name="iChar" type="int">Character position to create with</param>
            ///<returns>void</returns>
            ///
            public CharPosition(int iLine, int iChar)
            {
                LinePos = iLine;
                CharPos = iChar;
            }

            /*
            ***************************************************************************
            ** 
            ** Property(int): LinePos
            */
            ///<summary>
            /// Line Position
            ///</summary>
            ///<value>
            ///
            ///</value>
            ///
            public int LinePos
            {
                get { return m_iLine; }
                set { m_iLine = value; }
            }

            /*
            ***************************************************************************
            ** 
            ** Property(int): CharPos
            */
            ///<summary>
            /// Character Position
            ///</summary>
            ///<value>
            ///
            ///</value>
            ///
            public int CharPos
            {
                get { return m_iChar; }
                set { m_iChar = value; }
            }

            /*
            ***************************************************************************
            ** 
            ** Function: ToString
            */
            ///<summary>
            /// Convert the Object to a string
            ///</summary>
            ///<returns>string</returns>
            ///
            public override string ToString()
            {
                return (String.Format("{{L={0}, C={1}}}", LinePos, CharPos));
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Property(System.Drawing.Point): CaretPosition
        */
        ///<summary>
        /// Get the Caret Position
        ///</summary>
        ///<value>
        ///
        ///</value>
        ///
        public Point CaretPosition
        {
            get
            {
                Point pt = Point.Empty;
                Win32API.Window.GetCaretPos(ref pt);
                return pt;
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: LineIndex
        */
        ///<summary>
        ///The return value is the number of characters that
        ///precede the first character in the line containing
        ///the caret.
        ///
        ///</summary>
        ///<param name="iLine" type="int">line to get the Caracters to</param>
        ///<returns>int - Number of characters to the beginning of iLine</returns>
        ///
        public int LineIndex(int iLine)
        {
            return (int)Win32API.Window.SendMessage(new System.Runtime.InteropServices.HandleRef(this, Handle), EM_LINEINDEX, iLine, 0);
        }

        /*
        ***************************************************************************
        ** 
        ** Function: LineIndex
        */
        ///<summary>
        ///Send the EM_LINEINDEX message with the value of -1
        ///in wParam.
        ///
        ///</summary>
        ///<returns>int</returns>
        ///
        public int LineIndex()
        {
            return LineIndex(-1);
        }

        /*
        ***************************************************************************
        ** 
        ** Property(HexEdit.HexEditBase.CharPosition): Position
        */
        ///<summary>
        ///Get the Line Char positions in the buffer
        ///</summary>
        ///<value>
        ///
        ///</value>
        ///
        public CharPosition Position
        {
            get
            {
                CharPosition cp = new CharPosition();

                cp.LinePos = GetLineFromCharIndex(SelectionStart);
                cp.CharPos = SelectionStart - LineIndex();

                return cp;
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: GetDisplayChar
        */
        ///<summary>
        ///Get the Display char from a entered char
        ///this prevents non displayable characters from 
        ///going to the display
        ///</summary>
        ///<param name="cData" type="char">character to check</param>
        ///<returns>char</returns>
        ///
        protected char GetDisplayChar(char cData)
        {
            if (20 > cData)
            {
                cData = (char)0xB7;
            }

            return cData;
        }

        /*
        ***************************************************************************
        ** 
        ** Function: GetDisplayChar
        */
        ///<summary>
        ///<see cref="GetDisplayChar"/>Converts to byte to char before doing the
        ///display filtering
        ///</summary>
        ///<param name="byData" type="byte"></param>
        ///<returns>char</returns>
        ///
        protected char GetDisplayChar(byte byData)
        {
            return GetDisplayChar((char)byData);
        }

        /*
        ***************************************************************************
        ** 
        ** Function: GetFontWidth
        */
        ///<summary>
        /// Return the Font Width, assumes fixed font
        ///</summary>
        ///<returns>int - the width of the font</returns>
        ///
        protected int GetFontWidth()
        {
            Graphics g = CreateGraphics();
            int iWidth = 0;
            string strTest = "WWWWWWWWWWWWWWWW";
            SizeF Size;

            Size = g.MeasureString(strTest, Font);
            iWidth = (int)(Size.Width + .09) / strTest.Length;

            return iWidth;
        }

        /*
        ***************************************************************************
        ** 
        ** Function: SetTextTabLocations
        */
        ///<summary>
        ///
        ///</summary>
        ///<returns>void</returns>
        ///<exception cref="System.Exception">Thrown</exception>
        ///<remarks>
        ///</remarks>
        ///<example>How to use this function
        ///<code>
        ///</code>
        ///</example>
        ///
        public void SetTextTabLocations()
        {
            /*
            ** Local Variables
            */
            int iTabSize = 4;
            int iNoTabs = 32;
            int[] aiTabs = new int[iNoTabs];
            int iWidth = GetFontWidth();

            for (int i = 0; i < iNoTabs; i++)
            {
                aiTabs[i] = iWidth * ((i + 1) * iTabSize);
            }

            SelectionTabs = aiTabs;
        }


        /*
        ***************************************************************************
        ** 
        ** Function: HexCtoB
        */
        ///<summary>
        /// Convert a character to its Byte Equivalent
        ///</summary>
        ///<param name="cVar" type="char"></param>
        ///<returns>byte</returns>
        ///<example>char = A (0x41) returns a byte of A (0xA)
        ///</example>
        ///
        protected byte HexCtoB(char cVar)
        {
            byte byRet = 0;

            if ((cVar >= '0') && (cVar <= '9'))
            {
                byRet = (byte)(cVar - '0');
            }
            else
            {
                byRet = (byte)((cVar - 'A') + 10);
            }

            return byRet;
        }

        /*
        ***************************************************************************
        ** 
        ** Function: InitializeComponent
        */
        ///<summary>
        /// This is a overridable function to process any initialization stuff the box
        /// might need
        ///</summary>
        ///<returns>void</returns>
        ///
        virtual public void InitializeComponent()
        {
        }
    }

    /*
    ***************************************************************************
    ** 
    ** Class: HexEditBox
    ** 
    */
    ///<summary>
    ///This is the Hex Edit Box Display
    ///</summary>
    ///<remarks>
    ///</remarks>
    ///
    public class HexEditBox : HexEditBase
    {
        /*
        ** Class Local Variables
        */
        ///<summary>used for backup, so the position alignment will not keep if from moving</summary>
        protected bool m_bIgnorePart = false;

        ///<summary>This is the list of characters allowed in the hex window</summary>
        protected string m_strAllowed = "1234567890ABCDEF";

        ///<summary>This is the array where the data is stored</summary>
        protected byte[] m_abyData = null;

        ///<summary>ensures we dont get a Selection Change while we are processing one already</summary>
        protected bool m_bSelChangeProcess = false;

        ///<summary>This is our menuItem for copy</summary>
        protected MenuItem m_miSelectAll = null;

        ///<summary>This is our menuItem for copy</summary>
        protected MenuItem m_miCopy = null;

        ///<summary>This is our menuItem for Paste</summary>
        protected MenuItem m_miPasteASCII = null;

        ///<summary>This is our menuItem for Paste</summary>
        protected MenuItem m_miPasteBytes = null;

        ///<summary>This is our menuItem for copy to ASCII</summary>
        protected MenuItem m_miCopyASCII = null;

        ///<summary>This is our menuItem for copy bytes</summary>
        protected MenuItem m_miCopyBytes = null;


        /*
        ***************************************************************************
        ** 
        ** Function: InitializeComponent
        */
        ///<summary>
        /// This is the custom initialization we set our menu and font here
        ///</summary>
        ///<returns>void</returns>
        ///<exception cref="System.Exception">Thrown</exception>
        ///<remarks>
        ///</remarks>
        ///<example>How to use this function
        ///<code>
        ///</code>
        ///</example>
        ///
        override public void InitializeComponent()
        {
            /*
            ** Create the Context menu
            */
            m_menuContext = new ContextMenu();
            m_miCopy = new MenuItem();
            m_miPasteASCII = new MenuItem();
            m_miPasteBytes = new MenuItem();
            m_miCopyASCII = new MenuItem();
            m_miCopyBytes = new MenuItem();
            m_miSelectAll = new MenuItem();

            /*
            ** Add the Pop Message
            */
            m_menuContext.Popup += new System.EventHandler(MenuPopup);

            /*
            ** miSelectAll
            ** 
            */
            m_miSelectAll.Text = "Select All";
            m_miSelectAll.Click += new System.EventHandler(Menu_SelectAll);

            /*
            ** miCopyBytes
            ** 
            */
            m_miCopyBytes.Text = "Copy Data Bytes";
            m_miCopyBytes.Click += new System.EventHandler(Menu_CopyBytes);

            /*
            ** miCopyASCII
            ** 
            */
            m_miCopyASCII.Text = "Copy Bytes to ASCII";
            m_miCopyASCII.Click += new System.EventHandler(Menu_CopyASCII);

            /*
            ** miCopy
            ** 
            */
            m_miCopy.Text = "Copy Hex as ASCII";
            m_miCopy.Click += new System.EventHandler(Menu_Copy);

            /*
            ** miPasteASCII
            ** 
            */
            m_miPasteASCII.Text = "Paste ASCII";
            m_miPasteASCII.Click += new System.EventHandler(Menu_PasteASCII);

            /*
            ** miPasteBytes
            ** 
            */
            m_miPasteBytes.Text = "Paste Bytes";
            m_miPasteBytes.Click += new System.EventHandler(Menu_PasteBytes);

            // 
            // rtbHex
            // 
            this.AcceptsTab = true;
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Name = "rtbHex";
            this.TabIndex = 1;
            this.ContextMenu = m_menuContext;
            this.Text = "";
            this.WordWrap = false;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.SelectionChanged += new System.EventHandler(this.OnSelectionChange);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Resize += new System.EventHandler(this.OnResizeBox);

        }


        /*
        ***************************************************************************
        ** 
        ** Function: MenuPopup
        */
        ///<summary>
        ///
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        protected void MenuPopup(object sender, System.EventArgs e)
        {
            /*
            ** Local Variables
            */
            IDataObject DataObj = Clipboard.GetDataObject();
            int iIndex = 0;

            /*
            ** Clear the Items
            */
            m_menuContext.MenuItems.Clear();

            /*
            ** Add Items to the Context Menu
            */
            m_menuContext.MenuItems.Add(iIndex++, m_miSelectAll);

            /*
            ** Add in the Copy Stuff is there is a Selection
            */
            if (SelectionLength > 0)
            {
                m_menuContext.MenuItems.Add(iIndex++, new MenuItem("-"));
                m_menuContext.MenuItems.Add(iIndex++, m_miCopyBytes);
                m_menuContext.MenuItems.Add(iIndex++, m_miCopyASCII);
                m_menuContext.MenuItems.Add(iIndex++, m_miCopy);
            }

            /*
            ** Determines whether the data is in a format you can use.
            */
            if (DataObj.GetDataPresent(m_abyData.GetType()))
            {
                m_menuContext.MenuItems.Add(iIndex++, new MenuItem("-"));
                m_menuContext.MenuItems.Add(iIndex++, m_miPasteBytes);
                if (DataObj.GetDataPresent(DataFormats.Text))
                {
                    m_menuContext.MenuItems.Add(iIndex++, m_miPasteASCII);
                }
            }
            else
            {
                if (DataObj.GetDataPresent(DataFormats.Text))
                {
                    m_menuContext.MenuItems.Add(iIndex++, new MenuItem("-"));
                    m_menuContext.MenuItems.Add(iIndex++, m_miPasteASCII);
                }
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: Menu_SelectAll
        */
        ///<summary>
        ///Select all
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        protected void Menu_SelectAll(object sender, System.EventArgs e)
        {
            SelectAll();
        }

        /*
        ***************************************************************************
        ** 
        ** Function: Menu_Copy
        */
        ///<summary>
        /// Process the Copy Menu Item
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        protected void Menu_Copy(object sender, System.EventArgs e)
        {
            Copy();
        }

        /*
        ***************************************************************************
        ** 
        ** Function: Menu_CopyASCII
        */
        ///<summary>
        ///
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        protected void Menu_CopyASCII(object sender, System.EventArgs e)
        {
            /*
            ** Local Variables
            */
            int iStart = (SelectionStart / 3);
            int iLen = (SelectionLength / 3);
            StringBuilder sbVar = new StringBuilder(iLen);

            /*
            ** Make sure our size is only as much as we have
            */
            if (iLen > m_abyData.Length)
            {
                iLen = m_abyData.Length;
            }

            for (int i = iStart; i < iLen; i++)
            {
                char cData = GetDisplayChar(m_abyData[i]);
                sbVar.Append(cData);
            }

            /*
            ** Copy the bitmap to the clipboard.
            */
            Clipboard.SetDataObject(sbVar.ToString());
        }

        /*
        ***************************************************************************
        ** 
        ** Function: Menu_CopyBytes
        */
        ///<summary>
        /// Process the Menu Copy Bytes (Converts the hex to Asc in the Hex window)
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        protected void Menu_CopyBytes(object sender, System.EventArgs e)
        {
            /*
            ** Local Variables
            */
            int iStart = (SelectionStart / 3);
            int iLen = (SelectionLength / 3);
            byte[] abyCopy = new byte[iLen];

            /*
            ** Make sure our size is only as much as we have
            */
            if (iLen > m_abyData.Length)
            {
                iLen = m_abyData.Length;
            }

            /*
            ** Get the Bytes we want to mess with
            */
            Array.Copy(m_abyData, iStart, abyCopy, 0, iLen);

            /*
            ** Copy the bitmap to the clipboard.
            */
            Clipboard.SetDataObject(abyCopy);
        }

        /*
        ***************************************************************************
        ** 
        ** Function: Menu_PasteASCII
        */
        ///<summary>
        /// Processes the paste ASCII bytes to the hex winedow
        ///</summary>
        ///<remarks>
        ///This is marked internal so that the linked window can call it
        ///</remarks>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        internal void Menu_PasteASCII(object sender, System.EventArgs e)
        {
            /*
            ** Save our starting position to restore after we work
            */
            int iSave = SelectionStart;

            /*
            ** Declares an IDataObject to hold the data returned from the clipboard.
            ** Retrieves the data from the clipboard.
            */
            IDataObject DataObj = Clipboard.GetDataObject();

            /*
            ** Determines whether the data is in a format you can use.
            */
            if (DataObj.GetDataPresent(DataFormats.Text))
            {
                /*
                ** Local Variables
                */
                string strText = (string)DataObj.GetData(DataFormats.Text);
                int iPos = (SelectionStart / 3);
                int iLen = (strText.Length < (m_abyData.Length - iPos)) ? strText.Length : (m_abyData.Length - iPos);

                /*
                ** Yes it is, so display it in a text box.
                */
                for (int i = 0; i < iLen; i++)
                {
                    m_abyData[i + iPos] = (byte)strText[i];
                }

                UpdateDisplay();

            }
            else
            {
                MessageBox.Show("Nothing to paste");
            }

            /*
            ** Restore our original position
            */
            SelectionStart = iSave;
        }

        /*
        ***************************************************************************
        ** 
        ** Function: Menu_PasteBytes
        */
        ///<summary>
        ///
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        internal void Menu_PasteBytes(object sender, System.EventArgs e)
        {
            /*
            ** Save our starting position to restore after we work
            */
            int iSave = SelectionStart;

            /*
            ** Declares an IDataObject to hold the data returned from the clipboard.
            ** Retrieves the data from the clipboard.
            */
            IDataObject DataObj = Clipboard.GetDataObject();

            /*
            ** This is a Byte Paste
            */
            if (DataObj.GetDataPresent(m_abyData.GetType()))
            {
                /*
                ** Local Variables
                */
                byte[] abyCopy = (byte[])DataObj.GetData(m_abyData.GetType());
                int iPos = (SelectionStart / 3);
                int iLen = (abyCopy.Length < (m_abyData.Length - iPos)) ? abyCopy.Length : (m_abyData.Length - iPos);

                /*
                ** Yes it is, so display it in a text box.
                */
                for (int i = 0; i < iLen; i++)
                {
                    m_abyData[i + iPos] = abyCopy[i];
                }

                UpdateDisplay();
            }
            else
            {
                MessageBox.Show("Nothing to paste");
            }

            /*
            ** Restore our original position
            */
            SelectionStart = iSave;
        }


        /*
        ***************************************************************************
        ** 
        ** Function: GetDisplayForByte
        */
        ///<summary>
        /// Get the Display for a Data Byte
        ///</summary>
        ///<param name="byData" type="byte"></param>
        ///<example>byte = 0x42 = string = "42"
        ///</example>
        ///
        string GetDisplayForByte(byte byData)
        {
            return string.Format("{0:X2}", byData);
        }

        /*
        ***************************************************************************
        ** 
        ** Function: LoadData
        */
        ///<summary>
        /// Load data into the Hex Box
        ///</summary>
        ///<param name="abyData" type="byte[]"></param>
        ///<returns>void</returns>
        ///
        public void LoadData(byte[] abyData)
        {
            m_abyData = new byte[abyData.Length];
            Array.Copy(abyData, 0, m_abyData, 0, abyData.Length);
            UpdateDisplay();
        }

        /*
        ***************************************************************************
        ** 
        ** Function: NewData
        */
        ///<summary>
        ///Create a New Blank Display
        ///</summary>
        ///<param name="iSize" type="int"></param>
        ///<returns>void</returns>
        ///
        public void NewData(int iSize)
        {
            m_abyData = new byte[iSize];
            UpdateDisplay();
        }

        /*
        ***************************************************************************
        ** 
        ** Property(byte[]): Array
        */
        ///<summary>
        /// Get the Data that is the current buffer
        ///</summary>
        ///
        public byte[] ArrayData
        {
            get { return m_abyData; }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: UpdateDisplay
        */
        ///<summary>
        /// This updates the display with the data that is in abyData
        /// Can sync up if data has changed in the buffer due to a paste or
        /// like function
        ///</summary>
        ///
        public void UpdateDisplay()
        {
            /*
            ** Local Variables
            */
            int iLenData = m_abyData.Length;
            int iLenDisp = 0;
            int iCols = 0;
            StringBuilder sbVar = null;

            iCols = (this.Width / GetFontWidth());
            iCols = (iCols / 3) - 1;

            /*
            ** Get a copy of the Data so we can mess with it
            ** and also have it aligned where we want it
            */
            iLenDisp = ((iLenData + (iCols - 1)) / iCols) * iCols;
            sbVar = new StringBuilder(iLenDisp);

            for (int i = 0; i < iLenDisp; i++)
            {
                byte byData = 0;

                /*
                ** See if we are still in the real buffer range
                ** if so display data, else display spaces
                */
                if (i < iLenData)
                {
                    byData = m_abyData[i];
                    sbVar.Append(GetDisplayForByte(byData));
                }
                else
                {
                    sbVar.Append("  ");
                }

                if (0 != ((i + 1) % iCols))
                {
                    sbVar.Append(" ");
                }
                else
                {
                    sbVar.Append("\n");
                }
            }

            /*
            ** Remove the extra last char
            ** and display the data
            */
            sbVar.Remove(sbVar.Length - 1, 1);
            Text = sbVar.ToString();

        }

        /*
        ***************************************************************************
        ** 
        ** Function: GetByteAtCurrent
        */
        ///<summary>
        /// Gets the Byte at the current screen location in the hex editor
        ///</summary>
        ///<returns>byte</returns>
        ///
        protected byte GetByteAtCurrent()
        {
            int iPutBack = SelectionStart;
            int iStart = (SelectionStart - 1) / 3;
            byte by1 = 0;
            byte by2 = 0;

            /*
            ** Set the Current Position to where this byte begins
            */
            SelectionStart = iStart * 3;
            by1 = HexCtoB(GetCharFromPosition(CaretPosition));
            SelectionStart += 1;
            by2 = HexCtoB(GetCharFromPosition(CaretPosition));
            SelectionStart = iPutBack;

            by1 = (byte)(by1 << 4);
            by1 |= by2;

            return by1;
        }

        /*
        ***************************************************************************
        ** 
        ** Function: OnSelectionChange
        */
        ///<summary>
        ///This is our OnSelectionChange Listener
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///<remarks>
        ///here we can get our current position from selection start
        ///and if we are on a boundry (00^) then we skip one
        ///</remarks>
        ///
        protected void OnSelectionChange(object sender, System.EventArgs e)
        {
            if (!m_bSelChangeProcess)
            {
                m_bSelChangeProcess = true;
                int iPos = ((SelectionStart + 1) % 3);

                if ((0 == iPos) && !m_bIgnorePart)
                {
                    iPos = SelectionStart / 3;
                    if (iPos < m_abyData.Length)
                    {
                        m_abyData[iPos] = GetByteAtCurrent();
                        SelectionStart += 1;
                    }
                }
                m_bIgnorePart = false;
                m_bSelChangeProcess = false;
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: UpdateChar
        */
        ///<summary>
        /// Updates the character at the Position - Used to sync the
        /// LinkedBox and this box
        ///</summary>
        ///<param name="iPosition" type="int">Position of the Data To Update</param>
        ///<param name="cData" type="char">Data to Update with</param>
        ///<returns>void</returns>
        ///
        public void UpdateChar(int iPosition, char cData)
        {
            /*
            ** controled from the call to update to keep
            ** updates from linked window when we update it
            */
            if (!m_bNoUpdate)
            {
                /*
                ** Process any outstanding messages and set the focus to us
                */
                System.Windows.Forms.Application.DoEvents();
                Focus();

                /*
                ** Local Variables
                */
                int iPos = (iPosition * 3);
                string strVar = null;
                byte bData = (byte)cData;
                byte bHi = 0;
                byte bLo = 0;

                bLo = (byte)(bData & 0xF);
                bHi = (byte)(bData >> 4);

                strVar = string.Format("{0:X}{1:X}", bHi, bLo);
                SelectionStart = iPos;
                SelectionLength = 2;

                /*
                ** Send the keys to this window, and then process
                ** any ourstanding messages
                */
                SendKeys.Send(strVar);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: OnResizeBox
        */
        ///<summary>
        ///
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.EventArgs"></param>
        ///<returns>void</returns>
        ///
        protected void OnResizeBox(object sender, System.EventArgs e)
        {
            if (null != m_abyData)
            {
                UpdateDisplay();
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: OnKeyPress
        */
        ///<summary>
        /// KeyPress Listener Used to allow only the acceptable keys to be entered
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.Windows.Forms.KeyPressEventArgs"></param>
        ///<returns>void</returns>
        ///<exception cref="System.Exception">Thrown</exception>
        ///
        private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            /*
            ** see if it is in the allowed list
            */
            if (-1 != m_strAllowed.IndexOf(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                /*
                ** Are we still in the real data area,
                ** if so process if no done to it
                */
                if (Char.IsControl(e.KeyChar))
                    Console.WriteLine("");
//                else
                {
                    if (SelectionStart < (m_abyData.Length * 3))
                    {
                        /*
                        ** This forces a Replace instead of a insert
                        */
                        SelectionLength = 1;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {
                /*
                ** Local Variables
                */
                string strVar = string.Format("{0}", e.KeyChar).ToUpper();

                /*
                ** Tell the system not to handle this character
                */
                e.Handled = true;

                /*
                ** Ok lets see if the Upper case is in the list
                ** if it is good send the upper equavelent
                */
                if (-1 != m_strAllowed.IndexOf(strVar))
                {
                    SendKeys.Send(strVar);
                }
                else
                {
                    switch (e.KeyChar)
                    {
                        case (char)0x9:
                            break;
                    }
                }
            }
        }

        /*
        ***************************************************************************
        ** 
        ** Function: OnKeyDown
        */
        ///<summary>
        /// KeyPress Listener Used to disallow special keys from working
        ///</summary>
        ///<param name="sender" type="object"></param>
        ///<param name="e" type="System.Windows.Forms.KeyEventArgs"></param>
        ///<returns>void</returns>
        ///<exception cref="System.Exception">Thrown</exception>
        ///<remarks>
        ///</remarks>
        ///<example>How to use this function
        ///<code>
        ///</code>
        ///</example>
        ///
        protected void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = true;

            /*
            ** This allows the back arrow to work
            */
            switch (e.KeyCode)
            {
                default:
                    e.Handled = false;
                    break;

                case Keys.Left:
                    m_bIgnorePart = true;
                    e.Handled = false;
                    break;

                case Keys.Tab:
                    break;

                case Keys.Back:
                    {
                        int iPos = 0;

                        iPos = SelectionStart - 1;
                        if ((-1 != iPos) && (iPos < (m_abyData.Length * 3)))
                        {
                            SelectionStart = iPos;
                            m_bIgnorePart = true;
                        }
                    }
                    break;

                case Keys.Return:
                    {
                        CharPosition cp = Position;
                        int iPos = 0;

                        iPos = LineIndex(cp.LinePos + 1);
                        if ((-1 != iPos) && (iPos < (m_abyData.Length * 3)))
                        {
                            SelectionStart = iPos;
                        }
                    }
                    break;

                case Keys.Delete:
                    break;

                case Keys.Insert:
                    break;
            }

        }
    }

}