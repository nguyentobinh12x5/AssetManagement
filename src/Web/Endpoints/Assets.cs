using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Assets.Queries.GetDetailedAssets;

using Microsoft.AspNetCore.Mvc;

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
            .MapGet(GetAsset, "{id}");
    }

    public Task<PaginatedList<AssetBriefDto>> GetAssetList(ISender sender, [AsParameters] GetAssetsWithPaginationQuery query)

    public async Task<AssetDto> GetAsset(ISender sender, int id)
    {
	    return await sender.Send(new GetAssetByIdQuery(id));
	}
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