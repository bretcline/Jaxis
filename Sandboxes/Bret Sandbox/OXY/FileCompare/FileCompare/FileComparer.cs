using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileCompare
{
    public class FileComparer
    {
        private string m_FileOne;
        private string m_FileTwo;

        public FileComparer( string _fileOne, string _fileTwo )
        {
            m_FileTwo = _fileTwo;
            m_FileOne = _fileOne;
        }

        public void Compare( )
        {
            using( var reader = new StreamReader( m_FileTwo ) )
            {
                
            }
        }

        public List<Tuple<string, List<string>>> BuildFileObjects(string _fileName)
        {
            var dataLists = new List<Tuple<string,List<string>>>();
            using (var reader = new StreamReader(_fileName))
            {
                var columns = reader.ReadLine();
                if (columns != null)
                {
                    var columnList = columns.Split(',');
                    for (int i = 0; i < columnList.Length; ++i)
                    {
                        dataLists.Add(new Tuple<string, List<string>>(columnList[i], new List<string>()));
                    }
                    dataLists.Add(new Tuple<string, List<string>>("Data", new List<string>()));

                    var data = reader.ReadLine();
                    while( null != data )
                    {
                        var dataList = data.Split(',');

                        for (int i = 0; i < dataList.Length; ++i)
                        {
                            dataLists[i].Item2.Add( ( dataList[i] ) );                            
                        }
                        dataLists[dataList.Length].Item2.Add(data);
                        data = reader.ReadLine();
                    }
                }
            }
            return dataLists;
        }

        public IEnumerable<string> GetColumns(string _fileName)
        {
            IEnumerable<string> rc = null;
            using (var reader = new StreamReader(_fileName))
            {
                var columns = reader.ReadLine();
                if (columns != null)
                {
                    rc = columns.Split(',');
                }
            }
            return rc;
        }

        public IEnumerable<string> CompareFiles(List<bool> _checkedItems)
        {
            IEnumerable<string> rc = null;

            var FileOne = BuildFileObjects(m_FileOne);
            var FileTwo = BuildFileObjects(m_FileTwo);

            rc = CompareData(FileOne, FileTwo, _checkedItems);
            
            return rc;
        }

        private IEnumerable<string> CompareData(List<Tuple<string, List<string>>> _fileOne, List<Tuple<string, List<string>>> _fileTwo, List<bool>_checkedItems)
        {
            var rc = new List<string>();

            int count = _fileOne[0].Item2.Count < _fileTwo[0].Item2.Count ? _fileTwo[0].Item2.Count : _fileOne[0].Item2.Count;

            for (int i = 0; i < count; ++i)
            {
                for( int j = 0; j < _fileOne.Count - 1; ++j )
                {
                    if( _checkedItems[j] && !_fileOne[j].Item2[i].Equals(_fileTwo[j].Item2[i]))
                    {
                        rc.Add(string.Format("{0} {1}", i + 1, _fileOne.Last().Item2[i]));
                        rc.Add(string.Format("{0} {1}", i + 1, _fileTwo.Last().Item2[i]));
                        break;
                    }
                }
            }
            return rc;
        }
    }
}
