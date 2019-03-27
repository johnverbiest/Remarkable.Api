using System.Threading.Tasks;
using JohnVerbiest.Remarkable.Api.ServiceDiscovery;
using RestSharp;

namespace JohnVerbiest.Remarkable.Api.RestClientBuilder
{
    internal class ConfiguredRestClientsBuilder: IBuildConfiguredRestClients
    {
        private readonly IDiscoverTheServiceUrl _discoverTheServiceUrl;

        public ConfiguredRestClientsBuilder(IDiscoverTheServiceUrl discoverTheServiceUrl)
        {
            _discoverTheServiceUrl = discoverTheServiceUrl;
        }

        public async Task<IRestClient> GetClient()
        {
            var client = new RestClient(await _discoverTheServiceUrl.GetServiceBaseUrl());
            return client;
        }
    }
}