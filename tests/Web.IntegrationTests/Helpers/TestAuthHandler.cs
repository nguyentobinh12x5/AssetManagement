using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

using Ardalis.GuardClauses;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Web.IntegrationTests.Helpers;

public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public string UserId { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string UserName { get; set; } = null!;
}

public class TestAuthHandler : AuthenticationHandler<TestAuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<TestAuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization header not found"));
        }

        if (!AuthenticationHeaderValue.TryParse(authorizationHeader, out var authHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid authorization header format"));
        }

        // Extract parameters from authHeader.Parameter and validate them
        var parameters = authHeader?.Parameter?.Split(';')
            .Select(p => p.Split('='))
            .ToDictionary(kv => kv[0].Trim(), kv => kv[1].Trim());

        Guard.Against.Null(parameters);

        if (!parameters.TryGetValue("UserId", out var userId) || string.IsNullOrEmpty(userId))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!parameters.TryGetValue("UserName", out var userName))
        {
            userName = string.Empty;
        }

        if (!parameters.TryGetValue("Location", out var location))
        {
            location = string.Empty;
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim("UserName",userName),
            new Claim("Location",location)
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}