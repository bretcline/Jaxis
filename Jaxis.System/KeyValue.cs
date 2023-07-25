namespace Jaxis.JaxisSystem
{
    /// <summary>
    /// This class is patterned after the KeyValuePair struct, except the properties are read-write, instead of just read, 
    /// and the class is inheritable.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class KeyValue<TKey, TValue>
    {
        public virtual TKey Key { get; set; }

        public virtual TValue Value { get; set; }

        public KeyValue( TKey key, TValue value )
        {
            Key = key;
            Value = value;
        }
    }
}

