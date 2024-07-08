using System;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using FluentValidation;

using MediatR;

namespace AssetManagement.Application.ReturningRequests.Commands.Create;
[Authorize]
public record CreateRequestReturningAssetCommand : IRequest<int>
{
    public int AssignmentId { get; init; }
}
[Authorize]

public class CreateRequestReturningAssetCommandHandler : IRequestHandler<CreateRequestReturningAssetCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public CreateRequestReturningAssetCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(CreateRequestReturningAssetCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments.FindAsync(request.AssignmentId);
        Guard.Against.NotFound(request.AssignmentId, assignment);

        Guard.Against.NullOrEmpty(_currentUser.UserName);

        var returningRequest = new ReturningRequest
        {
            AssignmentId = request.AssignmentId,
            RequestedBy = _currentUser.UserName,
            State = ReturningRequestState.WaitingForReturning,
            IsDelete = false,
            Assignment = assignment!
        };

        _context.ReturningRequests.Add(returningRequest);
        await _context.SaveChangesAsync(cancellationToken);

        return returningRequest.Id;
    }
}