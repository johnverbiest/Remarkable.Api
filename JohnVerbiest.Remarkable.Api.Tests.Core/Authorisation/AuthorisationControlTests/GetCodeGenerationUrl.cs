using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using JohnVerbiest.Remarkable.Api.Authorisation;
using JohnVerbiest.Remarkable.Api.Configuration;
using JohnVerbiest.Remarkable.Api.Tests.Core.TestArchitecture;
using Xunit;

namespace JohnVerbiest.Remarkable.Api.Tests.Core.Authorisation.AuthorisationControlTests
{
    public class GetCodeGenerationUrl
    {
        [Theory, UnitTest]
        internal async Task GetCodeGenerationUrl_WhenCalled_ShouldReturnValueFromConfiguration(
            [Frozen] IAuthenticationSettings settings,
            AuthorisationControl sut
            )
        {
            // Arrange

            // Act
            var result = await sut.GetCodeGenerationUrl();

            // Assert
            result.Should().Be(settings.CodeGenerationUrl);
        }
    }
}