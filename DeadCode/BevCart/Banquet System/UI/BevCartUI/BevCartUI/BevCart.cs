using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BevCartUI
{
    public partial class BevCart : Form
    {
        private Point lastPoint = new Point();
        private bool bSwiping = false;
        private int deltaX = 0;
        private int deltaY = 0;
        private int currentX = 0;
        private int currentY = 0;
        private List<int> horizontalPoints = new List<int>();
        private int targetXPos = 0;
        private int targetYPos = 0;
        private Point mouseDownPoint = new Point();
        private Point mouseUpPoint = new Point();
        private bool bLockHorizontal = false;
        private bool bLockVertical = false;
        private int nCurrentScreen = 1;
        private string currentInputString = "";
        private SplashScreenDialog splashScreenDialog = new SplashScreenDialog();
        private bool bNumbersEntered = false;
        private bool bLettersEntered = false;


        public BevCart()
        {
            this.Hide();
            splashScreenDialog.ShowSplashScreen();
            splashScreenDialog.SetStatus("Initializing");
            InitializeComponent();
            //desktop1.notifyParent = new notifyParentCallbackType(moveDesktop);
            //tableLayoutPanel1
          /*  this.desktop1.inventoryList1.tableLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseMove);
            this.desktop1.inventoryList1.tableLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseDown);
            this.desktop1.inventoryList1.tableLayoutPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseUp);
            */
            setMouseHandlersForPanel(this.desktop1.lastTransactionPanel);
            setMouseHandlersForTableLayoutPanel(this.desktop1.transactionList1.tableLayoutPanel1);
            setMouseHandlersForPanel(this.desktop1.transactionListPanel);
            setMouseHandlersForLabel(this.desktop1.lastTransactionBeverateType);
            setMouseHandlersForLabel(this.desktop1.lastTransactionPourTime);
            setMouseHandlersForLabel(this.desktop1.lastTransactionPourAmount);
            setMouseHandlersForPictureBox(this.desktop1.lastTransactionBeverageLabel);
            setMouseHandlersForPictureBox(this.desktop1.lastTransactionHeader);
            setMouseHandlersForPictureBox(this.desktop1.lastTransactionFooter);
            setMouseHandlersForPictureBox(this.desktop1.transactionListHeader);
            setMouseHandlersForPictureBox(this.desktop1.transactionListFooter);
            desktop1.transactionList1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseMove);
            desktop1.transactionList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseUp);
            desktop1.transactionList1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseDown);
            desktop1.ClearKeyboardInput += new EventHandler(desktop1_ClearKeyboardInput);
            desktop1.ChangesMadeToInventory += new EventHandler(desktop1_ChangesMadeToInventory);
            desktop1.initializePanels();
            desktop1.initializeEngine();
            desktop1.startEngine();

            timer1.Interval = 50;
            timer1.Start();
            timer2.Interval = 50;
            timer2.Start();
            timer2.Enabled = false;
            timer3.Interval = 250;
            timer3.Start();
            timer3.Enabled = true;

            //desktop1.BackColor = Color.Transparent;
            prevPage.BackColor = Color.Transparent;
            nextPage.BackColor = Color.Transparent;
            statusMessage.BackColor = Color.Transparent;
            SectionHeader.BackColor = Color.FromArgb(162, 6, 11); //a20611
            SectionHeader.Text = "Receive Inventory";
            splashScreenDialog.CloseSplashScreen();


        }

        private void desktop1_ChangesMadeToInventory(object sender, EventArgs e)
        {
            displayCurrentStatusMessage();
        }

        private void BevCart_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
        }

        void desktop1_ClearKeyboardInput(object sender, EventArgs e)
        {
            currentInputString = "";
            displayCurrentStatusMessage();
        }

        private void setMouseHandlersForTableLayoutPanel(TableLayoutPanel target)
        {
            target.MouseMove += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseMove);
            target.MouseUp += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseUp);
            target.MouseDown += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseDown);
        }

        private void setMouseHandlersForPanel(Panel target)
        {
            target.MouseMove += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseMove);
            target.MouseUp += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseUp);
            target.MouseDown += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseDown);
        }

        private void setMouseHandlersForLabel(Label target)
        {
            target.MouseMove += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseMove);
            target.MouseUp += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseUp);
            target.MouseDown += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseDown);
        }

        private void setMouseHandlersForPictureBox(PictureBox target)
        {
            target.MouseMove += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseMove);
            target.MouseUp += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseUp);
            target.MouseDown += new System.Windows.Forms.MouseEventHandler(this.desktop1_MouseDown);
        }

        private int getClosestHorizontalPosition(int x)
        {
            int returnPos = 10000;
            int new_val = 0;
            if (x > 0)
                return 0;
            else
            {
                int diff = 0;
                int prev_diff = 10000;
                for (int j = 2; j >= 0; --j)
                {
                    new_val = (-j * panelDesktopContainer.Size.Width);
                    diff = System.Math.Abs(System.Math.Abs(new_val) - System.Math.Abs(x));
                    if (diff < prev_diff)
                    {
                        returnPos = new_val;
                        prev_diff = diff;
                    }
                }
                return returnPos;
            }
        }

        private int getClosestVerticalPosition(int y)
        {
            int returnPos = 10000;
            int new_val = 0;
            if (y > 0)
                return 0;
            else
            {
                int diff = 0;
                int prev_diff = 10000;
                for (int j = 1; j >= 0; --j)
                {
                    new_val = (-j * panelDesktopContainer.Size.Height);
                    diff = System.Math.Abs(System.Math.Abs(new_val) - System.Math.Abs(y));
                    if (diff < prev_diff)
                    {
                        returnPos = new_val;
                        prev_diff = diff;
                    }
                }
                return returnPos;
            }
        }

        private int getNextHorizontalPosition(int x)
        {
            return getClosestHorizontalPosition(x + panelDesktopContainer.Width);
        }

        private int getPrevHorizontalPosition(int x)
        {
            return getClosestHorizontalPosition(x - panelDesktopContainer.Width);
        }

        private int getNextVerticalPosition(int x)
        {
            return getClosestVerticalPosition(x + panelDesktopContainer.Height);
        }

        private int getPrevVerticalPosition(int x)
        {
            return getClosestVerticalPosition(x - panelDesktopContainer.Height);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        public void moveDesktop(int x, int y)
        {
            this.desktop1.Location = new Point(desktop1.Location.X - x, desktop1.Location.Y - y);
        }

        private void desktop1_MouseMove(object sender, MouseEventArgs e)
        {
            if (bSwiping)
            {
                prevPage.Visible = false;
                nextPage.Visible = false;
                currentX = System.Windows.Forms.Cursor.Position.X;
                currentY = System.Windows.Forms.Cursor.Position.Y;
                deltaX = lastPoint.X - currentX;
                deltaY = lastPoint.Y - currentY;
                //if ( bLockVertical )
                //    moveDesktop(0, deltaY);
                //else
                    moveDesktop(deltaX, 0);

                if (lastPoint.X != currentX && lastPoint.Y == currentY && !bLockVertical)
                    bLockHorizontal = true;
                else if (lastPoint.X == currentX && lastPoint.Y != currentY && !bLockHorizontal)
                    bLockVertical = true;
                lastPoint.X = currentX;
                lastPoint.Y = currentY;
            }

        }

        private void desktop1_MouseDown(object sender, MouseEventArgs e)
        {
            bSwiping = true;
            lastPoint.X = System.Windows.Forms.Cursor.Position.X;
            lastPoint.Y = System.Windows.Forms.Cursor.Position.Y;
            mouseDownPoint = System.Windows.Forms.Cursor.Position;
            timer1.Enabled = false;
        }

        private void desktop1_MouseUp(object sender, MouseEventArgs e)
        {
            bSwiping = false;
            bLockHorizontal = false;
            bLockVertical = false;
            lastPoint.X = -1;
            lastPoint.Y = -1;

            mouseUpPoint = System.Windows.Forms.Cursor.Position;
            if (System.Math.Abs(mouseDownPoint.X - mouseUpPoint.X) < 5 && System.Math.Abs(mouseDownPoint.Y - mouseUpPoint.Y) < 5)
            {
                Point clientPoint = PointToClient(System.Windows.Forms.Cursor.Position);
                if (clientPoint.X < 100)
                {
                    targetXPos = getNextHorizontalPosition(this.desktop1.Location.X);
                }
                else if (clientPoint.X > this.Width - 100)
                {
                    targetXPos = getPrevHorizontalPosition(this.desktop1.Location.X);
                }
                else if (clientPoint.Y < 100)
                {
                    targetYPos = getNextVerticalPosition(this.desktop1.Location.Y);
                }
                else if (clientPoint.Y > this.Height - 100)
                {
                    targetYPos = getPrevVerticalPosition(this.desktop1.Location.Y);
                }
            }
            else
            {
                targetXPos = getClosestHorizontalPosition(this.desktop1.Location.X);
                targetYPos = getClosestVerticalPosition(this.desktop1.Location.Y);
            }
            timer1.Enabled = true;
        }

     /*   private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
        }
        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("tableLayoutPanel1 mouseup");
        }
        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            float new_xpos = ((targetXPos + this.desktop1.Location.X) / 2);
            float new_ypos = ((targetYPos + this.desktop1.Location.Y) / 2);
            if (System.Math.Abs(new_xpos - this.desktop1.Location.X) <= 1.0)
                new_xpos = targetXPos;
            if (System.Math.Abs(new_ypos - this.desktop1.Location.Y) <= 1.0)
                new_ypos = targetYPos;
            if (new_xpos == targetXPos && new_ypos == targetYPos)
            {
                timer1.Enabled = false;
                calculateScreenPosition();
            }
            this.desktop1.Location = new Point((int)new_xpos, (int)new_ypos);
        }

        private void calculateScreenPosition()
        {
            nCurrentScreen = (int)System.Math.Abs(targetXPos / panelDesktopContainer.Size.Width);
            if (nCurrentScreen == 2)
            {
                screenIndicator1.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
                screenIndicator2.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
                screenIndicator3.BackgroundImage = global::BevCartUI.Properties.Resources.square_white;
                prevPage.Visible = true;
                nextPage.Visible = false;
                SectionHeader.Text = "Invoice";
                statusMessage.Text = "";
            }
            else if (nCurrentScreen == 1)
            {
                screenIndicator1.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
                screenIndicator2.BackgroundImage = global::BevCartUI.Properties.Resources.square_white;
                screenIndicator3.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
                prevPage.Visible = true;
                nextPage.Visible = true;
                SectionHeader.Text = "Transaction History";
                statusMessage.Text = "";
            }
            else
            {
                screenIndicator1.BackgroundImage = global::BevCartUI.Properties.Resources.square_white;
                screenIndicator2.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
                screenIndicator3.BackgroundImage = global::BevCartUI.Properties.Resources.square_black;
                prevPage.Visible = false;
                nextPage.Visible = true;
                SectionHeader.Text = "Receive Inventory";
                displayCurrentStatusMessage();
            }
        }

        private void displayCurrentStatusMessage()
        {
            if (desktop1.txtUPCCode.Text != "" && desktop1.txtDeviceID.Text != "" && (desktop1.picBeverageImage.Tag as string) == "")
                statusMessage.Text = "Set the fill level and choose a beverage type for this bottle";
            else if (desktop1.txtUPCCode.Text == "" && desktop1.txtDeviceID.Text == "" && (desktop1.picBeverageImage.Tag as string) != "")
                statusMessage.Text = "Scan the UPC label or tilt the RFID tag.";
            else if (desktop1.txtUPCCode.Text == "" && desktop1.txtDeviceID.Text != "" && (desktop1.picBeverageImage.Tag as string) != "")
                statusMessage.Text = "Scan the UPC label.";
            else if (desktop1.txtUPCCode.Text != "" && desktop1.txtDeviceID.Text == "" && (desktop1.picBeverageImage.Tag as string) != "")
                statusMessage.Text = "Tilt the RFID tag.";
            else if (desktop1.txtUPCCode.Text != "" && desktop1.txtDeviceID.Text == "" && (desktop1.picBeverageImage.Tag as string) == "")
                statusMessage.Text = "Select a Brand for this beverage or take a picture of the product.";
            else if (desktop1.txtUPCCode.Text != "" && desktop1.txtDeviceID.Text != "" && (desktop1.picBeverageImage.Tag as string) != "")
                statusMessage.Text = "Set the fill level or click \"Add Item\" to add this item to the inventory.";
            else
                statusMessage.Text = "To receive an item into inventory, scan the UPC label, take a picture, or tilt an RFID tag.";
        }

        private void desktop1_Load(object sender, EventArgs e)
        {
        }

        private void prevPage_Click(object sender, EventArgs e)
        {
            targetXPos = getNextHorizontalPosition(this.desktop1.Location.X);
            timer1.Enabled = true;
            prevPage.Visible = false;
            nextPage.Visible = false;
           
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            targetXPos = getPrevHorizontalPosition(this.desktop1.Location.X);
            timer1.Enabled = true;
            prevPage.Visible = false;
            nextPage.Visible = false;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (splashScreenDialog.Visible)
            {
                this.Hide();

            }
            else
            {
                timer3.Enabled = false;
                this.ShowInTaskbar = true;
                this.Show();
                this.BringToFront();
            }
        }

        private void picHeader_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
                this.TopMost = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.TopMost = false;
            }
        }

        private void BevCart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            {
                if (e.KeyChar >= 48 && e.KeyChar <= 57)
                    bNumbersEntered = true;
                else
                    bLettersEntered = true;
                currentInputString += e.KeyChar;
                statusMessage.Text = "Current input: " + currentInputString;
            }
            else
            {
                handleInputString();
            }
            e.Handled = true;
        }

        private void BevCart_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("key down");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                handleInputString();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void handleInputString()
        {
            if (bLettersEntered)
                desktop1.txtLabel.Text = currentInputString;
            else
            {
                if (currentInputString.Length > 9)
                {
                    desktop1.txtUPCCode.Text = currentInputString;
                }
                else if (currentInputString.Length > 3)
                {
                    desktop1.txtDeviceID.Text = currentInputString;
                }
                else
                {
                    desktop1.txtSetFillLevel.Text = currentInputString;
                }
            }
            bLettersEntered = false;
            bNumbersEntered = false;
            currentInputString = "";
            displayCurrentStatusMessage();
        }
    }
}
