using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpHelperUtility
{
    public class HttpHelper : IDisposable
    {
        private HttpClient client;

        public HttpHelper(bool useProxy = true, string proxyUrl = "http://bluecoat.media-saturn.com:80")
        {
            var handler = new HttpClientHandler();
            if (useProxy)
            {
                handler.Proxy = new WebProxy(proxyUrl)
                {
                    Credentials = CredentialCache.DefaultNetworkCredentials
                };
            }
            client = new HttpClient(handler, true);
        }
        public async Task<string> GetAsync(string uri)
        {
            var result = await client.GetStringAsync(uri);
            return result;
        }

        public string Get(string uri)
        {
            return GetAsync(uri).Result;
        }

        public HttpResponseMessage PostJson(string uri, string payload)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.PostAsync(uri, new StringContent(payload, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
            return result;
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
