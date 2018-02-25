using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader
{
    public class FileReader
    {
        private string _FileName = string.Empty;

        public FileReader( string filename )
        {
            _FileName = filename;
        }

        public string[] ReadLines()
        {
            string[] lines = File.ReadAllLines( _FileName );
            return lines;
        }
    }
}
