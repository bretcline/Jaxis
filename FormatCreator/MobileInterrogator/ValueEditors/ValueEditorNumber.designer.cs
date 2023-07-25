using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MobileInterrogator
{
    public partial class ValueEditorNumber
    {
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn1 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn2 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn3 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn4 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn5 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn6 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn7 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn8 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn9 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btn0 = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnDecimal = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnSign = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnBackspace = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.btnClear = new LFI.Mobile.Controls.Button.SkinnedButton();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(232, 12);
            this.textBox1.TabIndex = 0;
            // 
            // btn1 
            // 
            this.btn1.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.btn1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn1.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn1.ForeColorDown = System.Drawing.Color.Black;
            this.btn1.ShowIcon = false;
            this.btn1.Location = new System.Drawing.Point(82, 40);
            this.btn1.Size = new System.Drawing.Size(48, 48);
            this.btn1.ItemShiftOnClick = true;
            this.btn1.Name = "btn1";
            this.btn1.Text = "1";
            this.btn1.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn1.UseTransparency = true;
            this.btn1.Click += new System.EventHandler(this.OnClickNumber1);
            // 
            // btn2 
            // 
            this.btn2.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold); 
            this.btn2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn2.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn2.ForeColorDown = System.Drawing.Color.Black;
            this.btn2.ShowIcon = false;
            this.btn2.Location = new System.Drawing.Point(134, 40);
            this.btn2.Size = new System.Drawing.Size(48, 48);
            this.btn2.ItemShiftOnClick = true;
            this.btn2.Name = "btn2";
            this.btn2.Text = "2";
            this.btn2.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn2.UseTransparency = true;
            this.btn2.Click += new System.EventHandler(this.OnClickNumber2);
            // 
            // btn3 
            // 
            this.btn3.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.btn3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn3.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn3.ForeColorDown = System.Drawing.Color.Black;
            this.btn3.ShowIcon = false;
            this.btn3.Location = new System.Drawing.Point(186, 40);
            this.btn3.Size = new System.Drawing.Size(48, 48);
            this.btn3.ItemShiftOnClick = true;
            this.btn3.Name = "btn3";
            this.btn3.Text = "3";
            this.btn3.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn3.UseTransparency = true;
            this.btn3.Click += new System.EventHandler(this.OnClickNumber3);
            // 
            // btn4 
            // 
            this.btn4.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.btn4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn4.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn4.ForeColorDown = System.Drawing.Color.Black;
            this.btn4.ShowIcon = false;
            this.btn4.Location = new System.Drawing.Point(82, 92);
            this.btn4.Size = new System.Drawing.Size(48, 48);
            this.btn4.ItemShiftOnClick = true;
            this.btn4.Name = "btn4";
            this.btn4.Text = "4";
            this.btn4.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn4.UseTransparency = true;
            this.btn4.Click += new System.EventHandler(this.OnClickNumber4);
            // 
            // btn5 
            // 
            this.btn5.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold); 
            this.btn5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn5.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn5.ForeColorDown = System.Drawing.Color.Black;
            this.btn5.ShowIcon = false;
            this.btn5.Location = new System.Drawing.Point(134, 92);
            this.btn5.Size = new System.Drawing.Size(48, 48);
            this.btn5.ItemShiftOnClick = true;
            this.btn5.Name = "btn5";
            this.btn5.Text = "5";
            this.btn5.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn5.UseTransparency = true;
            this.btn5.Click += new System.EventHandler(this.OnClickNumber5);
            // 
            // btn6 
            // 
            this.btn6.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.btn6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn6.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn6.ForeColorDown = System.Drawing.Color.Black;
            this.btn6.ShowIcon = false;
            this.btn6.Location = new System.Drawing.Point(186, 92);
            this.btn6.Size = new System.Drawing.Size(48, 48);
            this.btn6.ItemShiftOnClick = true;
            this.btn6.Name = "btn6";
            this.btn6.Text = "6";
            this.btn6.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn6.UseTransparency = true;
            this.btn6.Click += new System.EventHandler(this.OnClickNumber6);
            // 
            // btn7 
            // 
            this.btn7.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold); 
            this.btn7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn7.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn7.ForeColorDown = System.Drawing.Color.Black;
            this.btn7.ShowIcon = false;
            this.btn7.Location = new System.Drawing.Point(82, 144);
            this.btn7.Size = new System.Drawing.Size(48, 48);
            this.btn7.ItemShiftOnClick = true;
            this.btn7.Name = "btn7";
            this.btn7.Text = "7";
            this.btn7.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn7.UseTransparency = true;
            this.btn7.Click += new System.EventHandler(this.OnClickNumber7);
            // 
            // btn8 
            // 
            this.btn8.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold); 
            this.btn8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn8.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn8.ForeColorDown = System.Drawing.Color.Black;
            this.btn8.ShowIcon = false;
            this.btn8.Location = new System.Drawing.Point(134, 144);
            this.btn8.Size = new System.Drawing.Size(48, 48);
            this.btn8.ItemShiftOnClick = true;
            this.btn8.Name = "btn8";
            this.btn8.Text = "8";
            this.btn8.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn8.UseTransparency = true;
            this.btn8.Click += new System.EventHandler(this.OnClickNumber8);
            // 
            // btn9 
            // 
            this.btn9.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.btn9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn9.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn9.ForeColorDown = System.Drawing.Color.Black;
            this.btn9.ShowIcon = false;
            this.btn9.Location = new System.Drawing.Point(186, 144);
            this.btn9.Size = new System.Drawing.Size(48, 48);
            this.btn9.ItemShiftOnClick = true;
            this.btn9.Name = "btn9";
            this.btn9.Text = "9";
            this.btn9.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn9.UseTransparency = true;
            this.btn9.Click += new System.EventHandler(this.OnClickNumber9);
            // 
            // btnDecimal 
            //
            this.btnDecimal.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.btnDecimal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDecimal.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnDecimal.ForeColorDown = System.Drawing.Color.Black;
            this.btnDecimal.ShowIcon = false;
            this.btnDecimal.Location = new System.Drawing.Point(82, 196);
            this.btnDecimal.Size = new System.Drawing.Size(48, 48);
            this.btnDecimal.ItemShiftOnClick = true;
            this.btnDecimal.Name = "btnDecimal";
            this.btnDecimal.Text = ".";
            this.btnDecimal.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnDecimal.UseTransparency = true;
            this.btnDecimal.Click += new System.EventHandler(this.OnClickDecimal);
            // 
            // btn0 
            // 
            this.btn0.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.btn0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn0.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btn0.ForeColorDown = System.Drawing.Color.Black;
            this.btn0.ShowIcon = false;
            this.btn0.Location = new System.Drawing.Point(134, 196);
            this.btn0.Size = new System.Drawing.Size(48, 48);
            this.btn0.ItemShiftOnClick = true;
            this.btn0.Name = "btn0";
            this.btn0.Text = "0";
            this.btn0.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btn0.UseTransparency = true;
            this.btn0.Click += new System.EventHandler(this.OnClickNumber0);
            // 
            // btnSign 
            // 
            this.btnSign.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnSign.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSign.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnSign.ForeColorDown = System.Drawing.Color.Black;
            this.btnSign.ShowIcon = true;
            this.btnSign.IconSize = new System.Drawing.Size(24, 24);
            this.btnSign.Icon = IconResources.Sign;
            this.btnSign.IconDisabled = IconResources.Sign;
            this.btnSign.IconAlign = LFI.Mobile.Controls.Align.Center;
            this.btnSign.Location = new System.Drawing.Point(186, 196);
            this.btnSign.Size = new System.Drawing.Size(48, 48);
            this.btnSign.ItemShiftOnClick = true;
            this.btnSign.Name = "btnSign";
            this.btnSign.Text = "";
            this.btnSign.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnSign.UseTransparency = true;
            this.btnSign.Click += new System.EventHandler(this.OnClickSign);
            // 
            // btnBackspace 
            // 
            this.btnBackspace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBackspace.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnBackspace.ForeColorDown = System.Drawing.Color.Black;            
            this.btnBackspace.ShowIcon = true;
            this.btnBackspace.IconSize = new System.Drawing.Size(24, 24);
            this.btnBackspace.Icon = IconResources.Backspace;
            this.btnBackspace.IconDisabled = IconResources.Backspace;
            this.btnBackspace.IconAlign = LFI.Mobile.Controls.Align.Center;
            this.btnBackspace.Location = new System.Drawing.Point(4, 40);
            this.btnBackspace.Size = new System.Drawing.Size(48, 48);
            this.btnBackspace.ItemShiftOnClick = true;
            this.btnBackspace.Name = "btnBackspace";
            this.btnBackspace.Text = "";
            this.btnBackspace.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnBackspace.UseTransparency = true;
            this.btnBackspace.Click += new System.EventHandler(this.OnClickBackspace);
            // 
            // btnClear 
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.ForeColorDisabled = System.Drawing.Color.DimGray;
            this.btnClear.ForeColorDown = System.Drawing.Color.Black;
            this.btnClear.ShowIcon = false;
            this.btnClear.Location = new System.Drawing.Point(4, 92);
            this.btnClear.Size = new System.Drawing.Size(48, 48);
            this.btnClear.ItemShiftOnClick = true;
            this.btnClear.Name = "btnClear";
            this.btnClear.Text = "Clear";
            this.btnClear.TextAlign = LFI.Mobile.Controls.Align.Center;
            this.btnClear.UseTransparency = true;
            this.btnClear.Click += new System.EventHandler(this.OnClickClear);
            // 
            // ValueEditorNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btnDecimal);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.btnBackspace);
            this.Controls.Add(this.btnClear);
            this.Name = "ValueEditorNumber";
            this.Size = new System.Drawing.Size(147, 155);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TextBox textBox1;
        private LFI.Mobile.Controls.Button.SkinnedButton btn1;
        private LFI.Mobile.Controls.Button.SkinnedButton btn2;
        private LFI.Mobile.Controls.Button.SkinnedButton btn3;
        private LFI.Mobile.Controls.Button.SkinnedButton btn4;
        private LFI.Mobile.Controls.Button.SkinnedButton btn5;
        private LFI.Mobile.Controls.Button.SkinnedButton btn6;
        private LFI.Mobile.Controls.Button.SkinnedButton btn7;
        private LFI.Mobile.Controls.Button.SkinnedButton btn8;
        private LFI.Mobile.Controls.Button.SkinnedButton btn9;
        private LFI.Mobile.Controls.Button.SkinnedButton btn0;
        private LFI.Mobile.Controls.Button.SkinnedButton btnDecimal;
        private LFI.Mobile.Controls.Button.SkinnedButton btnSign;
        private LFI.Mobile.Controls.Button.SkinnedButton btnBackspace;
        private LFI.Mobile.Controls.Button.SkinnedButton btnClear;
    }
}