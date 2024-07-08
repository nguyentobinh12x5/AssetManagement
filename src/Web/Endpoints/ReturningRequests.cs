using AssetManagement.Application.Common.Models;
using AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;

namespace AssetManagement.Web.Endpoints;

public class ReturningRequests : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetListReturningRequests);
    }

    public async Task<PaginatedList<ReturningRequestBriefDto>> GetListReturningRequests(ISender sender, [AsParameters] GetReturningRequestsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }
}