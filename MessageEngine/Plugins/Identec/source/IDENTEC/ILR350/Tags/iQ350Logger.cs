namespace IDENTEC.ILR350.Tags
{
    using IDENTEC;
    using IDENTEC.ILR350.Readers;
    using IDENTEC.Readers;
    using IDENTEC.Tags;
    using IDENTEC.Tags.Logging;
    using System;
    using System.Runtime.InteropServices;

    public class iQ350Logger : iQ350, ILoggerInterface
    {
        private const int ACTUAL_TEMPERATURE = 0x80030;
        private const int LOG_EXTREMEPOSITION = 0x80036;
        private const int LOG_FORMAT_OFFSET = 0;
        private const int LOG_INFO_ADDRESS = 0x80000;
        private const int LOG_INFO_SIZE = 0x22;
        private const int LOG_INTERVAL_OFFSET = 0x20;
        private const int LOG_MAX_SIZE_OFFSET = 2;
        private const int LOG_POINTER_OFFSET = 20;
        private const int LOG_SAMPLE_SIZE_OFFSET = 1;
        private const int LOG_SIZE_OFFSET = 0x10;
        private const int LOG_STARTTIME_OFFSET = 8;
        private const int LOG_STATE_OFFSET = 0x1b;
        private const int LOG_STOPTIME_OFFSET = 12;
        private const int LOG_TIME_LAST_LOG_OFFSET = 0x18;
        private const int LOG_TIME_OFFSET = 0x1c;

        public Temperature ReadCurrentTemperature(ILR350Reader reader)
        {
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_3, 0x80030, 2, iQ350.RETRIES);
            if (result.BytesRead != 2)
            {
                throw new PartialTagCommunicationsException();
            }
            Temperature temperature = new Temperature();
            Array.Reverse(result.Data, 0, 2);
            int num = BitConverter.ToInt16(result.Data, 0);
            temperature.m_temperatureDegreesC = ((float) num) / 10f;
            return temperature;
        }

        public RawLogData ReadFirstnLogSamples(ILR350Reader reader, int nLogToRead, int retries)
        {
            RawLogData data2;
            LogInfoData logInfo = this.ReadLogInfo(reader, 2);
            if ((nLogToRead == 0) || (nLogToRead > logInfo.TagSampleCount))
            {
                nLogToRead = logInfo.TagSampleCount;
            }
            this.ReadLogSamples(logInfo, out data2, 0, nLogToRead, reader, retries);
            return data2;
        }

        public RawLogData ReadLastnLogSamples(ILR350Reader reader, int nLogToRead, int retries)
        {
            RawLogData data2;
            LogInfoData logInfo = this.ReadLogInfo(reader, 2);
            if ((nLogToRead == 0) || (nLogToRead > logInfo.TagSampleCount))
            {
                nLogToRead = logInfo.TagSampleCount;
            }
            this.ReadLogSamples(logInfo, out data2, logInfo.TagSampleCount - nLogToRead, nLogToRead, reader, retries);
            return data2;
        }

        private LogInfoData ReadLogInfo(ILR350Reader reader, int retries)
        {
            return this.ReadLogInformation(reader);
        }

        public LogInfoData ReadLogInformation(ILR350Reader reader)
        {
            LogInfoData data = new LogInfoData();
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_3, 0x80000, 0x22, iQ350.RETRIES);
            data.m_dtReadTime = DateTime.Now;
            if (!result.Success)
            {
                throw new PartialTagCommunicationsException("Failed to read tag log time information.");
            }
            data.m_logFormat = result.Data[0];
            Array.Reverse(result.Data, 8, 4);
            data.m_dtLoggerStartTime = DateTimeConvertor.Convert_time_t(BitConverter.ToUInt32(result.Data, 8));
            Array.Reverse(result.Data, 12, 4);
            data.m_dtLoggerStopTime = DateTimeConvertor.Convert_time_t(BitConverter.ToUInt32(result.Data, 12));
            Array.Reverse(result.Data, 0x20, 2);
            data.m_tsLogInterval = new TimeSpan(0, 0, BitConverter.ToUInt16(result.Data, 0x20));
            data.m_LogSampleSize = result.Data[1];
            Array.Reverse(result.Data, 0x10, 4);
            data.m_LogSize = BitConverter.ToUInt32(result.Data, 0x10);
            Array.Reverse(result.Data, 2, 4);
            data.m_MaxSize = BitConverter.ToUInt32(result.Data, 2);
            if (data.m_LogSize >= data.m_MaxSize)
            {
                data.m_bWrapped = true;
            }
            Array.Reverse(result.Data, 0x18, 2);
            ushort seconds = BitConverter.ToUInt16(result.Data, 0x18);
            data.m_tsTimeSinceLastLog = new TimeSpan(0, 0, seconds);
            if (data.m_tsTimeSinceLastLog > data.LoggingInterval)
            {
                data.m_tsTimeSinceLastLog = TimeSpan.Zero;
            }
            if (result.Data[0x1b] == 1)
            {
                data.m_bLogging = true;
                return data;
            }
            data.m_bLogging = false;
            return data;
        }

        private void ReadLogSamples(LogInfoData logInfo, out RawLogData logData, int start, int len, ILR350Reader reader, int retries)
        {
            TagReadDataResult result;
            switch (logInfo.m_logFormat)
            {
                case 1:
                case 2:
                    logData = new TemperatureLogData();
                    break;

                default:
                    logData = new RawLogData();
                    break;
            }
            logData.m_LogInfo = logInfo;
            try
            {
                result = this.ReadDataFromRegister(reader, Register.REGISTER_3, logInfo.m_LogSampleSize * start, logInfo.m_LogSampleSize * len, retries);
            }
            catch (InvalidTagStatusException exception)
            {
                if (exception.StatusCode != 4)
                {
                    throw;
                }
                ILR350Tag.log.Trace("Tag has overwritten data so try again");
                logInfo = this.ReadLogInfo(reader, 2);
                result = this.ReadDataFromRegister(reader, Register.REGISTER_3, logInfo.m_LogSampleSize * start, logInfo.m_LogSampleSize * len, retries);
            }
            logData.m_LogInfo = logInfo;
            if (logData.Wrapped)
            {
                if (logData.Logging)
                {
                    logData.m_dtEnd = logInfo.m_dtReadTime;
                }
                else
                {
                    logData.m_dtEnd = logInfo.m_dtLoggerStopTime;
                }
                logData.m_dtEnd = logData.m_dtEnd.Add(-logInfo.m_tsTimeSinceLastLog);
                int num = (logInfo.TagSampleCount - start) - 1;
                logData.m_dtStart = logData.m_dtEnd.AddSeconds(-num * logData.LoggingInterval.TotalSeconds);
            }
            else
            {
                logData.m_dtEnd = logInfo.m_dtReadTime.Add(-logInfo.m_tsTimeSinceLastLog);
                int num2 = (logInfo.TagSampleCount - start) - 1;
                logData.m_dtStart = logData.m_dtEnd.AddSeconds(-num2 * logData.LoggingInterval.TotalSeconds);
                logData.m_dtStart = logInfo.m_dtLoggerStartTime.AddSeconds(start * logData.LoggingInterval.TotalSeconds);
            }
            logData.m_dtEnd = logData.m_dtStart.AddSeconds(((result.BytesRead / logInfo.m_LogSampleSize) - 1) * logData.LoggingInterval.TotalSeconds);
            logData.m_byBuffer = new byte[result.BytesRead];
            Array.Copy(result.Data, 0, logData.m_byBuffer, 0, logData.m_byBuffer.Length);
            TemperatureLogData data = logData as TemperatureLogData;
            if (data != null)
            {
                int num3 = logData.m_byBuffer.Length / logInfo.m_LogSampleSize;
                data.m_fTemperatures = new float[num3];
                for (int i = 0; i < num3; i++)
                {
                    Array.Reverse(logData.m_byBuffer, i * 2, 2);
                    data.m_fTemperatures[i] = BitConverter.ToInt16(logData.m_byBuffer, i * 2);
                    switch (logInfo.m_logFormat)
                    {
                        case 1:
                            data.m_fTemperatures[i] /= 100f;
                            break;

                        case 2:
                            data.m_fTemperatures[i] /= 10f;
                            break;

                        default:
                            ILR350Tag.log.Debug("Log format " + logInfo.m_logFormat + " unknown ");
                            throw new FormatException("Log format " + logInfo.m_logFormat + " unknown ");
                    }
                }
            }
        }

        public TimeSpan ReadMeasurementInterval(ILR350Reader reader)
        {
            TagReadDataResult result = new TagReadDataResult();
            result = this.ReadDataFromRegister(reader, Register.REGISTER_3, 0x80032, 2, iQ350.RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            ushort num = result.Data[0];
            num = (ushort) (num << 8);
            num = (ushort) (num + result.Data[1]);
            return TimeSpan.FromMilliseconds(num * 500.0);
        }

        public TemperatureExtremes ReadTemperatureExtremes(ILR350Reader reader)
        {
            LogInfoData data = this.ReadLogInformation(reader);
            TemperatureExtremes extremes = new TemperatureExtremes();
            TagReadDataResult result = this.ReadDataFromRegister(reader, Register.REGISTER_3, 0x80036, 4, iQ350.RETRIES);
            if (result.BytesRead != 4)
            {
                throw new PartialTagCommunicationsException();
            }
            Array.Reverse(result.Data, 0, 2);
            int num = BitConverter.ToInt16(result.Data, 0);
            extremes.m_MinimumDegreesCelsius = ((float) num) / 10f;
            Array.Reverse(result.Data, 2, 2);
            num = BitConverter.ToInt16(result.Data, 2);
            extremes.m_MaximumDegreesCelsius = ((float) num) / 10f;
            extremes.m_dtLogEnd = data.LoggerStopped;
            extremes.m_dtLogStart = data.LoggerStarted;
            return extremes;
        }

        public float ReadTemperatureOffset(ILR350Reader reader)
        {
            float num = 0.1f;
            TagReadDataResult result = new TagReadDataResult();
            result = this.ReadDataFromRegister(reader, Register.REGISTER_3, 0x80034, 1, iQ350.RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            return (num * ((sbyte) result.Data[0]));
        }

        public bool StartLogging(ILR350Reader reader, TimeSpan Interval)
        {
            uint utcNow = DateTimeConvertor.GetUtcNow();
            if (Interval.TotalMinutes > 15.0)
            {
                throw new ArgumentOutOfRangeException("samplingRate is limited to 15 minutes");
            }
            if (1.0 > Interval.TotalMinutes)
            {
                throw new ArgumentOutOfRangeException("minimum samplingRate is 1 minute");
            }
            int totalSeconds = (int) Interval.TotalSeconds;
            byte[] data = new byte[7];
            data[0] = 1;
            if (!this.ReadData(reader, 0, 1, iQ350.RETRIES).Success)
            {
                return false;
            }
            data[1] = (byte) ((utcNow >> 0x18) & 0xff);
            data[2] = (byte) ((utcNow >> 0x10) & 0xff);
            data[3] = (byte) ((utcNow >> 8) & 0xff);
            data[4] = (byte) (utcNow & 0xff);
            data[5] = (byte) ((totalSeconds >> 8) & 0xff);
            data[6] = (byte) (totalSeconds & 0xff);
            return this.WriteDataToRegister(reader, Register.REGISTER_3, 0x8001b, data, data.Length, iQ350.RETRIES).Success;
        }

        public bool StopLogging(ILR350Reader reader)
        {
            DateTime now = DateTime.Now;
            uint utcNow = DateTimeConvertor.GetUtcNow();
            byte[] data = new byte[] { 0, (byte) ((utcNow >> 0x18) & 0xff), (byte) ((utcNow >> 0x10) & 0xff), (byte) ((utcNow >> 8) & 0xff), (byte) (utcNow & 0xff) };
            return this.WriteDataToRegister(reader, Register.REGISTER_3, 0x8001b, data, data.Length, iQ350.RETRIES).Success;
        }

        public bool WriteMeasurementInterval(ILR350Reader reader, TimeSpan interval)
        {
            if ((interval > new TimeSpan(0, 0xff, 0)) || (interval < TimeSpan.Zero))
            {
                throw new ArgumentOutOfRangeException("interval must be between 0 and 255 minutes");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[2];
            double num = interval.TotalMilliseconds / 500.0;
            data[0] = (byte) (((short) num) >> 8);
            data[1] = (byte) num;
            if (!this.WriteDataToRegister(reader, Register.REGISTER_3, 0x80032, data, 2, iQ350.RETRIES).Success)
            {
                return false;
            }
            TimeSpan span = this.ReadMeasurementInterval(reader);
            if (interval != span)
            {
                throw new Exception("Tag has changed value to " + span.ToString());
            }
            return true;
        }

        public bool WriteTemperatureOffset(ILR350Reader reader, float Offset)
        {
            if ((Offset > 2f) || (Offset < -2f))
            {
                throw new ArgumentOutOfRangeException("offset must be between -2 and +2 \x00b0C ");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[1];
            Offset *= 10f;
            data[0] = (byte) Convert.ToSByte(Offset);
            return this.WriteDataToRegister(reader, Register.REGISTER_3, 0x80034, data, 1, iQ350.RETRIES).Success;
        }
    }
}

