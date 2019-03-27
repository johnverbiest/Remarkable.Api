using System;
using System.Threading.Tasks;
using JohnVerbiest.Remarkable.Api.Configuration;
using RestSharp;

namespace JohnVerbiest.Remarkable.Api.ServiceDiscovery
{
    internal class ServiceDiscoverer: IDiscoverTheServiceUrl
    {
        private readonly IServiceDiscoverySettings _serviceDiscoverySettings;
        private readonly IRestClient _restClient;

        public ServiceDiscoverer(IServiceDiscoverySettings serviceDiscoverySettings, IRestClient restClient)
        {
            _serviceDiscoverySettings = serviceDiscoverySettings;
            _restClient = restClient;
        }

        public async Task<Uri> GetServiceBaseUrl()
        {
            _restClient.BaseUrl = null;
            var request = new RestRequest(_serviceDiscoverySettings.ServiceDiscoveryGetUrl) {Method = Method.GET};
            var result = await _restClient.ExecuteTaskAsync<ResponseObject>(request);
            if (result.IsSuccessful == false)
            {
                throw new Exception("Unable to detect base url", result.ErrorException);
            }
            return new Uri(result.Data?.Host ?? throw new Exception("Unable to detect base url"));
        }

        internal class ResponseObject
        {
            public string Status { get; set; }
            public string Host { get; set; }
        }
    }
}