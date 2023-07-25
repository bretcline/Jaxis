using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DictionarySerialization
{
    public interface ITag
    {
        string TagID { get; set; }

        string Name { get; set; }

        int Count { get; set; }
    }

    [Serializable]
    public class Tag : ITag
    {
        public string TagID { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public override string ToString( )
        {
            return string.Format( "{0} - {1} - {2}", TagID, Name, Count );
        }
    }

    class Program
    {
        protected Dictionary<string, List<ITag>> m_Dict = new Dictionary<string, List<ITag>>( );

        static void Main( string[] args )
        {
            Program P = new Program( );
            P.FillIt( );
            P.WriteIt( );
            P.ReadIt( );
            P.ShowIt( );
        }

        public void FillIt( )
        {
            for( int i = 0; i < 10; ++i )
            {
                string Name = "Name" + i;
                Tag T1 = new Tag( ) { Count = i, Name = Name, TagID = Name };
                Tag T2 = new Tag( ) { Count = i, Name = Name + "a", TagID = Name + "a" };

                m_Dict[Name] = new List<ITag>( ) { T1, T2 };
            }
        }

        public void ShowIt( )
        {
            foreach( string Key in m_Dict.Keys )
            {
                foreach( Tag T in m_Dict[Key] )
                {
                    Console.WriteLine( T.ToString( ) );
                }
            }
        }

        public void WriteIt( )
        {
            byte[] Data = null;
            using( MemoryStream MSWriter = new MemoryStream( ) )
            {
                BinaryFormatter Writer = new BinaryFormatter( );
                Writer.Serialize( MSWriter, m_Dict );
                Data = MSWriter.ToArray( );
            }

            using( StreamWriter SWriter = new StreamWriter( "TagList.bin" ) )
            {
                BinaryWriter Writer = new BinaryWriter( SWriter.BaseStream );
                Writer.Write( Data );
            }
        }

        public void ReadIt( )
        {
            IFormatter formatter = new BinaryFormatter( );
            using( FileStream stream = new FileStream( "TagList.bin", FileMode.Open, FileAccess.Read, FileShare.None ) )
            {
                m_Dict = (Dictionary<string, List<ITag>>)formatter.Deserialize( stream );
            }
        }
    }
}