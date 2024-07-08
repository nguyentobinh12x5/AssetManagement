using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

namespace Web.IntegrationTests.Endpoints;

[Collection("Sequential")]
public class ReturningRequestsTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public ReturningRequestsTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.GetApplicationHttpClient();
    }
}