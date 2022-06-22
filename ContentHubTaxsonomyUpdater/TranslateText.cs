using System;
using System.Collections.Generic;
using System.Text;

namespace ContentHubTaxsonomyUpdater
{
    class TranslateText
    {
        public Translation[] translations { get; set; }

        public class Translation
        {
            public string text { get; set; }
            public string to { get; set; }
        }



    }
}
