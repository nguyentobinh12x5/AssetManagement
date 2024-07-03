using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Entities;

namespace AssetManagement.Application.Assignments.Commands.Create;
[Authorize]
public record CreateNewAssignmentCommand : IRequest<int>
{
    public string UserId { get; init; } = null!;
    public int AssetId { get; init; }
    public DateTime AssignedDate { get; init; } = DateTime.UtcNow;
    public string Note { get; init; } = null!;
}
[Authorize]
public class CreateNewAssignmentCommandHandler : IRequestHandler<CreateNewAssignmentCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IUser _currentUser;
    public CreateNewAssignmentCommandHandler(
        IApplicationDbContext context,
        IIdentityService identityService,
        IUser currentUser)
    {
        _context = context;
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(CreateNewAssignmentCommand request, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets
            .Include(a => a.AssetStatus)
            .FirstOrDefaultAsync(e => e.Id == request.AssetId);

        Guard.Against.NotFound(request.AssetId, asset);

        if (string.Compare(asset.AssetStatus.Name, "Available") is not 0)
        {
            throw new BadRequestException("asset is unavailable, please refresh the page and try again");
        }

        var assignedUserName = await _identityService.GetUserNameAsync(request.UserId);

        Guard.Against.NotFound(request.UserId, assignedUserName);

        Guard.Against.NullOrEmpty(_currentUser.UserName);

        var newAssignment = new Assignment
        {
            Asset = asset,
            AssignedBy = _currentUser.UserName,
            AssignedDate = request.AssignedDate,
            AssignedTo = assignedUserName,
            State = Domain.Enums.AssignmentState.WaitingForAcceptance,
            Note = request.Note
        };

        _context.Assignments.Add(newAssignment);

        var state = await _context.AssetStatuses.FirstOrDefaultAsync(e => e.Name == "Not Available");

        Guard.Against.NotFound("Not Available", state);

        asset.AssetStatus = state;

        await _context.SaveChangesAsync(cancellationToken);

        return newAssignment.Id;
    }
}