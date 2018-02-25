using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdDictionaryIndexUploader.Model
{
    public class GermanSearchModel
    {
        public GermanSearchModel( string germanText )
        {
            german = germanText;
            id = this.GetHashCode().ToString();
        }
        public string id { get; set; }

        public string german { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + german.GetHashCode();
                return hash;
            }
        }
    }
}
