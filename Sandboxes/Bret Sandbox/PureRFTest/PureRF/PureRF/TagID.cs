namespace PureRF
{
    using System;
    using System.Runtime.InteropServices;

    public class TagID
    {
        private uint mCustomerNum;
        private uint mGroupID;
        private uint mSiteID;
        private uint mTagNum;

        public TagID()
        {
            this.mCustomerNum = 0;
            this.mSiteID = 0;
            this.mGroupID = 0;
            this.mTagNum = 0;
        }

        public TagID(TagID tagID)
        {
            this.CustomerNum = tagID.CustomerNum;
            this.SiteID = tagID.SiteID;
            this.GroupID = tagID.GroupID;
            this.TagNum = tagID.TagNum;
        }

        public TagID(uint tagID)
        {
            this.SetPureRFTagID(tagID);
        }

        public TagID(uint customerNum, uint siteID, uint groupID, uint tagNum)
        {
            this.CustomerNum = customerNum;
            this.SiteID = siteID;
            this.GroupID = groupID;
            this.TagNum = tagNum;
        }

        public TagID Clone()
        {
            return new TagID(this);
        }

        public override bool Equals(object obj)
        {
            if ((obj != null) && (base.GetType() == obj.GetType()))
            {
                throw new Exception("The method or operation is not implemented.");
            }
            return false;
        }

        public void GetASKTagID(out byte SiteCode, out int TagID)
        {
            uint num = 0;
            num = this.TagNum | (this.GroupID << 10);
            num |= this.SiteID << 0x10;
            num |= this.CustomerNum << 0x1a;
            SiteCode = (byte) ((num >> 0x18) & 0xff);
            TagID = (int) (num & 0xffffff);
        }

        public override int GetHashCode()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public uint GetPureRFTagID()
        {
            uint num = 0;
            num = this.TagNum | (this.GroupID << 10);
            num |= this.SiteID << 0x10;
            return (num | (this.CustomerNum << 0x1a));
        }

        public static bool operator ==(TagID t1, TagID t2)
        {
            return (t1.GetPureRFTagID() == t2.GetPureRFTagID());
        }

        public static bool operator !=(TagID t1, TagID t2)
        {
            return !(t1 == t2);
        }

        public void SetPureRFTagID(uint tagID)
        {
            this.TagNum = tagID & 0x3ff;
            this.GroupID = (tagID >> 10) & 0x3f;
            this.SiteID = (tagID >> 0x10) & 0x3ff;
            this.CustomerNum = (tagID >> 0x1a) & 0x3f;
        }

        public override string ToString()
        {
            byte num;
            int num2;
            this.GetASKTagID(out num, out num2);
            return string.Format("Site:{0} TagID:{1}", num, num2);
        }

        public uint CustomerNum
        {
            get
            {
                return this.mCustomerNum;
            }
            set
            {
                this.mCustomerNum = value & 0x3f;
            }
        }

        public uint GroupID
        {
            get
            {
                return this.mGroupID;
            }
            set
            {
                this.mGroupID = value & 0x3f;
            }
        }

        public uint SiteID
        {
            get
            {
                return this.mSiteID;
            }
            set
            {
                this.mSiteID = value & 0x3ff;
            }
        }

        public uint TagNum
        {
            get
            {
                return this.mTagNum;
            }
            set
            {
                this.mTagNum = value & 0x3ff;
            }
        }
    }
}

