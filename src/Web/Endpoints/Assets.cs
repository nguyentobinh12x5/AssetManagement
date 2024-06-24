using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Application.Common.Models;

namespace AssetManagement.Web.Endpoints;

public class Assets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAssetList);
    }

    public Task<PaginatedList<AssetBriefDto>> GetAssetList(ISender sender, [AsParameters] GetAssetsWithPaginationQuery query)
    {
        return sender.Send(query);
    }
    
}