using AssetManagement.Application.Assets.Queries.GetDetailedAssets;

using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Web.Endpoints;

public class Assets : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetAsset, "{id}");
    }

    public async Task<AssetDto> GetAsset(ISender sender, int id)
    {
        return await sender.Send(new GetAssetByIdQuery(id));
    }
}