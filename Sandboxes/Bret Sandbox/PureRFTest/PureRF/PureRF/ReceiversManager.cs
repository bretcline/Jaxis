namespace PureRF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class ReceiversManager
    {
        public const int DEFAULT_TIMEOUT = 0x5dc;
        private Dictionary<string, Loop> Loops;
        public ArrayList LoopsList;
        private EventCallback mCallback;
        private Control mCallbackContext;
        private ArrayList mCurrentWorkers;
        private int mRequestNumReceivers;
        private int mRequestNumReceiversDone;
        private int mRequestNumThreadsWorking;
        private int mSerialTimeout;
        private Dictionary<string, ManagedReceiver> Receivers;
        public ArrayList ReceiversList;
        public object SyncRoot;

        public ReceiversManager() : this(0x5dc)
        {
        }

        public ReceiversManager(int serialTimeout)
        {
            this.SyncRoot = new object();
            this.LoopsList = new ArrayList();
            this.Loops = new Dictionary<string, Loop>();
            this.ReceiversList = new ArrayList();
            this.Receivers = new Dictionary<string, ManagedReceiver>();
            this.mCurrentWorkers = new ArrayList();
            this.mSerialTimeout = serialTimeout;
        }

        public RetVal AbortRequest()
        {
            if (this.IsReady)
            {
                return RetVal.NO_REQUEST_EXECUTING;
            }
            foreach (WorkerThreadRequest request in this.mCurrentWorkers)
            {
                request.Abort();
            }
            return RetVal.SUCCESS;
        }

        public RetVal AddIPLoop(string Name, string Host, int Port)
        {
            if (!this.IsLoopNameAvailable(Name))
            {
                return RetVal.NAME_IN_USE;
            }
            Loop loop = new Loop(Name);
            loop.ConnectIP(Host, Port, this.mSerialTimeout);
            return this.AddLoop(loop, true);
        }

        public RetVal AddLoop(Loop loop, bool needOpen)
        {
            string name = loop.Name;
            if (!this.IsLoopNameAvailable(name))
            {
                return RetVal.NAME_IN_USE;
            }
            if (needOpen && !loop.OpenPort())
            {
                return RetVal.PORT_NOT_AVAILABLE;
            }
            this.LoopsList.Add(loop);
            this.Loops[name] = loop;
            return RetVal.SUCCESS;
        }

        public RetVal AddReceiver(string Name, string LoopName, byte UnitID)
        {
            if (!this.IsReceiverNameAvailable(Name))
            {
                return RetVal.NAME_IN_USE;
            }
            if (!this.Loops.ContainsKey(LoopName))
            {
                return RetVal.NO_SUCH_LOOP;
            }
            ManagedReceiver receiver = new ManagedReceiver();
            receiver.Name = Name;
            receiver.Loop = this.Loops[LoopName];
            receiver.UnitID = UnitID;
            this.ReceiversList.Add(receiver);
            this.Receivers[Name] = receiver;
            return RetVal.SUCCESS;
        }

        public RetVal AddSerialLoop(string Name, string PortName, int BaudRate)
        {
            if (!this.IsLoopNameAvailable(Name))
            {
                return RetVal.NAME_IN_USE;
            }
            if (this.IsPortHandled(PortName))
            {
                return RetVal.PORT_ALREADY_HANDLED;
            }
            Loop loop = new Loop(Name);
            loop.ConnectSerial(PortName, BaudRate, this.mSerialTimeout);
            return this.AddLoop(loop, true);
        }

        public RetVal Clear()
        {
            ArrayList list = new ArrayList();
            if (!this.IsReady)
            {
                return RetVal.REQUEST_STILL_EXECUTING;
            }
            foreach (Loop loop in this.LoopsList)
            {
                loop.ClosePort();
                list.Add(loop.Name);
            }
            foreach (string str in list)
            {
                this.RemoveLoop(str);
            }
            this.LoopsList = new ArrayList();
            this.Loops = new Dictionary<string, Loop>();
            this.ReceiversList = new ArrayList();
            this.Receivers = new Dictionary<string, ManagedReceiver>();
            this.mCurrentWorkers = new ArrayList();
            return RetVal.SUCCESS;
        }

        public RetVal ClearReceivers()
        {
            if (!this.IsReady)
            {
                return RetVal.REQUEST_STILL_EXECUTING;
            }
            this.ReceiversList = new ArrayList();
            this.Receivers = new Dictionary<string, ManagedReceiver>();
            return RetVal.SUCCESS;
        }

        public RetVal Download(string[] receiverNames, byte[] firmware, int resendCount, int resendDelay, int pageDelay)
        {
            return this.StartRequest(receiverNames, RequestType.MANAGED_DOWNLOAD, new object[] { firmware, resendCount, resendDelay, pageDelay });
        }

        public RetVal FirmwareState(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.FIRMWARE_STATE, new object[0]);
        }

        public RetVal FlushTagBuffer(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.FLUSH_TAG_BUFFER, new object[0]);
        }

        public RetVal GetAllReceiverInfo(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.GET_ALL_RECEIVER_INFO, new object[0]);
        }

        public RetVal GetAllResults(out ResultSet Results)
        {
            Results = new ResultSet();
            if (!this.IsReady)
            {
                return RetVal.REQUEST_STILL_EXECUTING;
            }
            foreach (WorkerThreadRequest request in this.mCurrentWorkers)
            {
                for (int i = 0; i < request.mReceiversList.Length; i++)
                {
                    ReceiverResult result = new ReceiverResult();
                    result.Receiver = request.mReceiversList[i];
                    result.RetVal = request.mRetVals[i];
                    result.Result = request.mResults[i];
                    Results.Add(request.mReceiversList[i], result);
                }
            }
            return RetVal.SUCCESS;
        }

        public RetVal GetAllTags(string[] receiverNames, bool withTS)
        {
            return this.StartRequest(receiverNames, RequestType.GET_ALL_TAGS, new object[] { withTS });
        }

        public RetVal GetAntennaGain(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.GET_ANTENNA_GAIN, new object[0]);
        }

        public RetVal GetMode(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.GET_MODE, new object[0]);
        }

        public RetVal GetNoiseLevel(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.GET_NOISE_LEVEL, new object[0]);
        }

        public RetVal GetResult(string receiverName, out ReceiverResult Result)
        {
            Result = new ReceiverResult();
            if (!this.IsReady)
            {
                return RetVal.REQUEST_STILL_EXECUTING;
            }
            foreach (WorkerThreadRequest request in this.mCurrentWorkers)
            {
                for (int i = 0; i < request.mReceiversList.Length; i++)
                {
                    if (!(request.mReceiversList[i].Name != receiverName))
                    {
                        Result.Receiver = request.mReceiversList[i];
                        Result.RetVal = request.mRetVals[i];
                        Result.Result = request.mResults[i];
                        return RetVal.SUCCESS;
                    }
                }
            }
            return RetVal.NO_SUCH_RECEIVER;
        }

        public RetVal GetTags(string[] receiverNames, byte transactionID, byte maxTagsCount, bool withTS)
        {
            return this.StartRequest(receiverNames, RequestType.GET_TAGS, new object[] { transactionID, maxTagsCount, withTS });
        }

        public RetVal GetTime(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.GET_TIME, new object[0]);
        }

        public RetVal GetUnitInfo(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.GET_UNIT_INFO, new object[0]);
        }

        public RetVal GetUnitStatus(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.GET_UNIT_STATUS, new object[0]);
        }

        public bool IsLoopInUse(string LoopName)
        {
            foreach (ManagedReceiver receiver in this.ReceiversList)
            {
                if (receiver.Loop.Name == LoopName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsLoopNameAvailable(string Name)
        {
            return !this.Loops.ContainsKey(Name);
        }

        public bool IsPortHandled(string PortName)
        {
            foreach (Loop loop in this.LoopsList)
            {
                if (loop.Conn.Name == PortName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsReceiverInLoop(string LoopName, byte UnitID)
        {
            if (!this.IsLoopNameAvailable(LoopName))
            {
                foreach (ManagedReceiver receiver in this.ReceiversList)
                {
                    if (!(receiver.Loop.Name != LoopName) && (receiver.UnitID == UnitID))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsReceiverNameAvailable(string Name)
        {
            return !this.Receivers.ContainsKey(Name);
        }

        public bool LoadXML(string filename, out string errMsg)
        {
            XMLReceiversManagerDB rdb;
            RetVal eRROR;
            errMsg = "";
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XMLReceiversManagerDB));
                TextReader textReader = new StreamReader(filename);
                rdb = (XMLReceiversManagerDB) serializer.Deserialize(textReader);
            }
            catch
            {
                return false;
            }
            this.Clear();
            foreach (XMLLoop loop in rdb.Loops)
            {
                Loop loop2;
                switch (loop.LoopBusType)
                {
                    case ReceiverBusConnection.BusType.LOOP_SERIAL:
                    {
                        ReceiverBusConnection_Serial conn = new ReceiverBusConnection_Serial(loop.SettingsStr);
                        loop2 = new Loop(loop.Name);
                        loop2.ConnectBus(conn);
                        eRROR = this.AddLoop(loop2, true);
                        break;
                    }
                    case ReceiverBusConnection.BusType.LOOP_IP:
                    {
                        ReceiverBusConnection_IP n_ip = new ReceiverBusConnection_IP(loop.SettingsStr);
                        loop2 = new Loop(loop.Name);
                        loop2.ConnectBus(n_ip);
                        eRROR = this.AddLoop(loop2, true);
                        break;
                    }
                    default:
                        eRROR = RetVal.ERROR;
                        break;
                }
                if (eRROR != RetVal.SUCCESS)
                {
                    errMsg = string.Format("Error adding loop {0}: {1}", loop.Name, eRROR.ToString());
                    return false;
                }
            }
            foreach (XMLManagedReceiver receiver in rdb.ManagedReceivers)
            {
                eRROR = this.AddReceiver(receiver.Name, receiver.LoopName, receiver.UnitID);
                if (eRROR != RetVal.SUCCESS)
                {
                    errMsg = string.Format("Error adding receiver {0} (on loop {1}): {2}", receiver.Name, receiver.LoopName, eRROR.ToString());
                    return false;
                }
            }
            return true;
        }

        public int NumReceiversInLoop(string LoopName)
        {
            int num = 0;
            foreach (ManagedReceiver receiver in this.ReceiversList)
            {
                if (receiver.Loop.Name == LoopName)
                {
                    num++;
                }
            }
            return num;
        }

        private RetVal PrepareLoopsMap(string[] receiverNames, out Dictionary<Loop, ManagedReceiver[]> loopsMap)
        {
            Dictionary<Loop, ArrayList> dictionary;
            loopsMap = new Dictionary<Loop, ManagedReceiver[]>();
            RetVal val = this.PrepareLoopsMap(receiverNames, out dictionary);
            if (val != RetVal.SUCCESS)
            {
                return val;
            }
            foreach (Loop loop in dictionary.Keys)
            {
                ManagedReceiver[] receiverArray = new ManagedReceiver[dictionary[loop].Count];
                for (int i = 0; i < receiverArray.Length; i++)
                {
                    receiverArray[i] = dictionary[loop][i] as ManagedReceiver;
                }
                loopsMap.Add(loop, receiverArray);
            }
            return RetVal.SUCCESS;
        }

        private RetVal PrepareLoopsMap(string[] receiverNames, out Dictionary<Loop, ArrayList> loopsMap)
        {
            loopsMap = new Dictionary<Loop, ArrayList>();
            foreach (string str in receiverNames)
            {
                if (!this.Receivers.ContainsKey(str))
                {
                    return RetVal.NO_SUCH_RECEIVER;
                }
                Loop key = this.Receivers[str].Loop;
                if (!loopsMap.ContainsKey(key))
                {
                    loopsMap[key] = new ArrayList();
                }
                loopsMap[key].Add(this.Receivers[str]);
            }
            return RetVal.SUCCESS;
        }

        public RetVal RemoveLoop(string Name)
        {
            ArrayList list = new ArrayList();
            if (this.IsLoopNameAvailable(Name))
            {
                return RetVal.NO_SUCH_LOOP;
            }
            foreach (ManagedReceiver receiver in this.ReceiversList)
            {
                if (!(receiver.Loop.Name != Name))
                {
                    list.Add(receiver.Name);
                }
            }
            foreach (string str in list)
            {
                this.ReceiversList.Remove(this.Receivers[str]);
                this.Receivers.Remove(str);
            }
            this.Loops[Name].ClosePort();
            this.LoopsList.Remove(this.Loops[Name]);
            this.Loops.Remove(Name);
            return RetVal.SUCCESS;
        }

        public RetVal RemoveReceiver(string Name)
        {
            if (this.IsReceiverNameAvailable(Name))
            {
                return RetVal.NO_SUCH_RECEIVER;
            }
            this.ReceiversList.Remove(this.Receivers[Name]);
            this.Receivers.Remove(Name);
            return RetVal.SUCCESS;
        }

        public RetVal RenameReceiver(string oldName, string newName)
        {
            if (!this.IsReceiverNameAvailable(newName))
            {
                return RetVal.NAME_IN_USE;
            }
            if (!this.Receivers.ContainsKey(oldName))
            {
                return RetVal.NO_SUCH_RECEIVER;
            }
            this.Receivers[oldName].Name = newName;
            this.Receivers[newName] = this.Receivers[oldName];
            this.Receivers.Remove(oldName);
            return RetVal.SUCCESS;
        }

        private void ReportProgress(ProgressEvent e)
        {
            if (this.mCallback != null)
            {
                if (this.mCallbackContext == null)
                {
                    this.mCallback(this, e);
                }
                else if (this.mCallbackContext.InvokeRequired)
                {
                    try
                    {
                        this.mCallbackContext.Invoke(this.mCallback, new object[] { this, e });
                    }
                    catch
                    {
                    }
                }
                else
                {
                    this.mCallback(this, e);
                }
            }
        }

        private void ReportProgress(RequestType reqType, ProgressEvent.ID EventID)
        {
            ProgressEvent e = new ProgressEvent(reqType, EventID, "", new object[0]);
            if (e.EventID == ProgressEvent.ID.PROGRESS_UPDATE)
            {
                e.SetString("{0}% ({1}/{2})", new object[] { (this.mRequestNumReceiversDone * 100) / this.mRequestNumReceivers, this.mRequestNumReceiversDone, this.mRequestNumReceivers });
                e.SetEventArg("PERCENT_COMPLETE", (this.mRequestNumReceiversDone * 100) / this.mRequestNumReceivers);
                e.SetEventArg("NUM_RECEIVERS_DONE", this.mRequestNumReceiversDone);
                e.SetEventArg("NUM_RECEIVERS", this.mRequestNumReceivers);
            }
            this.ReportProgress(e);
        }

        private void ReportProgress(RequestType reqType, ProgressEvent.ID EventID, string fmt, params object[] args)
        {
            ProgressEvent e = new ProgressEvent(reqType, EventID, fmt, args);
            this.ReportProgress(e);
        }

        private static ReceiverRetVal RequestWrapper(WorkerThreadRequest Request, ManagedReceiver receiver, out object Result)
        {
            Receiver.Tag[] tagArray;
            Receiver.UnitNameParameter parameter;
            Receiver.RSSIFilterParameter parameter2;
            Receiver.SiteCodeParameter parameter3;
            Receiver.RFBaudRates rates;
            Receiver.ModulationParameter parameter4;
            Receiver.SerialNumParameter parameter5;
            Receiver receiverInterface = receiver.Loop.ReceiverInterface;
            byte unitID = receiver.UnitID;
            object[] mRequestArgs = Request.mRequestArgs;
            ReceiverRetVal eRROR = ReceiverRetVal.ERROR;
            Result = null;
            ProgressEvent e = new ProgressEvent(Request.mRequestType, ProgressEvent.ID.BEFORE_REQUEST_NOTIFICATION, "Sending request {0} to receiver {1} on loop {2}...", new object[] { Request.mRequestType.ToString(), receiver.Name, receiver.Loop.Name });
            Request.mParent.ReportProgress(e);
            switch (Request.mRequestType)
            {
                case RequestType.INVALID_REQUEST:
                    eRROR = ReceiverRetVal.ERROR;
                    break;

                case RequestType.GET_UNIT_INFO:
                case RequestType.BOOTLOADER_GET_UNIT_INFO:
                    Receiver.UnitInfo info;
                    eRROR = receiverInterface.GetUnitInfo(unitID, out info);
                    Result = info;
                    break;

                case RequestType.GET_UNIT_STATUS:
                    Receiver.UnitStatus status;
                    eRROR = receiverInterface.GetUnitStatus(unitID, out status);
                    Result = status;
                    break;

                case RequestType.GET_ALL_TAGS:
                    eRROR = receiverInterface.GetAllTags(unitID, out tagArray, (bool) mRequestArgs[0]);
                    Result = tagArray;
                    break;

                case RequestType.GET_TAGS:
                    int num2;
                    eRROR = receiverInterface.GetTags(unitID, (byte) mRequestArgs[0], (byte) mRequestArgs[1], out tagArray, out num2, (bool) mRequestArgs[2]);
                    Result = tagArray;
                    break;

                case RequestType.GET_NOISE_LEVEL:
                    ushort num3;
                    eRROR = receiverInterface.GetNoiseLevel(unitID, out num3);
                    Result = num3;
                    break;

                case RequestType.GET_ANTENNA_GAIN:
                {
                    Receiver.AntennaGain iNVALID = Receiver.AntennaGain.INVALID;
                    eRROR = receiverInterface.GetAntennaGain(unitID, out iNVALID);
                    Result = iNVALID;
                    break;
                }
                case RequestType.SET_ANTENNA_GAIN:
                    eRROR = receiverInterface.SetAntennaGain(unitID, (Receiver.AntennaGain) mRequestArgs[0]);
                    break;

                case RequestType.GET_TIME:
                    Receiver.Time time;
                    eRROR = receiverInterface.GetTime(unitID, out time);
                    Result = time;
                    break;

                case RequestType.SET_TIME:
                    eRROR = receiverInterface.SetTime(unitID, (Receiver.Time) mRequestArgs[0]);
                    break;

                case RequestType.FLUSH_TAG_BUFFER:
                    eRROR = receiverInterface.FlushTagBuffer(unitID);
                    break;

                case RequestType.GET_MODE:
                    eRROR = receiverInterface.GetMode(unitID);
                    break;

                case RequestType.SET_MODE_BOOTLOADER:
                    eRROR = receiverInterface.SetBootloaderMode(unitID);
                    break;

                case RequestType.SET_MODE_FIRMWARE:
                    eRROR = receiverInterface.SetMainMode(unitID);
                    break;

                case RequestType.FIRMWARE_STATE:
                case RequestType.BOOTLOADER_FIRMWARE_STATE:
                    ushort num4;
                    eRROR = receiverInterface.FirmwareChecksum(unitID, out num4);
                    Result = num4;
                    break;

                case RequestType.GET_ALL_RECEIVER_INFO:
                    Receiver.AllReceiverInfo info2;
                    eRROR = receiverInterface.GetAllReceiverInfo(unitID, out info2);
                    Result = info2;
                    break;

                case RequestType.ACTIVATE_RELAY:
                    eRROR = receiverInterface.ActivateRelay(unitID, (Receiver.ActivateRelayParameter) mRequestArgs[0]);
                    break;

                case RequestType.SET_UNIT_NAME:
                    parameter = (Receiver.UnitNameParameter) mRequestArgs[0];
                    eRROR = receiverInterface.SetUnitName(unitID, ref parameter);
                    Result = parameter;
                    break;

                case RequestType.GET_UNIT_NAME:
                    eRROR = receiverInterface.GetUnitName(unitID, out parameter);
                    Result = parameter;
                    break;

                case RequestType.SET_SITE_CODE:
                    parameter3 = (Receiver.SiteCodeParameter) mRequestArgs[0];
                    eRROR = receiverInterface.SetSiteCode(unitID, ref parameter3);
                    Result = parameter3;
                    break;

                case RequestType.GET_SITE_CODE:
                    eRROR = receiverInterface.GetSiteCode(unitID, out parameter3);
                    Result = parameter3;
                    break;

                case RequestType.GetRFBaudRate:
                    eRROR = receiverInterface.GetRFBaudRate(unitID, out rates);
                    Result = rates;
                    break;

                case RequestType.SetRFBaudRate:
                    rates = (Receiver.RFBaudRates) mRequestArgs[0];
                    eRROR = receiverInterface.SetRFBaudRate(unitID, ref rates);
                    Result = rates;
                    break;

                case RequestType.GetModulation:
                    eRROR = receiverInterface.GetModulation(unitID, out parameter4);
                    Result = parameter4;
                    break;

                case RequestType.SetModulation:
                    parameter4 = (Receiver.ModulationParameter) mRequestArgs[0];
                    eRROR = receiverInterface.SetModulation(unitID, ref parameter4);
                    Result = parameter4;
                    break;

                case RequestType.GetSerialNum:
                    eRROR = receiverInterface.GetSerialNum(unitID, out parameter5);
                    Result = parameter5;
                    break;

                case RequestType.SetSerialNum:
                    parameter5 = (Receiver.SerialNumParameter) mRequestArgs[0];
                    eRROR = receiverInterface.SetSerialNum(unitID, ref parameter5);
                    Result = parameter5;
                    break;

                case RequestType.SET_RSSI_THRESHOLD:
                    parameter2 = (Receiver.RSSIFilterParameter) mRequestArgs[0];
                    eRROR = receiverInterface.Set_RSSI_Threshold(unitID, ref parameter2);
                    Result = parameter2;
                    break;

                case RequestType.GET_RSSI_THRESHOLD:
                    eRROR = receiverInterface.Get_RSSI_Threshold(unitID, out parameter2);
                    Result = parameter2;
                    break;

                case RequestType.GET_POWER_CONTROL:
                    Receiver.Power_Control control;
                    eRROR = receiverInterface.GetPowerControl(unitID, out control);
                    Result = control;
                    break;
            }
            e = new ProgressEvent(Request.mRequestType, ProgressEvent.ID.AFTER_REQUEST_NOTIFICATION, "Request {0} to receiver {1} on loop {2} completed: {3}", new object[] { Request.mRequestType.ToString(), receiver.Name, receiver.Loop.Name, eRROR.ToString() });
            e.SetEventArg("RETVAL", eRROR);
            e.SetEventArg("RESULT", Result);
            Request.mParent.ReportProgress(e);
            return eRROR;
        }

        public bool SaveXML(string filename)
        {
            int num;
            XMLReceiversManagerDB o = new XMLReceiversManagerDB();
            o.Loops = new XMLLoop[this.LoopsList.Count];
            for (num = 0; num < this.LoopsList.Count; num++)
            {
                Loop loop = this.LoopsList[num] as Loop;
                o.Loops[num] = new XMLLoop();
                o.Loops[num].Name = loop.Name;
                o.Loops[num].LoopBusType = loop.Conn.ConnectionBusType;
                o.Loops[num].SettingsStr = loop.Conn.Serialize();
            }
            o.ManagedReceivers = new XMLManagedReceiver[this.ReceiversList.Count];
            for (num = 0; num < this.ReceiversList.Count; num++)
            {
                ManagedReceiver receiver = this.ReceiversList[num] as ManagedReceiver;
                o.ManagedReceivers[num] = new XMLManagedReceiver();
                o.ManagedReceivers[num].Name = receiver.Name;
                o.ManagedReceivers[num].LoopName = receiver.Loop.Name;
                o.ManagedReceivers[num].UnitID = receiver.UnitID;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XMLReceiversManagerDB));
                TextWriter textWriter = new StreamWriter(filename);
                serializer.Serialize(textWriter, o);
                textWriter.Close();
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public RetVal SetAntennaGain(string[] receiverNames, Receiver.AntennaGain antennaGain)
        {
            return this.StartRequest(receiverNames, RequestType.SET_ANTENNA_GAIN, new object[] { antennaGain });
        }

        public void SetEventCallback(EventCallback cb)
        {
            this.mCallback = cb;
            this.mCallbackContext = null;
        }

        public void SetEventCallback(EventCallback cb, Control CallbackContext)
        {
            this.mCallback = cb;
            this.mCallbackContext = CallbackContext;
        }

        public RetVal SetModeBootloader(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.SET_MODE_BOOTLOADER, new object[0]);
        }

        public RetVal SetModeFirmware(string[] receiverNames)
        {
            return this.StartRequest(receiverNames, RequestType.SET_MODE_FIRMWARE, new object[0]);
        }

        public RetVal SetTime(string[] receiverNames, Receiver.Time time)
        {
            return this.StartRequest(receiverNames, RequestType.SET_TIME, new object[] { time });
        }

        public RetVal StartRequest(string[] receiverNames, RequestType type, params object[] args)
        {
            Dictionary<Loop, ManagedReceiver[]> dictionary;
            if (!this.IsReady)
            {
                return RetVal.REQUEST_STILL_EXECUTING;
            }
            RetVal val = this.PrepareLoopsMap(receiverNames, out dictionary);
            if (val != RetVal.SUCCESS)
            {
                return val;
            }
            this.mCurrentWorkers.Clear();
            this.mRequestNumReceivers = receiverNames.Length;
            this.mRequestNumReceiversDone = 0;
            this.mRequestNumThreadsWorking = 0;
            foreach (Loop loop in dictionary.Keys)
            {
                WorkerThreadRequest request = new WorkerThreadRequest(this, loop, dictionary[loop], type, args);
                this.mCurrentWorkers.Add(request);
                this.mRequestNumThreadsWorking++;
                request.Start();
            }
            ProgressEvent e = new ProgressEvent(type, ProgressEvent.ID.STARTED, "", new object[0]);
            e.SetEventArg("NUM_LOOPS", dictionary.Count);
            this.ReportProgress(e);
            return RetVal.SUCCESS;
        }

        public RetVal WaitForRequestCompletion()
        {
            if (this.IsReady)
            {
                return RetVal.NO_REQUEST_EXECUTING;
            }
            foreach (WorkerThreadRequest request in this.mCurrentWorkers)
            {
                while (!request.Join(100))
                {
                    Application.DoEvents();
                }
            }
            return RetVal.SUCCESS;
        }

        private static void WorkerThread(object arg)
        {
            int num;
            WorkerThreadRequest request = arg as WorkerThreadRequest;
//            Console.WriteLine(string.Format("Thread started for loop: {0} ({1} receivers)", request.mLoop.Name, request.mReceiversList.Length));
            if (request.mRequestType == RequestType.MANAGED_DOWNLOAD)
            {
                for (num = 0; num < request.mReceiversList.Length; num++)
                {
                    request.mRetVals[num] = ReceiverRetVal.SUCCESS;
                }
                WorkerThread_ManagedDownload(request);
                request.mParent.mRequestNumReceiversDone += request.mReceiversList.Length;
            }
            else if (!request.mEnterBootloader || WorkerThread_EnterBootloader(request))
            {
                for (num = 0; num < request.mReceiversList.Length; num++)
                {
                    request.mRetVals[num] = RequestWrapper(request, request.mReceiversList[num], out request.mResults[num]);
                    lock (request.mParent.SyncRoot)
                    {
                        request.mParent.mRequestNumReceiversDone++;
                    }
                    request.mParent.ReportProgress(request.mRequestType, ProgressEvent.ID.PROGRESS_UPDATE);
                    ProgressEvent e = new ProgressEvent(request.mRequestType, ProgressEvent.ID.RESULT_UPDATE, "", new object[0]);
                    e.SetEventArg("RECEIVER", request.mReceiversList[num]);
                    e.SetEventArg("RETVAL", request.mRetVals[num]);
                    e.SetEventArg("RESULT", request.mResults[num]);
                    request.mParent.ReportProgress(e);
                    if (request.AbortRequested)
                    {
                        break;
                    }
                }
                if (request.mEnterBootloader)
                {
                    WorkerThread_EnterFirmware(request);
                }
            }
            lock (request.mParent.SyncRoot)
            {
                request.mParent.mRequestNumThreadsWorking--;
            }
            if (request.mParent.mRequestNumThreadsWorking == 0)
            {
                request.mParent.ReportProgress(request.mRequestType, ProgressEvent.ID.COMPLETED);
            }
        }

        private static bool WorkerThread_EnterBootloader(WorkerThreadRequest Request)
        {
            for (int i = 0; i < Request.mReceiversList.Length; i++)
            {
                ReceiverRetVal val = Request.mReceiversList[i].Loop.ReceiverInterface.SetBootloaderMode(Request.mReceiversList[i].UnitID);
                Request.mParent.ReportProgress(Request.mRequestType, ProgressEvent.ID.LOG_MSG, "Entering bootloader mode on receiver {0}: {1}", new object[] { Request.mReceiversList[i].Name, val.ToString() });
            }
            Request.mLoop.SetPortBaudrate(0xe100);
            Thread.Sleep(0x3e8);
            return true;
        }

        private static bool WorkerThread_EnterFirmware(WorkerThreadRequest Request)
        {
            for (int i = 0; i < Request.mReceiversList.Length; i++)
            {
                ReceiverRetVal val = Request.mReceiversList[i].Loop.ReceiverInterface.SetMainMode(Request.mReceiversList[i].UnitID);
                Request.mParent.ReportProgress(Request.mRequestType, ProgressEvent.ID.LOG_MSG, "Entering firmware mode on receiver {0}: {1}", new object[] { Request.mReceiversList[i].Name, val.ToString() });
            }
            Request.mLoop.SetPortBaudrate(0xe100);
            Thread.Sleep(0x3e8);
            return true;
        }

        private static bool WorkerThread_ManagedDownload(WorkerThreadRequest Request)
        {
            int num5;
            byte[] sourceArray = (byte[]) Request.mRequestArgs[0];
            int num = (int) Request.mRequestArgs[1];
            int millisecondsTimeout = (int) Request.mRequestArgs[2];
            int num3 = (int) Request.mRequestArgs[3];
            int argValue = sourceArray.Length / 0x10;
            if ((argValue * 0x10) != sourceArray.Length)
            {
                argValue++;
            }
            byte[][] bufferArray = new byte[argValue][];
            for (num5 = 0; num5 < argValue; num5++)
            {
                int length = 0x10;
                if ((num5 + 1) == argValue)
                {
                    length = sourceArray.Length % 0x10;
                    if (length == 0)
                    {
                        length = 0x10;
                    }
                }
                bufferArray[num5] = new byte[0x10];
                bufferArray[num5].Initialize();
                Array.Copy(sourceArray, num5 * 0x10, bufferArray[num5], 0, length);
            }
            for (num5 = 0; num5 < argValue; num5++)
            {
                for (int i = 0; i < num; i++)
                {
                    Request.mLoop.ReceiverInterface.Download(0, (ushort) num5, bufferArray[num5]);
                    Thread.Sleep(millisecondsTimeout);
                }
                if (((num5 + 1) % 0x10) == 0)
                {
                    Thread.Sleep(num3);
                }
                ProgressEvent e = new ProgressEvent(RequestType.MANAGED_DOWNLOAD, ProgressEvent.ID.DOWNLOAD_PROGRESS, "Sent packet {0}/{1} on loop {2}", new object[] { num5 + 1, argValue, Request.mLoop.Name });
                e.SetEventArg("SENT_PACKETS", num5 + 1);
                e.SetEventArg("TOTAL_PACKETS", argValue);
                e.SetEventArg("PERCENT_COMPLETE", ((num5 + 1) * 100) / argValue);
                e.SetEventArg("LOOP", Request.mLoop.Name);
                Request.mParent.ReportProgress(e);
            }
            return true;
        }

        public bool IsReady
        {
            get
            {
                return (this.mRequestNumThreadsWorking == 0);
            }
        }

        public delegate void EventCallback(ReceiversManager m, ReceiversManager.ProgressEvent e);

        public class Loop
        {
            public ReceiverBusConnection Conn;
            public string Name;
            public Receiver ReceiverInterface;

            public Loop(string _Name)
            {
                this.Name = _Name;
            }

            public Loop(string _Name, ReceiversManager.Loop _Loop)
            {
                this.Name = _Name;
                this.Conn = _Loop.Conn;
                this.ReceiverInterface = new Receiver(this.Conn);
            }

            public bool ClosePort()
            {
                try
                {
                    this.Conn.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public void ConnectBus(ReceiverBusConnection conn)
            {
                this.Conn = conn;
                this.ReceiverInterface = new Receiver(this.Conn);
            }

            public void ConnectIP(string Host, int Port, int Timeout)
            {
                this.Conn = new ReceiverBusConnection_IP(Host, Port, Timeout);
                this.ReceiverInterface = new Receiver(this.Conn);
            }

            public void ConnectSerial(string PortName, int BaudRate, int Timeout)
            {
                this.Conn = new ReceiverBusConnection_Serial(PortName, BaudRate, Timeout);
                this.ReceiverInterface = new Receiver(this.Conn);
            }

            public bool OpenPort()
            {
                try
                {
                    return this.Conn.Open();
                }
                catch
                {
                    return false;
                }
            }

            public void SetPortBaudrate(int BaudRate)
            {
                this.Conn.SetBaudRate(BaudRate);
            }
        }

        public class ManagedReceiver
        {
            public PureRF.ReceiversManager.Loop Loop;
            public string Name;
            public byte UnitID;
        }

        public class ProgressEvent
        {
            public Dictionary<string, object> EventArgs;
            public ID EventID;
            public string EventStr;
            public PureRF.ReceiversManager.RequestType RequestType;

            public ProgressEvent() : this(PureRF.ReceiversManager.RequestType.INVALID_REQUEST, ID.INVALID, "", new object[0])
            {
            }

            public ProgressEvent(PureRF.ReceiversManager.RequestType reqType, ID _EventID, string str, params object[] args)
            {
                this.RequestType = reqType;
                this.EventID = _EventID;
                this.EventArgs = new Dictionary<string, object>();
                this.SetString(str, args);
            }

            public void SetEventArg(string argName, object argValue)
            {
                this.EventArgs.Add(argName, argValue);
            }

            public void SetString(string str, params object[] args)
            {
                this.EventStr = string.Format(str, args);
            }

            public enum ID
            {
                INVALID,
                STARTED,
                COMPLETED,
                PROGRESS_UPDATE,
                BEFORE_REQUEST_NOTIFICATION,
                AFTER_REQUEST_NOTIFICATION,
                LOG_MSG,
                RESULT_UPDATE,
                DOWNLOAD_PROGRESS
            }
        }

        public class ReceiverResult
        {
            public ReceiversManager.ManagedReceiver Receiver;
            public object Result;
            public ReceiverRetVal RetVal;
        }

        public enum RequestType
        {
            INVALID_REQUEST,
            GET_UNIT_INFO,
            GET_UNIT_STATUS,
            GET_ALL_TAGS,
            GET_TAGS,
            GET_NOISE_LEVEL,
            GET_ANTENNA_GAIN,
            SET_ANTENNA_GAIN,
            GET_TIME,
            SET_TIME,
            FLUSH_TAG_BUFFER,
            GET_MODE,
            SET_MODE_BOOTLOADER,
            SET_MODE_FIRMWARE,
            DOWNLOAD,
            FIRMWARE_STATE,
            MANAGED_DOWNLOAD,
            GET_ALL_RECEIVER_INFO,
            BOOTLOADER_FIRMWARE_STATE,
            BOOTLOADER_GET_UNIT_INFO,
            ACTIVATE_RELAY,
            SET_UNIT_NAME,
            GET_UNIT_NAME,
            SET_SITE_CODE,
            GET_SITE_CODE,
            GetRFBaudRate,
            SetRFBaudRate,
            GetModulation,
            SetModulation,
            GetSerialNum,
            SetSerialNum,
            SET_RSSI_THRESHOLD,
            GET_RSSI_THRESHOLD,
            GET_POWER_CONTROL
        }

        public class ResultSet : Dictionary<ReceiversManager.ManagedReceiver, ReceiversManager.ReceiverResult>
        {
        }

        public enum RetVal
        {
            SUCCESS,
            ERROR,
            NAME_IN_USE,
            PORT_ALREADY_HANDLED,
            PORT_NOT_AVAILABLE,
            NO_SUCH_LOOP,
            NO_SUCH_RECEIVER,
            REQUEST_STILL_EXECUTING,
            NO_REQUEST_EXECUTING
        }

        private class WorkerThreadRequest
        {
            public bool mEnterBootloader;
            public ReceiversManager.Loop mLoop;
            public ReceiversManager mParent;
            public ReceiversManager.ManagedReceiver[] mReceiversList;
            public object[] mRequestArgs;
            public ReceiversManager.RequestType mRequestType;
            public object[] mResults;
            public ReceiverRetVal[] mRetVals;
            private bool mStopTrigger;
            private Thread mThread;

            public WorkerThreadRequest(ReceiversManager Parent, ReceiversManager.Loop _Loop, ReceiversManager.ManagedReceiver[] ReceiversList, ReceiversManager.RequestType _RequestType, params object[] RequestArgs)
            {
                this.mParent = Parent;
                this.mLoop = _Loop;
                this.mReceiversList = ReceiversList;
                this.mRequestType = _RequestType;
                this.mRequestArgs = RequestArgs;
                this.mRetVals = new ReceiverRetVal[this.mReceiversList.Length];
                this.mResults = new object[this.mReceiversList.Length];
                this.mThread = new Thread(new ParameterizedThreadStart(ReceiversManager.WorkerThread));
                this.mStopTrigger = false;
                this.mEnterBootloader = false;
                if ((_RequestType == ReceiversManager.RequestType.BOOTLOADER_FIRMWARE_STATE) || (_RequestType == ReceiversManager.RequestType.BOOTLOADER_GET_UNIT_INFO))
                {
                    this.mEnterBootloader = true;
                }
                this.mThread.Name = string.Format("ReceiversManagerWorkerThread({0})", this.mLoop.Name);
                for (int i = 0; i < this.mReceiversList.Length; i++)
                {
                    this.mRetVals[i] = ReceiverRetVal.ERROR;
                    this.mResults[i] = null;
                }
            }

            public bool Abort()
            {
                if (!this.IsAlive)
                {
                    return false;
                }
                this.mStopTrigger = true;
                return true;
            }

            public void Join()
            {
                this.mThread.Join();
            }

            public bool Join(int msTimeout)
            {
                return this.mThread.Join(msTimeout);
            }

            public void Start()
            {
                this.mThread.Start(this);
            }

            public bool AbortRequested
            {
                get
                {
                    return this.mStopTrigger;
                }
            }

            public bool IsAlive
            {
                get
                {
                    return this.mThread.IsAlive;
                }
            }
        }

        public class XMLLoop
        {
            public ReceiverBusConnection.BusType LoopBusType;
            public string Name;
            public string SettingsStr;
        }

        public class XMLManagedReceiver
        {
            public string LoopName;
            public string Name;
            public byte UnitID;
        }

        public class XMLReceiversManagerDB
        {
            public ReceiversManager.XMLLoop[] Loops;
            public ReceiversManager.XMLManagedReceiver[] ManagedReceivers;
        }
    }
}

