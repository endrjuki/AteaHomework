using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using StorageLibrary.Configuration;
using StorageLibrary.Models;

namespace StorageLibrary.Services
{
    public class ApiFetchService : IApiFetchService
    {
        private readonly ICustomConfigurationProvider _configurationProvider;


        public ApiFetchService(ICustomConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public async Task<PublicApiResponse> FetchData()
        {
            var client = HttpClientFactory.Create();
            var response = await client.GetAsync(_configurationProvider.ApiEndpoint);

            return new PublicApiResponse()
            {
                StatusCode = response.StatusCode.ToString(),
                Successful = response.IsSuccessStatusCode,
                Payload = await response.Content.ReadAsStringAsync()
            };
        }
    }
}