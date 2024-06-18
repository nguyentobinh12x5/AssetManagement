using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Application.Users.Queries.GetUser;

namespace AssetManagement.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .AllowAnonymous()
            .MapPut(UpdateUser, "{id}");
    }
    
    public async Task<IResult> UpdateUser(ISender sender, string id, UpdateUserCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }
}