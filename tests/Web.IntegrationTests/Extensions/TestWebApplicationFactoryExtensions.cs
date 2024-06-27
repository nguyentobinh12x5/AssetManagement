using System.Net.Http.Headers;

using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Extensions;

public static class TestWebApplicationFactoryExtensions
{
    public static HttpClient GetApplicationHttpClient(
        this TestWebApplicationFactory<Program> factory
    )
    {
        var httpClient = factory.CreateClient();
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        return httpClient;
    }
}