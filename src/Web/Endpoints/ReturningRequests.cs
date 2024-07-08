using AssetManagement.Application.ReturningRequests.Commands.Create;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagement.Web.Endpoints;

public class ReturningRequests : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapPost(CreateReturningRequest, "Create");
    }

    public Task<int> CreateReturningRequest(ISender sender, CreateRequestReturningAssetCommand command)
    {
        return sender.Send(command);
    }
}