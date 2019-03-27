using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FakeItEasy;
using FluentAssertions;
using JohnVerbiest.Remarkable.Api.Configuration;
using JohnVerbiest.Remarkable.Api.ServiceDiscovery;
using JohnVerbiest.Remarkable.Api.Tests.Core.TestArchitecture;
using RestSharp;
using Xunit;

namespace JohnVerbiest.Remarkable.Api.Tests.Core.ServiceDiscovery.ServiceDiscovererTests
{
    public class GetServiceBaseUrl
    {
        [Theory, UnitTest]
        internal async Task GetServiceBaseUrl_WhenCalled_ShouldSetTheBaseUrlAsNull(
            [Frozen] Uri expectedResult,
            [Frozen] IServiceDiscoverySettings settings,
            [Frozen] IRestClient restClient,
            IRestResponse<ServiceDiscoverer.ResponseObject> response,
            ServiceDiscoverer.ResponseObject innerResponse,
            ServiceDiscoverer sut
        )
        {
            // Arrange
            A.CallTo(() => response.IsSuccessful).Returns(true);
            innerResponse.Host = expectedResult.AbsoluteUri;
            A.CallTo(() => response.Data).Returns(innerResponse);
            A.CallTo(() => restClient.ExecuteTaskAsync<ServiceDiscoverer.ResponseObject>(A<IRestRequest>.That.Matches(request =>
                request.Method == Method.GET && request.Resource == settings.ServiceDiscoveryGetUrl))).Returns(response);

            // Act
            var result = await sut.GetServiceBaseUrl();

            // Assert
            restClient.BaseUrl.Should().BeNull("otherwise the URI will be f-upped");
        }

        [Theory, UnitTest]
        internal async Task GetServiceBaseUrl_WhenCalled_ShouldCallTheUriFromTheSettings(
            [Frozen] Uri expectedResult,
            [Frozen] IServiceDiscoverySettings settings,
            [Frozen] IRestClient restClient,
            IRestResponse<ServiceDiscoverer.ResponseObject> response,
            ServiceDiscoverer.ResponseObject innerResponse,
            ServiceDiscoverer sut
            )
        {
            // Arrange
            innerResponse.Host = expectedResult.AbsoluteUri;
            A.CallTo(() => response.IsSuccessful).Returns(true);
            A.CallTo(() => response.Data).Returns(innerResponse);
            A.CallTo(() => restClient.ExecuteTaskAsync<ServiceDiscoverer.ResponseObject>(A<IRestRequest>.That.Matches(request =>
                request.Method == Method.GET && request.Resource == settings.ServiceDiscoveryGetUrl))).Returns(response);

            // Act
            var result = await sut.GetServiceBaseUrl();

            // Assert
            A.CallTo(() => restClient.ExecuteTaskAsync<ServiceDiscoverer.ResponseObject>(A<IRestRequest>.That.Matches(request =>
                    request.Method == Method.GET && request.Resource == settings.ServiceDiscoveryGetUrl)))
                .MustHaveHappenedOnceExactly();
        }

        [Theory, UnitTest]
        internal async Task GetServiceBaseUrl_WhenCalled_ShouldReturnTheCorrectRequestResponse(
            [Frozen] Uri expectedResult,
            [Frozen] IServiceDiscoverySettings settings,
            [Frozen] IRestClient restClient,
            IRestResponse<ServiceDiscoverer.ResponseObject> response,
            ServiceDiscoverer.ResponseObject innerResponse,
            ServiceDiscoverer sut
        )
        {
            // Arrange
            innerResponse.Host = expectedResult.AbsoluteUri;
            A.CallTo(() => response.IsSuccessful).Returns(true);
            A.CallTo(() => response.Data).Returns(innerResponse);
            A.CallTo(() => restClient.ExecuteTaskAsync<ServiceDiscoverer.ResponseObject>(A<IRestRequest>.That.Matches(request =>
                    request.Method == Method.GET && request.Resource == settings.ServiceDiscoveryGetUrl))).Returns(response);

            // Act
            var result = await sut.GetServiceBaseUrl();

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory, UnitTest]
        internal async Task GetServiceBaseUrl_WhenCalledButUnavailable_ShouldThrowException(
            [Frozen] IServiceDiscoverySettings settings,
            [Frozen] IRestClient restClient,
            IRestResponse<ServiceDiscoverer.ResponseObject> response,
            ServiceDiscoverer sut
        )
        {
            // Arrange
            A.CallTo(() => response.IsSuccessful).Returns(true);
            A.CallTo(() => response.Data).Returns(null);
            A.CallTo(() => restClient.ExecuteTaskAsync<ServiceDiscoverer.ResponseObject>(A<IRestRequest>.That.Matches(request =>
                request.Method == Method.GET && request.Resource == settings.ServiceDiscoveryGetUrl))).Returns(response);

            // Act
            Func<Task> runner = async () => await sut.GetServiceBaseUrl();

            // Assert
            await runner.Should().ThrowAsync<Exception>();
        }

        [Theory, UnitTest]
        internal async Task GetServiceBaseUrl_WhenCalledButUnavailableBySuccessfullFalse_ShouldThrowException(
            [Frozen] Uri expectedResult,
            [Frozen] IServiceDiscoverySettings settings,
            [Frozen] IRestClient restClient,
            IRestResponse<ServiceDiscoverer.ResponseObject> response,
            ServiceDiscoverer.ResponseObject innerResponse,
            ServiceDiscoverer sut
        )
        {
            // Arrange
            A.CallTo(() => response.IsSuccessful).Returns(false);
            innerResponse.Host = expectedResult.AbsoluteUri;
            A.CallTo(() => response.Data).Returns(innerResponse);
            A.CallTo(() => restClient.ExecuteTaskAsync<ServiceDiscoverer.ResponseObject>(A<IRestRequest>.That.Matches(request =>
                request.Method == Method.GET && request.Resource == settings.ServiceDiscoveryGetUrl))).Returns(response);

            // Act
            Func<Task> runner = async () => await sut.GetServiceBaseUrl();

            // Assert
            await runner.Should().ThrowAsync<Exception>();
        }
    }
}