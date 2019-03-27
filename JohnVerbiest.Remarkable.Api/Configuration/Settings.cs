namespace JohnVerbiest.Remarkable.Api.Configuration
{
    internal class Settings : IServiceDiscoverySettings
    {
        public string ServiceDiscoveryGetUrl => "https://service-manager-production-dot-remarkable-production.appspot.com/service/json/1/document-storage?environment=production&group=auth0%7C5a68dc51cb30df3877a1d7c4&apiVer=2";
    }
}