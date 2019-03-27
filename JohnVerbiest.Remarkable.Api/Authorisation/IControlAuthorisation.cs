using System;
using System.Threading.Tasks;

namespace JohnVerbiest.Remarkable.Api.Authorisation
{
    internal interface IControlAuthorisation
    {
        Task<string> GetCodeGenerationUrl();
        Task<BearerToken> GetToken(Guid applicationId, string code);
        Task<BearerToken> GetToken(BearerToken oldToken);
    }
}