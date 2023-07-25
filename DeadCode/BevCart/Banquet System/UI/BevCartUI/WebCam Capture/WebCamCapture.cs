using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Jaxis.Util.Log4Net;

namespace WebCam_Capture
{
    //MLF [System.Drawing.ToolboxBitmap(typeof(WebCam_Booth), "CAMERA.ICO")] // toolbox bitmap
    [Designer("Sytem.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof(System.ComponentModel.Design.IDesigner))] // make composite
    public class WebCamCapture : Form
    {
        private IContainer components;

        // property variables
        private int m_Width = 320;
        private int m_Height = 240;
        private int m_CapHwnd;

        // global variables to make the video capture go faster
        private WebCam_Capture.WebcamEventArgs x = new WebCam_Capture.WebcamEventArgs();
        private IDataObject m_tempObj;
        private System.Drawing.Image m_tempImg;

        // event delegate
        public delegate void WebCamEventHandler(object source, WebCam_Capture.WebcamEventArgs e, int Frame);
        // fired when a new image is captured
        public event WebCamEventHandler ImageCaptured;

        //API Declarations
        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA")]
        public static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);
        [DllImport("user32", EntryPoint = "OpenClipboard")]
        public static extern int OpenClipboard(int hWnd);
        [DllImport("user32", EntryPoint = "EmptyClipboard")]
        public static extern int EmptyClipboard();
        [DllImport("user32", EntryPoint = "CloseClipboard")]
        public static extern int CloseClipboard();

        //API Constants
        public const int WM_USER = 1024;
        public const int WM_CAP_CONNECT = 1034;
        public const int WM_CAP_DISCONNECT = 1035;
        public const int WM_CAP_GET_FRAME = 1084;
        public const int WM_CAP_COPY = 1054;
        public const int WM_CAP_START = WM_USER;
        public const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41;
        public const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42;
        public const int WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43;
        public const int WM_CAP_GET_VIDEOFORMAT = WM_CAP_START + 44;
        public const int WM_CAP_SET_VIDEOFORMAT = WM_CAP_START + 45;
        public const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46;
        public const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
                                                                

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // 
            // WebCamCapture
            // 
            this.Name = "WebCamCapture";
            this.Size = new System.Drawing.Size(342, 252);
        }

        public int CaptureHeight
        {
            get
            { return m_Height; }

            set
            { m_Height = value; }
        }

        public int CaptureWidth
        {
            get
            { return m_Width; }

            set
            { m_Width = value; }
        }

        public void DoTakePicture(int Frame)
        {

            try
            {
                // Set cursor to wait
                Cursor.Current = Cursors.WaitCursor;

                // connect to the capture device
                Application.DoEvents();
                SendMessage(m_CapHwnd, WM_CAP_CONNECT, 0, 0);
                SendMessage(m_CapHwnd, WM_CAP_SET_PREVIEW, 0, 0);

                // loop for a few seconds to allow time to smile...
                for (int i = 0; i < 40; i++)
                {
                    // get the next frame;
                    SendMessage(m_CapHwnd, WM_CAP_GET_FRAME, 0, 0);

                    // copy the frame to the clipboard
                    SendMessage(m_CapHwnd, WM_CAP_COPY, 0, 0);

                    // paste the frame into the event args image
                    if (ImageCaptured != null)
                    {
                        // get from the clipboard
                        m_tempObj = Clipboard.GetDataObject();
                        m_tempImg = (System.Drawing.Bitmap)m_tempObj.GetData(DataFormats.Bitmap);

                        GC.Collect(); // MLF - force clipboard to clean up

                        /*
                        * For some reason, the API is not resizing the video
                        * feed to the width and height provided when the video
                        * feed was started, so we must resize the image here
                        */
                        x.WebCamImage = m_tempImg.GetThumbnailImage(m_Width, m_Height, null, System.IntPtr.Zero);

                        // raise the event to update the form
                        ImageCaptured(this, x, Frame);
                    }
                }
            }

            catch (Exception exp)
            {
                Log.WriteException("WebCamCapture.DoTakePicture()", exp);
                Stop();
            }

            finally
            {
                // Set cursor to normal
                Cursor.Current = Cursors.Default;
            }

        }

        public void Start()
        {
            try
            {
                // setup a capture window
                m_CapHwnd = capCreateCaptureWindowA("WebCap", 0, 0, 0, m_Width, m_Height, Handle.ToInt32(), 0);
            }
            catch (Exception exp)
            {
                Log.WriteException("WebCamCapture.Start()", exp);
            }
        }


        public void Stop()
        {
            try
            {
                // disconnect from the video source
                Application.DoEvents();
                SendMessage(m_CapHwnd, WM_CAP_DISCONNECT, 0, 0);
            }

            catch (Exception exp)
            {
                Log.WriteException("WebCamCapture.Stop()", exp);
            }

        }
    }
}
