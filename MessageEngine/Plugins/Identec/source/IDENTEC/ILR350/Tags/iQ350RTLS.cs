namespace IDENTEC.ILR350.Tags
{
    using IDENTEC.ILR350.Readers;
    using IDENTEC.Tags;
    using System;

    public class iQ350RTLS : iQ350Logger, IRTLSTagInterface
    {
        public MotionTriggeredRangingBehavior ReadMotionTriggeredRangingBehavior(ILR350Reader reader)
        {
            TagReadDataResult result = new TagReadDataResult();
            result = this.ReadDataFromRegister(reader, Register.REGISTER_8, 0x31, 1, iQ350.RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            if (result.Data[0] > 1)
            {
                return MotionTriggeredRangingBehavior.RangingAfterStopped;
            }
            return (MotionTriggeredRangingBehavior) result.Data[0];
        }

        public TimeSpan ReadMotionTriggeredRangingInterval(ILR350Reader reader)
        {
            TagReadDataResult result = new TagReadDataResult();
            result = this.ReadDataFromRegister(reader, Register.REGISTER_8, 0x35, 3, iQ350.RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            uint num = (uint) (result.Data[0] << 0x10);
            num += (uint) (result.Data[1] << 8);
            num += result.Data[2];
            if (num == 0xffffff)
            {
                return new TimeSpan(0, 0, 30);
            }
            return TimeSpan.FromMilliseconds((double) num);
        }

        public TimeSpan ReadRangingInterval(ILR350Reader reader)
        {
            TagReadDataResult result = new TagReadDataResult();
            result = this.ReadDataFromRegister(reader, Register.REGISTER_8, 50, 3, iQ350.RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            uint num = (uint) (result.Data[0] << 0x10);
            num += (uint) (result.Data[1] << 8);
            num += result.Data[2];
            if (num == 0xffffff)
            {
                return new TimeSpan(0, 5, 0);
            }
            return TimeSpan.FromMilliseconds((double) num);
        }

        public int ReadRangingTXPower(ILR350Reader reader)
        {
            TagReadDataResult result = new TagReadDataResult();
            result = this.ReadDataFromRegister(reader, Register.REGISTER_8, 0x30, 1, iQ350.RETRIES);
            if (!result.Success)
            {
                throw new Exception("Unable to read the data");
            }
            if (result.Data[0] > 20)
            {
                return 20;
            }
            return result.Data[0];
        }

        public bool WriteMotionTriggeredRangingBehavior(ILR350Reader reader, MotionTriggeredRangingBehavior behavior)
        {
            if ((behavior != MotionTriggeredRangingBehavior.RangingAfterStopped) && (behavior != MotionTriggeredRangingBehavior.RanginWhenInMotion))
            {
                throw new ArgumentOutOfRangeException("invalid parameter");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[] { (byte)behavior };
            if (!this.WriteDataToRegister(reader, Register.REGISTER_8, 0x31, data, 1, iQ350.RETRIES).Success)
            {
                return false;
            }
            return true;
        }

        public bool WriteMotionTriggeredRangingInterval(ILR350Reader reader, TimeSpan interval)
        {
            if (interval > new TimeSpan(0, 0xff, 0))
            {
                throw new ArgumentOutOfRangeException("interval must be less than 255 minutes");
            }
            if ((interval < new TimeSpan(0, 0, 1)) && (interval != TimeSpan.Zero))
            {
                throw new ArgumentOutOfRangeException("interval must be more than 1 second");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[3];
            double totalMilliseconds = interval.TotalMilliseconds;
            data[0] = (byte) (((short) totalMilliseconds) >> 0x10);
            data[1] = (byte) (((short) totalMilliseconds) >> 8);
            data[2] = (byte) totalMilliseconds;
            if (!this.WriteDataToRegister(reader, Register.REGISTER_8, 0x35, data, 3, iQ350.RETRIES).Success)
            {
                return false;
            }
            return true;
        }

        public bool WriteRangingInterval(ILR350Reader reader, TimeSpan interval)
        {
            if (interval > new TimeSpan(0, 0xff, 0))
            {
                throw new ArgumentOutOfRangeException("interval must be less than 255 minutes");
            }
            if ((interval < new TimeSpan(0, 0, 1)) && (interval != TimeSpan.Zero))
            {
                throw new ArgumentOutOfRangeException("interval must be more than 1 second");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[3];
            double totalMilliseconds = interval.TotalMilliseconds;
            data[0] = (byte) (((short) totalMilliseconds) >> 0x10);
            data[1] = (byte) (((short) totalMilliseconds) >> 8);
            data[2] = (byte) totalMilliseconds;
            if (!this.WriteDataToRegister(reader, Register.REGISTER_8, 50, data, 3, iQ350.RETRIES).Success)
            {
                return false;
            }
            return true;
        }

        public bool WriteRangingTXPower(ILR350Reader reader, int txpower)
        {
            if ((txpower > 20) || (txpower < 0))
            {
                throw new ArgumentOutOfRangeException("interval must be between 0 and 20");
            }
            TagWriteDataResult result = new TagWriteDataResult();
            byte[] data = new byte[] { (byte) txpower };
            if (!this.WriteDataToRegister(reader, Register.REGISTER_8, 0x30, data, 1, iQ350.RETRIES).Success)
            {
                return false;
            }
            return true;
        }
    }
}

