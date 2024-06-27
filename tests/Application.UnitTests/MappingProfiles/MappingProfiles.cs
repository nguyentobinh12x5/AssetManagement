using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Domain.Entities;

using AutoMapper;

namespace AssetManagement.Application.UnitTests.MappingProfiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<Asset, AssetBriefDto>();
        }
    }
}