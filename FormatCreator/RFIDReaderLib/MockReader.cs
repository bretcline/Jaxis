using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LFI.RFID.Format;
using LFI.RFID.Editor;

namespace Jaxis.RFID.Readers
{
    public class MockConfig : IRFIDConfig
    {
        public string FormatDefinitionPath { get; set; }
        public Dictionary<string,string> GetConfig( )
        {
            Dictionary<string, string> rc = new Dictionary<string, string>( );

            return rc;
        }

        public void AddValue( string key, string value )
        {

        }
    }

    public class MockReader : IRFIDReader
    {

        FormatManager m_FormatManager = null;
        IRFIDConfig m_Config = null;
        public void ConfigureDevice( IRFIDConfig _Config )
        {
            m_Config = _Config;

            m_FormatManager = new FormatManager( m_Config.FormatDefinitionPath );
        }

        public void WriteTag( TagData _Data )
        {
            int TagSize = 1024;
            ByteConverter Converter = new ByteConverter( );

            FormatDef Def = m_FormatManager.GetFormatByID( _Data.FormatID );
            if (Def != null)
            {
                byte[] Data = Converter.ToByteArray(Def, _Data, TagSize);

                if (!System.IO.Directory.Exists(".\\Tags\\"))
                {
                    System.IO.Directory.CreateDirectory(".\\Tags\\");
                }

                string TagID = Convert.ToBase64String(_Data.TagID);

                System.IO.StreamWriter BaseWriter = new System.IO.StreamWriter(".\\Tags\\" + ToFileName(TagID));
                System.IO.BinaryWriter Writer = new System.IO.BinaryWriter(BaseWriter.BaseStream);
                Writer.Write(Data);
                Writer.Close();
            }
        }

        public TagData ReadTag( )
        {
            TagData rc = null;
            string TagID = string.Empty;
            if( System.IO.Directory.Exists( ".\\Tags\\" ) )
            {
                string[] Files = System.IO.Directory.GetFiles(".\\Tags\\");
                if( 0 < Files.Length )
                {
                    Random rnd = new Random( );
                    int Index = ( 1 == Files.Length ) ? 0 : rnd.Next( Files.Length );
                    TagID = Files[Index];

                    System.IO.StreamReader BaseReader = new System.IO.StreamReader(TagID);
                    System.IO.BinaryReader Reader = new System.IO.BinaryReader(BaseReader.BaseStream);
                    byte[] Data = new byte[BaseReader.BaseStream.Length];
                    Reader.Read( Data, 0, Data.Length );
                    Reader.Close();
                    rc = new TagData( );
                    ByteConverter Converter = new ByteConverter( );
                    Guid FormatID = Converter.GetFormatID( Data );
                    FormatDef Format = m_FormatManager.GetFormatByID( FormatID );
                    rc = Converter.FromByteArray( Format, Data );
                    rc.TagID = Convert.FromBase64String( FromFileName( TagID.Replace( ".\\Tags\\", "" ) ) );                   
                }
                else
                {
                    rc = new TagData( );
                }
            }
            else
            {
                rc = new TagData( );
            }
            return rc;
        }

        public List<TagData> ReadTags( )
        {
            List<TagData> rc = new List<TagData>( );

            Random Rnd = new Random( );

            TagData TD = new TagData( );
            TD.HeaderRow = new TagDataRow( );
            TD.HeaderRow.Values.Add( "TagID", ( Rnd.NextDouble( ) * 10000 ).ToString( ) );
            TD.HeaderRow.Values.Add( "TagType", "ISO14443" );

            TagDataRow newDataRow = new TagDataRow( );
            for( int i = 0; i < 3; ++i )
            {
                newDataRow.Values[i.ToString( )] = Rnd.NextDouble( ).ToString( ).Substring( 0, 4 );
            }
            TD.DataRows.Add( newDataRow );
            newDataRow = new TagDataRow( );
            for( int i = 0; i < 3; ++i )
            {
                newDataRow.Values[i.ToString( )] = Rnd.NextDouble( ).ToString( ).Substring( 0, 4 );
            }
            TD.DataRows.Add( newDataRow );
            rc.Add( TD );

            TD = new TagData( );
            TD.HeaderRow = new TagDataRow( );
            TD.HeaderRow.Values.Add( "TagID", ( Rnd.NextDouble( ) * 10000 ).ToString( ) );
            TD.HeaderRow.Values.Add( "TagType", "ISO14443" );

            newDataRow = new TagDataRow( );
            for( int i = 0; i < 3; ++i )
            {
                newDataRow.Values[i.ToString( )] = Rnd.NextDouble( ).ToString( ).Substring( 0, 4 );
            }
            TD.DataRows.Add( newDataRow );
            newDataRow = new TagDataRow( );
            for( int i = 0; i < 3; ++i )
            {
                newDataRow.Values[i.ToString( )] = Rnd.NextDouble( ).ToString( ).Substring( 0, 4 );
            }
            TD.DataRows.Add( newDataRow );
            rc.Add( TD );

            TD = new TagData( );
            TD.HeaderRow = new TagDataRow( );
            TD.HeaderRow.Values.Add( "TagID", ( Rnd.NextDouble( ) * 10000 ).ToString( ) );
            TD.HeaderRow.Values.Add( "TagType", "ISO14443" );

            newDataRow = new TagDataRow( );
            for( int i = 0; i < 3; ++i )
            {
                newDataRow.Values[i.ToString( )] = Rnd.NextDouble( ).ToString( ).Substring( 0, 4 );
            }
            TD.DataRows.Add( newDataRow );
            newDataRow = new TagDataRow( );
            for( int i = 0; i < 3; ++i )
            {
                newDataRow.Values[i.ToString( )] = Rnd.NextDouble( ).ToString( ).Substring( 0, 4 );
            }
            TD.DataRows.Add( newDataRow );
            rc.Add( TD );


            return rc;
        }


        protected string ToFileName( string _Base64String )
        {
            return _Base64String.Replace( '\\', (char)225 ).Replace( '/', (char)226 );
        }

        protected string FromFileName( string _FileName )
        {
            return _FileName.Replace( (char)225, '\\' ).Replace( (char)226, '/' );
        }
    }
}
