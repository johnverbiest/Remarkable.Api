namespace JohnVerbiest.Remarkable.Api.Configuration
{
    internal class Settings : IServiceDiscoverySettings, IAuthenticationSettings
    {
        public string ServiceDiscoveryGetUrl => "https://service-manager-production-dot-remarkable-production.appspot.com/service/json/1/document-storage?environment=production&group=auth0%7C5a68dc51cb30df3877a1d7c4&apiVer=2";
        public string CodeGenerationUrl => "https://my.remarkable.com/generator-device";
        public string NewTokenEndpoint => "https://my.remarkable.com/token/json/2/device/new";
        public string RefreshTokenEndpoint => "https://my.remarkable.com/token/json/2/user/new";
        public string DeviceDesc => "desktop-windows";
    }
}