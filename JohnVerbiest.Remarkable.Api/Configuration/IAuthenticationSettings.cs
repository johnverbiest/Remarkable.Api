namespace JohnVerbiest.Remarkable.Api.Configuration
{
    internal interface IAuthenticationSettings
    {
        string CodeGenerationUrl { get; }
        string TokenEndpoint { get; }
        string DeviceDesc { get; }
    }
}