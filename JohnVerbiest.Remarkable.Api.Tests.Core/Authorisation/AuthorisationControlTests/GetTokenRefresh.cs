using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FakeItEasy;
using FluentAssertions;
using JohnVerbiest.Remarkable.Api.Authorisation;
using JohnVerbiest.Remarkable.Api.Configuration;
using JohnVerbiest.Remarkable.Api.Tests.Core.TestArchitecture;
using RestSharp;
using Xunit;

namespace JohnVerbiest.Remarkable.Api.Tests.Core.Authorisation.AuthorisationControlTests
{
    public class GetTokenRefresh
    {
        [Theory, UnitTest]
        internal async Task GetToken_WhenCallingForRefreshedToken_SetBaseUrlToSetting(
            BearerToken originalToken,
            [Frozen] IRestClient restClient,
            [Frozen] IAuthenticationSettings settings,
            AuthorisationControl sut
        )
        {
            // Arrange

            // Act
            var result = await sut.GetToken(originalToken);

            // Assert
            restClient.BaseUrl.Should().BeNull();
        }

        [Theory, UnitTest]
        internal async Task GetToken_WhenCallingForRefreshedToken_ShouldSendCorrectRequest(
            BearerToken originalToken,
            [Frozen] IRestClient restClient,
            [Frozen] IAuthenticationSettings settings,
            AuthorisationControl sut
        )
        {
            // Arrange
            A.CallTo(() => settings.RefreshTokenEndpoint).Returns("http://randomendpoint");
            A.CallTo(() => settings.DeviceDesc).Returns("Device Description");

            // Act
            var result = await sut.GetToken(originalToken);

            // Assert
            A.CallTo(() =>
                    restClient.ExecuteTaskAsync<string>(A<IRestRequest>.That.Matches(request =>
                        request.Method == Method.POST &&
                        request.Resource == settings.RefreshTokenEndpoint &&
                        request.Parameters.Any(parameter => (parameter.Value as string) == originalToken.ToString() &&
                                              parameter.Type == ParameterType.HttpHeader &&
                                              parameter.Name == "Authorization"))))
                .MustHaveHappenedOnceExactly();
        }

        [Theory, UnitTest]
        internal async Task GetToken_WhenCallingForRefreshedToken_ShouldReturnToken(
            string expectedToken,
            BearerToken originalToken,
            [Frozen] IRestResponse<string> response,
            [Frozen] IRestClient restClient,
            [Frozen] IAuthenticationSettings settings,
            AuthorisationControl sut
        )
        {
            // Arrange
            A.CallTo(() => response.Data).Returns(expectedToken);
            A.CallTo(() => settings.RefreshTokenEndpoint).Returns("http://randomendpoint");
            A.CallTo(() => settings.DeviceDesc).Returns("Device Description");
            A.CallTo(() =>
                    restClient.ExecuteTaskAsync<string>(A<IRestRequest>.That.Matches(request =>
                        request.Method == Method.POST &&
                        request.Resource == settings.RefreshTokenEndpoint &&
                        request.Parameters.Any(parameter => (parameter.Value as string) == originalToken.ToString() &&
                                                            parameter.Type == ParameterType.HttpHeader &&
                                                            parameter.Name == "Authorization"))))
                .Returns(response);

            // Act
            var result = await sut.GetToken(originalToken);

            // Assert
            result.Token.Should().Be(expectedToken);

        }
    }
}