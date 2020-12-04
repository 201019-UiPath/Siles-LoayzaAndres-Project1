using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreWebUI
{
    public static class StoreHttpClient
    {
        private static string _apiDomainName;
        public static string apiDomainName
        {
            get
            {
                if (_apiDomainName==null)
                {
                    string fileText = System.IO.File.ReadAllText("apiDomainName.json");
                    string[] text = JsonSerializer.Deserialize<string[]>(fileText);
                    _apiDomainName = text[0]+"/";
                }
                return _apiDomainName;
            }
        }

        private static HttpClient _client;
        public static HttpClient client
        {
            get
            {
                if (_client == null) { _client = new HttpClient(); }
                return _client;
            }
        }

        public static T GetData<T>(string url)
        {
            var response = client.GetAsync(apiDomainName+url);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();

                return readTask.Result;
            }
            throw new HttpRequestException();
        }

        public static bool PostDataAsJson<T>(string url, T data)
        {
            var response = client.PostAsJsonAsync(apiDomainName+url, data);
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return true; //success
            }
            return false; //post failed
        }

        public static bool SendRequest(string url, HttpRequestMessage request)
        {
            request.RequestUri = new Uri(apiDomainName+url);
            var response = client.SendAsync(request); //send put request
            response.Wait();

            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return true; //success
            }
            return false; //send failed
        }
    }
}
