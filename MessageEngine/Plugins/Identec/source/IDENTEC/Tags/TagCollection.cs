namespace IDENTEC.Tags
{
    using System;
    using System.Collections.Generic;

    public class TagCollection : List<Tag>
    {
        private object _thisLock;

        public TagCollection()
        {
            this._thisLock = new object();
        }

        public TagCollection(TagCollection tags) : base(tags)
        {
            this._thisLock = new object();
        }

        public TagCollection(int capacity) : base(capacity)
        {
            this._thisLock = new object();
        }

        public object SyncRoot
        {
            get
            {
                return this._thisLock;
            }
        }
    }
}

