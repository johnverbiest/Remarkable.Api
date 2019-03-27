using System.Threading.Tasks;
using RestSharp;

namespace JohnVerbiest.Remarkable.Api.RestClientBuilder
{
    internal interface IBuildConfiguredRestClients
    {
        Task<IRestClient> GetClient();
    }
}