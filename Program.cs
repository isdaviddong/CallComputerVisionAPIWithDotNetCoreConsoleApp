using System;
using System.Net.Http;

namespace cvconsole
{
    class Program
    {
        static string endpoint = "https://________.cognitiveservices.azure.com/";
        static string key = "_____e40cf8d4385018d4_____";
        static string file = @"C:\Users\_________\101.jpg";

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //Load File into Bytes
            var bytes = System.IO.File.ReadAllBytes(file);
            //call Computer Vision
            var ret = MakeRequest(endpoint, key, bytes);
            Console.Write(ret);
        }

        static dynamic MakeRequest(string endpoint, string subscriptionKey, byte[] byteData)
        {
            HttpClient client = new HttpClient();
            string uriBase = endpoint + "vision/v2.1/analyze";

            // Request headers.
            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", subscriptionKey);

            string requestParameters =
                "visualFeatures=Categories,Description,Color";

            // Assemble the URI for the REST API method.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            // Add the byte array as an octet stream to the request body.
            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses the "application/octet-stream" content type.
                // The other content types you can use are "application/json"
                // and "multipart/form-data".
                content.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                // Asynchronously call the REST API method.
                response = client.PostAsync(uri, content).Result;
            }

            // Asynchronously get the JSON response.
            string JSON = response.Content.ReadAsStringAsync().Result;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(JSON);
        }
    }
}
