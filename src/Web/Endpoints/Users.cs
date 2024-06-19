using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersBySearch;
using AssetManagement.Application.Users.Queries.GetUsersByType;

using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Web.Endpoints;

public class Users : EndpointGroupBase
{
    //public override void Map(WebApplication app)
    //{
    //    app.MapGroup(this)
    //        .AllowAnonymous()
    //        .MapGet(GetUserList)
    //        .MapGet("type", GetUserByType)
    //        .MapGet("search", SearchUsers);


    //}
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .AllowAnonymous();

        group.MapGet("/", GetUserList);
        group.MapGet("/type", GetUserByType);
        group.MapGet("/search", SearchUsers);
    }

    public Task<PaginatedList<UserBriefDto>> GetUserList(ISender sender, [AsParameters] GetUsersQuery query)
    {
        return sender.Send(query);
    }
    public Task<PaginatedList<UserBriefDto>> GetUserByType(ISender sender, [AsParameters] GetUsersByTypeQuery query)
    {
        return sender.Send(query);
    }
    public Task<List<UserBriefDto>> SearchUsers(ISender sender, [AsParameters] GetUsersBySearchQuery query) 
    {
        return sender.Send(query);
    }
}