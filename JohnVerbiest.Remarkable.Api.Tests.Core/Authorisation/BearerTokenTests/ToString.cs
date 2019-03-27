using FluentAssertions;
using JohnVerbiest.Remarkable.Api.Authorisation;
using JohnVerbiest.Remarkable.Api.Tests.Core.TestArchitecture;
using Xunit;

namespace JohnVerbiest.Remarkable.Api.Tests.Core.Authorisation.BearerTokenTests
{
    public class ToString
    {
        [Theory, UnitTest]
        internal void ToString_WhenCalled_ShouldReturnTheTokenWithBearerInFront(
            BearerToken sut)
        {
            // Arrange
            sut.Token = "ThisIsAnAwesomeToken";
            var expectedResult = "Bearer ThisIsAnAwesomeToken";

            // Act
            var result = sut.ToString();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}