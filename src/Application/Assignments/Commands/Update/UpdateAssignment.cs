using AssetManagement.Application.Common.Interfaces;

namespace AssetManagement.Application.Assignments.Commands.Update;

public record UpdateAssignmentCommand : IRequest<int>
{
    public int Id { get; init; }
    public string UserId { get; init; } = null!;

    public int AssetId { get; init; }

    public DateTime AssignedDate { get; init; } = DateTime.UtcNow;

    public string Note { get; init; } = null!;
}
public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IUser _currentUser;

    public UpdateAssignmentCommandHandler(
        IApplicationDbContext context,
        IIdentityService identityService,
        IUser currentUser)
    {
        _context = context;
        _identityService = identityService;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Asset)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, assignment);

        var assignedUserName = await _identityService.GetUserNameAsync(request.UserId);

        Guard.Against.NotFound(request.UserId, assignedUserName);

        Guard.Against.NullOrEmpty(_currentUser.UserName);

        var asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == request.AssetId, cancellationToken);

        Guard.Against.NotFound(request.AssetId, asset);

        assignment.Asset = asset;
        assignment.AssignedTo = assignedUserName;
        assignment.AssignedDate = request.AssignedDate;
        assignment.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}