using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Application.Common.Models;

namespace AssetManagement.Web.Endpoints;

public class Assets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAssetList)
            .MapGet(GetAssetCategories, "Categories")
            .MapGet(GetAssetStatus, "Status");
    }

    public Task<PaginatedList<AssetBriefDto>> GetAssetList(ISender sender, [AsParameters] GetAssetsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public async Task<IResult> GetAssetCategories(ISender sender)
    {
        var result = await sender.Send(new GetAssetCategories());
        return Results.Ok(result);
    }

    public async Task<IResult> GetAssetStatus(ISender sender)
    {
        var result = await sender.Send(new GetAssetStatus());
        return Results.Ok(result);
    }

}