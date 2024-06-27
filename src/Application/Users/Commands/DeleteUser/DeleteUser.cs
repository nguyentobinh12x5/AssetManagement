using AssetManagement.Application.Common.Interfaces;


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