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
public record DeleteAssignmentCommand(int id) : IRequest;

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
        var assignment = await _context.Assignments.FindAsync(request.id);

        Guard.Against.NotFound(request.id, assignment);

        _context.Assignments.Remove(assignment);

        await _context.SaveChangesAsync(cancellationToken);
    }
}