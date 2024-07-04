using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Assignments.Queries.GetMyAssignments;

public class MyAssignmentDto
{
    public int Id { get; init; }
    public string AssetCode { get; private set; } = null!;
    public string AssetName { get; private set; } = null!;
    public string CategoryName { get; private set; } = null!;
    public DateTime AssignedDate { get; private set; }
    public AssignmentState State { get; private set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Assignment, MyAssignmentDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Asset.Category.Name));
        }
    }
}