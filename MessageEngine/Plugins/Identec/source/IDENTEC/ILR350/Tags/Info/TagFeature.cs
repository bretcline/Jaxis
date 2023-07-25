namespace IDENTEC.ILR350.Tags.Info
{
    using System;

    public abstract class TagFeature : IComparable<TagFeature>
    {
        internal string name;

        protected TagFeature()
        {
        }

        public int CompareTo(TagFeature other)
        {
            return this.name.CompareTo(other.Name);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }
    }
}

