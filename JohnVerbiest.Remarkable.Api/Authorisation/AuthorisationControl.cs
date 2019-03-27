using System;
using System.Threading.Tasks;
using JohnVerbiest.Remarkable.Api.Configuration;
using RestSharp;

namespace JohnVerbiest.Remarkable.Api.Authorisation
{
    internal class AuthorisationControl: IControlAuthorisation
    {
        private readonly IAuthenticationSettings _settings;
        private readonly IRestClient _restClient;

        public AuthorisationControl(IAuthenticationSettings settings, IRestClient restClient)
        {
            _settings = settings;
            _restClient = restClient;
            _restClient.BaseUrl = null;
        }

        public Task<string> GetCodeGenerationUrl()
        {
            return Task.FromResult(_settings.CodeGenerationUrl);
        }

        public async Task<BearerToken> GetToken(Guid applicationId, string code)
        {
            var request = new RestRequest(_settings.NewTokenEndpoint) { Method = Method.POST };
            var payload = new PayLoad()
            {
                code = code,
                deviceDesc = _settings.DeviceDesc,
                deviceId = applicationId.ToString()
            };
            request.AddJsonBody(payload);
            var result = await _restClient.ExecuteTaskAsync<string>(request);

            return new BearerToken() {Token = result.Data};
        }

        public async Task<BearerToken> GetToken(BearerToken oldToken)
        {
            var request = new RestRequest(_settings.RefreshTokenEndpoint) { Method = Method.POST };
            request.AddHeader("Authorization", oldToken.ToString());
            var result = await _restClient.ExecuteTaskAsync<string>(request);

            return new BearerToken() { Token = result.Data };
        }

        public class PayLoad
        {
            public string code { get; set; }
            public string deviceDesc { get; set; }
            public string deviceId { get; set; }
        }
    }
}