using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Assignments.Queries.GetAssignment;

public record GetAssignmentByIdQuery(int Id) : IRequest<AssignmentDto>;


public class GetAssignmentByIdQueryHandler : IRequestHandler<GetAssignmentByIdQuery, AssignmentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _currentuser;

    public GetAssignmentByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IUser currentuser
    )
    {
        _context = context;
        _mapper = mapper;
        _currentuser = currentuser;
    }

    public async Task<AssignmentDto> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Where(a => a.Id == request.Id)
            .Include(a => a.Asset)
            .FirstOrDefaultAsync();

        Guard.Against.NotFound(request.Id, assignment);

        return _mapper.Map<AssignmentDto>(assignment);
    }
}