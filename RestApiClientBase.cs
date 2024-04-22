using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks; 
using System.Linq; 

namespace Wimika.MoneyGuard.Core.Android.REST
{
    public abstract class RestApiClientBase
    {
        
        // Insert variables below here
        protected static HttpClient _client;
        private static Func<HttpClientHandler> _factory;
        private static object _lock = new object();
        // Insert static constructor below here
        public static Func<HttpClientHandler> Factory
        {
            get
            {
                return _factory;
            }
            set
            {
                if (_factory == null)
                {
                    _factory = value;
                }
            }
        }
        static void EnsureClient()
        {
            lock (_lock)
            {
                if(_client == null)
                {
                    _client = new HttpClient( _factory == null ? new HttpClientHandler() : _factory());
                }
            }
        }

        private readonly string _urlPrefix = "";

        protected RestApiClientBase(string urlPrefix)
        {
            _urlPrefix = urlPrefix;
            EnsureClient();
        }


        private string ComposeUrl(string path)
        {
            return string.Format("{0}{1}", _urlPrefix, path);
        }


        // Insert CreateRequestMessage method below here
        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string path, Dictionary<string, string> headers = null)
        {
            var httpRequestMessage = new HttpRequestMessage(method, ComposeUrl(path));

          
            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    httpRequestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            return httpRequestMessage;
        }
         
        protected async Task<string> GetAsync(string path, Dictionary<string, string> headers = null)
        {
            return await AsyncWithoutBody(path, HttpMethod.Get, headers);
        }

        protected async Task<string> DeleteAsync(string path, Dictionary<string, string> headers = null)
        {
            return await AsyncWithoutBody(path, HttpMethod.Delete, headers);
        }

        private async Task<string> AsyncWithoutBody(string path, HttpMethod method,  Dictionary<string, string> headers = null)
        {
            using (var request = CreateRequestMessage(method, path, headers))
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        protected async Task<string> PostJsonAsync(string path, byte[] body, Dictionary<string, string> headers = null)
        {
            return await AsyncWithBody(path, HttpMethod.Post, "application/json", body,headers );
        }

        protected async Task<string> PutJsonAsync(string path, byte[] body, Dictionary<string, string> headers = null)
        {
            return await AsyncWithBody(path, HttpMethod.Put, "application/json", body, headers);
        }

         

        private async Task<string> AsyncWithBody(string path, HttpMethod method, string contentType, byte[] body, Dictionary<string, string> headers = null)
        {
            using (var request = CreateRequestMessage(method, path, headers))
            {
                request.Content = new ByteArrayContent(body);
                request.Content.Headers.ContentLength = body.LongLength;
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                using (var response = await _client.SendAsync(request))
                {


                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                    
                }
            }

        }
    }
}
