using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Assignments.Commands.Update;
public record UpdateMyAssignmentStateCommand : IRequest<int>
{
    public int Id { get; init; }
    public AssignmentState State { get; init; }
}
public class UpdateMyAssignmentStateCommandHandler : IRequestHandler<UpdateMyAssignmentStateCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UpdateMyAssignmentStateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateMyAssignmentStateCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Asset)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, assignment);

        var asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == assignment.Asset.Id, cancellationToken);

        Guard.Against.NotFound(assignment.Asset.Id, asset);

        if (request.State == AssignmentState.Accepted)
        {
            assignment.State = request.State;

            var state = await _context.AssetStatuses.FirstOrDefaultAsync(e => e.Name == "Assigned", cancellationToken);

            Guard.Against.NotFound("Assigned", state);

            asset.AssetStatus = state;
        }
        else if (request.State == AssignmentState.Declined)
        {
            assignment.State = request.State;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}