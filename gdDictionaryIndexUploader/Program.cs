using gdDictionaryIndexUploader.Converter;
using gdDictionaryIndexUploader.Model;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader
{
    /// <summary>
    /// Fuzzy search, Field-scoped query, 
    /// </summary>
    class Program
    {
        static void Main( string[] args )
        {
            Console.Title = "Cloud Synchronizer Translations";
            Console.ForegroundColor = ConsoleColor.Gray;

            // read file
            FileReader fr = new FileReader( @"C:/Sources/GeodeticDictionaryCloudUploader/gdDictionaryIndexUploader/words/Dictionary_Static_Content_11_04_2012_Surveying.txt" );
            string[] lines = fr.ReadLines();

            // convert to different models
			var translationConverter = new TranslationsConverter( lines );
			TranslationSearchModel[] translationModel = translationConverter.Convert();

            var englishConverter = new EnglishConverter( lines );
            EnglishSearchModel[] englishModel = englishConverter.Convert();

            var germanConverter = new GermanConverter( lines );
            GermanSearchModel[] germanModel = germanConverter.Convert();

            LogSuccess( "model created" );

            // create service clients
            var translationClient = SearchClientCreator.Create( "translations" );
            SearchIndexAccess<TranslationSearchModel> translationAccess = new SearchIndexAccess<TranslationSearchModel>( translationClient );
            
            var germanClient = SearchClientCreator.Create( "german" );
            SearchIndexAccess<GermanSearchModel> germanAccess = new SearchIndexAccess<GermanSearchModel>( germanClient );
            
            var englishClient = SearchClientCreator.Create( "english" );
            SearchIndexAccess<EnglishSearchModel> englishAccess = new SearchIndexAccess<EnglishSearchModel>( englishClient );

            LogSuccess( "client created" );

            // upload 
            LogInfo( "uploading translations..." );
            translationAccess.Upload( translationModel, 500 );

            LogInfo( "uploading german..." );
            germanAccess.Upload( germanModel, 500 );

            LogInfo( "uploading english..." );
            englishAccess.Upload( englishModel, 500 );

            LogSuccess( "upload finished" );
			Console.ReadLine();
		}

        private static void LogSuccess( string text )
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine( $"{DateTime.UtcNow}: {text}" );
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void LogMessage( string text )
        {
            Console.WriteLine( $"{DateTime.UtcNow}: {text}" );
        }

        private static void LogInfo( string text )
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine( $"{DateTime.UtcNow}: {text}" );
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
