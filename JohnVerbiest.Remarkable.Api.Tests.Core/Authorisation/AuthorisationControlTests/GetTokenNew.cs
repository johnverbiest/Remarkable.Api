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
    public class GetTokenNew
    {
        [Theory, UnitTest]
        internal async Task GetToken_WhenCallingForNewToken_SetBaseUrlToSetting(
            string token,
            Guid applicationId,
            string code,
            [Frozen] IRestClient restClient,
            [Frozen] IAuthenticationSettings settings,
            AuthorisationControl sut
        )
        {
            // Arrange

            // Act
            var result = await sut.GetToken(applicationId, code);

            // Assert
            restClient.BaseUrl.Should().BeNull();
        }

        [Theory, UnitTest]
        internal async Task GetToken_WhenCallingForNewToken_ShouldSendCorrectRequest(
            string token,
            Guid applicationId,
            string code,
            [Frozen] IRestClient restClient,
            [Frozen] IAuthenticationSettings settings,
            AuthorisationControl sut
        )
        {
            // Arrange
            A.CallTo(() => settings.NewTokenEndpoint).Returns("http://randomendpoint");
            A.CallTo(() => settings.DeviceDesc).Returns("Device Description");

            // Act
            var result = await sut.GetToken(applicationId, code);

            // Assert
            A.CallTo(() =>
                    restClient.ExecuteTaskAsync<string>(A<IRestRequest>.That.Matches(request =>
                        request.Method == Method.POST && 
                        request.Resource == settings.NewTokenEndpoint &&
                        request.Parameters.Any(parameter => (parameter.Value as AuthorisationControl.PayLoad).code == code && 
                                              (parameter.Value as AuthorisationControl.PayLoad).deviceDesc == settings.DeviceDesc &&
                                              (parameter.Value as AuthorisationControl.PayLoad).deviceId == applicationId.ToString() &&
                                              parameter.ContentType == "application/json"))))
                .MustHaveHappenedOnceExactly();
        }

        [Theory, UnitTest]
        internal async Task GetToken_WhenCallingForNewToken_ShouldReturnToken(
            string token,
            Guid applicationId,
            string code,
            string expectedToken,
            [Frozen] IRestResponse<string> response,
            [Frozen] IRestClient restClient,
            [Frozen] IAuthenticationSettings settings,
            AuthorisationControl sut
        )
        {
            // Arrange
            A.CallTo(() => response.Data).Returns(expectedToken);
            A.CallTo(() => settings.NewTokenEndpoint).Returns("http://randomendpoint");
            A.CallTo(() => settings.DeviceDesc).Returns("Device Description");
            A.CallTo(() =>
                    restClient.ExecuteTaskAsync<string>(A<IRestRequest>.That.Matches(x =>
                        x.Method == Method.POST &&
                        x.Resource == settings.NewTokenEndpoint &&
                        x.Parameters.Any(p => (p.Value as AuthorisationControl.PayLoad).code == code &&
                                              (p.Value as AuthorisationControl.PayLoad).deviceDesc ==
                                              settings.DeviceDesc &&
                                              (p.Value as AuthorisationControl.PayLoad).deviceId ==
                                              applicationId.ToString() &&
                                              p.ContentType == "application/json"))))
                .Returns(response);

            // Act
            var result = await sut.GetToken(applicationId, code);

            // Assert
            result.Token.Should().Be(expectedToken);

        }
    }
}