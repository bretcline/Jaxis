using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WebCam_Capture;
using JaxisInterfaces;
using JaxisEngine;
using JaxisEngine.Base;
using Jaxis.Util.Log4Net;
using BevClasses;
using BevWCFDB;

namespace BevCartUI
{   
    //public delegate void notifyParentCallbackType(int x, int y);
    
    public partial class Desktop : UserControl
    {
        public event EventHandler ClearKeyboardInput;
        public event EventHandler ChangesMadeToInventory;


        private bool bSwiping = false;
        private Point lastPoint = new Point();
        private int deltaX = 0;
        private int deltaY = 0;
        private int currentX = 0;
        private int currentY = 0;
        private bool bSettingLevel = false;
        private BevMessage lastMessage = null;
        //private bool bSwiping = false;
        //public notifyParentCallbackType notifyParent = null;
        private WebCamCapture m_WebCamCapture = new WebCamCapture();

        public Engine m_BevEngine;
        private UIDataConsumer m_Con;
        public List<Bottle> Bottles;
        public List<Beverage> Beverages;
        Dictionary<Guid, Bitmap> BeverageImages = new Dictionary<Guid, Bitmap>();
        WCFDB DB;

        public Desktop()
        {
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            btnSelectABrand.BackColor = Color.Transparent;
            btnInventory.BackColor = Color.Transparent;
            //pictureBox1.BackColor = Color.Transparent;
            panelReceiveInventory.Visible = true;
            panelReceiveInventory.Location = new Point(250, 110);
            panelInventory.Visible = true;
            panelInventory.Location = new Point(250, 280);
            panelBrand.Visible = false;
            panelBrand.Location = new Point(250, 280);
            panelSetFillLevel.Visible = false;
            panelSetFillLevel.Location = new Point(250, 280);
            inventoryList1.tableLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.inventoryList1_MouseMove);
            inventoryList1.tableLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.inventoryList1_MouseDown);
            inventoryList1.tableLayoutPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.inventoryList1_MouseUp);
            inventoryList1.deleteBottleFromInventry += new EventHandler(inventoryList1_deleteBottleFromInventry);
            beverageList1.tableLayoutPanel1.Controls.Clear();
            beverageList1.tableLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.brandList1_MouseMove);
            beverageList1.tableLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.brandList1_MouseDown);
            beverageList1.tableLayoutPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.brandList1_MouseUp);
            beverageList1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.brandList1_MouseMove);
            beverageList1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.brandList1_MouseDown);
            beverageList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.brandList1_MouseUp);
            beverageList1.BeverageSelected += new EventHandler(beverageList1_BeverageSelected);
            transactionList1.tableLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Desktop_MouseMove);
            transactionList1.tableLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Desktop_MouseDown);
            transactionList1.tableLayoutPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Desktop_MouseUp);
            
            txtSetFillLevel.Text = "100";
            //initializePanels();

            m_WebCamCapture.ImageCaptured += WebCamCapture_ImageCaptured;
            m_WebCamCapture.CaptureHeight = picBeverageImage.Height - 10;
            m_WebCamCapture.CaptureWidth = picBeverageImage.Width - 10;

            Disposed += OnDispose;

            IDeviceConfig Config = new DeviceConfig { Name = "Test Data Consumer", ID = Guid.NewGuid().ToString() };

            m_Con = new UIDataConsumer(Config);
            m_Con.Produce += ProcessData; // Using an IDevice Produce event to get data to Form...
            //initializeEngine();
            m_WebCamCapture.Start();
            //startEngine();
        }

        void inventoryList1_deleteBottleFromInventry(object sender, EventArgs e)
        {
            // We will need a DeleteBottle() call to remove the item from the inventory database;
            DB.RemoveBottle((sender as Bottle));
            //MessageBox.Show("need to implement DB.DeleteBottle()");
            refreshDatabaseLists();
        }

        public void initializeEngine()
        {
            m_BevEngine = new Engine();
            m_BevEngine.RegisterDevice(m_Con);
        }

        public void initializePanels()
        {
            
            DB = new WCFDB();
            refreshDatabaseLists();

        }

        private void refreshDatabaseLists()
        {
            if (DB != null)
            {
                if(Bottles != null)
                    Bottles.Clear();
                Bottles = DB.GetBottles();
                inventoryList1.Clear();
                foreach (Bottle this_bottle in Bottles)
                {
                    if (this_bottle.Beverage != null)
                    {
                        Bitmap bev_image = getBitmap(this_bottle.Beverage);
                        inventoryList1.InsertRow(this_bottle);
                    }
                }
                if(Beverages != null)
                    Beverages.Clear();
                Beverages = DB.GetBeverages();
                beverageList1.Clear();
                BeverageImages.Clear();
                foreach (Beverage this_beverage in Beverages)
                {
                    Bitmap this_image = null;
                    if (this_beverage.PicFile != "")
                    {
                        try
                        {
                            if (this_beverage.PicFile != null)
                            {
                                this_image = getBitmap(this_beverage);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.WriteException("Exception loading beverage image.", ex);
                        }
                    }
                    if (BeverageImages.ContainsKey(this_beverage.ID))
                        this_image = BeverageImages[this_beverage.ID];
                    beverageList1.InsertRow(this_image, this_beverage.Label, this_beverage.ID);
                }
            }
        }

        void beverageList1_BeverageSelected(object sender, EventArgs e)
        {
            foreach (Beverage this_beverage in Beverages)
            {
                if (beverageList1.SelectedBeverage == this_beverage.ID.ToString())
                {
                    Bitmap this_image = null;
                    if (BeverageImages.ContainsKey(this_beverage.ID))
                        this_image = BeverageImages[this_beverage.ID];

                    picBeverageImage.BackgroundImage = this_image;
                    picBeverageImage.Tag = beverageList1.SelectedBeverage;
                    ChangesMadeToInventory(this, new EventArgs());
                }
            }
            //MessageBox.Show("Event! "+ beverageList1.SelectedBeverage);
        }

        public void startEngine()
        {
            if (null != m_BevEngine)
            {
                m_BevEngine.Start();
            }
        }

        public void OnDispose(object sender, EventArgs e)
        {
            if (null != m_BevEngine)
            {
                m_BevEngine.Stop();
            }
        }

        private Bitmap getBitmap(Beverage bev)
        {
            Bitmap returnBitmap = null;
            if (BeverageImages.ContainsKey(bev.ID))
            {
                returnBitmap = BeverageImages[bev.ID];
            }
            else
            {
                returnBitmap = new Bitmap(bev.PicFile);
                BeverageImages.Add(bev.ID, returnBitmap);
            }
            return returnBitmap;
        }

        private string ProcessData(IMessage _Message)
        {
            string rc = null;
            BevMessage Msg = _Message as BevMessage;
            if (null != Msg)
            {
                string this_label = "";
                Bitmap this_image = null;
                SetPourText(string.Format("{0} {1} {2} {3}", Msg.Tag, Msg.ReadTime.ToString(), Msg.Pour.Duration.Seconds.ToString(), Msg.Pour.Amount.ToString()));
                setCurrentTransaction(Msg);

                if (lastMessage != null)
                {
                    this_label = "";
                    if (lastMessage.Pour != null)
                        if (lastMessage.Pour.Bottle != null)
                            if (lastMessage.Pour.Bottle.Beverage != null)
                            {
                                if (lastMessage.Pour.Bottle.Beverage.Label != null)
                                    this_label = lastMessage.Pour.Bottle.Beverage.Label;
                                this_image = getBitmap(lastMessage.Pour.Bottle.Beverage);
                            }
                    if (this_label == "")
                        this_label = lastMessage.ReadTime.ToString();

                    transactionList1.InsertRow(this_image, this_label, lastMessage.Pour.Duration.Seconds.ToString(), lastMessage.Pour.Amount.ToString());
                }
                lastMessage = Msg;
            }
            return rc;
        }

        delegate void setCurrentTransactionDelegate(BevMessage Msg);

        private void setCurrentTransaction(BevMessage Msg)
        {
            if (lastTransactionBeverageLabel.InvokeRequired)
            {
                lastTransactionBeverageLabel.Invoke(new setCurrentTransactionDelegate(setCurrentTransaction), Msg);
            }
            else
            {
                string this_label = "";
                Bitmap this_image = null;
                if (Msg.Pour != null)
                    if (Msg.Pour.Bottle != null)
                        if (Msg.Pour.Bottle.Beverage != null)
                        {
                            this_image = getBitmap(Msg.Pour.Bottle.Beverage);
                            if (Msg.Pour.Bottle.Beverage.Label != null)
                                this_label = Msg.Pour.Bottle.Beverage.Label;
                        }
                if (this_label == "")
                    this_label = Msg.ReadTime.ToString();
                lastTransactionBeverageLabel.Image = this_image;
                lastTransactionBeverateType.Text = this_label;
                lastTransactionPourTime.Text = Msg.Pour.Duration.Seconds.ToString();
                lastTransactionPourAmount.Text = Msg.Pour.Amount.ToString();
            }
        }

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);

        private void SetPourText(string _Text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (testMessage.InvokeRequired)
            {
                SetTextCallback d = SetPourText;
                Invoke(d, new object[] { _Text });
            }
            else
            {
                testMessage.Text = _Text;
            }
        }


        private void Desktop_MouseMove(object sender, MouseEventArgs e)
        {
            /*if (bSwiping)
            {
                int deltaX = lastPoint.X - e.X;
                int deltaY = lastPoint.Y - e.Y;
                if ( notifyParent != null )
                    notifyParent(deltaX, deltaY);
            }
            lastPoint.X = e.X;
            lastPoint.Y = e.Y;*/
        }

        private void Desktop_MouseDown(object sender, MouseEventArgs e)
        {
            panelInventory.Visible = false;
            panelBrand.Visible = false;
            /*bSwiping = true;
            lastPoint.X = e.X;
            lastPoint.Y = e.Y;*/
        }

        private void Desktop_MouseUp(object sender, MouseEventArgs e)
        {
            //bSwiping = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panelBrand.Visible = false;
            if (panelInventory.Visible == false)
                panelInventory.Visible = true;
            else
                panelInventory.Visible = false;
        }

        private void Desktop_Load(object sender, EventArgs e)
        {

        }

        public void moveInventoryList(int x, int y)
        {
            inventoryList1.Location = new Point(inventoryList1.Location.X - x, inventoryList1.Location.Y - y);
        }


        private void inventoryList1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bSwiping)
            {
                currentX = System.Windows.Forms.Cursor.Position.X;
                currentY = System.Windows.Forms.Cursor.Position.Y;
                deltaX = lastPoint.X - currentX;
                deltaY = lastPoint.Y - currentY;
                moveInventoryList(0, deltaY);
                lastPoint.X = currentX;
                lastPoint.Y = currentY;
            }

        }

        private void inventoryList1_MouseDown(object sender, MouseEventArgs e)
        {
            bSwiping = true;
            lastPoint.X = System.Windows.Forms.Cursor.Position.X;
            lastPoint.Y = System.Windows.Forms.Cursor.Position.Y;

        }

        private void inventoryList1_MouseUp(object sender, MouseEventArgs e)
        {
            bSwiping = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelBrand.Visible = false;
            if (panelInventory.Visible == false)
                panelInventory.Visible = true;
            else
                panelInventory.Visible = false;

        }

        private void pictureBox10_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void new_pictureBox_MouseUp(object sender, MouseEventArgs e)
        {

        }

      /*  private void pictureBox15_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox15.BackgroundImage = BevCartUI.Properties.Resources.SelectABrand_Down;
        }

        private void pictureBox15_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox15.BackgroundImage = BevCartUI.Properties.Resources.SelectABrand_Up;

        }*/
        private void toggleBrandPanel(bool alwaysShow)
        {
            panelSetFillLevel.Visible = false;
            panelInventory.Visible = false;
            if (alwaysShow)
            {
                panelBrand.Visible = true;
            }
            else
            {
                if (panelBrand.Visible == false)
                    panelBrand.Visible = true;
                else
                    panelBrand.Visible = false;
            }
        }

        private void toggleInventoryPanel(bool alwaysShow)
        {
            panelSetFillLevel.Visible = false;
            panelBrand.Visible = false;
            if (alwaysShow)
            {
                panelInventory.Visible = true;
            }
            else
            {
                if (panelInventory.Visible == false)
                    panelInventory.Visible = true;
                else
                    panelInventory.Visible = false;
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            toggleBrandPanel(false);
        }

        public void moveBrandList(int x, int y)
        {
            beverageList1.Location = new Point(beverageList1.Location.X - x, beverageList1.Location.Y - y);
        }

        private void brandList1_MouseDown(object sender, MouseEventArgs e)
        {
            bSwiping = true;
            lastPoint.X = System.Windows.Forms.Cursor.Position.X;
            lastPoint.Y = System.Windows.Forms.Cursor.Position.Y;

        }

        private void brandList1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bSwiping)
            {
                currentX = System.Windows.Forms.Cursor.Position.X;
                currentY = System.Windows.Forms.Cursor.Position.Y;
                deltaX = lastPoint.X - currentX;
                deltaY = lastPoint.Y - currentY;
                moveBrandList(0, deltaY);
                lastPoint.X = currentX;
                lastPoint.Y = currentY;
            }

        }

        private void brandList1_MouseUp(object sender, MouseEventArgs e)
        {
            bSwiping = false;

        }

        private void btnSelectABrand_Click(object sender, EventArgs e)
        {
            panelInventory.Visible = false;
            if (panelBrand.Visible == false)
                panelBrand.Visible = true;
            else
                panelBrand.Visible = false;

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            panelSetFillLevel.Visible = false;
            panelBrand.Visible = false;
            if (panelInventory.Visible == false)
                panelInventory.Visible = true;
            else
                panelInventory.Visible = false;

        }

        private void pictureBox13_MouseDown(object sender, MouseEventArgs e)
        {
            bSettingLevel = true;
            picBottleEmpty.Size = new Size(picBottleEmpty.Size.Width, e.Y);
        }

        private void pictureBox13_MouseMove(object sender, MouseEventArgs e)
        {
            if (bSettingLevel)
            {
                if (e.Y < 333)
                {
                    picBottleEmpty.Size = new Size(picBottleEmpty.Size.Width, e.Y);
                    double level = 0;
                    if (e.Y < 86)
                    {
                        txtSetFillLevel.Text = "100";
                        lblFillLevel.Text = txtSetFillLevel.Text;
                    }
                    else
                    {
                        level = (e.Y - 86) / 247.0;
                        txtSetFillLevel.Text = Convert.ToInt32(100 - level * 100).ToString();
                        lblFillLevel.Text = txtSetFillLevel.Text;
                    }
                }
            }
        }

        private void pictureBox13_MouseUp(object sender, MouseEventArgs e)
        {
            bSettingLevel = false;
        }

        private void label21_Click(object sender, EventArgs e)
        {
            panelSetFillLevel.Visible = true;
            panelInventory.Visible = false;
            panelBrand.Visible = false;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            panelSetFillLevel.Visible = true;
            panelInventory.Visible = false;
            panelBrand.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // close the SetFillLevel panel and open the Inventory panel.
            panelSetFillLevel.Visible = false;
            if ((picBeverageImage.Tag as string) == "")
            {
                panelInventory.Visible = false;
                panelBrand.Visible = true;
            }
            else
            {
                panelInventory.Visible = true;
                panelBrand.Visible = false;
            }
            ChangesMadeToInventory(this, new EventArgs());            
        }

        private void Desktop_ControlRemoved(object sender, ControlEventArgs e)
        {
            // stop the video capture since the desktop control is going away.
            m_WebCamCapture.Stop();
        }

        private void btnWebCam_Click(object sender, EventArgs e)
        {
            // start the video capture.
            m_WebCamCapture.DoTakePicture(0);

        }

        private void WebCamCapture_ImageCaptured(object source, WebcamEventArgs e, int Frame)
        {
            // set the picturebox picture
            picBeverageImage.BackgroundImage = e.WebCamImage;
            picBeverageImage.Tag = new Guid().ToString(); 
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // Attempt to brand an RFID tag to a bottle. First check to make sure the required fields 
            // have been filled in. If not, pop up a simple message box.
            if (txtUPCCode.Text == "" || txtDeviceID.Text == "" || (picBeverageImage.Tag as string) == "")
            {
                string messageBoxText = "The following conditions have not been met:";
                int nConditions = 0;
                if (txtUPCCode.Text == "")
                {
                    ++nConditions;
                    messageBoxText += "\n" + nConditions + ". You must scan a bottle.";
                }
                if (txtDeviceID.Text == "")
                {
                    ++nConditions;
                    messageBoxText += "\n" + nConditions + ". No RFID tag was detected.";
                }
                if ((picBeverageImage.Tag as string) == "")
                {
                    ++nConditions;
                    messageBoxText += "\n" + nConditions + ". You have not selected a beverage type.";
                }
                MessageBox.Show(messageBoxText);
            }
            else
            {
                // Create a new bottle object and populate all of the important fields.
                Bottle new_bottle = new Bottle();

                // First, check to see if this is an existing bottle. If so then I suppose we might
                // want to brand a new rfid tag to it. Perhaps the existing one isn't working.
                Bottle existing_bottle = null;
                foreach (Bottle this_bottle in Bottles)
                {
                    if (this_bottle.UPC == txtUPCCode.Text)
                        existing_bottle = this_bottle;
                }
                // If we found the bottle, then we should use the existing bottle object.
                if (existing_bottle != null)
                    new_bottle = existing_bottle;

                // If the bottle object doesn't have a UPC code, then assume that it should get a new one.
                if (new_bottle.UPC == "" || new_bottle.UPC == null)
                    new_bottle.UPC = txtUPCCode.Text;


                // See if this bottle object already has a beverage type assigned to it. If not, use
                // the one that is currently selected.
                if (existing_bottle == null)
                {
                    // Check to see if this is an existing beverage type. If not, assume that its a new one 
                    // created with the webcam.
                    Beverage existing_beverage = null;
                    foreach (Beverage this_beverage in Beverages)
                    {
                        if (this_beverage.ID.ToString() == (picBeverageImage.Tag as string))
                            existing_beverage = this_beverage;
                    }
                    if (existing_beverage != null)
                        new_bottle.Beverage = existing_beverage;
                    else
                        new_bottle.Beverage = new Beverage();

                    // If this appears to be a new beverage type, then we need to save the webcam image and populate
                    // the new beverage object with some data.
                    if (existing_beverage == null)
                    {
                        new_bottle.Beverage.ID = Guid.NewGuid();
                        string destinationFilename = "images\\" + new_bottle.Beverage.ID.ToString() + ".png";
                        Image img = picBeverageImage.BackgroundImage;
                        img.Save(destinationFilename, ImageFormat.Png);
                        new_bottle.Beverage.PicFile = destinationFilename;
                        if (txtLabel.Text != "")
                            new_bottle.Beverage.Label = txtLabel.Text;
                        else
                            new_bottle.Beverage.Label = "Generic";
                        DB.AddUpdateBeverage(new_bottle.Beverage);
                    }
                    new_bottle.QuantityLeft = Convert.ToDouble(txtSetFillLevel.Text); //This probably needs to be converted to an amount.

                    // Add the new bottle and beverage to the database.
                    new_bottle.Tag = "Tag";
                    new_bottle.Cart = "Cart";
                    DB.AddUpdateBottle(new_bottle);

                    // Since we just added a new bottle to the database and we don't have a good Guid ID for the object,
                    // it probably won't hurt to refresh the bottle list and beverage list.
                    refreshDatabaseLists();
                    toggleInventoryPanel(true);
                    //if (existing_bottle == null)
                    //    Bottles.Add(new_bottle);
                }
                
            }
        }

        private void picBeverageImage_DoubleClick(object sender, EventArgs e)
        {
            toggleBrandPanel(true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUPCCode.Text = "";
            txtDeviceID.Text = "";
            txtSetFillLevel.Text = "100";
            picBeverageImage.BackgroundImage = global::BevCartUI.Properties.Resources.bottle_faded;
            picBeverageImage.Tag = null;
            if (ChangesMadeToInventory != null)
                ClearKeyboardInput(this, new EventArgs());

        }

        private void txtUPCCode_TextChanged(object sender, EventArgs e)
        {
            if (ChangesMadeToInventory != null)
                ChangesMadeToInventory(this, new EventArgs());            
        }

        private void txtDeviceID_TextChanged(object sender, EventArgs e)
        {
            if (ChangesMadeToInventory != null)
                ChangesMadeToInventory(this, new EventArgs());            
        }

        private void txtSetFillLevel_TextChanged(object sender, EventArgs e)
        {
            if ( ChangesMadeToInventory != null )
                ChangesMadeToInventory(this, new EventArgs());            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /*protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }*/
    }
}
