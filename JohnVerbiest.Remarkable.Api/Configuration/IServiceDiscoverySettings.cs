namespace JohnVerbiest.Remarkable.Api.Configuration
{
    /// <summary>
    /// Contains the settings for the Service Url Discovery Service
    /// </summary>
    internal interface IServiceDiscoverySettings
    {
        /// <summary>
        /// Fixed URL to know where to find the rest of the API
        /// </summary>
        string ServiceDiscoveryGetUrl { get; }
    }
}