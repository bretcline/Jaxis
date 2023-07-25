namespace ReceiverApp
{
    using System;
    using System.Collections;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class PortScanner
    {
        private EventCallback m_Callback;
        private Control m_CallbackContext;
        private System.Collections.Queue m_IPQueue;
        private bool m_StopTrigger;
        private Thread[] m_Threads;
        private int m_ThreadsRunning;
        private int m_Timeout;

        public void PostEvent(EventID eID)
        {
            this.PostEvent(eID, null);
        }

        public void PostEvent(EventID eID, object Arg)
        {
            if (this.m_Callback != null)
            {
                if (this.m_CallbackContext == null)
                {
                    this.m_Callback(this, eID, Arg);
                }
                else if (this.m_CallbackContext.InvokeRequired)
                {
                    this.m_CallbackContext.Invoke(this.m_Callback, new object[] { this, eID, Arg });
                }
                else
                {
                    this.m_Callback(this, eID, Arg);
                }
            }
        }

        private static void ScanThread(object arg)
        {
            PortScanner scanner = arg as PortScanner;
            while (!scanner.m_StopTrigger)
            {
                IPEndPoint point;
                try
                {
                    point = (IPEndPoint) scanner.m_IPQueue.Dequeue();
                }
                catch
                {
                    break;
                }
                try
                {
                    TimedConnect(point, scanner.m_Timeout).Close();
                }
                catch
                {
                    scanner.PostEvent(EventID.PORT_CLOSED, point);
                    continue;
                }
                scanner.PostEvent(EventID.PORT_OPENED, point);
            }
            scanner.m_ThreadsRunning--;
            if (scanner.m_ThreadsRunning == 0)
            {
                scanner.PostEvent(EventID.FINISHED);
            }
        }

        public void SetCallback(EventCallback cb, Control context)
        {
            if (!this.IsReady)
            {
                throw new PortScannerAlreadyStartedException();
            }
            this.m_Callback = cb;
            this.m_CallbackContext = context;
        }

        public int Start(string strIPStart, string strIPEnd, int port, int numThreads, int connectTimeout)
        {
            int num = 0;
            int num2 = 0;
            int host = 0;
            int index = 0;
            int arg = 0;
            if (this.m_ThreadsRunning != 0)
            {
                throw new PortScannerAlreadyStartedException();
            }
            try
            {
                num = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(IPAddress.Parse(strIPStart).GetAddressBytes(), 0));
                num2 = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(IPAddress.Parse(strIPEnd).GetAddressBytes(), 0));
            }
            catch
            {
                throw new PortScannerInvalidIPRangeException();
            }
            if (num2 < num)
            {
                throw new PortScannerInvalidIPRangeException();
            }
            this.m_IPQueue = System.Collections.Queue.Synchronized(new System.Collections.Queue());
            for (host = num; host <= num2; host++)
            {
                IPAddress address = new IPAddress(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(host)));
                IPEndPoint point = new IPEndPoint(address, port);
                this.m_IPQueue.Enqueue(point);
            }
            arg = this.m_IPQueue.Count;
            this.m_Timeout = connectTimeout;
            this.m_Threads = new Thread[numThreads];
            this.m_StopTrigger = false;
            this.m_ThreadsRunning = numThreads;
            for (index = 0; index < numThreads; index++)
            {
                this.m_Threads[index] = new Thread(new ParameterizedThreadStart(PortScanner.ScanThread));
                this.m_Threads[index].Start(this);
            }
            this.PostEvent(EventID.STARTED, arg);
            return arg;
        }

        public void Stop()
        {
            if (this.IsReady)
            {
                throw new PortScannerNotStartedException();
            }
            this.m_StopTrigger = true;
        }

        private static Socket TimedConnect(IPEndPoint endPoint, int timeoutMs)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                socket.Blocking = false;
                socket.Connect(endPoint);
            }
            catch (SocketException exception)
            {
                if (exception.ErrorCode != 0x2733)
                {
                    throw;
                }
                int microSeconds = timeoutMs * 0x3e8;
                if (!socket.Poll(microSeconds, SelectMode.SelectWrite))
                {
                    throw new Exception("The host failed to connect");
                }
            }
            socket.Blocking = true;
            return socket;
        }

        public void WaitForScanCompletion()
        {
            if (!this.IsReady)
            {
                foreach (Thread thread in this.m_Threads)
                {
                    while (!thread.Join(100))
                    {
                        Application.DoEvents();
                    }
                }
            }
        }

        public EventCallback Callback
        {
            get
            {
                return this.m_Callback;
            }
            set
            {
                if (this.m_Threads != null)
                {
                    throw new PortScannerAlreadyStartedException();
                }
                this.m_Callback = value;
            }
        }

        public bool IsReady
        {
            get
            {
                return (this.m_ThreadsRunning == 0);
            }
        }

        public delegate void EventCallback(PortScanner Instance, PortScanner.EventID eID, object Arg);

        public enum EventID
        {
            INVALID,
            STARTED,
            FINISHED,
            PORT_OPENED,
            PORT_CLOSED
        }
    }
}

