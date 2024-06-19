using System.Net.Http.Headers;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Options;

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