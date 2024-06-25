using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;

public class AssetBriefDto
{
    public int Id { get; init; }

    public string Code { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string CategoryName { get; init; } = null!;

    public string AssetStatusName { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Asset, AssetBriefDto>();
        }
    }
}