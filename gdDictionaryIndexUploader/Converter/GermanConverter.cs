using gdDictionaryIndexUploader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader.Converter
{
    public class GermanConverter
    {
        private string[] _Lines;

        public GermanConverter( string[] lines )
        {
            _Lines = lines;
        }

        public GermanSearchModel[] Convert()
        {
            List<GermanSearchModel> translations = new List<GermanSearchModel>();
            foreach ( string line in _Lines )
            {
                string[] splitted = line.Split( ';' );
                if ( splitted.Count() != 2 )
                {
                    Console.WriteLine( string.Concat( splitted ) );
                    continue;
                }

                string[] separated = splitted[0].Split( ' ' );

                foreach ( string sep in separated )
                {
                    GermanSearchModel w = new GermanSearchModel( sep );
                    translations.Add( w );
                }
            }
            return translations.ToArray();
        }
    }
}
