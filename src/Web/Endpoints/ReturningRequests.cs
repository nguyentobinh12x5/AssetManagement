using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.ReturningRequests.Commands.Create;
using AssetManagement.Domain.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;

namespace AssetManagement.Web.Endpoints;

public class ReturningRequests : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapPost(CreateReturningRequest, "Create")
            .MapGet(GetListReturningRequests);
    }

    public Task<int> CreateReturningRequest(ISender sender, CreateRequestReturningAssetCommand command)
    {
        return sender.Send(command);
    }
    public async Task<PaginatedList<ReturningRequestBriefDto>> GetListReturningRequests(ISender sender, [AsParameters] GetReturningRequestsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
}