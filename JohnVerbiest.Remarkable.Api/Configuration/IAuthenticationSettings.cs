namespace JohnVerbiest.Remarkable.Api.Configuration
{
    internal interface IAuthenticationSettings
    {
        string CodeGenerationUrl { get; }
        string NewTokenEndpoint { get; }
        string RefreshTokenEndpoint { get; }
        string DeviceDesc { get; }
    }
}