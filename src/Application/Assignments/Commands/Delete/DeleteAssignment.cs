using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Commands.Delete;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;

namespace AssetManagement.Application.Assignments.Commands.Delete;

[Authorize]
public record DeleteAssignmentCommand(int Id) : IRequest;

[Authorize]
public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAssignmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Asset)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, assignment);

        _context.Assignments.Remove(assignment);

        var asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == assignment.Asset.Id, cancellationToken);

        Guard.Against.NotFound(assignment.Asset.Id, asset);

        var state = await _context.AssetStatuses.FirstOrDefaultAsync(e => e.Name == "Available", cancellationToken);

        Guard.Against.NotFound("Available", state);

        asset.AssetStatus = state;

        await _context.SaveChangesAsync(cancellationToken);
    }
}