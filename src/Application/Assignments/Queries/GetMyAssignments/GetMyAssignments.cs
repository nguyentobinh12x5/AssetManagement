
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Mappings;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Common.Security;

namespace AssetManagement.Application.Assignments.Queries.GetMyAssignments;

[Authorize]
public record GetMyAssignmentsQuery : QueryParams, IRequest<PaginatedList<MyAssignmentDto>>
{
    public GetMyAssignmentsQuery(
        string sortColumnName,
        int pageNumber = 1,
        int pageSize = 10,
        string sortColumnDirection = "asc") : base(sortColumnName, pageNumber, pageSize, sortColumnDirection)
    {
    }
}


public class GetMyAssignmentsQueryHandler : IRequestHandler<GetMyAssignmentsQuery, PaginatedList<MyAssignmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _currentUser;

    public GetMyAssignmentsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<PaginatedList<MyAssignmentDto>> Handle(GetMyAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        return await _context.Assignments
                        .Where(x => x.AssignedTo.Equals(_currentUser.UserName) && (x.AssignedDate.Date <= today))
                        .OrderByDynamic(request.SortColumnName, request.SortColumnDirection)
                        .ProjectTo<MyAssignmentDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }

}