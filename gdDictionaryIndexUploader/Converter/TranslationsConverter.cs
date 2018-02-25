using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader
{
    public class TranslationsConverter
    {
        private string[] _Lines;

        public TranslationsConverter( string[] lines )
        {
            _Lines = lines;
        }

        public TranslationSearchModel[] Convert()
        {
			List<TranslationSearchModel> translations = new List<TranslationSearchModel>();
			foreach ( string line in _Lines )
			{
				string[] splitted = line.Split( ';' );
				if ( splitted.Count() != 2 )
				{
					Console.WriteLine( string.Concat( splitted ) );
					continue;
				}

                TranslationSearchModel w = new TranslationSearchModel( splitted[0], splitted[1] );
                translations.Add( w );
            }
			return translations.ToArray();
        }
    }
}
