using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ContentHubTaxsonomyUpdater
{
    class Taxonomy
    {

        public string version { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public int id { get; set; }
            public string identifier { get; set; }
            public string[] cultures { get; set; }
            public Properties properties { get; set; }
            public Relations relations { get; set; }
            public Created_By created_by { get; set; }
            public DateTime created_on { get; set; }
            public Modified_By modified_by { get; set; }
            public DateTime modified_on { get; set; }
            public Entitydefinition entitydefinition { get; set; }
            public Copy copy { get; set; }
            public Permissions permissions { get; set; }
            public Lifecycle lifecycle { get; set; }
            public Saved_Selections saved_selections { get; set; }
            public Roles roles { get; set; }
            public Annotations annotations { get; set; }
            public bool is_root_taxonomy_item { get; set; }
            public bool is_path_root { get; set; }
            public bool inherits_security { get; set; }
            public bool is_system_owned { get; set; }
            public int version { get; set; }
            public Self self { get; set; }
        }

        public class Properties
        {
            public string TagName { get; set; }
            public Taglabel TagLabel { get; set; }
            public Synonyms Synonyms { get; set; }
            public bool AutoCreated { get; set; }
            public object EntityVisualization { get; set; }
        }

        public class Taglabel
        {
            [JsonProperty("en-US")]
            public string enUS { get; set; }
            [JsonProperty("ja-JP", NullValueHandling=NullValueHandling.Ignore)]
            public string jaJP { get; set; }

        }

        public class Synonyms
        {
        }

        public class Relations
        {
            public Tagtoself TagToSelf { get; set; }
        }

        public class Tagtoself
        {
            public Parent parent { get; set; }
            public object[] children { get; set; }
        }

        public class Parent
        {
            public string href { get; set; }
        }

        public class Created_By
        {
            public string href { get; set; }
        }

        public class Modified_By
        {
            public string href { get; set; }
        }

        public class Entitydefinition
        {
            public string href { get; set; }
            public string title { get; set; }
        }

        public class Copy
        {
            public object href { get; set; }
        }

        public class Permissions
        {
            public object href { get; set; }
        }

        public class Lifecycle
        {
            public object href { get; set; }
        }

        public class Saved_Selections
        {
            public object href { get; set; }
        }

        public class Roles
        {
            public object href { get; set; }
        }

        public class Annotations
        {
            public object href { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

    }
}
