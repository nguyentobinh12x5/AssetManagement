using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Application.Users.Queries.GetUsers;

namespace AssetManagement.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .AllowAnonymous()
            .MapGet(GetUserList)
            .MapGet(GetUser, "{id}")
            .MapPut(UpdateUser, "{id}")
            .MapPost(CreateUser);
    }

    public Task<PaginatedList<UserBriefDto>> GetUserList(ISender sender, [AsParameters] GetUsersQuery query)
    {
        return sender.Send(query);
    }

    public async Task<IResult> GetUser(ISender sender, string id)
    {
        var result = await sender.Send(new GetUserQuery { Id = id });
        return Results.Ok(result);
    }

    public Task<string> CreateUser(ISender sender, CreateUserCommand command)
    {
        return sender.Send(command);
    }
    
    public async Task<IResult> UpdateUser(ISender sender, string id, UpdateUserCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

}