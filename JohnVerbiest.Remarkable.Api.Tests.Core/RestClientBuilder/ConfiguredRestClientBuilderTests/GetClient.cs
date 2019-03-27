using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FakeItEasy;
using FluentAssertions;
using JohnVerbiest.Remarkable.Api.RestClientBuilder;
using JohnVerbiest.Remarkable.Api.ServiceDiscovery;
using JohnVerbiest.Remarkable.Api.Tests.Core.TestArchitecture;
using Xunit;

namespace JohnVerbiest.Remarkable.Api.Tests.Core.RestClientBuilder.ConfiguredRestClientBuilderTests
{
    public class GetClient
    {
        [Theory, UnitTest]
        internal async Task GetClient_WhenCalled_ShouldReturnAIRestClientConfiguredAsDiscovered(
            Uri expectedResult,
            [Frozen] IDiscoverTheServiceUrl serviceUrlDiscoverer,
            ConfiguredRestClientsBuilder sut
        )
        {
            // Arrange
            A.CallTo(() => serviceUrlDiscoverer.GetServiceBaseUrl()).Returns(expectedResult);

            // Act
            var result = await sut.GetClient();

            // Assert
            result.BaseUrl.Should().Be(expectedResult);
        }
    }
}