using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class PredictionApiClient
    {
        public async Task<string> GetPredictionFromApiAsync(int[] inputValues)
        {
            using (var httpClient = new HttpClient())
            {
                string apiUrl = "http://127.0.0.1:8000/predict";
                var jsonPayload = new
                {
                    input_values = inputValues
                };

                string jsonString = JsonSerializer.Serialize(jsonPayload);
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var predictionObj = JsonDocument.Parse(jsonResponse).RootElement.GetProperty("prediction");
                return predictionObj.GetString();
            }
        }
    }

}
