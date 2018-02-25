using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader
{
    public static class SearchClientCreator
    {
        public static SearchIndexClient Create( string indexName )
        {
            // Put your search service name here. This is the hostname portion of your service URL.
            // For example, if your service URL is https://myservice.search.windows.net, then your
            // service name is myservice.
            string searchServiceName = "[service name]";
            string apiKey = "[api key]";

            SearchServiceClient serviceClient = null;
            try
            {
                serviceClient = new SearchServiceClient( searchServiceName, new SearchCredentials( apiKey ) );
            }
            catch ( Exception ex )
            {
                throw new ApplicationException($"service client exception: {ex.Message}");
            }

            SearchIndexClient indexClient = null;
            try
            {
                indexClient = serviceClient.Indexes.GetClient( indexName );
            }
            catch ( Exception ex )
            {
                throw new ApplicationException( $"service client exception: {ex.Message}" );
            }
            return indexClient;
        }
    }
}
