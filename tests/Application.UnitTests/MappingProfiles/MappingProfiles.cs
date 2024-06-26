using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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