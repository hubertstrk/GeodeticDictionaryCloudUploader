using gdDictionaryIndexUploader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader.Converter
{
    public class EnglishConverter
    {
        private string[] _Lines;

        public EnglishConverter( string[] lines )
        {
            _Lines = lines;
        }

        public EnglishSearchModel[] Convert()
        {
            List<EnglishSearchModel> translations = new List<EnglishSearchModel>();
            foreach ( string line in _Lines )
            {
                string[] splitted = line.Split( ';' );
                if ( splitted.Count() != 2 )
                {
                    Console.WriteLine( string.Concat( splitted ) );
                    continue;
                }

                string[] separated = splitted[1].Split( ' ' );

                foreach ( string sep in separated )
                {
                    EnglishSearchModel w = new EnglishSearchModel( sep );
                    translations.Add( w );
                }
            }
            return translations.ToArray();
        }
    }
}
