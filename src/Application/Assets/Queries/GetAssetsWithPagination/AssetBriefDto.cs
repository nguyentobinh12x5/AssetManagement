using System.ComponentModel.DataAnnotations;

using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;

public class AssetBriefDto
{
    public int Id { get; init; }

    public string Code { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string Category { get; init; } = null!;

    public string AssetStatus { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Asset, AssetBriefDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AssetStatus, opt => opt.MapFrom(src => src.AssetStatus.Name));
        }
    }
}