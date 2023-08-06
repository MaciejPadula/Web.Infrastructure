using FluentAssertions;
using System.Net.Http.Json;
using System.Text;
using Web.Infrastructure.Microservices.Client.Logic;

namespace Web.Infrastructure.Microservices.Client.Tests.Logic;

public class DefaultHttpMessageProviderTests
{
    private DefaultHttpMessageProvider _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new DefaultHttpMessageProvider();
    }

    [Test]
    public async Task Provide_WhenUrlAndActionProvidedWithoutData_ShouldCreateMessageWithEmptyBody()
    {
        // Arrange
        var uri = new Uri("http://test.com");
        var action = "test";
        var expectedContent = "{}";
        var expectedResult = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            Content = new StringContent("", Encoding.UTF8, "application/json"),
            RequestUri = new Uri("http://test.com/test")
        };

        // Act
        var result = _sut.Provide(uri, action, null, HttpMethod.Post);
        var content = await result.Content.ReadAsStringAsync();

        // Assert
        result.RequestUri.Should().Be(expectedResult.RequestUri);
        result.Method.Should().Be(expectedResult.Method);
        content.Should().BeEquivalentTo(expectedContent);
    }

    [Test]
    public async Task Provide_WhenUrlAndActionProvidedWithData_ShouldCreateMessageWithSerializedObject()
    {
        // Arrange
        var uri = new Uri("http://test.com");
        var action = "test";
        var body = new
        {
            Test = 2
        };
        var expectedContent = "{\"Test\":2}";
        var expectedResult = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            Content = new StringContent("", Encoding.UTF8, "application/json"),
            RequestUri = new Uri("http://test.com/test")
        };

        // Act
        var result = _sut.Provide(uri, action, new[] { body }, HttpMethod.Post);
        var content = await result.Content.ReadAsStringAsync();

        // Assert
        result.RequestUri.Should().Be(expectedResult.RequestUri);
        result.Method.Should().Be(expectedResult.Method);
        content.Should().BeEquivalentTo(expectedContent);
    }
}
