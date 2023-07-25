using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;

namespace JaxisExtensions
{
    public static class Extensions
    {
        #region Extensions for String

        public static string ReplaceDiacritics(this string source)
        {
            string sourceInFormD = source.Normalize(NormalizationForm.FormD);

            var output = new StringBuilder();
            foreach (char c in sourceInFormD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark && uc != UnicodeCategory.OtherSymbol )
                {
                    output.Append(c);
                }
                else
                {
                    output.Append(' ');
                }

            }

            return (output.ToString().Normalize(NormalizationForm.FormC));
        }

        public static bool TryParseAsGuid( this string stringGuid, out Guid result )
        {
            bool rc = false;
            result = Guid.Empty;
            if ( Regex.Match( stringGuid, "[0-9A-F]{8}-([0-9A-F]{4}-){3}[0-9A-F]{12}", RegexOptions.IgnoreCase ).Success )
            {
                result = new Guid( stringGuid );
                rc = true;
            }
            return rc;
        }

        public static T DeserializeObject<T>( this string pXmlizedString ) where T : class
        {
            try
            {
                XmlSerializer xs = new XmlSerializer( typeof( T ) );
                UTF8Encoding encoding = new UTF8Encoding( );
                Byte[] byteArray = encoding.GetBytes( pXmlizedString );
                using( MemoryStream memoryStream = new MemoryStream( byteArray ) )
                {
                    return (T)xs.Deserialize( memoryStream );
                }
            }
            catch( Exception err )
            {
                throw err;
            }
        }

        #endregion Extensions for String

        #region Extensions for StreamWriter

        public static void SerializeObject<T>( this StreamWriter Writer, T data ) where T : class
        {
            try
            {
                XmlSerializer xs = new XmlSerializer( typeof( T ) );
                UTF8Encoding encoding = new UTF8Encoding( );
                xs.Serialize( Writer, data );
            }
            catch( Exception err )
            {
                throw err;
            }
        }

        #endregion Extensions for StreamWriter

        #region Extensions for object


        public static object Clone( this object _Obj )
        {
            var rc = new object();
            if (_Obj is string)
            {
                rc = _Obj as string;
            }
            else
            {
                rc = Activator.CreateInstance(_Obj.GetType());

                foreach (PropertyInfo pi in _Obj.GetType().GetProperties())
                {
                    try
                    {
                        // Get the value of a property and try 
                        // to assign it to a same-named property of T
                        PropertyInfo Prop = rc.GetType().GetProperty(pi.Name);
                        if (null != Prop)
                        {
                            //if( Prop.PropertyType.IsValueType )
                            //{
                            //    rc = pi.GetValue( _Obj, null );
                            //}
                            //else 

                            if (typeof (ICollection).IsAssignableFrom(Prop.PropertyType) ||
                                typeof (ICollection<>).IsAssignableFrom(Prop.PropertyType))
                            {
                                Type IEnumerableType = Prop.PropertyType.GetInterface("IEnumerable", true);
                                if (IEnumerableType != null)
                                {
                                    //Get the IEnumerable interface from the field.
                                    var IObj = Prop.GetValue(_Obj, null);
                                    IEnumerable IEnum = (IEnumerable) IObj;
                                    //This version support the IList and the 
                                    //IDictionary interfaces to iterate on collections.
                                    Type IListType = Prop.PropertyType.GetInterface("IList", true);
                                    Type IDicType = Prop.PropertyType.GetInterface("IDictionary", true);
                                    int j = 0;
                                    if (IListType != null)
                                    {
                                        if (Prop.PropertyType.IsArray)
                                        {
                                            Type elementType =
                                                Type.GetType(Prop.PropertyType.FullName.Replace("[]", string.Empty));
                                            var array = IEnum as Array;
                                            if (null != array)
                                            {
                                                Array copied = Array.CreateInstance(elementType, array.Length);
                                                for (int i = 0; i < array.Length; i++)
                                                {
                                                    copied.SetValue(array.GetValue(i), i);
                                                }
                                                Prop.SetValue(rc, copied, null);
                                            }
                                        }
                                        else
                                        {
                                            //Getting the IList interface.
                                            IList list = Activator.CreateInstance(Prop.PropertyType) as IList;
                                            if (null != list)
                                            {
                                                foreach (object obj in IEnum)
                                                {
                                                    list.Add(obj.Clone());
                                                    j++;
                                                }
                                                Prop.SetValue(rc, list, null);
                                            }
                                        }
                                    }
                                    else if (IDicType != null)
                                    {
                                        IDictionary dict = null;
                                        using (var ms = new MemoryStream())
                                        {
                                            var val = pi.GetValue(_Obj, null) as IDictionary;
                                            var formatter = new BinaryFormatter();
                                            formatter.Serialize(ms, val);
                                            ms.Position = 0;

                                            dict = formatter.Deserialize(ms) as IDictionary;
                                        }
                                        Prop.SetValue(rc, dict, null);

                                        //IDictionary dict = Activator.CreateInstance(Prop.PropertyType) as IDictionary;
                                        //if (null != dict)
                                        //{
                                        //    var val = pi.GetValue(_Obj, null) as IDictionary;
                                            
                                        //    if (Prop.PropertyType.IsGenericType)
                                        //    {
                                        //        foreach (KeyValuePair<object,object> value in IEnum)
                                        //        {
                                        //            dict[value.Key] = value.Value.Clone();
                                        //        }
                                        //        //foreach( KeyValuePair<,> kvp in IEnum )
                                        //        {

                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        foreach (DictionaryEntry de in IEnum)
                                        //        {
                                        //            dict[de.Key] = de.Value.Clone();
                                        //        }
                                        //    }
                                        //    Prop.SetValue(rc, dict, null);
                                        //}
                                    }
                                }
                                else
                                {
                                    var val = pi.GetValue(_Obj, null);
                                    Prop.SetValue(rc, val.Clone(), null);
                                }
                            }
                            else
                            {
                                if (Prop.CanWrite)
                                {
                                    Prop.SetValue(rc, pi.GetValue(_Obj, null), null);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return rc;
        }



        //public static object Clone( this object _Obj )
        //{
        //    //First we create an instance of this specific type.
        //    object newObject = Activator.CreateInstance( _Obj.GetType( ) );
        //    //We get the array of fields for the new type instance.
        //    FieldInfo[] fields = newObject.GetType( ).GetFields( );
        //    int i = 0;
        //    foreach( FieldInfo fi in _Obj.GetType( ).GetFields( ) )
        //    {
        //        //We query if the fiels support the ICloneable interface.
        //        Type ICloneType = fi.FieldType.
        //                    GetInterface( "ICloneable", true );
        //        if( ICloneType != null )
        //        {
        //            //Getting the ICloneable interface from the object.
        //            ICloneable IClone = (ICloneable)fi.GetValue( _Obj );
        //            //We use the clone method to set the new value to the field.
        //            fields[i].SetValue( newObject, IClone.Clone( ) );
        //        }
        //        else
        //        {
        //            // If the field doesn't support the ICloneable 
        //            // interface then just set it.
        //            fields[i].SetValue( newObject, fi.GetValue( _Obj ) );
        //        }

        //        //Now we check if the object support the 
        //        //IEnumerable interface, so if it does
        //        //we need to enumerate all its items and check if 
        //        //they support the ICloneable interface.

        //        Type IEnumerableType = fi.FieldType.GetInterface( "IEnumerable", true );
        //        if( IEnumerableType != null )
        //        {
        //            //Get the IEnumerable interface from the field.
        //            IEnumerable IEnum = (IEnumerable)fi.GetValue( _Obj );
        //            //This version support the IList and the 
        //            //IDictionary interfaces to iterate on collections.
        //            Type IListType = fields[i].FieldType.GetInterface
        //                                ( "IList", true );
        //            Type IDicType = fields[i].FieldType.GetInterface
        //                                ( "IDictionary", true );
        //            int j = 0;
        //            if( IListType != null )
        //            {
        //                //Getting the IList interface.
        //                IList list = (IList)fields[i].GetValue( newObject );
        //                foreach( object obj in IEnum )
        //                {
        //                    //Checking to see if the current item 
        //                    //support the ICloneable interface.
        //                    ICloneType = obj.GetType( ).
        //                        GetInterface( "ICloneable", true );

        //                    if( ICloneType != null )
        //                    {
        //                        //If it does support the ICloneable interface, 
        //                        //we use it to set the clone of
        //                        //the object in the list.
        //                        ICloneable clone = (ICloneable)obj;
        //                        list[j] = clone.Clone( );
        //                    }

        //                    //NOTE: If the item in the list is not 
        //                    //support the ICloneable interface then in the 
        //                    //cloned list this item will be the same 
        //                    //item as in the original list
        //                    //(as long as this type is a reference type).

        //                    j++;
        //                }
        //            }
        //            else if( IDicType != null )
        //            {
        //                //Getting the dictionary interface.
        //                IDictionary dic = (IDictionary)fields[i].
        //                                    GetValue( newObject );
        //                j = 0;
        //                foreach( DictionaryEntry de in IEnum )
        //                {
        //                    //Checking to see if the item 
        //                    //support the ICloneable interface.
        //                    ICloneType = de.Value.GetType( ).GetInterface( "ICloneable", true );
        //                    if( ICloneType != null )
        //                    {
        //                        ICloneable clone = (ICloneable)de.Value;
        //                        dic[de.Key] = clone.Clone( );
        //                    }
        //                    j++;
        //                }
        //            }
        //        }
        //        i++;
        //    }
        //    return newObject;
        //}


        public static object DeepCopy( this object obj )
        {
            if( obj == null )
                throw new ArgumentNullException( "Object cannot be null" );
            return Process( obj );
        }

        static object Process( object obj )
        {
            if( obj == null )
                return null;
            Type type = obj.GetType( );
            if( type.IsValueType || type == typeof( string ) )
            {
                return obj;
            }
            else if( type.IsArray )
            {
                Type elementType = Type.GetType(
                     type.FullName.Replace( "[]", string.Empty ) );
                var array = obj as Array;
                Array copied = Array.CreateInstance( elementType, array.Length );
                for( int i = 0; i < array.Length; i++ )
                {
                    copied.SetValue( Process( array.GetValue( i ) ), i );
                }
                return Convert.ChangeType( copied, obj.GetType( ) );
            }
            else if( type.IsClass )
            {
                object toret = Activator.CreateInstance( obj.GetType( ) );

                foreach( PropertyInfo pi in obj.GetType( ).GetProperties( ) )
                {
                    try
                    {
                        PropertyInfo Prop = toret.GetType( ).GetProperty( pi.Name );
                        if( null != Prop && Prop.CanWrite )
                        {
                            Prop.SetValue( toret, Process( Prop.GetValue( obj, null ) ), null );
                        }
                    }
                    catch
                    {
                    }
                }

                FieldInfo[] fields = type.GetFields( BindingFlags.Public |
                            BindingFlags.NonPublic | BindingFlags.Instance );
                foreach( FieldInfo field in fields )
                {
                    object fieldValue = field.GetValue( obj );
                    if( fieldValue != null )
                    {
                        field.SetValue( toret, Process( fieldValue ) );
                        
                    }
                }
                return toret;
            }
            else
                throw new ArgumentException( "Unknown type" );
        }


        
        /// <summary>
        /// Given an object of unknown type, convert it to T by copying whatever Properties
        /// have the same name; skip over any exceptions, and return a new T.
        /// NOTE: Does not work for Properties that are Collections.
        /// </summary>
        public static T ToType<T>( this object _obj ) where T : class, new( )
        {
            T tmp = new T( );
            ToType<T>( _obj, tmp );
            return tmp;
        }

        /// <summary>
        /// Given an object of unknown type and an object of type T, convert the former to the latter
        /// by copying whatever Properties have the same name; skip over any exceptions.
        /// NOTE: Does not work for Properties that are Collections.
        /// </summary>
        public static void ToType<T>(this object _obj, T _dest)
        {
            // Loop through the properties of the object you want to covert
            if (null != _obj)
            {
                foreach (PropertyInfo pi in _obj.GetType().GetProperties())
                {
                    try
                    {
                        var name = pi.Name;
                        // Get the value of a property and try 
                        // to assign it to a same-named property of T
                        var Prop = _dest.GetType().GetProperty(name);
                        if (null != Prop)
                        {
                            Prop.SetValue(_dest, pi.GetValue(_obj, null), null);
                        }
                        else
                        {
                            name = char.ToUpper(name[0]) + name.Substring(1);
                            Prop = _dest.GetType().GetProperty(name);
                            if (null != Prop)
                            {
                                Prop.SetValue(_dest, pi.GetValue(_obj, null), null);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Given an object, return null if it happens to be DBNull.Value
        /// </summary>
        public static object NullIfDBNull( this object _object )
        {
            return ( _object == DBNull.Value ? null : _object );
        }

        #endregion

        #region Extensions for IList

        /// <summary>
        /// Given an IList, convert each item in it to <T>, using ToType<T>
        /// </summary>
        public static List<T> ToListType<T>( this IList list ) where T : class, new( )
        {
            //define system Type representing List of objects of type T
            Type genericType = typeof( List<> ).MakeGenericType( typeof( T ) );

            //create an object instance of defined type
            List<T> l = Activator.CreateInstance( genericType ) as List<T>;

            //get the Add method from List<T>
            MethodInfo addMethod = genericType.GetMethod( "Add" );

            //loop through the calling list:
            foreach( var item in list )
            {
                //convert each object of the list into T object 
                //by calling extension method ToType<T>( ) and
                //add this object to newly created list:
                addMethod.Invoke( l, new object[ ] { item.ToType<T>( ) } );
            }

            //return List of T objects:
            return l;
        }

        #endregion Extensions for IList

        #region Extensions for List
        // Split a list into buckets of a given size.
        public static List<List<T>> GetSets<T>( this List<T> _set, int _bucketSize )
        {
            List<List<T>> rc = new List<List<T>>( );
            if ( _bucketSize > _set.Count || _bucketSize < 1 )
            {
                rc.Add( _set );
            }
            else
            {
                var needsNewSet = true;
                List<T> currentSet = null;
                _set.ForEach( s =>
                {
                    if ( needsNewSet )
                    {
                        currentSet = new List<T>( );
                        rc.Add( currentSet );
                    }
                    currentSet.Add( s );
                    needsNewSet = currentSet.Count == _bucketSize;
                } );
            }
            return rc;
        }
        #endregion

        #region Extensions for IQueryable<T>
        static public List<T> CastToList<U, T>( this IQueryable<U> _q )
            where T : class
            where U : class, T
        {
            return _q.AsEnumerable( )
                .Cast<T>( )
                .ToList( );
        }

        #endregion Extensions for IQueryable<T>

        #region Extensions for XDocument

        /// <summary>
        /// Given an XDocument with elements _elementName, convert them to objects of type T
        /// and return a List<T>
        /// </summary>
        public static List<T> Get<T>( this XDocument _xdoc, string _elementName )
        {
            List<T> rc = new List<T>( );
            XmlSerializer serializer = new XmlSerializer( typeof( T ) );
            List<XElement> elements = new List<XElement>(
                from element in _xdoc.Descendants( _elementName )
                select element );
            foreach( XElement e in elements )
            {
                using( StringReader stringReader = new StringReader( e.ToString( ) ) )
                {
                    using( XmlTextReader xmlReader = new XmlTextReader( stringReader ) )
                    {
                        T newT = ( T ) serializer.Deserialize( xmlReader );
                        rc.Add( newT );
                    }
                }
            }
            return rc;
        } 

        #endregion

        #region Extensions for uint

        static public uint ShiftRight( this uint z_value, int z_shift )
        {
            return ( ( z_value >> z_shift ) | ( z_value << ( 16 - z_shift ) ) ) & 0x0000FFFF;
        }

        static public uint ShiftLeft( this uint z_value, int z_shift )
        {
            return ( ( z_value << z_shift ) | ( z_value >> ( 16 - z_shift ) ) ) & 0x0000FFFF;
        }

        #endregion

        #region Extensions for byte

        static public byte ShiftRight( this byte z_value, int z_shift )
        {
            return (byte)( ( ( z_value >> z_shift ) | ( z_value << ( 4 - z_shift ) ) ) & 0xFF );
        }

        static public byte ShiftLeft( this byte z_value, int z_shift )
        {
            byte low = (byte)( z_value << z_shift );
            byte high = (byte)( z_value >> ( 8 - z_shift ) );

            return (byte)( low | high );
        }

        #endregion Extensions for byte

        #region Extensions for IEnumerable

        public static IEnumerable<T> SelectDeep<T>(
        this IEnumerable<T> source, Func<T, IEnumerable<T>> selector )
        {
            foreach ( T item in source )
            {
                yield return item;
                foreach ( T subItem in SelectDeep( selector( item ), selector ) )
                {
                    yield return subItem;
                }
            }
        }

        #endregion Extensions for IEnumerable

        #region Extensions for DateTime

        public static DateTime Truncate( this DateTime date )
        {
            return new DateTime( date.Year, date.Month, date.Day );
        }

        #endregion Extensions for DateTime

        #region Extensions for Enum
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        #endregion Extensions for Enum
    }
}
