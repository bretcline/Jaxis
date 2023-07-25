namespace IDENTEC.ILR350.Tags
{
    using System;
    using System.Collections;

    public class CompareTagByManufacturerID : IComparer
    {
        public int Compare(object x, object y)
        {
            ILR350Tag tag = x as ILR350Tag;
            ILR350Tag tag2 = y as ILR350Tag;
            return tag.ManufacturerID.CompareTo(tag2.ManufacturerID);
        }
    }
}

