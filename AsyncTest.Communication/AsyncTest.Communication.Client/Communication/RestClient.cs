using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AsyncTest.Communication.Client.Communication
{
    public class RestClient : IRestClient, IDisposable
    {
        private HttpClient _httpClient;

        public RestClient(IAuthorizationContainer authorizationContainer)
        {
            _httpClient = HttpClientFactory.Create(new HMAC256Handler(authorizationContainer));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<T> GetAsync<T>(Uri uri)
        {
            HttpResponseMessage result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        public Task DeleteAsync(Uri uri)
        {
            return _httpClient.DeleteAsync(uri);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_httpClient != null)
                {
                    _httpClient.Dispose();
                    _httpClient = null;
                }
            }
        }
    }
}