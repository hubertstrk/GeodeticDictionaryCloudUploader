using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader
{
	public class TranslationSearchModel
	{
		public TranslationSearchModel( string germanText, string englishText )
		{
			german = germanText;
			english = englishText;
			id = this.GetHashCode().ToString();
		}
		public string id { get; set; }

		public string german { get; set; }

		public string english { get; set; }

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash = hash * 23 + german.GetHashCode();
				hash = hash * 23 + english.GetHashCode();
				return hash;
			}
		}
	}
}
