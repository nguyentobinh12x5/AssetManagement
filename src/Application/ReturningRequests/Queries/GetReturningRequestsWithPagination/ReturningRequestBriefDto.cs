using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;

public class ReturningRequestBriefDto
{
    public int Id { get; set; }
    public string AssetCode { get; set; } = null!;
    public string AssetName { get; set; } = null!;
    public string RequestedBy { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public string? AcceptedBy { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public ReturningRequestState State { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ReturningRequest, ReturningRequestBriefDto>()
                .ForMember(dest => dest.AssetCode, opt => opt.MapFrom(src => src.Assignment.Asset.Code))
                .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Assignment.Asset.Name))
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => src.Assignment.AssignedDate));
        }
    }
}