using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI.WebControls;
using Newtonsoft.Json;
using PitneyBowes.Developer.ShippingApi.Model;
using shippingapi;


namespace shippingapi.Services
{
    public class Shipping360Service
    {
        private readonly HttpClient _httpClient;
        private readonly string _shipping360ApiKey;
        private readonly string _shipping360ApiUrl;

        public Shipping360Service(string shipping360ApiKey, string shipping360ApiUrl)
        {
            _httpClient = new HttpClient();
            _shipping360ApiKey = shipping360ApiKey;
            _shipping360ApiUrl = shipping360ApiUrl;
        }

        public async Task<AutogenResponse> GenerateLabelAsync(shippingapi.Services.Shipping360Service shipment)
        {
            var request = new AutogenRequest
            {
                //Shipment = shipment
            };
            string json = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_shipping360ApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to generate label: {response.StatusCode}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            AutogenResponse autogenResponse = JsonConvert.DeserializeObject<AutogenResponse>(responseBody);

            return autogenResponse;
        }

        public class AutogenRequest
        {
            //public shippingapi.Model.Shipment Shipment { get; set; }
        }

        public class AutogenResponse
        {
            public string LabelUrl { get; set; }
            public string TrackingNumber { get; set; }
        }
    }
}