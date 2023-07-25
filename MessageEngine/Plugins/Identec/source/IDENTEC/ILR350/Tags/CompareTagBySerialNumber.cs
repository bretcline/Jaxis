namespace IDENTEC.ILR350.Tags
{
    using System;
    using System.Collections;

    public class CompareTagBySerialNumber : IComparer
    {
        public int Compare(object x, object y)
        {
            ILR350Tag tag = x as ILR350Tag;
            ILR350Tag tag2 = y as ILR350Tag;
            return tag.SerialLabel.CompareTo(tag2.SerialLabel);
        }

        public int Compare(string x, string y)
        {
            string str = x;
            string strB = y;
            return str.CompareTo(strB);
        }
    }
}

