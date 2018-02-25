using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader
{
	public class SearchIndexAccess<T> where T : class
	{
		private SearchIndexClient _Client;

		public SearchIndexAccess( SearchIndexClient client )
		{
			_Client = client;
		}

		public void Upload( T[] words, int batchSize )
		{
			// upload happens in batches
			IList<T> batch = new List<T>();
			foreach ( T w in words )
			{
				batch.Add( w );
				if ( batch.Count.Equals( batchSize ) )
				{
					MergeOrUpload( batch );
					batch = new List<T>();
				}
			}
			if ( batch.Count > 0 )
				MergeOrUpload( batch );
		}

		private void MergeOrUpload( IEnumerable<T> toUpload )
		{
			try
			{
				Stopwatch sw = new Stopwatch();
				sw.Start();

				var batch = IndexBatch.MergeOrUpload( toUpload );
				_Client.Documents.Index( batch );
                
				sw.Stop();
                Console.WriteLine( $"{DateTime.UtcNow}: uploading {toUpload.Count()} items took {sw.ElapsedMilliseconds} ms" );
				sw.Reset();
			}
			catch ( IndexBatchException e )
			{
				// Sometimes when your Search service is under load, indexing will fail for some of the documents in
				// the batch. Depending on your application, you can take compensating actions like delaying and
				// retrying. For this simple demo, we just log the failed document keys and continue.
				Console.WriteLine(
					"Failed to index some of the documents: {0}",
					String.Join( ", ", e.IndexingResults.Where( r => !r.Succeeded ).Select( r => r.Key ) ) );
			}

			// Wait a while for indexing to complete.
			Thread.Sleep( 1000 );
		}

		public void Delete(T[] words)
		{
			try
			{
				var batch = IndexBatch.Delete( words );
				_Client.Documents.Index( batch );
			}
			catch ( IndexBatchException e )
			{
				// Sometimes when your Search service is under load, indexing will fail for some of the documents in
				// the batch. Depending on your application, you can take compensating actions like delaying and
				// retrying. For this simple demo, we just log the failed document keys and continue.
				Console.WriteLine(
					"Failed to index some of the documents: {0}",
					String.Join( ", ", e.IndexingResults.Where( r => !r.Succeeded ).Select( r => r.Key ) ) );
			}
		}
	}
}
