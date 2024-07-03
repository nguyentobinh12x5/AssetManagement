using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Assignments.Queries.GetAssignment;
public class AssignmentDto
{
    public int Id { get; set; }
    public string AssetCode { get; set; } = null!;
    public string AssetName { get; set; } = null!;
    public string Specification { get; set; } = null!;
    public string AssignedTo { get; set; } = null!;
    public string AssignedBy { get; set; } = null!;
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    public AssignmentState State { get; set; } = AssignmentState.WaitingForAcceptance;
    public string Note { get; set; } = null!;
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Assignment, AssignmentDto>()
                .ForMember(dest => dest.Specification, opt => opt.MapFrom(src => src.Asset.Specification));
        }
    }
}