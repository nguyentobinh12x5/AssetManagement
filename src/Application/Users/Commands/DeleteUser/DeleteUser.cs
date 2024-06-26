using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Extensions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Common;
using AssetManagement.Domain.Events;


namespace AssetManagement.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(string Id) : IRequest;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IIdentityService identityService, IApplicationDbContext context)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteUserAsync(request.Id);

        await _context.SaveChangesAsync(cancellationToken);
    }

}