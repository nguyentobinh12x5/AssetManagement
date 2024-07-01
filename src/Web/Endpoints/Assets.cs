using AssetManagement.Application.Assets.Commands.Create;
using AssetManagement.Application.Assets.Commands.Delete;
using AssetManagement.Application.Assets.Commands.Update;
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
            .MapGet(GetAsset, "{id}")
            .MapGet(GetAssetCategories, "Categories")
            .MapGet(GetAssetStatus, "Status")
            .MapPost(AddAsset)
            .MapPut(UpdateAsset, "{id}")
            .MapDelete(DeleteAsset, "{id}");
    }

    public Task<PaginatedList<AssetBriefDto>> GetAssetList(ISender sender, [AsParameters] GetAssetsWithPaginationQuery query)
    {
        return sender.Send(query);
    }
    public async Task<AssetDto> GetAsset(ISender sender, int id)
    {
        return await sender.Send(new GetAssetByIdQuery(id));
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

    public Task<int> AddAsset(ISender sender, CreateNewAssetCommand command)
    {
        return sender.Send(command);
    }
    public async Task<IResult> UpdateAsset(ISender sender, int id, UpdateAssetCommand command)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }

        //command.Id = id;

        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteAsset(ISender sender, int id)
    {
        await sender.Send(new DeleteAssetCommand(id));
        return Results.NoContent();
    }
}