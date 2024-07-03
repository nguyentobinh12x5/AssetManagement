
using System.Security.Claims;

using AssetManagement.Application.Auth.Commands.ChangePassword;
using AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;
using AssetManagement.Application.Auth.Commands.Login;
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

    public async Task<IResult> Login(
        ISender sender,
        [FromBody] LoginRequest loginRequest,
        [FromQuery] bool? useCookies,
        [FromQuery] bool? useSessionCookies)
    {
        await sender.Send(new LoginCommand(
            loginRequest.Email,
            loginRequest.Password,
            loginRequest.TwoFactorCode,
            loginRequest.TwoFactorRecoveryCode,
            useCookies,
            useSessionCookies
        ));
        return Results.Ok();
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