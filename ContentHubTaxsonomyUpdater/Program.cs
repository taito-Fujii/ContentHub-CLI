using System;
using System.IO.Compression;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContentHubTaxsonomyUpdater
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json")
                .Build();

            var distdir = "dist";
            if (Directory.Exists(distdir))
            {
                Directory.Delete(distdir, true);
            }

            var importfile = "import.zip";
            if (File.Exists(importfile))
            {
                File.Delete(importfile);
            }

            ZipFile.ExtractToDirectory(@"export.zip", distdir);

            var files = Directory.EnumerateFiles(@$"{distdir}/taxonomies/M.Tag", "*.json", SearchOption.AllDirectories);


            foreach (var file in files)
            {
                Console.WriteLine(file);

                var taxonomy = new Taxonomy();
                using (StreamReader sr = new StreamReader(file))
                {
                    taxonomy = JsonConvert.DeserializeObject<Taxonomy>(sr.ReadToEnd());


                    var jsonResult = await TranlateAsync(taxonomy.data.properties.TagLabel.enUS, configuration);

                    var result = JsonConvert.DeserializeObject<List<TranslateText>>(jsonResult);
                    taxonomy.data.properties.TagLabel.jaJP = result[0].translations[0].text;
                }

                // update Json
                using (var sw = new StreamWriter(file))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(taxonomy));
                }
                
            }

            ZipFile.CreateFromDirectory(distdir, importfile);
        }

        static async Task<string> TranlateAsync(string text, IConfiguration config)
        {
            string route = "/translate?api-version=3.0&from=en&to=ja";
            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(config["Endpoint"] + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", config["Key"]);
                request.Headers.Add("Ocp-Apim-Subscription-Region", config["Location"]);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }
    }
}
