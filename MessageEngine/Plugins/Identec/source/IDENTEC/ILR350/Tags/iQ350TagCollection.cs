namespace IDENTEC.ILR350.Tags
{
    using System;
    using System.Collections.Generic;

    public class iQ350TagCollection : List<iQ350>
    {
        public iQ350TagCollection()
        {
        }

        public iQ350TagCollection(int capacity) : base(capacity)
        {
        }

        public ILR350TagCollection ConvertToILR350Collection()
        {
            ILR350TagCollection tags = new ILR350TagCollection();
            foreach (ILR350Tag tag in this)
            {
                tags.Add(tag);
            }
            return tags;
        }
    }
}

