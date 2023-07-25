using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FillGlassTest
{
    public partial class FillGlass : UserControl
    {
        public int fillLevel = 100;
        private int incr = 0;
        private float step = 0.0f;
        private bool bDark;

        public FillGlass()
        {
            InitializeComponent();
            timer1.Interval = 15;
        }

        public void SetLevel(int level)
        {
            fillLevel = level;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (picShotDark.Visible || picShotLight.Visible)
                picShotEmpty.Height = 334 - (int)Math.Floor(incr * step);
            else if (picBeerDark.Visible || picBeerLight.Visible)
                picBeerEmpty.Height = 369 - (int)Math.Floor(incr * step);
            else if (picWineDark.Visible || picWineLight.Visible)
                picWineEmpty.Height = 233 - (int)Math.Floor(incr * step);


            if (incr >= fillLevel)
            {
                timer1.Stop();
            }
            incr++;
        }

        public void Fill()
        {
            if (picShotDark.Visible || picShotLight.Visible)
            {
                picShotEmpty.Height = 334;
                step = picShotEmpty.Height / 100.0f;
            }
            else if (picBeerDark.Visible || picBeerLight.Visible)
            {
                picBeerEmpty.Height = 369;
                step = picBeerEmpty.Height / 100.0f;
            }
            else if (picWineDark.Visible || picWineLight.Visible)
            {
                picWineEmpty.Height = 233;
                step = picWineEmpty.Height / 100.0f;
            }

            incr = 1;
            timer1.Start();
        }

        public void Reset()
        {
            picShotEmpty.Height = 334;
            picBeerEmpty.Height = 369;
            picWineEmpty.Height = 233;
        }

        public void SelectColor(bool dark)
        {
            bDark = dark;
            if (dark)
            {
                if (picWineDark.Visible || picWineLight.Visible)
                {
                    picWineDark.Visible = true;
                    picWineLight.Visible = false;
                }
                else if (picBeerDark.Visible || picBeerLight.Visible)
                {
                    picBeerDark.Visible = true;
                    picBeerLight.Visible = false;
                }
                else if (picShotDark.Visible || picShotLight.Visible)
                {
                    picShotDark.Visible = true;
                    picShotLight.Visible = false;
                }
            }
            else
            {
                if (picWineDark.Visible || picWineLight.Visible)
                {
                    picWineDark.Visible = false;
                    picWineLight.Visible = true;
                }
                else if (picBeerDark.Visible || picBeerLight.Visible)
                {
                    picBeerDark.Visible = false;
                    picBeerLight.Visible = true;
                }
                else if (picShotDark.Visible || picShotLight.Visible)
                {
                    picShotDark.Visible = false;
                    picShotLight.Visible = true;
                }
            }
        }
        public void ShowWine()
        {
            picWineEmpty.Visible = true;
            if (bDark)
            {
                picWineDark.Visible = true;
                picWineLight.Visible = false;
            }
            else
            {
                picWineDark.Visible = false;
                picWineLight.Visible = true;
            }
            picBeerDark.Visible = false;
            picBeerLight.Visible = false;
            picBeerEmpty.Visible = false;
            picShotDark.Visible = false;
            picShotLight.Visible = false;
            picShotEmpty.Visible = false;
        }

        public void ShowBeer()
        {
            picBeerEmpty.Visible = true;
            if (bDark)
            {
                picBeerDark.Visible = true;
                picBeerLight.Visible = false;
            }
            else
            {
                picBeerDark.Visible = false;
                picBeerLight.Visible = true;
            }
            picWineDark.Visible = false;
            picWineLight.Visible = false;
            picWineEmpty.Visible = false;
            picShotDark.Visible = false;
            picShotLight.Visible = false;
            picShotEmpty.Visible = false;
        }

        public void ShowShot()
        {
            picShotEmpty.Visible = true;
            if (bDark)
            {
                picShotDark.Visible = true;
                picShotLight.Visible = false;
            }
            else
            {
                picShotDark.Visible = false;
                picShotLight.Visible = true;
            }
            picBeerDark.Visible = false;
            picBeerLight.Visible = false;
            picBeerEmpty.Visible = false;
            picWineDark.Visible = false;
            picWineLight.Visible = false;
            picWineEmpty.Visible = false;
        }
    }
}
