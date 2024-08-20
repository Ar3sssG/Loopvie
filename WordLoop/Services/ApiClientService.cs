using System.Net.Http.Json;
using System.Text.Json;
using WLCommon.Models.Request;
using WordLoop.Settings;

namespace WordLoop.Services
{
    public class ApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiClientServiceSettings _settings;
        private string _apiKey;
        public ApiClientService(ApiClientServiceSettings settings)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
            _httpClient.BaseAddress = new Uri(settings.BaseUrl); 
        }

        public async Task RetreiveToken(AuthRequestModel requestModel)
        {
            var token = await _httpClient.PostAsJsonAsync(_settings.BaseUrl, requestModel);
            if (token.IsSuccessStatusCode)
            {
                
            }
        }
    }
}
