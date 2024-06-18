using AssetManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AssetManagement.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using System.Security.Claims;
using AssetManagement.Application.ChangePassword.Commands.UpdatePassword;

namespace AssetManagement.Web.Endpoints;

public class Auth : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
        .RequireAuthorization()
        .AllowAnonymous()
        .MapPost(ChangePassword, "change-password");
    }


    public async Task<IResult> ChangePassword(ISender sender, [FromBody] UpdatePasswordCommand command, ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            throw new UnauthorizedAccessException();
        }

        await sender.Send(command);
        return Results.NoContent();
    }
}
