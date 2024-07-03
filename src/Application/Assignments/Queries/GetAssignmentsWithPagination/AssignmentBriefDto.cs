using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Assignments.Queries.GetAssignmentsWithPagination;

public class AssignmentBriefDto
{
    public int Id { get; init; }

    public string AssetCode { get; init; } = null!;

    public string AssetName { get; init; } = null!;

    public string AssignedTo { get; init; } = null!;

    public string AssignedBy { get; init; } = null!;

    public DateTime AssignedDate { get; init; } = DateTime.UtcNow;

    public AssignmentState State { get; init; } = AssignmentState.Accepted;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Assignment, AssignmentBriefDto>()
                .ForMember(dest => dest.AssetCode, opt => opt.MapFrom(src => src.Asset.Code))
                .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => src.Asset.Name));
        }
    }
}