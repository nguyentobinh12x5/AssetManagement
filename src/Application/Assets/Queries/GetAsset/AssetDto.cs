using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Assets.Queries.GetDetailedAssets
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Specification { get; set; } = null!;
        public DateTime InstalledDate { get; set; }
        public string CategoryName { get; set; } = null!;
        public string AssetStatusName { get; set; } = null!;

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Asset, AssetDto>();
            }
        }
    }
}