
using System.Security.Claims;

using AssetManagement.Application.Auth.Commands.ChangePassword;
using AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;
using AssetManagement.Application.Auth.Commands.Logout;
using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Web.Endpoints;

public class Auth : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .AllowAnonymous()
            .MapPost(ChangePassword, "change-password")
            .MapPost(ChangePasswordFirstTime, "change-password-first-time")
            .MapPost(Login, "login")
            .MapGet(GetUserInfo, "manage/info")
            .MapPost(Logout, "logout");
    }

    public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login(
        [FromBody] LoginRequest loginRequest,
        [FromQuery] bool? useCookies,
        [FromQuery] bool? useSessionCookies,
        [FromServices] IServiceProvider sp)
    {
        var signInManager = sp.GetRequiredService<SignInManager>();

        var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        var isPersistent = (useCookies == true) && (useSessionCookies != true);
        signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        // Login user
        var result = await signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, isPersistent, lockoutOnFailure: true);

        if (result.RequiresTwoFactor)
        {
            if (!string.IsNullOrEmpty(loginRequest.TwoFactorCode))
            {
                result = await signInManager.TwoFactorAuthenticatorSignInAsync(loginRequest.TwoFactorCode, isPersistent, rememberClient: isPersistent);
            }
            else if (!string.IsNullOrEmpty(loginRequest.TwoFactorRecoveryCode))
            {
                result = await signInManager.TwoFactorRecoveryCodeSignInAsync(loginRequest.TwoFactorRecoveryCode);
            }
        }

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                return TypedResults.Problem("Your account is disabled. Please contact with IT Team", statusCode: StatusCodes.Status401Unauthorized);
            }
            return TypedResults.Problem("Username or password is incorrect. Please try again", statusCode: StatusCodes.Status401Unauthorized);
        }

        // The signInManager already produced the needed response in the form of a cookie or bearer token.
        return TypedResults.Empty;
    }

    public async Task<UserInfoDto> GetUserInfo(ISender sender, [AsParameters] GetCurrentUserInfoQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<IResult> ChangePassword(ISender sender, [FromBody] UpdatePasswordCommand command, ClaimsPrincipal claimsPrincipal)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> ChangePasswordFirstTime(ISender sender, [FromBody] ChangePasswordFirstTimeCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> Logout(ISender sender)
    {
        await sender.Send(new LogoutCommand());
        return Results.NoContent();
    }
}