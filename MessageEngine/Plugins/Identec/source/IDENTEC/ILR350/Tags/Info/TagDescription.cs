namespace IDENTEC.ILR350.Tags.Info
{
    using IDENTEC.ILR350.Tags;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class TagDescription : IComparable
    {
        private TimeSpan _AwakeTime;
        private TimeSpan _FastSniffTime;
        private List<TagFeature> _features;
        private string _name;
        private Type _tagType;
        private TimeSpan _WakeUpTime;

        public TagDescription(Type tagType, string name, TimeSpan AwakeTime, TimeSpan WakeUpTime, List<TagFeature> features)
        {
            this._tagType = tagType;
            this._AwakeTime = AwakeTime;
            this._WakeUpTime = WakeUpTime;
            this._FastSniffTime = iQ350.MAX_FAST_SNIFF_TIME;
            this._features = features;
            this._features.Sort();
            this._name = name;
        }

        public TagDescription(Type tagType, string name, TimeSpan AwakeTime, TimeSpan FastSniffTime, TimeSpan WakeUpTime, List<TagFeature> features)
        {
            this._tagType = tagType;
            this._AwakeTime = AwakeTime;
            this._WakeUpTime = WakeUpTime;
            this._FastSniffTime = FastSniffTime;
            this._features = features;
            this._features.Sort();
            this._name = name;
        }

        public virtual int CompareTo(object obj)
        {
            TagDescription description = obj as TagDescription;
            if (description == null)
            {
                throw new ArgumentException("Not correct object type");
            }
            return this.Name.CompareTo(description.Name);
        }

        public override bool Equals(object obj)
        {
            TagDescription description = obj as TagDescription;
            if (description == null)
            {
                throw new ArgumentException("Not correct object type");
            }
            return this.Name.Equals(description.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        internal ILR350Tag TagFactory()
        {
            if (this._tagType != null)
            {
                ILR350Tag tag = null;
                ConstructorInfo constructor = this._tagType.GetConstructor(new Type[0]);
                if (constructor == null)
                {
                    return new iQ350();
                }
                tag = constructor.Invoke(null) as ILR350Tag;
                if (tag != null)
                {
                    return tag;
                }
            }
            return new iQ350();
        }

        public TimeSpan AwakeTime
        {
            get
            {
                return this._AwakeTime;
            }
        }

        public TimeSpan FastSniffTime
        {
            get
            {
                return this._FastSniffTime;
            }
        }

        public List<TagFeature> Features
        {
            get
            {
                return this._features;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public Type type
        {
            get
            {
                return this._tagType;
            }
        }

        public TimeSpan WakeUpTime
        {
            get
            {
                return this._WakeUpTime;
            }
        }
    }
}

