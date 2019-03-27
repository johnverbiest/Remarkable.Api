using FluentAssertions;
using JohnVerbiest.Remarkable.Api.Configuration;
using JohnVerbiest.Remarkable.Api.Tests.Core.TestArchitecture;
using Xunit;

namespace JohnVerbiest.Remarkable.Api.Tests.Core.ConfigurationTests
{
    public class SettingTests
    {
        [Theory, UnitTest]
        internal void Settings_OnRequestingServiceDiscoveryGetUrl_ShouldReturnPublishedUrl(Settings sut)
        {
            // Arrange
            var expected =
                "https://service-manager-production-dot-remarkable-production.appspot.com/service/json/1/document-storage?environment=production&group=auth0%7C5a68dc51cb30df3877a1d7c4&apiVer=2";

            // Act
            var actual = sut.ServiceDiscoveryGetUrl;

            // Assert
            actual.Should().Be(expected);
        }
    }
}