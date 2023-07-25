using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MobileInterrogator
{
    public partial class ValueEditorText
    {
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 155);
            this.textBox1.TabIndex = 0;
            // 
            // ValueEditorText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.textBox1);
            this.Name = "ValueEditorText";
            this.Size = new System.Drawing.Size(147, 155);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TextBox textBox1;
    }
}