using System;
using System.Windows.Forms;
using WebCam_Capture;
using Jaxis.Util.Log4Net;

namespace Sunglass_Mirror
{
    public partial class SunglassMirror : Form
    {
        private WebCamCapture m_WebCamCapture = new WebCamCapture();

        public SunglassMirror()
        {
            InitializeComponent();

            Disposed += OnClose;

            // set the image capture size
            m_WebCamCapture.ImageCaptured += WebCamCapture_ImageCaptured;
            m_WebCamCapture.CaptureHeight = sbPicture1.Height - 10;
            m_WebCamCapture.CaptureWidth = sbPicture1.Width - 10;

            m_WebCamCapture.Start();
        }

        public void OnClose (object sender, EventArgs e)
        {
            m_WebCamCapture.Stop();
        }
     
        private void sbPicture1_Click(object sender, EventArgs e)
        {
            // start the video capture.
            m_WebCamCapture.DoTakePicture(0);
        }

        private void sbPicture2_Click(object sender, EventArgs e)
        {
            // start the video capture.
            m_WebCamCapture.DoTakePicture(1);
        }

        private void sbPicture3_Click(object sender, EventArgs e)
        {
            // start the video capture.
            m_WebCamCapture.DoTakePicture(2);
        }

        private void sbPicture4_Click(object sender, EventArgs e)
        {
            // start the video capture.
            m_WebCamCapture.DoTakePicture(3);
        }

        private void sbPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraPrinting.
                using (PicReport Report = new PicReport())
                {
                    Report.xrPictureBox1.Image = sbPicture1.Image;
                    Report.xrPictureBox2.Image = sbPicture2.Image;
                    Report.xrPictureBox3.Image = sbPicture3.Image;
                    Report.xrPictureBox4.Image = sbPicture3.Image;
#if DEBUG
                    Report.ShowPreviewDialog();
#else
                Report.Print();
#endif
                }
            }
            catch (Exception exp)
            {
                Log.WriteException("SunglassMirror.sbPrint_Click()", exp);
            }

        }

        private void sbClear_Click(object sender, EventArgs e)
        {
            sbPicture1.Image = null;
            sbPicture2.Image = null;
            sbPicture3.Image = null;
            sbPicture4.Image = null;
        }

        private void sbPicture1_FullSize(object sender, EventArgs e)
        {
            try
            {
                using (FullSizeForm FullSize = new FullSizeForm(sbPicture1.Image))
                {
                    FullSize.ShowDialog();
                }
                ResetFullSizeButton();
            }
            catch (Exception exp)
            {
                Log.WriteException("SunglassMirror.sbPicture1_FullSize()", exp);
            }
       }

        private void sbPicture2_FullSize(object sender, EventArgs e)
        {
            try
            {
                using (FullSizeForm FullSize = new FullSizeForm(sbPicture2.Image))
                {
                    FullSize.ShowDialog();
                }
                ResetFullSizeButton();
            }
            catch (Exception exp)
            {
                Log.WriteException("SunglassMirror.sbPicture2_FullSize()", exp);
            }
        }

        private void sbPicture3_FullSize(object sender, EventArgs e)
        {
            try
            {
                using (FullSizeForm FullSize = new FullSizeForm(sbPicture3.Image))
                {
                    FullSize.ShowDialog();
                }
                ResetFullSizeButton();
            }
            catch (Exception exp)
            {
                Log.WriteException("SunglassMirror.sbPicture3_FullSize()", exp);
            }
        }

        private void sbPicture4_FullSize(object sender, EventArgs e)
        {
            try
            {
                using (FullSizeForm FullSize = new FullSizeForm(sbPicture4.Image))
                {
                    FullSize.ShowDialog();
                }
                ResetFullSizeButton();
            }
            catch (Exception exp)
            {
                Log.WriteException("SunglassMirror.sbPicture4_FullSize()", exp);
            }
        }

        private void ResetFullSizeButton()
        {
            sbPicture1.Click -= sbPicture1_FullSize;
            sbPicture2.Click -= sbPicture2_FullSize;
            sbPicture3.Click -= sbPicture3_FullSize;
            sbPicture4.Click -= sbPicture4_FullSize;
            sbPicture1.Click += sbPicture1_Click;
            sbPicture2.Click += sbPicture2_Click;
            sbPicture3.Click += sbPicture3_Click;
            sbPicture4.Click += sbPicture4_Click;
            sbFullScreen.Text = "View Full Screen";
            sbFullScreen.Enabled = true;
        }

        private void WebCamCapture_ImageCaptured(object source, WebcamEventArgs e, int Frame)
        {
            // set the picturebox picture
            switch (Frame)
            {
                case 0:
                    sbPicture1.Image = e.WebCamImage;
                    break;
                case 1:
                    sbPicture2.Image = e.WebCamImage;
                    break;
                case 2:
                    sbPicture3.Image = e.WebCamImage;
                    break;
                case 3:
                    sbPicture4.Image = e.WebCamImage;
                    break;
            }
        }

        private void sbFullScreen_Click(object sender, EventArgs e)
        {
            sbFullScreen.Enabled = false;
            sbPicture1.Click -= sbPicture1_Click;
            sbPicture2.Click -= sbPicture2_Click;
            sbPicture3.Click -= sbPicture3_Click;
            sbPicture4.Click -= sbPicture4_Click;
            sbPicture1.Click += sbPicture1_FullSize;
            sbPicture2.Click += sbPicture2_FullSize;
            sbPicture3.Click += sbPicture3_FullSize;
            sbPicture4.Click += sbPicture4_FullSize;
            sbFullScreen.Text = "Select Picture";
        }

        private void btnClose_Click( object sender, EventArgs e )
        {
            this.Close( );
        }
    }
}