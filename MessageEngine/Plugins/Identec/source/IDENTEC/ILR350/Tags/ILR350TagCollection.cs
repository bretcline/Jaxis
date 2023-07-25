namespace IDENTEC.ILR350.Tags
{
    using System;
    using System.Collections.Generic;

    public class ILR350TagCollection : List<ILR350Tag>
    {
        public ILR350TagCollection()
        {
        }

        public ILR350TagCollection(int capacity) : base(capacity)
        {
        }

        public iQ350TagCollection GetiQ350Tags()
        {
            iQ350TagCollection tags = new iQ350TagCollection();
            foreach (ILR350Tag tag in this)
            {
                iQ350 item = tag as iQ350;
                if (item != null)
                {
                    tags.Add(item);
                }
            }
            return tags;
        }
    }
}

