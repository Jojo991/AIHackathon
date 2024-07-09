using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShipNavigator
{
    public class HttpClientHelper
    {
        public static async Task<T> PostAsync<T>(string url, string body, IDictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                var content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }
    }
}

