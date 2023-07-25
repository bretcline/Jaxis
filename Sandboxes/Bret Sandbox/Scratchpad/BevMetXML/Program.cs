using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JaxisExtensions;
using System.IO;
using System.Xml;

namespace BevMetXML
{
    class Program
    {
        static void Main( string[] args )
        {
            string FileData = string.Empty;
            using( XmlTextReader Reader = new XmlTextReader( "Data\\EntityConfiguration.xml" ) )
            {
                using( StreamWriter Writer = new StreamWriter( "Output.csv" ) )
                {
                    Reader.WhitespaceHandling = WhitespaceHandling.None;
                    Reader.Read( );
                    while( Reader.Read( ) )
                    {
                        if( Reader.HasAttributes && Reader.Name.Equals( "Material" ) )
                        {
                            string Value = string.Format( "{0}| {1}", Reader.GetAttribute( "shortDescription" ), Reader.GetAttribute( "materialNumber" ) );
                            Writer.WriteLine( Value );
                            Console.WriteLine( Value );
                            Reader.MoveToElement( );
                        }
                    }
                }
            }
        }
    }
}
