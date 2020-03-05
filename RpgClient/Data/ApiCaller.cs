using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RpgClient.Data
{
    public class ApiCaller
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;

        public ApiCaller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal async Task<T> CallApiGet<T>(string endpoint)
        {
            try
            {
                var apiUri = _configuration.GetValue<Uri>("apiUrl");

                var response = await client.GetAsync(new Uri(apiUri, endpoint)).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonSerializer.Deserialize<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

        internal async Task<T> CallApiPost<T>(string endpoint, T item)
        {
            try
            {
                var apiUri = _configuration.GetValue<Uri>("apiUrl");

                var json = JsonSerializer.Serialize(item);
                using HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(new Uri(apiUri, endpoint), content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonSerializer.Deserialize<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

        internal async Task CallApiPut<T>(string endpoint, T item)
        {
            try
            {
                var apiUri = new Uri(_configuration.GetValue<string>("apiUrl"));

                var json = JsonSerializer.Serialize(item);
                using HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(new Uri(apiUri, endpoint), content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }
        }

        internal async Task CallApiDelete(string endpoint)
        {
            try
            {
                var apiUri = _configuration.GetValue<Uri>("apiUrl");
                var response = await client.DeleteAsync(new Uri(apiUri, endpoint)).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }
        }
    }
}