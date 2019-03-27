using System;
using System.Threading.Tasks;

namespace JohnVerbiest.Remarkable.Api.ServiceDiscovery
{
    internal interface IDiscoverTheServiceUrl
    {
        Task<Uri> GetServiceBaseUrl();
    }
}