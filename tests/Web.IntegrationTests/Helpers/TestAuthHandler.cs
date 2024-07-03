using System.Security.Claims;
using System.Text.Encodings.Web;

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
        // Add more option later
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Options.UserId),
            new Claim("UserName",Options.UserName),
            new Claim("Location",Options.Location)
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }


}